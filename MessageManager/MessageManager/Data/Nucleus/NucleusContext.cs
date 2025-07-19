using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Data.Nucleus
{
    public class NucleusContext : DbContext
    {
        public NucleusContext(DbContextOptions<NucleusContext> options)
            : base(options)
        {
        }

        public DbSet<MessageManager.Models.Nucleus.Sermon> Sermons { get; set; }
        public DbSet<MessageManager.Models.Nucleus.Audio> Audio { get; set; }
        public DbSet<MessageManager.Models.Nucleus.Notes> Notes { get; set; }
        public DbSet<MessageManager.Models.Nucleus.Playlist> Playlists { get; set; }
    }
}