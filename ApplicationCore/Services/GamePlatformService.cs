using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using AutoMapper;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class GamePlatformService : IGamePlatformService
    {

        private IGamePlatformRepository _gamePlatformRepository { get; }
        private IMapper _mapper { get; }

        public GamePlatformService(
            IGamePlatformRepository gamePlatformRepository,
            IMapper mapper
        ){
            _gamePlatformRepository = gamePlatformRepository;
            _mapper = mapper;
        }

        public async Task<List<GameResponseDto>> GetGamesByPlatformId(Guid genreId)
        {
            var gameEntities = await _gamePlatformRepository.GetGamesByPlatformIdAsync(genreId);

            List<GameResponseDto> result = gameEntities.Select(e => _mapper.Map<GameResponseDto>(e)).ToList();

            return result;
        }

        public async Task<List<PlatformDto>> GetPlatformsByGameKey(string gameKey)
        {
            var genres = await _gamePlatformRepository.GetPlatformsByGameKey(gameKey);

            List<PlatformDto> result = genres.Select(e => _mapper.Map<PlatformDto>(e)).ToList();

            return result;
        }


    }
}
