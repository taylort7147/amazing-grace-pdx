using System;
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
    public class NotesController : ControllerBase
    {
        private readonly MessageContext _context;

        public NotesController(MessageContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notes>>> GetNotes()
        {
            return await _context.Notes
                   .Include(m => m.Message)
                   .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetNotes(int id)
        {
            var Notes = await _context.Notes
                        .Include(v => v.Message)
                        .FirstOrDefaultAsync(v => v.Id == id);

            if (Notes == null)
            {
                return NotFound();
            }

            return Notes;
        }

        [AllowAnonymous]
        [HttpGet("latest")]
        public async Task<ActionResult<Notes>> GetLatestNotes()
        {
            var notes = await _context.Notes
                        .Include(n => n.Message)
                        .Where(n => EF.Functions.DateDiffDay(
                            DateTools.GetNominalDateForDayOfWeek(
                                DayOfWeek.Sunday),
                            n.Message.Date) % 7 == 0)
                        .OrderByDescending(n => n.Message.Date)
                        .FirstOrDefaultAsync();

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }
    }
}