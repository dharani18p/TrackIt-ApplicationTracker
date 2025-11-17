using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationTrackingSystem.Data;
using ApplicationTrackingSystem.Models;
using System.Security.Claims;

namespace ApplicationTrackingSystem.Controllers
{
    [ApiController]
    [Route("applicant")]
    [Authorize(Roles = "Applicant")]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get logged in user's ID from JWT
        private int GetUserId()
        {
            return int.Parse(User.Claims.First(c => c.Type == "id").Value);
        }

        // ------------------------
        // 1. Create Application
        // ------------------------
        [HttpPost("apply")]
        public async Task<IActionResult> CreateApplication(int jobRoleId)
        {
            var applicantId = GetUserId();

            var application = new ApplicationModel
            {
                ApplicantId = applicantId,
                JobRoleId = jobRoleId,
                Status = "Applied",
                CreatedAt = DateTime.UtcNow
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Create log
            _context.ApplicationLogs.Add(new ApplicationLog
            {
                ApplicationId = application.Id,
                OldStatus = "None",
                NewStatus = "Applied",
                UpdatedByRole = "Applicant",
                Comment = "Application submitted",
            });

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Application created", ApplicationId = application.Id });
        }

        // ------------------------
        // 2. View My Applications
        // ------------------------
        [HttpGet("my-applications")]
        public async Task<IActionResult> MyApplications()
        {
            var applicantId = GetUserId();

            var apps = await _context.Applications
                .Include(a => a.JobRole)
                .Where(a => a.ApplicantId == applicantId)
                .ToListAsync();

            return Ok(apps);
        }

        // ------------------------
        // 3. View Application by ID
        // ------------------------
        [HttpGet("application/{id}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var applicantId = GetUserId();

            var application = await _context.Applications
                .Include(a => a.JobRole)
                .FirstOrDefaultAsync(a => a.Id == id && a.ApplicantId == applicantId);

            if (application == null)
                return NotFound("Application not found.");

            return Ok(application);
        }

        // ------------------------
        // 4. View Logs
        // ------------------------
        [HttpGet("application/{id}/logs")]
        public async Task<IActionResult> GetLogs(int id)
        {
            var applicantId = GetUserId();

            var exists = await _context.Applications
                .AnyAsync(a => a.Id == id && a.ApplicantId == applicantId);

            if (!exists)
                return NotFound("Application not found.");

            var logs = await _context.ApplicationLogs
                .Where(l => l.ApplicationId == id)
                .OrderBy(l => l.Timestamp)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
