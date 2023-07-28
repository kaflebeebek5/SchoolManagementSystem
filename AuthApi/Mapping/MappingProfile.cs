using AuthApi.API.DbModel;
using AuthApi.API.RequestModel;
using AutoMapper;

namespace AuthApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserReuestModel>().ReverseMap();
        }
    }
}
