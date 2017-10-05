using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneWallet.Api.Handlers;
using StoneWallet.Api.Settings;
using StoneWallet.Application.Core.Middlewares;
using StoneWallet.Domain.Contracts;
using StoneWallet.Repository;
using System;
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
            services.Configure<TokenSettings>(Configuration.GetSection(nameof(TokenSettings)));

            services.AddSingleton(new SigningSettings());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationMiddleware<,>));

            services.AddScoped<MongoDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddLogging();
            services.AddMediatR();

            var provider = services.BuildServiceProvider();
            var commandSettings = provider.GetService<IOptionsSnapshot<CommandSettings>>();
            var tokenSettings = provider.GetService<IOptionsSnapshot<TokenSettings>>();
            var signingSettings = provider.GetService<IOptionsSnapshot<SigningSettings>>();
            ;
            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.Load(new AssemblyName(commandSettings.Value.Assembly)))
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingSettings.Value.Key;
                paramsValidation.ValidAudience = tokenSettings.Value.Audience;
                paramsValidation.ValidIssuer = tokenSettings.Value.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();

            app.UseMiddleware(typeof(ErrorHandling));
            app.UseMvc();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Stone Wallet is online! =)");
            });
        }
    }
}
