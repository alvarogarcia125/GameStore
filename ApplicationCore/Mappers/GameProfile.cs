using ApplicationCore.DTO;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.Mappers
{
    public class GameProfile:Profile
    {
        public GameProfile()
        {

            CreateMap<GameDto, Game>();
            CreateMap<Game, GameResponseDto>();
            CreateMap<GameResponseDto, Game>();
        }
    }
}
