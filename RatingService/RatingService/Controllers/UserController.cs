using Core.Interfaces.Directors;
using DTO.DTO_Models;
using DTO.FormModels;
using DTO.ModelsForCreation;
using DTO.ModelsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Members

        /// <summary>
        /// Rating Director
        /// </summary>
        private readonly IUserDirector _userDirector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDirector"></param>
        public UserController(IUserDirector userDirector)
        {
            _userDirector = userDirector ?? throw new ArgumentNullException(nameof(userDirector));
        }

        #endregion

        #region Methods

        /// <summary>
        /// GET method to return a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}", Name = "GetUserAsync")]
        public async Task<ActionResult<UserDTO>> GetUserAsync(string userId)
        {
            try
            {
                var response = await _userDirector.GetUserAsync(userId);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// POST method to store a user to DB.
        /// </summary>
        /// <param name="userObject"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddUserAsync")]
        public async Task<ActionResult> AddUserAsync([FromBody] UserForCreation userObject)
        {
            try
            {
                return Ok(await _userDirector.AddUserAsync(userObject));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method to change values of the user.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="jsonPatchDocument">JsonPatchDocument that contains the changes that need to be applied.</param>
        /// <returns></returns>
        [HttpPatch("{userId}", Name = "UpdateUserAsync")]
        public async Task<ActionResult> UpdateUserAsync(string userId, JsonPatchDocument<UserForUpdate> jsonPatchDocument)
        {
            try
            {
                if (!(await _userDirector.UpdateUserAsync(userId, jsonPatchDocument)))
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method used to update user's password.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm"></param>
        /// <returns></returns>
        [HttpPatch("{userId}/updatePassword", Name = "ChangeUserPasswordAsync")]
        public async Task<ActionResult> ChangeUserPasswordAsync(string userId, [FromBody] UserForm userForm)
        {
            try
            {
                if (!(await _userDirector.ChangeUserPasswordAsync(userId, userForm)))
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method used to update user's username.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm"></param>
        /// <returns></returns>
        [HttpPatch("{userId}/updateUsername", Name = "ChangeUserUsernameAsync")]
        public async Task<ActionResult> ChangeUserUsernameAsync(string userId, [FromBody] UserForm userForm)
        {
            try
            {
                if (!(await _userDirector.ChangeUserUsernameAsync(userId, userForm)))
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method used to update user's email.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm"></param>
        /// <returns></returns>
        [HttpPatch("{userId}/updateEmail", Name = "ChangeUserEmailAsync")]
        public async Task<ActionResult> ChangeUserEmailAsync(string userId, [FromBody] UserForm userForm)
        {
            try
            {
                if (!(await _userDirector.ChangeUserEmailAsync(userId, userForm)))
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method used to activate user's account.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        [HttpPatch("activateAccount", Name = "ActivateAccountAsync")]
        public async Task<ActionResult> ActivateAccountAsync([FromBody] IdForm idForm)
        {
            try
            {
                if (!await _userDirector.ActivateAccountAsync(idForm.Id))
                    return NotFound();

                return NoContent();
            } 
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// GET method that retreive if the account is activated.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        [HttpGet("{userId}/isActivated", Name = "CheckIfActivatedAsync")]
        public async Task<ActionResult> CheckIfActivatedAsync(string userId)
        {
            try
            {
                var response = await _userDirector.CheckIfActivatedAsync(userId);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
