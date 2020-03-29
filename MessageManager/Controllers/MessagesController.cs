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
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(string series)
        {
            if(!string.IsNullOrEmpty(series))
            {
                return await GetMessagesBySeries(series);
            }

            return await _context.Message
                   .Include(m => m.Series)
                   .Include(m => m.Audio)
                   .Include(m => m.Video)
                   .Include(m => m.Notes)
                   .OrderByDescending(m => m.Date)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Message
                          .Include(m => m.Audio)
                          .Include(m => m.Video)
                          .Include(m => m.Notes)
                          .FirstOrDefaultAsync(m => m.Id == id);

            if(message == null)
            {
                return NotFound();
            }

            return message;
        }

        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySeries(string series)
        {
            var selectedSeries = await _context.Series.FirstOrDefaultAsync(s => s.Name.ToLower() == series.ToLower());
            if(selectedSeries == null)
            {
                return NotFound();
            }

            await _context.Entry(selectedSeries).Collection(s => s.Messages).LoadAsync();
            selectedSeries.Messages = selectedSeries.Messages.OrderBy(m => m.Date);
            foreach(var message in selectedSeries.Messages)
            {
                await _context.Entry(message).Reference(m => m.Video).LoadAsync();
                await _context.Entry(message).Reference(m => m.Audio).LoadAsync();
                await _context.Entry(message).Reference(m => m.Notes).LoadAsync();
            }
            var messages = selectedSeries.Messages.ToList();

            if(messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<Message>>> GetLatestNMessages(int? n)
        {
            var messages = await _context.Message
                          .Include(m => m.Audio)
                          .Include(m => m.Video)
                          .Include(m => m.Notes)
                          .OrderByDescending(m => m.Date)
                          .Take(n.GetValueOrDefault(1))
                          .ToListAsync();

            if(messages == null)
            {
                return NotFound();
            }

            return messages;
        }
    }
}
