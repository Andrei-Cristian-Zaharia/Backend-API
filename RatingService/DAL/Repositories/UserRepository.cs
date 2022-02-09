using AutoMapper;
using Core.DatabaseSettings;
using Core.Features;
using Core.Helpers;
using Core.Interfaces.Repositories;
using DTO.DTO_Models;
using DTO.Models;
using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// This class is used to make the connection to DB for USER. 
    /// All the methods for getting info, inserting or updating it are found here.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Members

        /// <summary>
        /// Used to make calls to DB.
        /// </summary>
        private readonly IMongoCollection<User> _users;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize _mapper and _users at the beggining of program.
        /// </summary>
        /// <param name="settings"></param>
        public UserRepository(IDatabaseSettings settings)
        {
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
            var client = new MongoClient(mongoSettings);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UserCollection);
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Returns an user object from DB using his unique id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        public async Task<User> GetUser(string userId)
        {
            var userFromDB = await _users.FindAsync(u => u.Id == userId);
 
            return userFromDB.FirstOrDefault();
        }

        /// <summary>
        /// Returns an user object from DB using his credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="encryptionSettings"></param>
        /// <returns></returns>
        public async Task<User> GetUserByCredentials(string username, string password, IEncryptionSettings encryptionSettings)
        {
            var userFromDB = await _users.FindAsync(u => u.Username == username);
            var user = userFromDB.FirstOrDefault();

            if (PasswordEncryption.Decrypt(user.Password, encryptionSettings) == password)
                return user;

            return null;
        }

        /// <summary>
        /// Stores an object for the user in DB.
        /// </summary>
        /// <param name="userObject">The object to be stored.</param>
        /// <returns></returns>
        public async Task AddUser(User userObject)
        {
            await _users.InsertOneAsync(userObject);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(d => d.Id, user.Id);

            if (filter == null) return false;

            await _users.ReplaceOneAsync(filter, user);

            return true;
        }

        /// <summary>
        /// Checks if the user is existent in DB.
        /// </summary>
        /// <param name="userId">User's id to be checked.</param>
        /// <returns></returns>
        public async Task<bool> UserExists(string userId)
        {
            var userFromDB = await _users.FindAsync(u => u.Id == userId);

            if (userFromDB.FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newPassword">The password to replace the old password.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserPasswod(User user, string newPassword)
        {
            var filter = Builders<User>.Filter.Eq(d => d.Id, user.Id);

            if (filter == null) return false;

            var update = Builders<User>.Update.Set(u => u.Password, newPassword);

            await _users.UpdateOneAsync(filter, update);

            return true;
        }

        /// <summary>
        /// Updates the username.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newUsername">The username to replace the old username.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserUsername(User user, string newUsername)
        {
            var filter = Builders<User>.Filter.Eq(d => d.Id, user.Id);

            if (filter == null) return false;

            var update = Builders<User>.Update.Set(u => u.Username, newUsername);

            await _users.UpdateOneAsync(filter, update);

            return true;
        }

        /// <summary>
        /// Updates the email.
        /// </summary>
        /// <param name="user">ID of the user.</param>
        /// <param name="newEmail">The email to replace the old email.</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserEmail(User user, string newEmail)
        {
            var filter = Builders<User>.Filter.Eq(d => d.Id, user.Id);

            if (filter == null) return false;

            var update = Builders<User>.Update.Set(u => u.Email, newEmail);

            await _users.UpdateOneAsync(filter, update);

            return true;
        }

        /// <summary>
        /// Activates an account using userId.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        public async Task<bool> ActivateAccount(string userId)
        {
            var filter = Builders<User>.Filter.Eq(d => d.Id, userId);

            if (filter == null) return false;

            var update = Builders<User>.Update.Set(u => u.IsActivated, true);

            await _users.UpdateOneAsync(filter, update);

            return true;
        }

        #endregion
    }
}
