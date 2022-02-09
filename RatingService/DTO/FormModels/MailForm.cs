using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.FormModels
{
    /// <summary>
    /// This contains info of how the email must look like, attachments are not always needed.
    /// </summary>
    public class MailForm
    {
        /// <summary>
        /// ToEmail address.
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// Subject of the mail.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Text that will be placed in the body of the mail.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Attachments like photos, documents.
        /// This is not requireable.
        /// </summary>
        public List<IFormFile> Attachments { get; set; }
    }
}
