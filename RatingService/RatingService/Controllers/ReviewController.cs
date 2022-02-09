using Core.Interfaces.Directors;
using DTO.DTO_Models;
using DTO.ModelsForCreation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        #region Members

        /// <summary>
        /// Rating Director
        /// </summary>
        private readonly IRatingDirector _ratingDirector;

        #endregion

        #region Constructor

        /// <summary>
        /// UserController constructor.
        /// </summary>
        /// <param name="ratingDirector"></param>
        public ReviewController(IRatingDirector ratingDirector)
        {
            _ratingDirector = ratingDirector ?? throw new ArgumentNullException(nameof(ratingDirector));
        }

        #endregion

        #region Methods

        /// <summary>
        /// GET method to return a review.
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        [HttpGet("{reviewId}", Name = "GetReviewAsync")]
        public async Task<ActionResult<ReviewDTO>> GetReviewAsync(string reviewId)
        {
            try
            {
                var response = await _ratingDirector.GetReviewAsync(reviewId);

                if (response == null)
                    return BadRequest();

                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// POST method to store a review to DB.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddReviewForMovieAsync")]
        public async Task<ActionResult> AddReviewForMovieAsync(ReviewForCreation review)
        {
            try
            {
                return Created("", await _ratingDirector.AddReviewAsync(review));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
