using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Services
{
    public class KnightService : IKnightService
    {
        private IKnightRepository _knightRepository;

        public KnightService(IKnightRepository knightRepository)
        {
            this._knightRepository = knightRepository;
        }

        public Task CreateKnight(int userId, string name)
        {
            return _knightRepository.CreateKnight(userId, name);
        }

        public Task DeleteKnight(int userId, string name)
        {
            return _knightRepository.DeleteKnight(userId, name);
        }

        public async Task<ICollection<ExportUserKnightDto>> GetUserKnights(int userId)
        {
            return await _knightRepository.GetUserKnights(userId);
        }
    }
}

