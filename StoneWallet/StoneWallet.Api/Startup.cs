using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoneWallet.Api.Settings;
using StoneWallet.Application.Core.Middlewares;
using StoneWallet.Domain.Contracts;
using StoneWallet.Repository;
using System.Reflection;

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
            services.Configure<CommandSettings>(Configuration.GetSection(nameof(CommandSettings)));

            var provider = services.BuildServiceProvider();
            var settings = provider.GetService<IOptionsSnapshot<CommandSettings>>();

            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.Load(new AssemblyName(settings.Value.Assembly)))
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationMiddleware<,>));
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMediatR();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Stone Wallet is online! =)");
            });
        }
    }
}
