using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Core.Models;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IKnightService
    {
        Task CreateKnight(int userId, string name);
        Task DeleteKnight(int userId, string name);
        Task<ICollection<ExportUserKnightDto>> GetUserKnights(int userId);
    }
}

