using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationTrackingSystem.Data;
using System.Security.Claims;

namespace ApplicationTrackingSystem.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.Claims.First(c => c.Type == "id").Value);
        }

        // -------------------------
        // 1. ADMIN DASHBOARD
        // -------------------------
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var totalApplications = await _context.Applications.CountAsync();
            var totalTechnical = await _context.Applications
                .Include(a => a.JobRole)
                .CountAsync(a => a.JobRole.IsTechnical == true);
            var totalNonTech = await _context.Applications
                .Include(a => a.JobRole)
                .CountAsync(a => a.JobRole.IsTechnical == false);

            var totalUsers = await _context.Users.CountAsync();

            return Ok(new
            {
                TotalApplications = totalApplications,
                TechnicalApplications = totalTechnical,
                NonTechnicalApplications = totalNonTech,
                TotalUsers = totalUsers
            });
        }

        // -------------------------
        // 2. APPLICANT DASHBOARD
        // -------------------------
        [HttpGet("applicant")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> ApplicantDashboard()
        {
            var userId = GetUserId();

            var totalMyApps = await _context.Applications
                .CountAsync(a => a.ApplicantId == userId);

            var hiredCount = await _context.Applications
                .CountAsync(a => a.ApplicantId == userId && a.Status == "Hired");

            var pendingCount = await _context.Applications
                .CountAsync(a => a.ApplicantId == userId && a.Status != "Hired");

            return Ok(new
            {
                MyApplications = totalMyApps,
                Pending = pendingCount,
                Hired = hiredCount
            });
        }

        // -------------------------
        // 3. BOT DASHBOARD
        // -------------------------
        [HttpGet("bot")]
        [Authorize(Roles = "BotMimic")]
        public async Task<IActionResult> BotDashboard()
        {
            var pending = await _context.Applications
                .Include(a => a.JobRole)
                .CountAsync(a => a.JobRole.IsTechnical && a.Status != "Hired");

            var completed = await _context.Applications
                .Include(a => a.JobRole)
                .CountAsync(a => a.JobRole.IsTechnical && a.Status == "Hired");

            return Ok(new
            {
                PendingTechnicalJobs = pending,
                CompletedTechnicalJobs = completed
            });
        }
    }
}
