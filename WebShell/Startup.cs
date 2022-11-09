using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShell.API.Hubs;
using WebShell.Data.Context;
using WebShell.Data.Repository;
using WebShell.Domain;

namespace WebShell
{
    public class Startup
    {

        private readonly string _corsPolicyName = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IRepository<>),typeof(EFRepository<>));

            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("WebShellDb")
                    )
                );

            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _corsPolicyName,
                    policy =>
                {
                    policy.WithOrigins("http://localhost:3000",
                            "https://localhost:3000", "http://localhost:3001",
                            "https://localhost:3001")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();

                });
            });


            services.AddLogging();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();


            app.UseRouting();
            app.UseCors(_corsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ShellHub>("/shellHub");
            });
        }
    }
}
