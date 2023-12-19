using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoProject.DTO;
using TodoProject.Models;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly SimpleDbContext _context;

        public object UserName { get; private set; }

        public TodoesController(SimpleDbContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
          if (_context.Todos == null)
          {
              return NotFound();
          }
            return await _context.Todos.ToListAsync();
        }

        [HttpGet("GetRecentlyCompleted")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetRecentlyCompleted()
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }

            try
            {
                return await _context.Todos
                    .Where(t => t.ModifiedDate < DateTime.Now.AddDays(-7) && t.IsComplete == true)
                    .OrderByDescending(o => o.ModifiedDate)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return Problem("Server failed the query request");
            }
        }

        [HttpGet("GetRecentlyCompletedCount")]
        public async Task<ActionResult<IEnumerable<TodoCount>>> GetRecentlyCompletedCount()
        {
            if (_context.TodoCounts == null)
            {
                return NotFound();
            }

            try
            {   
                FormattableString query = @$"
                    SELECT 
                        COUNT(A.UserProfileId) Completed, UserName 
                    FROM Todo A 
                    INNER JOIN UserProfile B 
                        ON A.UserProfileId = B.UserProfileId 
                    WHERE 
                        A.ModifiedDate > DATEADD(day,-7, GETDATE()) AND 
                        IsComplete = 1 
                    GROUP BY A.UserProfileId, UserName 
                    ORDER BY 1 DESC";
                return await _context.TodoCounts.FromSql(query).ToListAsync();
            }
            catch (Exception)
            {
                return Problem("Server failed the query request");
            }
        }

        // GET: api/Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
          if (_context.Todos == null)
          {
              return NotFound();
          }
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.TodoId)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
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

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
          if (_context.Todos == null)
          {
              return Problem("Unable to create Todo");
          }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.TodoId }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return (_context.Todos?.Any(e => e.TodoId == id)).GetValueOrDefault();
        }
    }
}
