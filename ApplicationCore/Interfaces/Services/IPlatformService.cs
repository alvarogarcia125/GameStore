using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces.Services
{
    public interface IPlatformService
    {
        Task CreatePlatform(PlatformRequestDto platformRequest);

        Task<PlatformDto> GetPlatformById(Guid id);

        Task<PlatformDto> GetPlatformByIdAsNoTracking(Guid id);

        Task<List<PlatformDto>> GetAllPlatforms();

        Task UpdatePlatform(PlatformDto platformToUpdate);

        void DeletePlatform(PlatformDto platform);
    }
}
