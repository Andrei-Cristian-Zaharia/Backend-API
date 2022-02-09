using DTO.DTO_Models;
using DTO.FormModels;
using DTO.ModelsForCreation;
using DTO.ModelsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace Core.Interfaces.Directors
{
    /// <summary>
    /// This interface offers methods used to manage USER's repository.
    /// </summary>
    public interface IUserDirector
    {
        #region Methods

        /// <summary>
        /// Make the call to User's repository to get a user by id.
        /// </summary>
        /// <param name="userId">ID of the user you want to return.</param>
        /// <returns></returns>
        Task<UserDTO> GetUserAsync(string userId);

        /// <summary>
        /// Make the call to User's repository to add a new user.
        /// </summary>
        /// <param name="userObject">The object you want to add.</param>
        /// <returns></returns>
        Task<UserDTO> AddUserAsync(UserForCreation userObject);

        /// <summary>
        /// Make the call to User's repository to change some values of the user.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="patchDocument">JsonPatchDocument that contains the changes that need to be applied.</param>
        /// <returns></returns>
        Task<bool> UpdateUserAsync(string userId, JsonPatchDocument<UserForUpdate> patchDocument);

        /// <summary>
        /// Use userRepository to change the password only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains old password and new password.</param>
        /// <returns></returns>
        Task<bool> ChangeUserPasswordAsync(string userId, UserForm userForm);

        /// <summary>
        /// Use userRepository to change the username only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains the password and the new username.</param>
        /// <returns></returns>
        Task<bool> ChangeUserUsernameAsync(string userId, UserForm userForm);

        /// <summary>
        /// Use userRepository to change the email only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains the password and the new email.</param>
        /// <returns></returns>
        Task<bool> ChangeUserEmailAsync(string userId, UserForm userForm);

        /// <summary>
        /// Activates user's account.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task<bool> ActivateAccountAsync(string userId);

        /// <summary>
        /// Checks if the user's account is or is not activated.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task<bool> CheckIfActivatedAsync(string userId);

        #endregion
    }
}
