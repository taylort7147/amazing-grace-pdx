using System;
using Editor.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Editor.Areas.Identity.IdentityHostingStartup))]
namespace Editor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<EditorIdentityDbContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("EditorIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EditorIdentityDbContext>();
            });
        }
    }
}