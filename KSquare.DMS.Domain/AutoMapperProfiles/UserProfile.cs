using AutoMapper;
using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Domain.Models.RequestModel;

namespace KSquare.DMS.Domain.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<UserCategoryModel, UserCategory>().ReverseMap();
            CreateMap<AddUserRequestModel, User>().ReverseMap();
        }
    }
}
