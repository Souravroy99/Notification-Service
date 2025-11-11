using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationCenter.DTOs;
using NotificationCenter.Data;
using NotificationCenter.Models;
using NotificationCenter.Services;


namespace NotificationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateTask(NotificationTaskDto dto)
        {
            if (dto.ScheduledTime <= DateTime.Now)
                return BadRequest("Scheduled time must be in the future.");


            var task = new NotificationTask
            {
                Title = dto.Title,
                Email = dto.Email,
                ScheduledTime = dto.ScheduledTime,
                IsSent = false
            };

            await _context.NotificationTasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Task created successfully",
                task
            });
        }


        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.NotificationTasks
                .Select(t => new NotificationTaskResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Email = t.Email,
                    ScheduledTime = t.ScheduledTime,
                    IsSent = t.IsSent
                })
                .ToListAsync();

            if (tasks.Count == 0)
                return Ok(new { message = "No tasks found." });

            return Ok(tasks);
        }


        [HttpPost("Send/{id}")]
        public async Task<IActionResult> SendEmail(int id, [FromServices] EmailService emailService)
        {
            var task = await _context.NotificationTasks.FindAsync(id);
            if (task == null) return NotFound("Task not found");

            await emailService.SendEmailAsync(task.Email, task.Title, task.Description);
            task.IsSent = true;
            await _context.SaveChangesAsync();

            return Ok("Email sent successfully");
        }
    }
}
