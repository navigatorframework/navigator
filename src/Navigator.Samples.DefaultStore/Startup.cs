using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Context;

namespace Navigator.Samples.DefaultStore
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddNavigator(options =>
            {
                options.BotToken = Configuration["BOT_TOKEN"];
                options.BaseWebHookUrl = Configuration["BASE_WEBHOOK_URL"];
            }, typeof(Startup).Assembly);

            services.AddNavigatorStore(builder =>
            {
                builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Navigator.Samples.DefaultStore"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<NavigatorDbContext>().Database.Migrate();
            
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapNavigator();
            });
        }
    }
}