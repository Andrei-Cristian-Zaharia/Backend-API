using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DTO.Models
{
    /// <summary>
    /// Original object of user stored in DB.
    /// </summary>
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; } // change this to byte[] and implement encrypt and decrypt methods

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        [BsonElement("isActivated")]
        public bool IsActivated { get; set; }

        [BsonElement("activationCode")]
        public string ActivationCode { get; set; }

        [BsonElement("profilePicture")]
        public string ProfilePicture { get; set; }

        [BsonElement("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [BsonElement("following")]
        public IEnumerable<string> Following { get; set; }
    }
}
