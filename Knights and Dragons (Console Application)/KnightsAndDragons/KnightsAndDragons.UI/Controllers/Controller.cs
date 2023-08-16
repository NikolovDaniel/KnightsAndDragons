using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.UI.Utilities.Messages;
using System.Text;

namespace KnightsAndDragons.UI.Controllers
{
    public class Controller : IController
    {
        private IDragonService _dragonService; 
        private IKnightService _knightService;
        private IUserService _userService;

        public Controller(IDragonService dragonService, IKnightService knightService, IUserService userService)
        {
            this._dragonService = dragonService;
            this._knightService = knightService;
            this._userService = userService;
        }

        /// <summary>
        /// Method used to retrieve User's knight collection. It retrieves the User and 
        /// get its collection of Knights.
        /// </summary>
        /// <param name="token">Token to retrieve the currently logged in User.</param>
        /// <returns>Collection of Knights</returns>
        public async Task<ICollection<ExportUserKnightDto>> GetUserKnights(string token)
        {
            var user = _userService.GetUserWithToken(token);

            var knights = await this._knightService.GetUserKnights(user.Id);

            return knights!;
        }
       
        /// <summary>
        /// Method used to retrieve User's dragon collection. It retrieves the User and 
        /// get its collection of Dragons.
        /// </summary>
        /// <param name="token">Token to retrieve the currently logged in User.</param>
        /// <returns>Collection of Dragons</returns>
        public async Task<ICollection<ExportUserDragonDto>> GetUserDragons(string token)
        {
            var user = _userService.GetUserWithToken(token);

            var dragons = await this._dragonService.GetUserDragons(user.Id);

            return dragons;
        }

        /// <summary>
        /// Method to retrive all owned characters by the user and print their info.
        /// </summary>
        /// <param name="token">Token to retrieve the currently logged in User.</param>
        /// <returns>StringBuilder as string with information about all the owned characters.</returns>
        public async Task<string> GetUserCharacters(string token)
        {
            var user = GetUser(token);

            var knights = await this._knightService.GetUserKnights(user.Id);
            var dragons = await this._dragonService.GetUserDragons(user.Id);

            return PrintCharactersDetails(knights, dragons);
        }

        /// <summary>
        /// Method to create different type of character depending on user input data.
        /// </summary>
        /// <param name="token">Token to retrieve the currently logged in User.</param>
        /// <param name="characterType">Type of the character, chosen from the User.</param>
        /// <param name="name">Name of the character.</param>
        /// <returns>String with a message about the creation of the character.</returns>
        /// <exception cref="ArgumentException">Throws an error if the user choose wrong type.</exception>
        public async Task<string> CreateCharacter(string token, string characterType, string name)
        {
            var user = GetUser(token);

            if (characterType == nameof(Dragon))
            {
                await this._dragonService.CreateDragon(user.Id, name);
                return string.Format(OutputMessages.SuccessfullyAddedDragon);
            }
            else if (characterType == nameof(Knight))
            {
                await this._knightService.CreateKnight(user.Id, name);
                return string.Format(OutputMessages.SuccessfullyAddedKnight);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.CharacterTypeNotValid);
            }
        }

        /// <summary>
        /// Method to delete different type of character depending on user input data.
        /// </summary>
        /// <param name="token">Token to retrieve the currently logged in User.</param>
        /// <param name="characterType">Type of the character, chosen from the User.</param>
        /// <param name="name">Name of the character.</param>
        /// <returns>String with a message about the removal of the character.</returns>
        /// <exception cref="ArgumentException">Throws an error if the user choose wrong type.</exception>
        public async Task<string> RemoveCharacter(string token, string characterType, string name)
        {
            var user = GetUser(token);

            if (characterType == nameof(Dragon))
            {
                await this._dragonService.DeleteDragon(user.Id, name);
                return string.Format(OutputMessages.SuccessfullyRemovedDragon);
            }
            else if (characterType == nameof(Knight))
            {
                await this._knightService.DeleteKnight(user.Id, name);
                return string.Format(OutputMessages.SuccessfullyRemovedKnight);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.CharacterTypeNotValid);
            }
        }

        /// <summary>
        /// Private method to retrieve a User with token.
        /// </summary>
        /// <param name="token">Token to retrieve a User.</param>
        /// <returns>The retrieved user.</returns>
        /// <exception cref="Exception">Throws an error if the token is invalid.</exception>
        private ExportUserDto GetUser(string token)
        {
            var user = this._userService.GetUserWithToken(token);

            if (user == null)
            {
                throw new Exception(string.Format(ExceptionMessages.InvalidToken));
            }

            return user;
        }

        /// <summary>
        /// Private method to represent the info of all characters of a User as a string.
        /// </summary>
        /// <param name="knights">User's collection of Knights.</param>
        /// <param name="dragons">User's colection of Dragons.</param>
        /// <returns>String with info about the characters of the User.</returns>
        private string PrintCharactersDetails(ICollection<ExportUserKnightDto> knights, ICollection<ExportUserDragonDto> dragons)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Knights:");

            if (knights.Count() == 0)
            {
                sb.AppendLine(string.Format(OutputMessages.UserHasNoKnights));
            }

            foreach (var k in knights.OrderByDescending(k => k.Level))
            {
                sb.AppendLine(string.Format(OutputMessages.CharacterDetails, k.Name, k.AttackPower, k.Health, k.Level, k.Experience));
            }

            sb.AppendLine("\nDragons:");

            if (dragons.Count() == 0)
            {
                sb.AppendLine(string.Format(OutputMessages.UserHasNoDragons));
            }

            foreach (var d in dragons.OrderByDescending(d => d.Level))
            {
                sb.AppendLine(string.Format(OutputMessages.CharacterDetails, d.Name, d.AttackPower, d.Health, d.Level, d.Experience));
            }

            return sb.ToString().TrimEnd();
        }
    }
}
