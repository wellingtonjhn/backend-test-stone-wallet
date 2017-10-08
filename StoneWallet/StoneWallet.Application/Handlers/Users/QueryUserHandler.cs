using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
{
    /// <summary>
    /// Responsável por tratar o comando de Consulta de Dados do Usuário
    /// </summary>
    public class QueryUserHandler : IAsyncRequestHandler<QueryUserInformation, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Cria um tratador para o comando de Consulta de Dados do Usuário
        /// </summary>
        /// <param name="repository">Repositório do Usuário</param>
        /// <param name="authenticatedUser">Usuário autenticado</param>
        public QueryUserHandler(IUsersRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de consulta de dados do usuário</param>
        /// <returns>Resposta da execução do comando</returns>
        public async Task<Response> Handle(QueryUserInformation message)
        {
            try
            {
                var user = await _repository.Get(_authenticatedUser.UserId);

                if (user == null)
                {
                    return new Response().AddError("Nenhum registro encontrado");
                }

                return new Response(user);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}