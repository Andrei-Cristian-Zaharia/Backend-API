using DTO.DTO_Models;
using DTO.ModelsForCreation;
using DTO.ModelsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Directors
{
    /// <summary>
    /// This interface offers methods used to manage MEDIA's repository.
    /// </summary>
    public interface IMediaDirector
    {
        #region Methods

        /// <summary>
        /// Make the call to Movie's repository to get a movie by id.
        /// </summary>
        /// <param name="movieId">ID of the movie you want to return.</param>
        /// <returns></returns>
        Task<MovieDTO> GetMovieAsync(string movieId);

        /// <summary>
        /// Make the call to Movie's repository to get a list with all the movies.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MovieDTO>> GetAllMoviesAsync();

        /// <summary>
        /// Make the call to Movie's repository to add a new movie.
        /// </summary>
        /// <param name="movie">The object you want to add.</param>
        /// <returns></returns>
        Task<MovieDTO> AddMovieAsync(MovieForCreation movie);

        /// <summary>
        /// Make the call to Movie's repository to change some values of the movie.
        /// </summary>
        /// <param name="movieId">ID of the movie.</param>
        /// <param name="patchDocument">JsonPatchDocument that contains the changes that need to be applied.</param>
        Task<bool> UpdateMovieAsync(string movieId, JsonPatchDocument<MovieForUpdate> patchDocument);

        #endregion
    }
}
