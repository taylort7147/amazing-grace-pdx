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
    public class VideosController : ControllerBase
    {
        private readonly MessageContext _context;

        public VideosController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            return await _context.Video
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideos(int id)
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

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Video>> GetLatestVideo()
        {
            var video = await _context.Video
                        .Include(v => v.Message)
                        .OrderByDescending(v => v.Message.Date)
                        .FirstOrDefaultAsync();


            if(video == null)
            {
                return NotFound();
            }

            return video;
        }
    }
}