using Core.Features;
using DTO.DTO_Models;
using DTO.Models;
using DTO.ModelsForCreation;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    /// <summary>
    /// The interface of the userRepository, offeres methods to comunicate with DB.
    /// </summary>
    public interface IUserRepository
    {
        #region Methods

        /// <summary>
        /// Returns an user object from DB using his unique id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        Task<User> GetUser(string userId);

        /// <summary>
        /// Stores an object for the user in DB.
        /// </summary>
        /// <param name="userObject">The object to be stored.</param>
        /// <returns></returns>
        Task AddUser(User userObject);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(User user);

        /// <summary>
        /// Checks if the user is existent in DB.
        /// </summary>
        /// <param name="userId">User's id to be checked.</param>
        /// <returns></returns>
        Task<bool> UserExists(string userId);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newPassword">The password to replace the old password.</param>
        /// <returns></returns>
        Task<bool> UpdateUserPasswod(User user, string newPassword);

        /// <summary>
        /// Updates the username.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newUsername">The username to replace the old username.</param>
        /// <returns></returns>
        Task<bool> UpdateUserUsername(User user, string newUsername);

        /// <summary>
        /// Updates the email.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newEmail">The email to replace the old email.</param>
        /// <returns></returns>
        Task<bool> UpdateUserEmail(User user, string newEmail);

        /// <summary>
        /// Activates an account using userId.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task<bool> ActivateAccount(string userId);

        /// <summary>
        /// Returns an user object from DB using his credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="encryptionSettings"></param>
        /// <returns></returns>
        Task<User> GetUserByCredentials(string username, string password, IEncryptionSettings encryptionSettings);

        #endregion
    }
}
