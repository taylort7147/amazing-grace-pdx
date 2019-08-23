using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Editor.Models;

namespace Editor.Controllers
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
        public async Task<ActionResult<IEnumerable<Notes>>> GetMessages()
        {
            return await _context.Notes
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetMessage(int id)
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
    }
}