using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoneWallet.Api.Extensions;
using StoneWallet.Api.Settings;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Queries;
using StoneWallet.Application.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace StoneWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SigningSettings _signingSettings;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IMediator mediator, IOptions<JwtSettings> jwtSettings, SigningSettings signingSettings)
        {
            _mediator = mediator;
            _signingSettings = signingSettings;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new QueryUserInformation());

            if (response == null)
            {
                return NoContent();
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="command">Informações do usuário</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Created("/accounts", response.Result);
        }

        /// <summary>
        /// Autentica um usuário na api
        /// </summary>
        /// <param name="command">Informações de login</param>
        /// <returns>Token JWT</returns>
        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var identity = GetClaimsIdentity((UserResponse)response.Result);
            var jwt = identity.CreateJwtToken(_jwtSettings, _signingSettings);

            return Ok(jwt);
        }

        private ClaimsIdentity GetClaimsIdentity(UserResponse user)
        {
            return new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Iat, _jwtSettings.IssuedAt.ToUnixEpochDateToString(), ClaimValueTypes.Integer64),
                }
            );
        }
    }
}
