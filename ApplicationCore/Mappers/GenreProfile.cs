using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.Mappers
{
    public class GenreProfile:Profile
    {
        public GenreProfile()
        {

            CreateMap<GenreRequestDto, Genre>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, GenreListDto>();
        }
    }
}
