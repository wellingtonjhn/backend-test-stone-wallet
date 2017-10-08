using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoneWallet.Api.Extensions;
using StoneWallet.Api.Middlewares;

namespace StoneWallet.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepository();
            services.AddMediatR(Configuration);
            services.AddJwtOptions(Configuration);
            services.AddLogging();
            services.AddMvcWithCustomConfiguration();
            services.AddSingleton(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();

            app.UseErrorHandling();
            app.UseAuthenticationScheme(JwtBearerDefaults.AuthenticationScheme);
            app.UseMvc();
        }
    }
}
