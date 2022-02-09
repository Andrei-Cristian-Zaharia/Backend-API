using DTO.DTO_Models;
using DTO.Models;
using DTO.ModelsForCreation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Directors
{
    /// <summary>
    /// This interface offers methods used to manage REVIEW's repository.
    /// </summary>
    public interface IRatingDirector
    {
        #region Methods

        /// <summary>
        /// Make the call to Review's repository to get a review by id.
        /// </summary>
        /// <param name="reviewId">ID of the review you want to return.</param>
        /// <returns></returns>
        Task<ReviewDTO> GetReviewAsync(string reviewId);

        /// <summary>
        /// Make the call to Review's repository to add a new review.
        /// </summary>
        /// <param name="review">The object you want to add.</param>
        /// <returns></returns>
        Task<ReviewDTO> AddReviewAsync(ReviewForCreation review);

        #endregion
    }
}
