using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Options
{
    /// <summary>
    /// Email strings interface.
    /// </summary>
    public interface IEmailSettings {

        #region Members

        /// <summary>
        /// The email address from where the message is sent.
        /// </summary>
        string Mail { get; set; }

        /// <summary>
        /// Displayed name.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Password of the account the message is sent.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Host or SMTP Server address.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Port – Use 465 (SSL) or 587 (TLS).
        /// </summary>
        int Port { get; set; }

        #endregion
    }

    /// <summary>
    /// Email strings.
    /// </summary>
    public class MailSettings : IEmailSettings
    {
        #region Members

        /// <summary>
        /// The email address from where the message is sent.
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Displayed name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Password of the account the message is sent.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Host or SMTP Server address.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port – Use 465 (SSL) or 587 (TLS).
        /// </summary>
        public int Port { get; set; }

        #endregion
    }
}
