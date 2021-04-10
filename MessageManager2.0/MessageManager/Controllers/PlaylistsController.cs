using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly MessageContext _context;

        public PlaylistsController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _context.Playlist
                .Include(p => p.Series)
                .OrderBy(p => p.Series.Name)
                .ToListAsync();
            return playlists;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _context.Playlist
                .Include(p => p.Series)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Playlist>> GetLatestPlaylist()
        {
            var playlist = await _context.Message
                          .Where(m => m.SeriesId != null)
                          .OrderByDescending(m => m.Date)
                          .Include(m => m.Series)
                          .Join(_context.Playlist,
                            m => m.SeriesId,
                            p => p.SeriesId,
                            (m, p) => p)
                          .FirstOrDefaultAsync();

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }
    }
}
