using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PlatformService : IPlatformService
    {
        IPlatformRepository _platformRepository;

        private IMapper _mapper { get; }

        public PlatformService(
            IPlatformRepository platformRepository,
            IMapper mapper
        )
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        public async Task CreatePlatform(PlatformRequestDto platformRequest)
        {
            Platform platform = _mapper.Map<Platform>(platformRequest);
            await _platformRepository.AddPlatform(platform);

        }

        public async Task<PlatformDto> GetPlatformById(Guid id)
        {
            var platform = await _platformRepository.GetByIdAsync(id);
            return _mapper.Map<PlatformDto>(platform);
        }

        public async Task<PlatformDto> GetPlatformByIdAsNoTracking(Guid id)
        {
            var platform = await _platformRepository.GetPlatformByIdAsNoTracking(id);
            return _mapper.Map<PlatformDto>(platform);
        }

        public async Task<List<PlatformDto>> GetAllPlatforms()
        {
            var platforms = await _platformRepository.GetAllAsync();

            var response = platforms.Select(p => _mapper.Map<PlatformDto>(p)).ToList();

            return response;
        }

        public async Task UpdatePlatform(PlatformDto platformToUpdate)
        {

            Platform platform = _mapper.Map<Platform>(platformToUpdate);

            await _platformRepository.UpdatePlatform(platform);
        }

        public void DeletePlatform(PlatformDto platform)
        {
            Platform platformToDelete = _mapper.Map<Platform>(platform);

            _platformRepository.Remove(platformToDelete);

        }
    }
}
