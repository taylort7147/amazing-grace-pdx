using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Data;
using MessageManager.Models;
using MessageManager.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : Controller
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(string series, bool? loadContent)
        {
            if (!string.IsNullOrEmpty(series))
            {
                return await GetMessagesBySeries(series, loadContent);
            }

            var selectedMessages = _context.Message.OrderByDescending(m => m.Date);

            if (selectedMessages == null)
            {
                return NotFound();
            }

            if(loadContent == null || loadContent == true)
            {
                foreach (var message in selectedMessages)
                {
                    await _context.Entry(message).Reference(m => m.Series).LoadAsync();
                    await _context.Entry(message).Reference(m => m.Video).LoadAsync();
                    await _context.Entry(message).Reference(m => m.Audio).LoadAsync();
                    await _context.Entry(message).Reference(m => m.Notes).LoadAsync();
                    await _context.Entry(message).Collection(m => m.BibleReferences).LoadAsync();
                }
            }

            var messages = selectedMessages.ToList();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Message
                          .Include(m => m.Audio)
                          .Include(m => m.Video)
                          .Include(m => m.Notes)
                          .Include(m => m.Series)
                          .Include(m => m.BibleReferences)
                          .FirstOrDefaultAsync(m => m.Id == id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySeries(string series, bool? loadContent)
        {
            var selectedSeries = await _context.Series.FirstOrDefaultAsync(s => s.Name.ToLower() == series.ToLower());
            if (selectedSeries == null)
            {
                return NotFound();
            }

            await _context.Entry(selectedSeries).Collection(s => s.Messages).LoadAsync();
            selectedSeries.Messages = selectedSeries.Messages.OrderBy(m => m.Date);
            foreach (var message in selectedSeries.Messages)
            {
                await _context.Entry(message).Reference(m => m.Video).LoadAsync();
                await _context.Entry(message).Reference(m => m.Audio).LoadAsync();
                await _context.Entry(message).Reference(m => m.Notes).LoadAsync();
                await _context.Entry(message).Collection(m => m.BibleReferences).LoadAsync();
            }
            var messages = selectedSeries.Messages.ToList();

            if (messages == null)
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
                          .Include(m => m.Series)
                          .Include(m => m.BibleReferences)
                          .Where(m => m.Audio != null || m.Video != null || m.Notes != null)
                          .OrderByDescending(m => m.Date)
                          .Take(n.GetValueOrDefault(1))
                          .ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [AllowAnonymous]
        [HttpGet("latest_audio")]
        public async Task<ActionResult<IEnumerable<Message>>> GetLatestNMessagesWithAudio(int? n)
        {
            var messages = await _context.Message
                          .Include(m => m.Audio)
                          .Include(m => m.Series)
                          .Where(m => m.Audio != null)
                          .OrderByDescending(m => m.Date)
                          .Take(n.GetValueOrDefault(1))
                          .ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [AllowAnonymous]
        [HttpGet("latest_notes")]
        public async Task<ActionResult<IEnumerable<Message>>> GetLatestNMessagesWithNotes(int? n)
        {
            var messages = await _context.Message
                          .Include(m => m.Notes)
                          .Include(m => m.Series)
                          .Where(m => m.Notes != null)
                          .OrderByDescending(m => m.Date)
                          .Take(n.GetValueOrDefault(1))
                          .ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [AllowAnonymous]
        [HttpGet("latest_video")]
        public async Task<ActionResult<IEnumerable<Message>>> GetLatestNMessagesWithVideo(int? n)
        {
            var messages = await _context.Message
                          .Include(m => m.Video)
                          .Include(m => m.Series)
                          .Where(m => m.Video != null)
                          .OrderByDescending(m => m.Date)
                          .Take(n.GetValueOrDefault(1))
                          .ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        public class SearchResult
        {
            public bool Success { get; set; }
            public IEnumerable<string> Errors { get; set; }
            public IEnumerable<Message> Messages { get; set; }
            public IEnumerable<string> MatchingBibleReferences { get; set; }
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<SearchResult>> SearchMessages(string searchText)
        {
            var searchResult = new SearchResult();
            var criteria = MessageSearch.GetCriteria(searchText);
            var result = MessageSearch.Search(_context, criteria);
            
            searchResult.Success = result.Success;
            searchResult.Errors = result.Errors;
            if(!result.Success)
            {
                return searchResult;
            }

            searchResult.Messages = await result.Messages.ToListAsync();
            searchResult.MatchingBibleReferences = result.MatchingBibleReferences;
            if (searchResult.Messages == null)
            {
                searchResult.Messages = new List<Message>();
            }
            return searchResult;           
        }

    }
}
