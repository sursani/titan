using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Titan.Contexts;

namespace Titan.Data.StartupInitializer
{
    public class DbStartup
    {
        public void SetupDb(IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<TitanContext>(options => options.UseNpgsql(connectionString));
        }
    }
}