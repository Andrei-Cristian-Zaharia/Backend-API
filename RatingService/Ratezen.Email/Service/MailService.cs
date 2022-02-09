using Core.Options;
using DTO.FormModels;
using DTO.FormModels.MailForms;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.ServicesClasses
{
    /// <summary>
    /// This class contains methods to send an email to an user.
    /// Messages like welcome or just info messages.
    /// </summary>
    public class MailService : IMailService
    {
        #region Members

        /// <summary>
        /// Interface that contains MailSettings.
        /// </summary>
        private readonly IEmailSettings _mailSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailSettings">Mail settings.</param>
        public MailService(IEmailSettings mailSettings)
        {
            _mailSettings = mailSettings ?? throw new ArgumentNullException(nameof(mailSettings));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Send an email that may contain attachments. A regular mail.
        /// </summary>
        /// <param name="mailRequest">This contains everything a regular mail does, ToEmail, Subject, Body message, Attachments.</param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(MailForm mailRequest)
        {
            if (mailRequest == null)
                return false;

            var email = new MimeMessage(); // create message

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail); // set sender address
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail)); // set the destination address
            email.Subject = mailRequest.Subject; // set the subject

            var builder = new BodyBuilder(); // creates body structure of a mail

            if (mailRequest.Attachments != null) // check if there are any attachments , and if true, add them to builder
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body; // set the body of the builder

            email.Body = builder.ToMessageBody(); // set html file as body

            var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password); // login to gmail
            await smtp.SendAsync(email); // make request to send email
            smtp.Disconnect(true);

            return true;
        }

        /// <summary>
        /// This method sends a welcome mail.
        /// </summary>
        /// <param name="mailRequest">Contains ToEmail and Username of the user this email is needed to send to.</param>
        /// <returns></returns>
        public async Task<bool> SendWelcomeMailAsync(MailTemplateForm mailRequest)
        {
            if (mailRequest == null)
                return false;

            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\welcomeTemplate.html"; // Store the template route
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd(); // Get all the text from html page
            str.Close(); // close file

            MailText = MailText.Replace("[user]", mailRequest.Username); // replace [user] with username from mailRequest

            var email = new MimeMessage(); // create message

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail); // set sender address
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail)); // set the destination address
            email.Subject = $"Welcome {mailRequest.ToEmail}"; // set the subject

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText; // convert string to html file

            email.Body = builder.ToMessageBody(); // set html file as body

            var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password); // login to gmail
            await smtp.SendAsync(email); // make request to send email
            smtp.Disconnect(true);

            return true;
        }

        #endregion
    }
}