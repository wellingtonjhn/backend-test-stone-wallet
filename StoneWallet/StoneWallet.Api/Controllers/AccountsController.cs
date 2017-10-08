using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoneWallet.Api.Extensions;
using StoneWallet.Api.Settings;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Domain.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace StoneWallet.Api.Controllers
{
    /// <summary>
    /// Representa um controlador dos endpoints de Usuário
    /// </summary>
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SigningSettings _signingSettings;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Construtor da controller de Usuário
        /// </summary>
        /// <param name="mediator">Instância do Mediator</param>
        /// <param name="jwtSettings">Configurações do token JWT</param>
        /// <param name="signingSettings">Credênciais para assinatura do token</param>
        public AccountsController(IMediator mediator, IOptions<JwtSettings> jwtSettings, SigningSettings signingSettings)
        {
            _mediator = mediator;
            _signingSettings = signingSettings;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="command">Informações do usuário</param>
        /// <returns>Informações básicas do usuário recém criado</returns>
        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            if (response.Errors.Any())
            {
                return BadRequest(response);
            }
            return Created("accounts/profile", response);
        }

        /// <summary>
        /// Autentica um usuário na api
        /// </summary>
        /// <param name="command">Informações de login</param>
        /// <returns>Token JWT</returns>
        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            if (response.Errors.Any())
            {
                return BadRequest(response);
            }

            var identity = GetClaimsIdentity((User)response.Result);
            var jwt = identity.CreateJwtToken(_jwtSettings, _signingSettings);

            return Ok(jwt);
        }

        /// <summary>
        /// Obtém os dados do usuário logado
        /// </summary>
        /// <returns>Dados do usuário logado</returns>
        [HttpGet, Route("profile")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new QueryUserInformation());

            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        /// <summary>
        /// Alteara a senha do usuário logado
        /// </summary>
        /// <param name="command">Comando de alteração de senha</param>
        /// <returns>Mensagem de sucesso ou de erro</returns>
        [HttpPut, Route("profile/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            return new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Iat, _jwtSettings.IssuedAt.ToUnixEpochDateToString(), ClaimValueTypes.Integer64),
                }
            );
        }
    }
}
