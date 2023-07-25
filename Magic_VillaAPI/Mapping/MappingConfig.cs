using AutoMapper;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;

namespace Magic_VillaAPI.Mapping
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, VillNoCreatedDto>().ReverseMap();
            CreateMap<VillaNumber, VillNoUpdatedDto>().ReverseMap();
            CreateMap<ApplicationUser,UserDTO>().ReverseMap();
        }
    }
}
