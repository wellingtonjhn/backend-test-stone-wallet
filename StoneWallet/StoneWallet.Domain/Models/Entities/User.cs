using StoneWallet.Domain.Models.ValueObjects;
using System;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa um usuário do sistema
    /// </summary>
    public class User : Entity
    {
        public string Name { get; }
        public string Email { get; }
        public Password Password { get; private set; }

        protected User() { }

        /// <summary>
        /// Cria um novo usuário no sistema
        /// </summary>
        /// <param name="name">Nome do usuário</param>
        /// <param name="email">E-mail de acesso</param>
        /// <param name="password">Senha de acesso</param>
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = new Password(password);
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

            Password = new Password(newPassword);
        }
    }
}