using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoneWallet.Application.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Result);
        }

        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserCommand command)
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
