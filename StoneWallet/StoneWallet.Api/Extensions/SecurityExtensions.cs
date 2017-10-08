using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoneWallet.Api.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StoneWallet.Api.Extensions
{
    /// <summary>
    /// Representa extensões para configurar o token JWT (Json Web Token)
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Registra as configurações necessárias para criar um token JWT no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        /// <param name="configuration">Instância das configurações da aplicação</param>
        public static void AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SigningSettings>();
            services.Configure<JwtSettings>(configuration.GetSection("Authentication:JwtSettings"));

            var provider = services.BuildServiceProvider();
            var tokenSettings = provider.GetService<IOptions<JwtSettings>>();
            var signingSettings = provider.GetService<SigningSettings>();

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidIssuer = tokenSettings.Value.Issuer;

                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = tokenSettings.Value.Audience;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.IssuerSigningKey = signingSettings.SigningCredentials.Key;

                paramsValidation.RequireExpirationTime = true;
                paramsValidation.ValidateLifetime = true;

                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
        }

        /// <summary>
        /// Converte a data em fomato UNIX
        /// </summary>
        /// <param name="date">Objeto de Data e Hora</param>
        /// <returns></returns>
        public static long ToUnixEpochDate(this DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// Converte a Data e Hora em formato Unix para String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUnixEpochDateToString(this DateTime date)
        {
            return $"{ToUnixEpochDate(date)}";
        }

        /// <summary>
        /// Cria um novo token JWT válido
        /// </summary>
        /// <param name="identity">Identidade do usuário</param>
        /// <param name="jwtSettings">Configurações do token JWT</param>
        /// <param name="signingSettings">Credênciais para assinatura do token</param>
        /// <returns>Um token JWT válido</returns>
        public static object CreateJwtToken(this ClaimsIdentity identity, JwtSettings jwtSettings, SigningSettings signingSettings)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                IssuedAt = jwtSettings.IssuedAt,
                NotBefore = jwtSettings.NotBefore,
                Expires = jwtSettings.Expiration,
                SigningCredentials = signingSettings.SigningCredentials
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return new
            {
                access_token = encodedJwt,
                token_type = JwtBearerDefaults.AuthenticationScheme.ToLower(),
                expires_in = (int)jwtSettings.ValidFor.TotalSeconds,
            };
        }
    }
}