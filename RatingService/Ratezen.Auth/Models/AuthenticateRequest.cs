using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ratezen.Auth.Models
{

    /// <summary>
    /// Form that contains a requird username and password for the user to auth.
    /// </summary>
    public class AuthenticateRequest
    {
        #region Members

        /// <summary>
        /// Requird username of the user.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Requird password of the user.
        /// </summary>
        [Required]
        public string Password { get; set; }

        #endregion
    }
}
