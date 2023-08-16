using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Services
{
    public class DragonService : IDragonService
    {
        private IDragonRepository _dragonRepository;

        public DragonService(IDragonRepository dragonRepository)
        {
            this._dragonRepository = dragonRepository;
        }

        public Task CreateDragon(int userId, string name)
        {
            return this._dragonRepository.CreateDragon(userId, name);
        }

        public Task DeleteDragon(int userId, string name)
        {
            return this._dragonRepository.DeleteDragon(userId, name);
        }

        public async Task<ICollection<ExportUserDragonDto>> GetUserDragons(int userId)
        {
            return await this._dragonRepository.GetUserDragons(userId);
        }
    }
}

