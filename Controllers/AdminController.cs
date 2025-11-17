using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationTrackingSystem.Data;
using ApplicationTrackingSystem.Models;

namespace ApplicationTrackingSystem.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ----------------------------------------
        // 1. Create Job Role (Technical or Non-Tech)
        // ----------------------------------------
        [HttpPost("create-jobrole")]
        public async Task<IActionResult> CreateJobRole(string roleName, bool isTechnical)
        {
            var role = new JobRole
            {
                RoleName = roleName,
                IsTechnical = isTechnical
            };

            _context.JobRoles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Job role created", Role = role });
        }

        // ----------------------------------------
        // 2. View ALL Applications (Admin Full Access)
        // ----------------------------------------
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications()
        {
            var apps = await _context.Applications
                .Include(a => a.Applicant)
                .Include(a => a.JobRole)
                .ToListAsync();

            return Ok(apps);
        }

        // ----------------------------------------
        // 3. Update Status (Only for NON-TECH Roles)
        // ----------------------------------------
        [HttpPut("application/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus, string? comment)
        {
            var app = await _context.Applications
                .Include(a => a.JobRole)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (app == null)
                return NotFound("Application not found.");

            if (app.JobRole.IsTechnical)
                return BadRequest("Admin cannot update TECHNICAL applications. Bot will handle these.");

            var oldStatus = app.Status;
            app.Status = newStatus;

            await _context.SaveChangesAsync();

            // Create log
            _context.ApplicationLogs.Add(new ApplicationLog
            {
                ApplicationId = id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                UpdatedByRole = "Admin",
                Comment = comment ?? "Status updated by admin"
            });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Status updated successfully",
                ApplicationId = id,
                OldStatus = oldStatus,
                NewStatus = newStatus
            });
        }
    }
}
