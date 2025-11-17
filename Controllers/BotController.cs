using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationTrackingSystem.Data;
using ApplicationTrackingSystem.Models;

namespace ApplicationTrackingSystem.Controllers
{
    [ApiController]
    [Route("bot")]
    [Authorize(Roles = "BotMimic")]
    public class BotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Workflow stages for technical roles
        private readonly List<string> Workflow = new()
        {
            "Applied",
            "Reviewed",
            "Interview",
            "Offer",
            "Hired"
        };

        // ------------------------------------------------------
        // BOT RUN ENDPOINT (Main Automated Workflow)
        // ------------------------------------------------------
        [HttpPost("run")]
        public async Task<IActionResult> RunAutomation()
        {
            var technicalApps = await _context.Applications
                .Include(a => a.JobRole)
                .Where(a => a.JobRole.IsTechnical == true)
                .Where(a => a.Status != "Hired")  // Skip completed
                .ToListAsync();

            if (technicalApps.Count == 0)
                return Ok("No technical applications pending automation.");

            foreach (var app in technicalApps)
            {
                var currentIndex = Workflow.IndexOf(app.Status);

                if (currentIndex == -1) continue;

                // Move to next stage
                var nextIndex = currentIndex + 1;

                // Already completed?
                if (nextIndex >= Workflow.Count)
                    continue;

                var newStatus = Workflow[nextIndex];

                // Update in DB
                var oldStatus = app.Status;
                app.Status = newStatus;

                // Save update
                await _context.SaveChangesAsync();

                // Log
                _context.ApplicationLogs.Add(new ApplicationLog
                {
                    ApplicationId = app.Id,
                    OldStatus = oldStatus,
                    NewStatus = newStatus,
                    UpdatedByRole = "BotMimic",
                    Comment = $"Auto-update: Status changed to {newStatus}"
                });

                await _context.SaveChangesAsync();
            }

            return Ok($"Bot automation completed. Updated {technicalApps.Count} applications.");
        }
    }
}
