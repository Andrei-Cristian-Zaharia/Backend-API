using DTO.FormModels;
using DTO.FormModels.MailForms;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// The interface of EmailService.
    /// This class contains methods to send an email to an user.
    /// Messages like welcome or just info messages.
    /// </summary>
    public interface IMailService
    {
        #region Methods

        /// <summary>
        /// Send an email that may contain attachments. A regular mail.
        /// </summary>
        /// <param name="mailRequest">This contains everything a regular mail does, ToEmail, Subject, Body message, Attachments.</param>
        /// <returns></returns>
        Task<bool> SendEmailAsync(MailForm mailRequest);

        /// <summary>
        /// This method sends a welcome mail.
        /// </summary>
        /// <param name="mailRequest">Contains ToEmail and Username of the user this email is needed to send to.</param>
        /// <returns></returns>
        Task<bool> SendWelcomeMailAsync(MailTemplateForm mailRequest);

        #endregion
    }
}
