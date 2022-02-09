using AutoMapper;
using DTO.DTO_Models;
using DTO.Models;
using DTO.ModelsForCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Profiles
{
    /// <summary>
    /// Makes conversions between Review, ReviewDTO, and ReviewForCreation models.
    /// </summary>
    class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewForCreation, ReviewDTO>();
            CreateMap<ReviewForCreation, Review>();
        }
    }
}
