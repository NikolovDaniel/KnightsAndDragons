using Microsoft.EntityFrameworkCore;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.Infrastructure.Data;
using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Infrastructure.Utilities.Messages;

namespace KnightsAndDragons.Infrastructure.Repositories
{
    public class KnightRepository : IKnightRepository
    {
        private readonly KnightsAndDragonsDbContext _context;

        public KnightRepository(KnightsAndDragonsDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Method to retrieve all User's Knights.
        /// </summary>
        /// <param name="userId">Used to filter the collection.</param>
        /// <returns>Collection of the User's Knights.</returns>
        public async Task<ICollection<ExportUserKnightDto>> GetUserKnights(int userId)
        {
            var knights = await _context.Knights
                .Where(k => k.UserId == userId)
                .Select(k => new ExportUserKnightDto()
                {
                    Name = k.Name,
                    AttackPower = k.AttackPower,
                    Health = k.Health,
                    Level = k.Level,
                    Experience = k.Experience
                })
                .ToListAsync();

            return knights!;
        }

        /// <summary>
        /// Method to create a character of type Knight.
        /// </summary>
        /// <param name="userId">Used to assign the Knight to the correct User.</param>
        /// <param name="name">Name of the Knight.</param>
        public async Task CreateKnight(int userId, string name)
        {
            Knight knight = new Knight
            {
                Name = name,
                AttackPower = 20,
                Health = 150,
                Level = 1,
                Experience = 0,
                Gold = 10,
                UserId = userId
            };

            await _context.Knights.AddAsync(knight);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to delete the Knight.
        /// </summary>
        /// <param name="userId">Used to allocate the corrent Knight.</param>
        /// <param name="name">Name of the Knight.</param>
        /// <exception cref="Exception">Catch an error if user input is invalid.</exception>
        public async Task DeleteKnight(int userId, string name)
        {
            var knight = await this._context.Knights.Include(k => k.UserId).FirstOrDefaultAsync(k => k.UserId == userId && k.Name == name);

            if (knight == null)
            {
                throw new Exception(ExceptionMessages.InvalidCharacterName);
            }

            this._context.Knights.Remove(knight);
            await this._context.SaveChangesAsync();
        }
    }
}

