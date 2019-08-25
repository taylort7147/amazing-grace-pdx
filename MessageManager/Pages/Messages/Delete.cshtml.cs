using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MessageManager.Authorization;
using MessageManager.Models;

namespace MessageManager.Pages_Messages
{
    [Authorize(Policy = Constants.ReadWritePolicy)]
    public class DeleteModel : PageModel
    {
        private readonly MessageContext _context;

        public DeleteModel(MessageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Message.FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Message.FindAsync(id);

            if (Message != null)
            {
                if(Message.Video != null)
                {
                    _context.Video.Remove(Message.Video);
                }

                if(Message.Audio != null)
                {
                    _context.Audio.Remove(Message.Audio);
                }

                if(Message.Notes != null)
                {
                    _context.Notes.Remove(Message.Notes);
                }

                _context.Message.Remove(Message);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
