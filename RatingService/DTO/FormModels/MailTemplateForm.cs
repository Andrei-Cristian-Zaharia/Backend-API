using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.FormModels.MailForms
{
    /// <summary>
    /// This is a special mailForm that contains only the ToEmail and username to configure some mails like "welcome".
    /// </summary>
    public class MailTemplateForm
    {
        /// <summary>
        /// ToEmail address.
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Username { get; set; }
    }
}
