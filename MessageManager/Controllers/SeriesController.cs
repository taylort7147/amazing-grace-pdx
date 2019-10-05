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
    public class SeriesController : ControllerBase
    {
        private readonly MessageContext _context;

        public SeriesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
        {
            return await _context.Series
                   .Include(m => m.Messages)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            var series = await _context.Series
                         .Include(s => s.Messages)
                         .FirstOrDefaultAsync(s => s.Id == id);

            if(series == null)
            {
                return NotFound();
            }

            return series;
        }

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Series>> GetLatestSeries()
        {
            var message = await _context.Message
                          .Where(m => m.SeriesId != null)
                          .OrderByDescending(m => m.Date)
                          .Include(m => m.Series)
                          .FirstOrDefaultAsync();

            if(message == null)
            {
                return NotFound();
            }

            return message.Series;
        }
    }
}