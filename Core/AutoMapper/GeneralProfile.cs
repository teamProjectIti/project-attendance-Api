using AutoMapper;
using WebApplication1.Dto.User;
using WebApplication1.Model.User;

namespace WebApplication1.Core.AutoMapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ManageUser, UserDto>().ReverseMap();
        }
    }
}
