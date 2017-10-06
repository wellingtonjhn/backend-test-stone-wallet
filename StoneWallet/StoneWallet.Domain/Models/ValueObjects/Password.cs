using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StoneWallet.Domain.Models.ValueTypes
{
    public sealed class Password
    {
        public string Encoded { get; }

        public Password(string password)
        {
            Encoded = EncodePassword(password);
        }

        private static string EncodePassword(string password)
        {
            var result = string.Empty;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using (var sha256 = new SHA256Managed())
                {
                    var hash = sha256.ComputeHash(bytes);
                    stream.Write(hash, 0, hash.Length);

                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes);
                }

            }
            return result;
        }
    }
}
