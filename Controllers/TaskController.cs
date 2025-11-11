using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationCenter.DTOs;
using NotificationService.Data;
using NotificationService.DTOs;
using NotificationService.Models;


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

        [HttpPost]
        public async Task<ActionResult> CreateTask(NotificationTaskDto dto)
        {
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

        [HttpGet]
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

            return Ok(tasks);
        }
    }
}
