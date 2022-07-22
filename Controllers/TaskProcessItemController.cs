using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskProcessor.Models;

namespace TaskProcessor.Controllers
{
    [Route("task")]
    [ApiController]
    public class TaskProcessItemController : ControllerBase
    {
        private readonly TaskProcessContext _context;

        public TaskProcessItemController(TaskProcessContext context)
        {
            _context = context;
        }

        // GET: api/TaskProcessItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskProcessItem>>> GetTaskProcessItems()
        {
          if (_context.TaskProcessItems == null)
          {
              return NotFound();
          }
            return await _context.TaskProcessItems.ToListAsync();
        }

        // GET: api/TaskProcessItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskProcessItem>> GetTaskProcessItem(int id)
        {
          if (_context.TaskProcessItems == null)
          {
              return NotFound();
          }
            var taskProcessItem = await _context.TaskProcessItems.FindAsync(id);

            if (taskProcessItem == null)
            {
                return NotFound();
            }

            return taskProcessItem;
        }

        // PUT: api/TaskProcessItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskProcessItem(int id, TaskProcessItem taskProcessItem)
        {
            if (id != taskProcessItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskProcessItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskProcessItemExists(id))
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

        // POST: api/TaskProcessItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskProcessItem>> PostTaskProcessItem(TaskProcessItem taskProcessItem)
        {
          if (_context.TaskProcessItems == null)
          {
              return Problem("Entity set 'TaskProcessContext.TaskProcessItems'  is null.");
          }
            _context.TaskProcessItems.Add(taskProcessItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskProcessItem", new { id = taskProcessItem.Id }, taskProcessItem);
        }

        // DELETE: api/TaskProcessItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskProcessItem(int id)
        {
            if (_context.TaskProcessItems == null)
            {
                return NotFound();
            }
            var taskProcessItem = await _context.TaskProcessItems.FindAsync(id);
            if (taskProcessItem == null)
            {
                return NotFound();
            }

            _context.TaskProcessItems.Remove(taskProcessItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskProcessItemExists(int id)
        {
            return (_context.TaskProcessItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
