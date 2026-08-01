using DriveHubMongo.DTO;

namespace DriveHubMongo.Repositories
{
    public interface IChatRepository
    {
        Task<List<ChatSearchResultDto>> SearchVehiclesAsync(
            string? vehicleName,
            string? location);
    }
}
