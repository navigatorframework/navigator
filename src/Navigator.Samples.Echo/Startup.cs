using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator.Extensions.Shipyard;
using Navigator.Extensions.Store;
using Navigator.Samples.Echo.Entity;
using Navigator.Samples.Echo.Persistence;

namespace Navigator.Samples.Echo
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

            services.AddApiVersioning();

            services.AddNavigator(
                options =>
                {
                    options.SetTelegramToken(Configuration["BOT_TOKEN"]);
                    options.SetWebHookBaseUrl(Configuration["BASE_WEBHOOK_URL"]);
                    options.RegisterActionsFromAssemblies(typeof(Startup).Assembly);
                }
            ).AddNavigatorStore<NavigatorSampleDbContext, SampleUser>(
                builder =>
                {
                    builder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Navigator.Samples.Echo"));
                },
                options => { options.SetUserMapper<SampleUserMapper>(); }
            ).AddShipyard(options =>
            {
                options.SetShipyardApiKey(Configuration["SHIPYARD_API_KEY"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            serviceScope?.ServiceProvider.GetRequiredService<NavigatorSampleDbContext>().Database.Migrate();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapNavigator();
            });
        }
    }
}