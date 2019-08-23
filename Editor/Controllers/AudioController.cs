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
    public class AudioController : ControllerBase
    {
        private readonly MessageContext _context;

        public AudioController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audio>>> GetMessages()
        {
            return await _context.Audio
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetMessage(int id)
        {
            var Audio = await _context.Audio
                        .Include(a => a.Message)
                        .FirstOrDefaultAsync(a => a.Id == id);

            if(Audio == null)
            {
                return NotFound();
            }

            return Audio;
        }
    }
}