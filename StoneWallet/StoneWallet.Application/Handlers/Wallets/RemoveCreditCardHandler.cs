using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    public class RemoveCreditCardHandler : IAsyncRequestHandler<RemoveCreditCardCommand, Response>
    {
        private readonly ICreditCardRepository _repository;

        public RemoveCreditCardHandler(ICreditCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(RemoveCreditCardCommand message)
        {
            try
            {
                var creditCard = await _repository.Get(message.CreditCardId);
                if (creditCard == null)
                {
                    return new Response().AddError("Nenhum cartão de crédito encontrado");
                }
                
                await _repository.RemoveCreditCard(creditCard);

                return new Response("Cartão de crédito removido com sucesso");
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}