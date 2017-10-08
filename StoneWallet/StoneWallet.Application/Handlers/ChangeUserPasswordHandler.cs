using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class ChangeUserPasswordHandler : IAsyncRequestHandler<ChangeUserPasswordCommand, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public ChangeUserPasswordHandler(IUsersRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

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