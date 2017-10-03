using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoneWallet.Application.Commands;

namespace StoneWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            var response = await _mediator.Send(command);

            return Ok(response.Result);
        }
    }
}
