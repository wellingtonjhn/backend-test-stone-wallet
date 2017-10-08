using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    /// <summary>
    /// Define um repositório de Usuário
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Obtém um usuário por Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Retorna um Usuário</returns>
        Task<User> Get(Guid id);

        /// <summary>
        /// Cria um novo Usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>
        Task CreateUser(User user);

        /// <summary>
        /// Verifica se já existe um usuário com determinado E-mail
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <returns>True para usuário já existente, False para não existente</returns>
        Task<bool> ExistsUser(string email);

        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Usuário autenticado</returns>
        Task<User> Authenticate(string email, string password);

        /// <summary>
        /// Altera a senha de um usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>
        Task ChangePassword(User user);
    }
}