using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronBookStoreAuthJWT.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IronBookStoreAuthJWT
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //TODO: migrate & seed the database.  Best practice = in Main, using service scope
            RunSeeding(host);
            host.Run();
        }

        private static void RunSeeding(IHost host)
        {
            //TODO: Create scope & ask for db service directly to the Provider because we can inject ApplicationDbContext 
            //by constructor here.
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    //Check DB exists and apply migrations
                    context.Database.Migrate();
                    context.EnsureSeedDataForContext().Wait();
                }
                catch (System.Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
