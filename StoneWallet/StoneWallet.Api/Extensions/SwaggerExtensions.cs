using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace StoneWallet.Api.Extensions
{
    /// <summary>
    /// Representa extensões para configurar o Swagger para gerar documentação dos endpoints da API
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Registra o Swagger no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Stone Wallet",
                        Version = "v1",
                        Description = "Possível solução para o Desafio do Cartão de Multi-Crédito para a vaga de backend developer na Stone Pagamentos",
                        Contact = new Contact
                        {
                            Name = "Wellington Nascimento",
                            Url = "https://github.com/wellingtonjhn"
                        }
                    });

                var applicationPath = PlatformServices.Default.Application.ApplicationBasePath;
                var applicationName = PlatformServices.Default.Application.ApplicationName;
                var xmlDocPath = Path.Combine(applicationPath, $"{applicationName}.xml");

                c.IncludeXmlComments(xmlDocPath);
            });
        }
    }
}