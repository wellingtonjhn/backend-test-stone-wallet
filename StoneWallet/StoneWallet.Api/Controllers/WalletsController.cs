using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoneWallet.Application.Commands.Wallets;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class WalletsController : Controller
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new QueryWalletInformation());

            if (response == null)
            {
                return NoContent();
            }
            return Ok(response.Result);
        }

        [HttpPut, Route("limit")]
        public async Task<IActionResult> ChangeWalletLimit([FromBody] ChangeWalletLimitCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Result);
        }
    }
}