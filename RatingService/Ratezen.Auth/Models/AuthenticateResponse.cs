using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ratezen.Auth.Models
{
    /// <summary>
    /// Auth response of the user.
    /// </summary>
    public class AuthenticateResponse
    {
        #region Members

        /// <summary>
        /// Id of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// IsActivated check of the user.
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Token of the user.
        /// </summary>
        public string Token { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of response.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">Token needed to be attached to the user.</param>
        public AuthenticateResponse (User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            IsActivated = user.IsActivated;
            Token = token;
        }

        #endregion
    }
}
