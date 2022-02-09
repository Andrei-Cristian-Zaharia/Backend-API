using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ModelsForCreation
{
    /// <summary>
    /// Object of review that contains all the data needed to create a review in DB.
    /// </summary>
    public class ReviewForCreation
    {
        #region Members

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
