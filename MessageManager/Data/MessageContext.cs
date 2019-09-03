using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MessageManager.Models;

public class MessageContext : DbContext
{
    public MessageContext (DbContextOptions<MessageContext> options)
        : base(options)
    {
    }

    public DbSet<MessageManager.Models.Message> Message { get; set; }

    public DbSet<MessageManager.Models.Video> Video { get; set; }

    public DbSet<MessageManager.Models.Audio> Audio { get; set; }

    public DbSet<MessageManager.Models.Notes> Notes { get; set; }

    public DbSet<MessageManager.Models.Series> Series { get; set; }
}
