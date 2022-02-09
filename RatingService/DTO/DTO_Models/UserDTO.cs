using System;
using System.Collections.Generic;

namespace DTO.DTO_Models
{
    /// <summary>
    /// Object of user that is returned in GET requests or responses.
    /// </summary>
    public class UserDTO
    {
        #region Members

        /// <summary>
        /// User id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Returns a bool that tells if the account was activated.
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// ActivationCode
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string ProfilePicture { get; set; }

        /// <summary>
        /// Profile picture url.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Id's of the users, that this user is following.
        /// </summary>
        public IEnumerable<string> Following { get; set; }

        #endregion
    }
}
