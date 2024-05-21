using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.Mappers
{
    public class PlatformProfile:Profile
    {
        public PlatformProfile()
        {

            CreateMap<PlatformRequestDto, Platform>();
            CreateMap<Platform, PlatformDto>();
            CreateMap<PlatformDto, Platform>();
        }
    }
}
