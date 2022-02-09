using AutoMapper;
using Core.Extensions;
using Core.Features;
using Core.Helpers;
using Core.Interfaces.Directors;
using Core.Interfaces.Repositories;
using DTO.DTO_Models;
using DTO.FormModels;
using DTO.Models;
using DTO.ModelsForCreation;
using DTO.ModelsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace DAL.Directors
{
    /// <summary>
    /// This class is used to manage USER's repository.
    /// </summary>
    public class UserDirector : IUserDirector
    {
        #region Members

        /// <summary>
        /// Gets the User Repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Sets mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Encryption setting used to get the encryption key.
        /// </summary>
        private readonly IEncryptionSettings _encryptionSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// User Director constructor used to initialize all the variables using injection dependency.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserDirector (IUserRepository userRepository, IMapper mapper, IEncryptionSettings encryptionSettings)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            _encryptionSettings = encryptionSettings ?? throw new ArgumentNullException(nameof(encryptionSettings));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make the call to User's repository to get a user by id.
        /// </summary>
        /// <param name="userId">ID of the user you want to return.</param>
        /// <returns></returns>
        public async Task<UserDTO> GetUserAsync(string userId)
        {
            var user = await _userRepository.GetUser(userId);

            return _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Make the call to User's repository to add a new user.
        /// </summary>
        /// <param name="userObject">The object you want to add.</param>
        /// <returns></returns>
        public async Task<UserDTO> AddUserAsync(UserForCreation userObject)
        {
            var userToAdd = _mapper.Map<User>(userObject);

            userToAdd.Id = Guid.NewGuid().ToString();
            userToAdd.CreatedDate = DateTimeOffset.UtcNow;
            userToAdd.IsAdmin = false;

            userToAdd.EncryptPassword(_encryptionSettings);

            await _userRepository.AddUser(userToAdd);

            return _mapper.Map<UserDTO>(userToAdd);
        }

        /// <summary>
        /// Make the call to User's repository to change some values of the user.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="patchDocument">JsonPatchDocument that contains the changes that need to be applied.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAsync(string userId, JsonPatchDocument<UserForUpdate> patchDocument)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            var userToUpdate = await _userRepository.GetUser(userId);
            var userToPatch = _mapper.Map<UserForUpdate>(userToUpdate);
            patchDocument.ApplyTo(userToPatch);

            _mapper.Map(userToPatch, userToUpdate);

            await _userRepository.UpdateUser(userToUpdate);

            return true;
        }

        /// <summary>
        /// Use userRepository to change the password only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains old password and new password.</param>
        /// <returns></returns>
        public async Task<bool> ChangeUserPasswordAsync(string userId, UserForm userForm)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            var user = await _userRepository.GetUser(userId);

            if (user.CheckPassword(userForm.OldPassword, _encryptionSettings) == false)
                return false;

            user.Password = userForm.NewPassword; // add new password to the user
            user.EncryptPassword(_encryptionSettings); // encrypt new password

            await _userRepository.UpdateUserPasswod(user, user.Password);

            return true;
        }

        /// <summary>
        /// Use userRepository to change the username only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains the password and the new username.</param>
        /// <returns></returns>
        public async Task<bool> ChangeUserUsernameAsync(string userId, UserForm userForm)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            var user = await _userRepository.GetUser(userId);

            if (user.CheckPassword(userForm.Password, _encryptionSettings) == false)
                return false;

            await _userRepository.UpdateUserUsername(user, userForm.NewUsername);

            return true;
        }

        /// <summary>
        /// Use userRepository to change the email only if provided password is equal with the one in DB.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="userForm">Contains the password and the new email.</param>
        /// <returns></returns>
        public async Task<bool> ChangeUserEmailAsync(string userId, UserForm userForm)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            var user = await _userRepository.GetUser(userId);

            if (user.CheckPassword(userForm.Password, _encryptionSettings) == false)
                return false;

            await _userRepository.UpdateUserEmail(user, userForm.NewEmail);

            return true;
        }

        /// <summary>
        /// Activates user's account.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        public async Task<bool> ActivateAccountAsync(string userId)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            await _userRepository.ActivateAccount(userId);

            return true;
        }

        /// <summary>
        /// Checks if the user's account is or is not activated.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        public async Task<bool> CheckIfActivatedAsync(string userId)
        {
            if (await _userRepository.UserExists(userId) == false)
                return false;

            var user = await _userRepository.GetUser(userId);

            return user.IsActivated;
        }

        #endregion
    }
}
