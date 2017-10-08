using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    /// <summary>
    /// Representa uma consulta de dados do suário logado
    /// </summary>
    public class QueryUserInformation : IRequest<Response>
    {
    }
}