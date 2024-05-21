using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using AutoMapper;
using iText.Commons.Actions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class GenreService : IGenreService
    {
        IGenreRepository _genreRepository;

        private IMapper _mapper { get; }

        public GenreService(
            IGenreRepository genreRepository,
            IMapper mapper
        )
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task CreateGenre(GenreRequestDto genreRequest)
        {
            Genre genre = _mapper.Map<Genre>(genreRequest);
            await _genreRepository.AddGenre(genre);

        }

        public async Task<GenreDto> GetGenreById(Guid id)
        {
            var gameEntity = await _genreRepository.GetByIdAsync(id);
            return _mapper.Map<GenreDto>(gameEntity);
        }

        public async Task<GenreDto> GetGenreByIdAsNoTracking(Guid id)
        {
            var gameEntity = await _genreRepository.GetGenreByIdAsNoTracking(id);
            return _mapper.Map<GenreDto>(gameEntity);
        }

        public async Task<List<GenreListDto>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllAsync();

            var response = genres.Select(genre => _mapper.Map<GenreListDto>(genre)).ToList();

            return response;
        }

        public async Task<List<GenreListDto>> GetGenresByParentId(Guid parentId)
        {
            var genres = await _genreRepository.GetByParentIdAsync(parentId);

            var response = genres.Select(genre => _mapper.Map<GenreListDto>(genre)).ToList();

            return response;
        }

        public async Task UpdateGenre(GenreDto genreToUpdate)
        {

            Genre game = _mapper.Map<Genre>(genreToUpdate);

            await _genreRepository.UpdateGenre(game);
        }

        public void DeleteGame(GenreDto genre)
        {
            Genre genreToDelete = _mapper.Map<Genre>(genre);

            _genreRepository.Remove(genreToDelete);

        }
    }
}
