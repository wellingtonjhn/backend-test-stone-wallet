using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StoneWallet.Api.Settings
{
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ValidForMinutes { get; set; }
        public string Subject { get; set; }

        public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForMinutes);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

        //public SecurityKey Key { get; }
        //public SigningCredentials SigningCredentials { get; }

        //public JwtSettings()
        //{
        //    using (var provider = new RSACryptoServiceProvider(2048))
        //    {
        //        Key = new RsaSecurityKey(provider.ExportParameters(true));
        //    }
        //    SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        //}
    }

    public class SigningSettings
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningSettings()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}