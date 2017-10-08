using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando de Exclusão de um Cartão de Crédito
    /// </summary>
    public class RemoveCreditCardHandler : IAsyncRequestHandler<RemoveCreditCardCommand, Response>
    {
        private readonly ICreditCardRepository _repository;

        /// <summary>
        /// Cria um tratador para o comando de Exclusão de Cartão de Crédito 
        /// </summary>
        /// <param name="repository">Repositório de Cartão de Crédito</param>
        public RemoveCreditCardHandler(ICreditCardRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de Exclusão de Cartão de Crédito</param>
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