using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Titan.Contexts;

namespace Titan.Data
{

    public class DbStartup
    {
        public void SetupDb(IServiceCollection services, string baseConnectionString)
        {
            var connectionString = new ApplicationContext().GetConnectionString(baseConnectionString);

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<TitanContext>(options => options.UseNpgsql(connectionString));
        }
    }
}