using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.UI.Controllers;
using KnightsAndDragons.UI.Utilities.Messages;

namespace KnightsAndDragons.UI.Views
{
    public class LoggedInView
    {
        private readonly IUserService _userService;

        public LoggedInView(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Shows the User view when logged in.
        /// </summary>
        /// <param name="token">The token which we use to validate the User.</param>
        /// <param name="controller">Controller which retrieves data about the User from the Database.</param>
        /// <param name="authController">Authorization Controller used Logout the user.</param>
        /// <returns>String with a message corresponding to the user input data.</returns>
        public async Task<string> CreateLoggedInView(string token, Controller controller, AuthController authController)
        {
            var user = _userService.GetUserWithToken(token);
            var knights = await controller.GetUserKnights(token);
            var dragons = await controller.GetUserDragons(token);

            Console.Write(string.Format(OutputMessages.MainMenuText, user.Username, dragons.Count(), knights.Count()));
            
            ConsoleKeyInfo key = Console.ReadKey(); 

            try
            {
                if (key.KeyChar == '1') // LIST ALL AVAILABLE CHARACTERS
                {
                    Console.Clear();
                    string userChars = await controller.GetUserCharacters(token);
                    return userChars;
                }
                else if (key.KeyChar == '2') // CREATE CHARACTER
                { 
                    string result = await PrintCreateCharacter(controller, token);
                    return result;
                }
                else if (key.KeyChar == '3') // REMOVE CHARACTER
                {
                    string result = await PrintRemoveCharacter(controller, token); 
                    return result;
                }
                else if (key.KeyChar == '4') // LOGOUT 
                {
                    string result = $"{await authController.Logout(token)} / Logout";
                    return result;
                }
                else
                {
                    throw new Exception(ExceptionMessages.InvalidNumber);
                }
            }
            catch 
            {
                throw;
            }
        }
        
        /// <summary>
        /// This method reads user input data and pass it to the controller to do the CRUD operation.
        /// </summary>
        /// <param name="controller">Controller which manages the creation of the character.</param>
        /// <param name="token">The token is passed as a parameter to validate the User.</param>
        /// <returns>String with the successful creation of the character.</returns>
        /// <exception cref="Exception">Throws an error when the User press a wrong key.</exception>
        private async Task<string> PrintCreateCharacter(Controller controller, string token) 
        {
            Console.Clear();
            Console.Write(OutputMessages.ChooseType);

            ConsoleKeyInfo key = Console.ReadKey();

            string type = string.Empty;

            if (key.KeyChar == '1')
            {
                type = "Dragon";
            }
            else if (key.KeyChar == '2')
            {
                type = "Knight";
            }
            else
            {
                throw new Exception(string.Format(ExceptionMessages.InvalidNumber));
            }

            Console.Write(OutputMessages.ChooseName);

            string name = Console.ReadLine()!;

            return await controller.CreateCharacter(token, type, name);
        }

        /// <summary>
        /// This method reads user input data and pass it to the Controller to do the CRUD operation.
        /// </summary>
        /// <param name="controller">Controller which manages the removal of the character.</param>
        /// <param name="token">The token is passed as a parameter to validate the User.</param>
        /// <returns>String with the successful removal of the character.</returns>
        private async Task<string> PrintRemoveCharacter(Controller controller, string token)
        {
            string characters = await controller.GetUserCharacters(token);

            Console.Clear();
            Console.WriteLine(characters);
            Console.WriteLine(OutputMessages.CharacterToDelete);
            Console.Write(OutputMessages.ChooseNameToDelete);

            string name = Console.ReadLine()!;

            Console.Write(OutputMessages.ChooseTypeToDelete);

            string type = Console.ReadLine()!;

            return await controller.RemoveCharacter(token, type, name);
        }
    }
}

