using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageManager.Models;

namespace MessageManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly MessageContext _context;

        public VideosController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetMessages()
        {
            return await _context.Video
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetMessage(int id)
        {
            var video = await _context.Video
                        .Include(v => v.Message)
                        .FirstOrDefaultAsync(v => v.Id == id);

            if(video == null)
            {
                return NotFound();
            }

            return video;
        }
    }
}