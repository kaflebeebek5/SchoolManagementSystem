using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AuthApi.Authentication.Model;
using AutoMapper;

namespace AuthApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserReuestModel>().ReverseMap();
            CreateMap<User, UserClaimModel>().ReverseMap();
        }
    }
}
