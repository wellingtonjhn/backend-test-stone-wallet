using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StoneWallet.Domain.Models.ValueObjects
{
    /// <summary>
    /// Representa uma senha
    /// </summary>
    public sealed class Password
    {
        public string Encoded { get; }

        /// <summary>
        /// Cria uma senha criptografada
        /// </summary>
        /// <param name="password">Senha em plain-text à ser criptografada</param>
        public Password(string password)
        {
            Encoded = EncodePassword(password);
        }

        private static string EncodePassword(string password)
        {
            string result;
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
