using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.FormModels
{
    /// <summary>
    /// This form is used to update email, username or password of an account.
    /// </summary>
    public class UserForm
    {
        /// <summary>
        /// Old password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// New password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Current password for verification.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// New username.
        /// </summary>
        public string NewUsername { get; set; }

        /// <summary>
        /// New email.
        /// </summary>
        public string NewEmail { get; set; }
    }
}
