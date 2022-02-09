using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    /// <summary>
    /// The interface of the reviewRepository, offeres methods to comunicate with DB.
    /// </summary>
    public interface IReviewRepository
    {
        #region Methods

        /// <summary>
        /// Get a review from DB using a specific ID.
        /// </summary>
        /// <param name="reviewId">ID of the review to return.</param>
        /// <returns></returns>
        Task<Review> GetReviewAsync(string reviewId);

        /// <summary>
        /// Add a review to DB.
        /// </summary>
        /// <param name="review">Object of the review to store.</param>
        /// <returns></returns>
        Task AddReviewAsync(Review review);

        #endregion
    }
}
