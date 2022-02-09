using AutoMapper;
using Core.DatabaseSettings;
using Core.Interfaces.Repositories;
using DTO.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// This class is used to make the connection to DB for REVIEW. 
    /// All the methods for getting info, inserting or updating it are found here.
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        #region Members

        /// <summary>
        /// Used to make calls to DB.
        /// </summary>
        private readonly IMongoCollection<Review> _reviews;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize _mapper and _reviews at the beggining of program.
        /// </summary>
        /// <param name="settings"></param>
        public ReviewRepository(IDatabaseSettings settings)
        {
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
            var client = new MongoClient(mongoSettings);
            var database = client.GetDatabase(settings.DatabaseName);

            _reviews = database.GetCollection<Review>(settings.ReviewCollection);
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Get a review from DB using a specific ID.
        /// </summary>
        /// <param name="reviewId">ID of the review to return.</param>
        /// <returns></returns>
        public async Task<Review> GetReviewAsync(string reviewId)
        {
            var reviewToReturn = await _reviews.FindAsync(r => r.Id == reviewId);

            return reviewToReturn.FirstOrDefault();
        }

        /// <summary>
        /// Add a review to DB.
        /// </summary>
        /// <param name="review">Object of the review to store.</param>
        /// <returns></returns>
        public async Task AddReviewAsync(Review review)
        {
            await _reviews.InsertOneAsync(review);
        }

        #endregion
    }
}
