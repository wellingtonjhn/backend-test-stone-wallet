using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoneWallet.Api.Settings;
using StoneWallet.Application.Core.Security;
using StoneWallet.Domain.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StoneWallet.Api.Extensions
{
    public static class SecurityExtensions
    {
        public static void AddMvcWithPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        }

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

        public static long ToUnixEpochDate(this DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        public static string ToUnixEpochDateToString(this DateTime date)
        {
            return $"{ToUnixEpochDate(date)}";
        }

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