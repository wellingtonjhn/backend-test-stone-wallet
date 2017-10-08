using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class ChangeWalletLimitHandler : IAsyncRequestHandler<ChangeWalletLimitCommand, Response>
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IWalletRepository _repository;

        public ChangeWalletLimitHandler(IAuthenticatedUser authenticatedUser, IWalletRepository repository)
        {
            _authenticatedUser = authenticatedUser;
            _repository = repository;
        }

        public async Task<Response> Handle(ChangeWalletLimitCommand message)
        {
            try
            {
                var wallet = await _repository.GetWalletByUser(_authenticatedUser.UserId);
                wallet.ChangeWalletLimit(message.Limit);

                await _repository.ChangeWalletLimit(wallet);

                return new Response($"Limite da Wallet alterado para {wallet.WalletLimit}");
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}