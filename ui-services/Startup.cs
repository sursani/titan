﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Titan.Contexts;
using Titan.Data;
using Titan.Models;
using Titan.Services;

namespace Titan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add the service required for using options.
            services.AddOptions();
            services.Configure<ConnectionStringOptions>(Configuration.GetSection("ConnectionStrings"));

            RegisterServices(services);

            // sql
            var baseConnectionString = Configuration.GetConnectionString("TitanContext");
            new DbStartup().SetupDb(services, baseConnectionString);

            services.AddMvc();

#region authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //ValidAudience = "the audience you want to validate"
                            ValidateAudience = false,
                            //ValidIssuer = "the isser you want to validate"
                            ValidateIssuer = false,
                            
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")), 
                            
                            ValidateLifetime = true, //validate the expiration and not before values in the token

                            ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                        };
                    });
#endregion

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdministrator", policy => policy.RequireClaim(ClaimTypes.Name, "SuperAdministrator"));
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey"});
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
