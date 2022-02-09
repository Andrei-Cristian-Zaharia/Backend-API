using DTO.FormModels;
using DTO.FormModels.MailForms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatingService.Controllers
{
    [Authorize(Policy = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        #region Members

        /// <summary>
        /// Interface that contains MailSettings.
        /// </summary>
        private readonly IMailService _mailService;

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor of the EmailController used to initialize _mailService.
        /// </summary>
        /// <param name="mailService">Contains methods used to send email.</param>
        public EmailController(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// POST method to send a regular mail with attachments (that are not requireable.).
        /// </summary>
        /// <param name="emailForm"></param>
        /// <returns></returns>
        [HttpPost("send", Name = "SendMailAsync")]
        public async Task<IActionResult> SendMailAsync([FromForm] MailForm emailForm)
        {
            try
            {
                if (!(await _mailService.SendEmailAsync(emailForm)))
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// POST method to send the welcome mail to a new user.
        /// </summary>
        /// <param name="mailForm"></param>
        /// <returns></returns>
        [HttpPost("welcome", Name = "SendWelcomeMessageAsync")]
        public async Task<ActionResult> SendWelcomeMessageAsync([FromForm] MailTemplateForm mailForm)
        {
            try
            {
                if (!(await _mailService.SendWelcomeMailAsync(mailForm)))
                    return NotFound();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
