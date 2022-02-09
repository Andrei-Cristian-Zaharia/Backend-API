using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    /// <summary>
    /// Original object of movie stored in DB.
    /// </summary>
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("movieName")]
        public string MovieName { get; set; }

        [BsonElement("raiting")]
        public string Raiting { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("countryOrigin")]
        public string CountryOrigin { get; set; }

        [BsonElement("releaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [BsonElement("productionCompanies")]
        public IEnumerable<string> ProductionCompanies { get; set; }

        [BsonElement("photosUrls")]
        public IEnumerable<string> PhotosUrls { get; set; }

        [BsonElement("tags")]
        public IEnumerable<string> Tags { get; set; }

        [BsonElement("languages")]
        public IEnumerable<string> Languages { get; set; }
    }
}
