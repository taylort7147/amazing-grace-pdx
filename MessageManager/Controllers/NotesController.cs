using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageManager.Models;

namespace MessageManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly MessageContext _context;

        public NotesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notes>>> GetNotes()
        {
            return await _context.Notes
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetNotes(int id)
        {
            var Notes = await _context.Notes
                        .Include(v => v.Message)
                        .FirstOrDefaultAsync(v => v.Id == id);

            if(Notes == null)
            {
                return NotFound();
            }

            return Notes;
        }

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Notes>> GetLatestNotes()
        {
            var notes = await _context.Notes
                        .Include(n => n.Message)
                        .Where(n => n.Message.Date.DayOfWeek == System.DayOfWeek.Sunday)
                        .OrderByDescending(n => n.Message.Date)
                        .FirstOrDefaultAsync();


            if(notes == null)
            {
                return NotFound();
            }

            return notes;
        }
    }
}