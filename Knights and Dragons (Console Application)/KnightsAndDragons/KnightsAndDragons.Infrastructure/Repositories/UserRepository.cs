
using Microsoft.EntityFrameworkCore;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Infrastructure.Data;
using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Core.DTOs.ImportDtos;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.Infrastructure.Utilities.Messages;

namespace KnightsAndDragons.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KnightsAndDragonsDbContext _context;

        public UserRepository(KnightsAndDragonsDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Method to retrieve the currently logged in User with a token.
        /// </summary>
        /// <param name="token">Token to retrieve a User.</param>
        /// <returns>Currently logged in User</returns>
        public ExportUserDto GetUserWithToken(string token)
        {
            var user = _context.Users
                .Include(x => x.Dragons)
                .Include(x => x.Knights)
                .Where(x => x.Token == token)
                .Select(x => new ExportUserDto()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Token = x.Token,
                    Knights = x.Knights,
                    Dragons = x.Dragons
                })
                .FirstOrDefault();

            return user!;
        }

        /// <summary>
        /// Method for Login validatio, 
        /// </summary>
        /// <param name="username">User submitted username.</param>
        /// <param name="password">User submitted password.</param>
        /// <returns>Logged in User.</returns>
        /// <exception cref="Exception">Throws an error of wrong username or password.</exception>
        public async Task<string> LoginUser(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            //Verify the user's username
            if (user == null)
            {
                throw new Exception(string.Format(ExceptionMessages.WrongUsernameOrPassword));
            }

            // Verify the user's password
            bool passwordIsValid = user.Password == password;

            if (!passwordIsValid)
            {
                throw new Exception(string.Format(ExceptionMessages.WrongUsernameOrPassword));
            }

            // Generate a login token
            var token = Guid.NewGuid().ToString();

            // Store the token in the database
            user.Token = token;

            await _context.SaveChangesAsync();

            return user.Token;
        }

        /// <summary>
        /// Method to create a User using DTO and save it to the Database.
        /// </summary>
        /// <param name="username">User submitted username.</param>
        /// <param name="password">User submitted password.</param>
        /// <exception cref="Exception">Throws an error if username is taken.</exception>
        public async Task CreateUser(string username, string password)
        {
            bool isTaken = IsTaken(username);

            if (isTaken)
            {
                throw new Exception(string.Format(ExceptionMessages.UsernameTaken, username));
            }

            try
            {
                ImportUserDto userDto = new ImportUserDto(username, password);

                User user = new User
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Token = null!
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to be called from the outside to save changes in the Database. 
        /// </summary>
        public async Task RemoveUserTokenAndSaveDbChanges(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Token == token);

            user!.Token = null!;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to check for a certain username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolean result of whether there is a user with given username.</returns>
        private bool IsTaken(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}

