using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentProcessor.Data;

namespace PaymentProcessor
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
            services.AddDbContext<PaymentProcessorContext>(x => x.UseSqlite("Data Source=LocalDatabase.db"));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payment Processor API",
                    Description = "A Web API built with C# that accepts JSON-formatted opening hours of a " +
                        "restaurant as an input, either as a string or JSON object, and outputs hours in more " +
                        "human readable format",
                    Contact = new OpenApiContact
                    {
                        Name = "Aruorihwo Asagbra",
                        Email = "aruorihwoasagbra@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/aruorihwo-asagbra/"),
                    }
                });
            });

            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, 
            PaymentProcessorContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Opening Hours v1.0");
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile("Logs/OpeningHoursLog-{Date}.txt", isJson: true);
        }
    }
}
