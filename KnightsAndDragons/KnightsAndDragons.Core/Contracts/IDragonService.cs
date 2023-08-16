using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IDragonService
    {
        Task CreateDragon(int userId, string name);
        Task DeleteDragon(int userId, string name);
        Task<ICollection<ExportUserDragonDto>> GetUserDragons(int userId);
    }
}

