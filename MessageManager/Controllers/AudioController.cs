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
    public class AudioController : ControllerBase
    {
        private readonly MessageContext _context;

        public AudioController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audio>>> GetAudio()
        {
            return await _context.Audio
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetAudio(int id)
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

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Audio>> GetLatestAudio()
        {
            var audio = await _context.Audio
                        .Include(a => a.Message)
                        .OrderByDescending(a => a.Message.Date)
                        .FirstOrDefaultAsync();


            if(audio == null)
            {
                return NotFound();
            }

            return audio;
        }
    }
}