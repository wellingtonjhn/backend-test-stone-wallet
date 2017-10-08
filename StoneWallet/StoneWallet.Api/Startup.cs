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
    /// <summary>
    /// Responsável por fazer a inicialização da aplicação
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Cria um Inicializador para a aplicação
        /// </summary>
        /// <param name="configuration">Configurações da aplicação</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Registra os serviços no injetor de dependências
        /// </summary>
        /// <param name="services">Injetor de dependências</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepository();
            services.AddMediatR(Configuration);
            services.AddJwtOptions(Configuration);
            services.AddLogging();
            services.AddMvcWithCustomConfiguration();
            services.AddSwagger();
            services.AddSingleton(Configuration);
        }

        /// <summary>
        /// Configura a aplicação
        /// </summary>
        /// <param name="app">Builder da aplicação</param>
        /// <param name="env">Define variáveis de ambiente</param>
        /// <param name="loggerFactory">Factory para criação de Logger</param>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stone Wallet");
            });
        }
    }
}
