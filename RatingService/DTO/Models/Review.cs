using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    /// <summary>
    /// Original object of review stored in DB.
    /// </summary>
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("totalVotes")]
        public int TotalVotes { get; set; }

        [BsonElement("likedVotes")]
        public int LikedVotes { get; set; }

        [BsonElement("authorId")]
        public string AuthorId { get; set; }

        [BsonElement("generalId")]
        public string GeneralId { get; set; }
    }
}
