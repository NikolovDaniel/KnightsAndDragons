using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IKnightRepository
    {
        Task CreateKnight(int userId, string name);
        Task DeleteKnight(int userId, string name);
        Task<ICollection<ExportUserKnightDto>> GetUserKnights(int userId);
    }
}
