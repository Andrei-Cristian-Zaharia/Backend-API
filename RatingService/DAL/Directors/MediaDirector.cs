using AutoMapper;
using Core.Interfaces.Directors;
using Core.Interfaces.Repositories;
using DTO.DTO_Models;
using DTO.Models;
using DTO.ModelsForCreation;
using DTO.ModelsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Directors
{
    /// <summary>
    /// This class is used to manage MEDIA's repository.
    /// </summary>
    public class MediaDirector : IMediaDirector
    {
        #region Members

        /// <summary>
        /// Gets the Movie Repository.
        /// </summary>
        private readonly IMovieRepository _movieRepository;

        /// <summary>
        /// Sets mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Media Director constructor used to initialize all the variables using injection dependency.
        /// </summary>
        /// <param name="movieRepository"></param>
        /// <param name="mapper"></param>
        public MediaDirector (IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make the call to Movie's repository to get a movie by id.
        /// </summary>
        /// <param name="movieId">ID of the movie you want to return.</param>
        /// <returns></returns>
        public async Task<MovieDTO> GetMovieAsync(string movieId)
        {
            var movie = await _movieRepository.GetMovieAsync(movieId);

            return _mapper.Map<MovieDTO>(movie);
        }

        /// <summary>
        /// Make the call to Movie's repository to get a list with all the movies.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        /// <summary>
        /// Make the call to Movie's repository to add a new movie.
        /// </summary>
        /// <param name="movie">The object you want to add.</param>
        /// <returns></returns>
        public async Task<MovieDTO> AddMovieAsync(MovieForCreation movie)
        {
            Movie movieToAdd = _mapper.Map<Movie>(movie);

            movieToAdd.Id = Guid.NewGuid().ToString();

            if (movieToAdd == null)
                return null;

            await _movieRepository.AddMovieAsync(movieToAdd);

            return _mapper.Map<MovieDTO>(movieToAdd);
        }

        /// <summary>
        /// Make the call to Movie's repository to change some values of the movie.
        /// </summary>
        /// <param name="movieId">ID of the movie.</param>
        /// <param name="patchDocument">JsonPatchDocument that contains the changes that need to be applied.</param>
        public async Task<bool> UpdateMovieAsync(string movieId, JsonPatchDocument<MovieForUpdate> patchDocument)
        {
            if (await _movieRepository.MovieExists(movieId) == false)
                return false;

            var movieToUpdate = await _movieRepository.GetMovieAsync(movieId);
            var movieToPatch = _mapper.Map<MovieForUpdate>(movieToUpdate);
            patchDocument.ApplyTo(movieToPatch);

            _mapper.Map(movieToPatch, movieToUpdate);

            return await _movieRepository.UpdateMovie(movieToUpdate);
        }

        #endregion
    }
}
