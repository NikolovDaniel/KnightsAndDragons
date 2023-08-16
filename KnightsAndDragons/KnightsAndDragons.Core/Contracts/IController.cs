using KnightsAndDragons.Core.Models;
using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IController 
    {
        Task<ICollection<ExportUserDragonDto>> GetUserDragons(string token);
        Task<ICollection<ExportUserKnightDto>> GetUserKnights(string token);
        Task<string> CreateCharacter(string token, string characterType, string name);
        Task<string> RemoveCharacter(string token, string characterType, string name);
    }
}