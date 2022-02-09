using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Profiles
{
    /// <summary>
    /// Makes conversions between Movie, MovieDTO, and MovieForCreation models.
    /// </summary>
    class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Models.Movie, DTO_Models.MovieDTO>();
            CreateMap<IEnumerable<Models.Movie>, IEnumerable<Models.Movie>>();
            CreateMap<ModelsForCreation.MovieForCreation, DTO_Models.MovieDTO>();
            CreateMap<ModelsForCreation.MovieForCreation, Models.Movie>();
            CreateMap<Models.Movie, ModelsForCreation.MovieForCreation>();
            CreateMap<Models.Movie, ModelsForUpdate.MovieForUpdate>();
            CreateMap<ModelsForUpdate.MovieForUpdate, Models.Movie>();
        }
    }
}
