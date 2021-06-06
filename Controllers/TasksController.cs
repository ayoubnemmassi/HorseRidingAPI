using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseRidingAPI.Models;
using Task = HorseRidingAPI.Models.Task;

namespace HorseRidingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly HorseRidingClubContext _context;

        public TasksController(HorseRidingClubContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }
        // GET: api/Tasks/5
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetUserTask(int userid)
        {
            var task = _context.Tasks
          .FromSqlRaw("SELECT * FROM tasks")
          .Where(b => b.UserFk == userid)
          .ToListAsync();

            if (task == null)
            {
                return NotFound();
            }

            return await task;
        }
        [HttpGet("getwithdate/{date}")]
        public async Task<ActionResult<Task>> GetTask(DateTime date)
        {
            var task = _context.Tasks
           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => b.StartDate == date)
           .FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }

            return   task;
        }      [HttpGet("daytasks/{date}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetDayTask(DateTime date)
        {
            var task = _context.Tasks
           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => b.StartDate.Date == date)
           .ToList();

            if (task == null)
            {
                return NotFound();
            }

            return   task;
        }      [HttpGet("daytask/{date}/{id}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTaskWithID(DateTime date,int id)
        {
            var task = _context.Tasks

           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => b.StartDate.Date == date && b.UserFk==id)
           .ToList();

            if (task == null)
            {
                return NotFound();
            }

            return   task;
        }
        [HttpGet("{date}/{date1}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTask(DateTime date, DateTime date1)
        {
            var task = _context.Tasks
           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => b.StartDate >= date && b.StartDate <= date1)
           .ToListAsync();

            if (task == null)
            {
                return NotFound();
            }

            return await task;
        }
        [HttpGet("all/{date}/{date1}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(DateTime date, DateTime date1)
        {
            var task = _context.Tasks
           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => b.StartDate.Date >= date && b.StartDate.Date <= date1)
           .ToListAsync();

            if (task == null)
            {
                return NotFound();
            }

            return await task;
        }
        [HttpGet("{date}/{date1}/{id}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetSeance(DateTime date, DateTime date1, int id)
        {
            var tasks = _context.Tasks
           .FromSqlRaw("SELECT * FROM tasks")
           .Where(b => (b.StartDate >= date && b.StartDate <= date1) && b.UserFk == id)
           .ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return await tasks;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Task>> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
