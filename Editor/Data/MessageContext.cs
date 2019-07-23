using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Editor.Models;

    public class MessageContext : DbContext
    {
        public MessageContext (DbContextOptions<MessageContext> options)
            : base(options)
        {
        }

        public DbSet<Editor.Models.Message> Message { get; set; }

        public DbSet<Editor.Models.Video> Video { get; set; }

        public DbSet<Editor.Models.Audio> Audio { get; set; }

        public DbSet<Editor.Models.Notes> Notes { get; set; }
    }