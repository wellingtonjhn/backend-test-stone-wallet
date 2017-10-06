using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoneWallet.Application.Queries;
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
            var userId = HttpContext.User.Identity.Name;

            var query = new QueryWalletInformation(userId);
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NoContent();
            }
            return Ok(response.Result);
        }
    }
}