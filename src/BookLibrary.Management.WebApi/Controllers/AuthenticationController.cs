using BookLibrary.Management.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookLibrary.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IConfiguration configuration,
            IHttpContextAccessor context)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._context = context;
        }

        /// <summary>
        /// Authorize with email and password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     //Admin
        ///     {
        ///        "email": "admin@booklibrary.com",
        ///        "password": "admin"
        ///     }
        ///     
        ///     //Customer1
        ///     {
        ///        "email": "customer1@booklibrary.com",
        ///        "password": "customer1"
        ///     }
        ///     
        ///     //Customer2
        ///     {
        ///        "email": "customer2@booklibrary.com",
        ///        "password": "customer2"
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Authorize([FromBody] AuthenticationCredentialsDto item)
        {
            this._logger.LogDebug("try authenticate");

            try
            {
                //HACK:Add authenticate logic
                if (item.Email == "admin@booklibrary.com" && item.Password == "admin")
                {
                    var token = this.GetToken(item.Email, "admin");
                    return StatusCode(StatusCodes.Status200OK, token);
                }

                if (item.Email == "customer1@booklibrary.com" && item.Password == "customer1")
                {
                    var token = this.GetToken(item.Email, "user");
                    return StatusCode(StatusCodes.Status200OK, token);
                }

                if (item.Email == "customer2@booklibrary.com" && item.Password == "customer2")
                {
                    var token = this.GetToken(item.Email, "user");
                    return StatusCode(StatusCodes.Status200OK, token);
                }

                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, "Cannot authenticate");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Check authentication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Check")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationInfo))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Check()
        {
            try
            {
                var item = this._context.HttpContext.User.Identity;
                
                return StatusCode(StatusCodes.Status200OK, new AuthenticationInfo
                {
                    Email = item.Name,
                });
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, "Cannot check");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private TokenRequestDto GetToken(string emailAddress, string role)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, emailAddress),
                new Claim(JwtRegisteredClaimNames.UniqueName, emailAddress),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtAuthentication:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(this._configuration["JwtAuthentication:Issuer"],
                this._configuration["JwtAuthentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new TokenRequestDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}