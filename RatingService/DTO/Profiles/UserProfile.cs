using AutoMapper;

namespace DTO.Profiles
{
    /// <summary>
    /// Makes conversions between User, UserDTO, and UserForCreation models.
    /// </summary>
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.User, DTO_Models.UserDTO>();
            CreateMap<ModelsForCreation.UserForCreation, DTO_Models.UserDTO>();
            CreateMap<ModelsForCreation.UserForCreation, Models.User>();
            CreateMap<Models.User, ModelsForUpdate.UserForUpdate>();
            CreateMap<ModelsForUpdate.UserForUpdate, Models.User>();
        }
    }
}
