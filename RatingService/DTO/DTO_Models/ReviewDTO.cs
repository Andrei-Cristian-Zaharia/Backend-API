using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTO_Models
{
    /// <summary>
    /// Object of review that is returned in GET requests or responses.
    /// </summary>
    public class ReviewDTO
    {
        #region Members

        /// <summary>
        /// Review id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Review description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of likes and dislikes.
        /// </summary>
        public int TotalVotes { get; set; }

        /// <summary>
        /// Number of likes.
        /// </summary>
        public int LikedVotes { get; set; }

        /// <summary>
        /// Owner of the review.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Id of the movie/book/game.
        /// </summary>
        public string GeneralId { get; set; }

        #endregion
    }
}
