using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ModelsForCreation
{
    /// <summary>
    /// Object of movie that contains all the data needed to create a movie in DB.
    /// </summary>
    public class MovieForCreation
    {
        #region Members

        /// <summary>
        /// Movie name.
        /// </summary>
        public string MovieName { get; set; }

        /// <summary>
        /// Raiting of the movie.
        /// </summary>
        public string Raiting { get; set; }

        /// <summary>
        /// Movie description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Movie's country.
        /// </summary>
        public string CountryOrigin { get; set; }

        /// <summary>
        /// Release date of the movie.
        /// </summary>
        public DateTimeOffset ReleaseDate { get; set; }

        /// <summary>
        /// Production companies that worked to made that movie.
        /// </summary>
        public IEnumerable<string> ProductionCompanies { get; set; }

        /// <summary>
        /// Photo's url of the movie.
        /// </summary>
        public IEnumerable<string> PhotosUrls { get; set; }

        /// <summary>
        /// Movie's tags.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Languages that are present in the movie.
        /// </summary>
        public IEnumerable<string> Languages { get; set; }

        #endregion
    }
}
