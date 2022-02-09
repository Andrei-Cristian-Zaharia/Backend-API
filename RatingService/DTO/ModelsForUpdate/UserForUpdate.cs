using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ModelsForUpdate
{
    /// <summary>
    /// Object of user that contains all the data needed to update a user in DB.
    /// </summary>
    public class UserForUpdate
    {
        /// <summary>
        /// Profile picture url.
        /// </summary>
        public string ProfilePicture { get; set; }
    }
}
