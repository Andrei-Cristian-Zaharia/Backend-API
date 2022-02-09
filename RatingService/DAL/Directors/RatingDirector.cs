using Core.Interfaces.Directors;
using Core.Interfaces.Repositories;
using DTO.DTO_Models;
using DTO.ModelsForCreation;
using System;
using System.Threading.Tasks;
using AutoMapper;
using DTO.Models;
using System.Collections.Generic;

namespace DAL.Directors
{
    /// <summary>
    /// This class is used to manage REVIEWS repository.
    /// </summary>
    public class RatingDirector : IRatingDirector
    {
        #region Members

        /// <summary>
        /// Gets the User Repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Gets the Movie Repository.
        /// </summary>
        private readonly IMovieRepository _movieRepository;

        /// <summary>
        /// Gets the Review Repository.
        /// </summary>
        private readonly IReviewRepository _reviewRepository;

        /// <summary>
        /// Sets mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Rating Director constructor used to initialize all the variables using injection dependency.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="movieRepository"></param>
        /// <param name="reviewRepository"></param>
        /// <param name="mapper"></param>
        public RatingDirector(IUserRepository userRepository, IMovieRepository movieRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _movieRepository = movieRepository ?? throw new ArgumentException(nameof(movieRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make the call to Review's repository to get a review by id.
        /// </summary>
        /// <param name="reviewId">ID of the review you want to return.</param>
        /// <returns></returns>
        public async Task<ReviewDTO> GetReviewAsync(string reviewId)
        {
            var reviewFromRepo = await _reviewRepository.GetReviewAsync(reviewId);

            if (reviewFromRepo == null)
                return null;

            return _mapper.Map<ReviewDTO>(reviewFromRepo);
        }

        /// <summary>
        /// Make the call to Review's repository to add a new review.
        /// </summary>
        /// <param name="review">The object you want to add.</param>
        /// <returns></returns>
        public async Task<ReviewDTO> AddReviewAsync(ReviewForCreation review)
        {
            if (await _userRepository.UserExists(review.AuthorId) == false)
                return null;

            if (await _movieRepository.MovieExists(review.GeneralId) == false)
                return null;

            Review reviewToAdd = _mapper.Map<Review>(review);
            reviewToAdd.Id = Guid.NewGuid().ToString();

            await _reviewRepository.AddReviewAsync(reviewToAdd);

            return _mapper.Map<ReviewDTO>(reviewToAdd);
        }

        #endregion
    }
}
