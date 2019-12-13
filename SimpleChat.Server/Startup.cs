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

namespace SimpleChat.Server
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
            services.AddControllers();

            services.AddSingleton(GetOptions);
            services.AddScoped<ChatContext, ChatContext>();
            services.AddScoped<IChatService, ChatService>();
        }

        DbContextOptions<ChatContext> GetOptions(IServiceProvider service)
        {
            DbContextOptionsBuilder<ChatContext> optionsBuilder = new DbContextOptionsBuilder<ChatContext>();

            string dbFilePath = @"chat.db";

            optionsBuilder.UseSqlite($"Data Source = {dbFilePath}");

            return optionsBuilder.Options;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var context = new ChatContext(GetOptions(app.ApplicationServices)))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            
        }
    }
}
