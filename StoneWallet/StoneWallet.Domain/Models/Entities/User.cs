using System;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa um usuário do sistema
    /// </summary>
    public sealed class User : Entity
    {
        public string Name { get; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public User(string name, string email, string password)
        {
            Password = password;
            Name = name;
            Email = email;
        }

        /// <summary>
        /// Altera a senha do usuário
        /// </summary>
        /// <param name="newPassword">Nova senha</param>
        /// <param name="newPasswordConfirmation">Confirmação da nova senha</param>
        public void ChangePassword(string newPassword, string newPasswordConfirmation)
        {
            if (!newPassword.Equals(newPasswordConfirmation))
            {
                throw new InvalidOperationException("As senhas não conferem");
            }

            Password = newPassword;
        }
    }
}