using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
{
    /// <summary>
    /// Responsável por tratar o comando de Alteração de Senha do Usuário
    /// </summary>
    public class ChangeUserPasswordHandler : IAsyncRequestHandler<ChangeUserPasswordCommand, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Cria um tratador para o comando de Alteração de Senha do Usuário
        /// </summary>
        /// <param name="repository">Repositório do Usuário</param>
        /// <param name="authenticatedUser">Usuário autenticado</param>
        public ChangeUserPasswordHandler(IUsersRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de alteração de senha do usuário</param>
        /// <returns>Resposta da execução do comando</returns>
        public async Task<Response> Handle(ChangeUserPasswordCommand message)
        {
            try
            {
                var user = await _repository.Get(_authenticatedUser.UserId);
                user.ChangePassword(message.NewPassword, message.NewPasswordConfirmation);

                await _repository.ChangePassword(user);

                return new Response("Senha alterada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }

        }
    }
}