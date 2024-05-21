using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using AutoMapper;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository { get; }

        private IGameGenreRepository _gameGenreRepository { get; }
        private IMapper _mapper { get; }

        public GameService(
            IGameRepository gameRepository,
            IGameGenreRepository gameGenreRepository,
            IMapper mapper
        ){
            _gameRepository = gameRepository;
            _gameGenreRepository = gameGenreRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateGame(GameRequestDto gameRequest)
        {
            GameDto gameDto = gameRequest.Game;

            var entity = _mapper.Map<Game>(gameDto);

            entity.Key ??= entity.Name;

            Guid createdId = await _gameRepository.AddNewGame(entity, gameRequest.Genres, gameRequest.Platforms);

            return createdId;

        }

        public async Task<GameResponseDto> GetGameById(Guid id)
        {
            var gameEntity = await _gameRepository.GetByIdAsync(id);
            return _mapper.Map<GameResponseDto>(gameEntity);
        }

        public async Task<GameResponseDto> GetGameByKeyAsync(string key)
        {
            var gameEntity = await _gameRepository.GetByKeyAsync(key);
            return _mapper.Map<GameResponseDto>(gameEntity);
        }

        public GameResponseDto GetGameByKey(string key)
        {
            var gameEntity = _gameRepository.GetByKey(key);
            return _mapper.Map<GameResponseDto>(gameEntity);
        }

        public async Task<List<GameResponseDto>> GetGameByGenreId(Guid genreId)
        {
            var gameEntities = await _gameGenreRepository.GetGamesByGenreId(genreId);

            List<GameResponseDto> result = gameEntities.Select(e => _mapper.Map<GameResponseDto>(e)).ToList();

            return result;
        }


        public async Task UpdateGame(GameUpdateDto gameUpdate)
        {

            Game game = _mapper.Map<Game>(gameUpdate.Game);

            await _gameRepository.UpdateAsync(game, gameUpdate.Platforms, gameUpdate.Genres);
        }

        public void DeleteGame(GameResponseDto game)
        {
            Game gameToDelete = _mapper.Map<Game>(game);

            _gameRepository.Remove(gameToDelete);

        }

        public string GeneratePdf(string key)
        {
            var game = GetGameByKey(key);

            if (game == null)
                return string.Empty;

            string jsonString = JsonConvert.SerializeObject(game, Formatting.Indented);
            string fileName = $"{game.Key.Trim()}_document.pdf";
            string relativeRoute = Path.Combine("SearchedGames", fileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), relativeRoute);

            if (File.Exists(filePath))
                return relativeRoute;

            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            {
                new iText.Layout.Document(pdf)
                    .Add(new Paragraph(jsonString));
            }

            return relativeRoute;
        }

        public async Task<List<GameResponseDto>> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            var response = games.Select(game => _mapper.Map<GameResponseDto>(game)).ToList();

            return response;
        }

        public async Task<List<GenreListDto>> GetGenreByGameKey(string gameKey)
        {
            var genres = await _gameGenreRepository.GetGenreByGameKey(gameKey);

            List<GenreListDto> result = genres.Select(e => _mapper.Map<GenreListDto>(e)).ToList();

            return result;
        }


    }
}
