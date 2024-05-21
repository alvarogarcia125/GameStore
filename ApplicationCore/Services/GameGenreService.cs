using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using AutoMapper;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class GameGenreService : IGameGenreService
    {

        private IGameGenreRepository _gameGenreRepository { get; }
        private IMapper _mapper { get; }

        public GameGenreService(
            IGameGenreRepository gameGenreRepository,
            IMapper mapper
        ){
            _gameGenreRepository = gameGenreRepository;
            _mapper = mapper;
        }

        public async Task<List<GameResponseDto>> GetGamesByGenreId(Guid genreId)
        {
            var gameEntities = await _gameGenreRepository.GetGamesByGenreId(genreId);

            List<GameResponseDto> result = gameEntities.Select(e => _mapper.Map<GameResponseDto>(e)).ToList();

            return result;
        }

        public async Task<List<GenreListDto>> GetGenresByGameKey(string gameKey)
        {
            var genres = await _gameGenreRepository.GetGenreByGameKey(gameKey);

            List<GenreListDto> result = genres.Select(e => _mapper.Map<GenreListDto>(e)).ToList();

            return result;
        }


    }
}
