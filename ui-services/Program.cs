using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Titan.Contexts;
using Titan.Initializers;
using NLog.Web;

namespace Titan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host;
            
            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog("./NLog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                host = BuildWebHost(args);
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                logger.Error(e, "Stopped program because of exception");
                throw;
            }

            #if DEBUG
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TitanContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                }
            }
            #endif
            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
    }
}
