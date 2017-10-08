using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    public class QueryUserInformation : IRequest<Response>
    {
    }
}