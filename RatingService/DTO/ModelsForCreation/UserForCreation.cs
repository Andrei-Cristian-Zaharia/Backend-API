using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ModelsForCreation
{
    /// <summary>
    /// Object of user that contains all the data needed to create a user in DB.
    /// </summary>
    public class UserForCreation
    {
        #region Members

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Profile picture url.
        /// </summary>
        public string ProfilePicture { get; set; }

        #endregion
    }
}
