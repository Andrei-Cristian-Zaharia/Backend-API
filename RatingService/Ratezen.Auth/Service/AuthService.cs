using Core.Features;
using Core.Interfaces.Repositories;
using Core.Options;
using DTO.Models;
using Microsoft.IdentityModel.Tokens;
using Ratezen.Auth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ratezen.Auth.Service
{
    /// <summary>
    /// The interface of EmailService.
    /// This contains methods that allow an user to authenticate.
    /// </summary>
    public interface IAuthService 
    {
        #region Methods

        /// <summary>
        /// This method allow an user to authenticate as a client.
        /// </summary>
        /// <param name="request">Contains the username and the password of the user.</param>
        /// <returns></returns>
        Task<AuthenticateResponse> AuthenticateClient(AuthenticateRequest request);

        /// <summary>
        /// This method allows an user to authenticate as an admin.
        /// </summary>
        /// <param name="request">Contains the username and the password of the user.</param>
        /// <returns></returns>
        Task<AuthenticateResponse> AuthenticateAdmin(AuthenticateRequest request);

        #endregion
    }

    /// <summary>
    /// This contains methods that allow an user to authenticate.
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Members

        /// <summary>
        /// Interface of user repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Interface of authSettings.
        /// </summary>
        private readonly IAuthSettings _authService;

        /// <summary>
        /// Interface of encryptionSettings.
        /// </summary>
        private readonly IEncryptionSettings _encryptionSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository">UserRepository</param>
        /// <param name="authSettings">AuthSettings</param>
        /// <param name="encryptionSettings">EncryptionSettings</param>
        public AuthService (IUserRepository userRepository, IAuthSettings authSettings, IEncryptionSettings encryptionSettings)
        {
            _encryptionSettings = encryptionSettings ?? throw new ArgumentNullException(nameof(encryptionSettings));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authService = authSettings ?? throw new ArgumentNullException(nameof(authSettings));
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow an user to authenticate as a client.
        /// </summary>
        /// <param name="request">Contains the username and the password of the user.</param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> AuthenticateClient(AuthenticateRequest request)
        {
            if (request.Username == null || request.Password == null)
                return null;

            if (await _userRepository.UserExists(request.Username))
                return null;

            var user = await _userRepository.GetUserByCredentials(request.Username, request.Password, _encryptionSettings);

            if (user == null) // user password or username is incorect
                return null;

            var claims = GetClientClaims();

            GenerateTokenDescriptor(user, claims, out var tokenDescriptor);
            return new AuthenticateResponse(user, GenerateSecurityToken(tokenDescriptor));
        }

        /// <summary>
        /// This method allows an user to authenticate as an admin.
        /// </summary>
        /// <param name="request">Contains the username and the password of the user.</param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> AuthenticateAdmin(AuthenticateRequest request)
        {
            if (request.Username == null || request.Password == null)
                return null;

            if (await _userRepository.UserExists(request.Username))
                return null;

            var user = await _userRepository.GetUserByCredentials(request.Username, request.Password, _encryptionSettings);

            if (user == null) // user password or username is incorect
                return null;

            if (!CheckIfAdmin(user))
                return null;

            var claims = GetAdminClaims();

            GenerateTokenDescriptor(user, claims, out var tokenDescriptor);
            return new AuthenticateResponse(user, GenerateSecurityToken(tokenDescriptor));
        }

        /// <summary>
        /// Generates security token based on token descriptor.
        /// </summary>
        /// <param name="tokenDescriptor">Contains token settings.</param>
        /// <returns></returns>
        private string GenerateSecurityToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHolder = new JwtSecurityTokenHandler();
            var token = tokenHolder.CreateToken(tokenDescriptor);
            return tokenHolder.WriteToken(token);
        }

        /// <summary>
        /// Generates token descriptor containing token settings.
        /// </summary>
        /// <param name="user">User that needs a token.</param>
        /// <param name="claims">Claims that need to be applied.</param>
        /// <param name="tokenDescriptor">Contains token settings.</param>
        private void GenerateTokenDescriptor(User user, List<Claim> claims, out SecurityTokenDescriptor tokenDescriptor)
        {
            var key = Encoding.ASCII.GetBytes(_authService.Secret);

            var defaultClaims = new List<Claim>
            {
                new Claim("id", user.Id.ToString())
            };

            claims.AddRange(defaultClaims);

            tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        /// <summary>
        /// Checks if the user is an admin.
        /// </summary>
        /// <param name="user">User needed to be verified.</param>
        /// <returns></returns>
        private bool CheckIfAdmin(User user)
        {
            if (!user.IsAdmin)
                return false;

            return true;
        }

        /// <summary>
        /// Get admin claims.
        /// </summary>
        /// <returns></returns>
        private List<Claim> GetAdminClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin")
            };
        }

        /// <summary>
        /// Get client claims.
        /// </summary>
        /// <returns></returns>
        private List<Claim> GetClientClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Client")
            };
        }

        #endregion
    }
}
