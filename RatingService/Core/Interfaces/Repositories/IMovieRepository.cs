using DTO.DTO_Models;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    /// <summary>
    /// The interface of the movieRepository, offeres methods to comunicate with DB.
    /// </summary>
    public interface IMovieRepository
    {
        #region Methods

        /// <summary>
        /// Get a movie from DB using a specific ID.
        /// </summary>
        /// <param name="movieId">ID of the movie to return.</param>
        /// <returns></returns>
        Task<Movie> GetMovieAsync(string movieId);

        /// <summary>
        /// Get the list of all movies from DB.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        /// <summary>
        /// Stores an object for the movie in DB.
        /// </summary>
        /// <param name="movie">The object to be stored.</param>
        /// <returns></returns>
        Task AddMovieAsync(Movie movie);

        /// <summary>
        /// Updates the movie.
        /// </summary>
        /// <param name="movie">Movie we want to update.</param>
        /// <returns></returns>
        Task<bool> UpdateMovie(Movie movie);

        /// <summary>
        /// Checks if the movie is existent in DB.
        /// </summary>
        /// <param name="movieId">Movie's id to be checked.</param>
        /// <returns></returns>
        Task<bool> MovieExists(string movieId);

        #endregion
    }
}
