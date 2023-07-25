using AutoMapper;
using Magic_Villa_Web.Models.Dto;

namespace Magic_Villa_Web.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO,VillaCreateDto>().ReverseMap();
            CreateMap<VillaDTO,VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumberDto,VillaNoCreatedDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillNoUpdatedDto>().ReverseMap();
        }
    }
}
