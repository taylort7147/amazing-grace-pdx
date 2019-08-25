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
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Message
                   .Include(m => m.Audio)
                   .Include(m => m.Video)
                   .Include(m => m.Notes)
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
    }
}