using Microsoft.EntityFrameworkCore;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Infrastructure.Data;
using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Infrastructure.Utilities.Messages;

namespace KnightsAndDragons.Infrastructure.Repositories
{
    public class DragonRepository : IDragonRepository
    {
        private readonly KnightsAndDragonsDbContext _context;

        public DragonRepository(KnightsAndDragonsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to retrieve all User's Dragons.
        /// </summary>
        /// <param name="userId">Used to filter the collection.</param>
        /// <returns>Collection of the User's Dragons.</returns>
        public async Task<ICollection<ExportUserDragonDto>> GetUserDragons(int userId)
        {
            var dragons = await _context.Dragons
                .Where(d => d.UserId == userId)
                .Select(d => new ExportUserDragonDto()
                {
                    Name = d.Name,
                    AttackPower = d.AttackPower,
                    Health = d.Health,
                    Level = d.Level,
                    Experience = d.Experience
                })
                .ToListAsync();

            return dragons!;
        }

        /// <summary>
        /// Method to create a character of type Dragon.
        /// </summary>
        /// <param name="userId">Used to assign the Dragon to the correct User.</param>
        /// <param name="name">Name of the Dragon.</param>
        public async Task CreateDragon(int userId, string name)
        {
            Dragon dragon = new Dragon
            {
                Name = name,
                AttackPower = 20,
                Health = 150,
                Level = 1,
                Experience = 0,
                Gold = 10,
                UserId = userId
            };

            await _context.Dragons.AddAsync(dragon);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to delete the Dragon.
        /// </summary>
        /// <param name="userId">Used to allocate the corrent Dragon.</param>
        /// <param name="name">Name of the Dragon.</param>
        /// <exception cref="Exception">Catch an error if user input is invalid.</exception>
        public async Task DeleteDragon(int userId, string name)
        {
            var dragon = await this._context.Dragons.Include(d => d.User).FirstOrDefaultAsync(d => d.UserId == userId && d.Name == name);

            if (dragon == null)
            {
                throw new Exception(ExceptionMessages.InvalidCharacterName);
            }

            this._context.Dragons.Remove(dragon);
            await this._context.SaveChangesAsync();
        }
    }
}

