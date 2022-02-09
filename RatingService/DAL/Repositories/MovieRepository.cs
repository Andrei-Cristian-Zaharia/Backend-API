using Core.DatabaseSettings;
using Core.Interfaces.Repositories;
using DTO.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// This class is used to make the connection to DB for MOVIE. 
    /// All the methods for getting info, inserting or updating it are found here.
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        #region Members

        /// <summary>
        /// Used to make calls to DB.
        /// </summary>
        public IMongoCollection<Movie> _movies;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize _mapper and _movies at the beggining of program.
        /// </summary>
        /// <param name="settings"></param>
        public MovieRepository(IDatabaseSettings settings)
        {
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
            var client = new MongoClient(mongoSettings);
            var database = client.GetDatabase(settings.DatabaseName);

            _movies = database.GetCollection<Movie>(settings.MovieCollection);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a movie from DB using a specific ID.
        /// </summary>
        /// <param name="movieId">ID of the movie to return.</param>
        /// <returns></returns>
        public async Task<Movie> GetMovieAsync(string movieId)
        {
            var movieFromDB = await _movies.FindAsync(m => m.Id == movieId);

            return movieFromDB.FirstOrDefault();
        }

        /// <summary>
        /// Get the list of all movies from DB.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var moviesFromDB = await _movies.FindAsync(m => true);

            return moviesFromDB.ToEnumerable<Movie>();
        }

        /// <summary>
        /// Stores an object for the movie in DB.
        /// </summary>
        /// <param name="movie">The object to be stored.</param>
        /// <returns></returns>
        public async Task AddMovieAsync(Movie movie)
        {
            await _movies.InsertOneAsync(movie);
        }

        /// <summary>
        /// Updates the movie.
        /// </summary>
        /// <param name="movie">Movie we want to update.</param>
        /// <returns></returns>
        public async Task<bool> UpdateMovie(Movie movie)
        {
            var filter = Builders<Movie>.Filter.Eq(d => d.Id, movie.Id);

            if (filter == null) return false;

            await _movies.ReplaceOneAsync(filter, movie);

            return true;
        }

        /// <summary>
        /// Checks if the movie is existent in DB.
        /// </summary>
        /// <param name="movieId">Movie's id to be checked.</param>
        /// <returns></returns>
        public async Task<bool> MovieExists(string movieId)
        {
            var movieFromDB = await _movies.FindAsync(m => m.Id == movieId);
            
            return movieFromDB.Any();
        }
        #endregion
    }
}
