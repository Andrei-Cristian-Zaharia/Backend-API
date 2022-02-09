using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Directors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ratezen.Auth.Models;
using Ratezen.Auth.Service;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Members

        /// <summary>
        /// Interface of authService.
        /// </summary>
        private readonly IAuthService _authService;

        /// <summary>
        /// Interface of configuration.
        /// </summary>
        public IConfiguration _configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService">AuthService</param>
        /// <param name="configuration">Configuration</param>
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion

        #region Methods

        /// <summary>
        /// POST method that make the auth for a client.
        /// </summary>
        /// <param name="model">Model of the user that contains an username and a password.</param>
        /// <returns></returns>
        [HttpPost("client")]
        public async Task<IActionResult> AuthenticateClient(AuthenticateRequest model)
        {
            var response = await _authService.AuthenticateClient(model);

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect !" });

            return Ok(response);
        }

        /// <summary>
        /// POST method that make the auth for an admin.
        /// </summary>
        /// <param name="model">Model of the user that contains an username and a password.</param>
        /// <returns></returns>
        [HttpPost("admin")]
        public async Task<IActionResult> AuthenticateAdmin(AuthenticateRequest model)
        {
            var response = await _authService.AuthenticateAdmin(model);

            if (response == null)
                return Unauthorized(new { message = "Incorrect account or this account is not an admin !" });

            return Ok(response);
        }

        #endregion
    }
}