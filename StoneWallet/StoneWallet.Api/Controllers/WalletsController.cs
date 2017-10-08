using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWallet.Api.Controllers
{
    /// <summary>
    /// Representa um controlador dos endpoints da Wallet
    /// </summary>
    [Route("api/[controller]")]
    public class WalletsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor da controller da Wallet
        /// </summary>
        /// <param name="mediator">Instância do Mediator</param>
        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consultar informações da Wallet
        /// </summary>
        /// <returns>Informações da wallet</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new QueryWalletInformation();
            return await ProcessCommand(command);
        }

        /// <summary>
        /// Alterar limite da Wallet
        /// </summary>
        /// <param name="command">Valor do novo limite</param>
        /// <returns>Mensagem de sucesso ou erro na alteração do limite da Wallet</returns>
        [HttpPut, Route("limit")]
        public async Task<IActionResult> ChangeWalletLimit([FromBody] ChangeWalletLimitCommand command)
        {
            return await ProcessCommand(command);
        }

        /// <summary>
        /// Adiciona um novo cartão de crédito na Wallet
        /// </summary>
        /// <param name="command">Dados do cartão de crédito</param>
        /// <returns>Dados do cartão adicionado ou mensagens de erro</returns>
        [HttpPost, Route("creditcards")]
        public async Task<IActionResult> AddCreditCard([FromBody] AddCreditCardCommand command)
        {
            return await ProcessCommand(command);
        }

        /// <summary>
        /// Exclui um cartão de crédito
        /// </summary>
        /// <param name="command">Dados do cartão para exclusão</param>
        /// <returns>Mensagem de sucesso ou erro na exclusão do cartão</returns>
        [HttpDelete, Route("creditcards")]
        public async Task<IActionResult> RemoveCreditCard([FromBody] RemoveCreditCardCommand command)
        {
            return await ProcessCommand(command);
        }

        /// <summary>
        /// Realiza uma compra
        /// </summary>
        /// <param name="command">Valor da compra</param>
        /// <returns>Mensagem de sucesso ou erro na compra</returns>
        [HttpPost, Route("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseCommand command)
        {
            return await ProcessCommand(command);
        }

        /// <summary>
        /// Realiza o pagamento de determinado valor no cartão de crédito
        /// </summary>
        /// <param name="command">Dados para pagamento do cartão</param>
        /// <returns>Mensagem de sucesso ou erro no pagamento do cartão</returns>
        [HttpPost, Route("creditcards/payment")]
        public async Task<IActionResult> PayCreditCard([FromBody] PayCreditCardCommand command)
        {
            return await ProcessCommand(command);
        }

        private async Task<IActionResult> ProcessCommand(IRequest<Response> command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            if (response == null)
            {
                return NoContent();
            }

            if (response.Errors.Any())
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}