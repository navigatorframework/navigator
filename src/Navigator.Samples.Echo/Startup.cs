using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator.Configuration;
using Navigator.Providers.Telegram;

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

            services.AddNavigator(options =>
            {
                options.SetWebHookBaseUrl(Configuration["BASE_WEBHOOK_URL"]);
                options.RegisterActionsFromAssemblies(typeof(Startup).Assembly);
            }).WithProvider.Telegram(options =>
            {
                options.SetTelegramToken(Configuration["BOT_TOKEN"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapNavigator()
                    .ForProvider.Telegram();
            });
        }
    }
}