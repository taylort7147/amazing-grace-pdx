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
    public class SeriesController : Controller
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
            var seriesList = await _context.Series
                .Include(s => s.Messages)
                .OrderBy(s => s.Messages
                    .Max(m => m.Date))
                .ToListAsync();
            foreach (var series in seriesList)
            {
                series.Messages = series.Messages.OrderBy(s => s.Date);
            }
            return seriesList;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            var series = await _context.Series
                .Include(s => s.Messages)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (series == null)
            {
                return NotFound();
            }

            series.Messages = series.Messages.OrderBy(s => s.Date);
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

            if (message == null)
            {
                return NotFound();
            }

            return message.Series;
        }
    }
}
