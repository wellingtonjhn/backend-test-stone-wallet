using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace StoneWallet.Api.Settings
{
    /// <summary>
    /// Configuração para geração do token JWT
    /// </summary>
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ValidForMinutes { get; set; }

        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForMinutes);
        public DateTime Expiration => IssuedAt.AddMinutes(ValidFor.TotalMinutes);

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
    }

    /// <summary>
    /// Credenciais para geração do token JWT
    /// </summary>
    public class SigningSettings
    {
        public SigningCredentials SigningCredentials { get; }

        public SigningSettings(IConfiguration configuration)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SigningSettings:Key"]));
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}