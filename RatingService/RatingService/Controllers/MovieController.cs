using Core.Interfaces.Directors;
using DTO.DTO_Models;
using DTO.ModelsForCreation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DTO.ModelsForUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        #region Members

        /// <summary>
        /// Rating Director
        /// </summary>
        private readonly IMediaDirector _mediaDirector;

        #endregion

        #region Constructor

        /// <summary>
        /// UserController constructor.
        /// </summary>
        /// <param name="ratingDirector"></param>
        public MovieController(IMediaDirector mediaDirector)
        {
            _mediaDirector = mediaDirector ?? throw new ArgumentNullException(nameof(mediaDirector));
        }

        #endregion

        #region Methods

        /// <summary>
        /// GET method to return a movie.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("{movieId}", Name = "GetMovieAsync")]
        public async Task<IActionResult> GetMovieAsync(string movieId)
        {
            try
            {
                var movieToReturn = await _mediaDirector.GetMovieAsync(movieId);

                if (movieToReturn == null)
                    return BadRequest();

                return Ok(movieToReturn);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// GET method to return a list of movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetMoviesAsync")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesAsync()
        {
            try
            {
                var moviesToReturn = await _mediaDirector.GetAllMoviesAsync();

                if (moviesToReturn.FirstOrDefault() == null)
                    return BadRequest();

                return Ok(moviesToReturn);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// POST method to add a movie to DB,
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddMovieAsync")]
        public async Task<ActionResult> AddMovieAsync([FromBody] MovieForCreation movie)
        {
            try
            {
                return Created("", await _mediaDirector.AddMovieAsync(movie));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// PATCH method to change values of the movie.
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{movieId}" , Name = "UpdateMovieAsync")]
        public async Task<ActionResult> UpdateMovieAsync(string movieId, JsonPatchDocument<MovieForUpdate> patchDocument)
        {
            try
            {
                if (!(await _mediaDirector.UpdateMovieAsync(movieId, patchDocument)))
                    return NotFound();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
