using KnightsAndDragons.UI.Controllers;
using KnightsAndDragons.UI.Utilities.Messages;

namespace KnightsAndDragons.UI.Views
{
    public class StartMenuView
    {
        /// <summary>
        /// This method reads the User input data and does CRUD operations corresponding to the input.
        /// </summary>
        /// <param name="authController">Authorization Controller used for Login and Register the User.</param>
        /// <returns>Token as a string.</returns>
        /// <exception cref="Exception">Throws an error if the User press a wrong key.</exception>
        public async Task<string> CreateStartMenuView(AuthController authController)
        {
            Console.Write(OutputMessages.StartMenuText);

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.KeyChar == '1') // LOGIN
            {
                string token = await Login(authController);
                return token;
            }
            else if (key.KeyChar == '2') // REGISTER
            {
                string result = await Register(authController);
                return result ; 
            }
            else if (key.KeyChar == '3') // EXIT THE GAME
            {
                throw new Exception(ExceptionMessages.ExitingTheGame);
            }
            else
            {
                throw new Exception(ExceptionMessages.InvalidNumber);
            }

        }

        /// <summary>
        /// This method read the user input and use it to login the user and returns the newly created token.
        /// </summary>
        /// <param name="authController">Authorization Controller to authorize the login.</param>
        /// <returns>The token as a string.</returns>
        private async Task<string> Login(AuthController authController)
        {
            Console.Clear();
            Console.Write(OutputMessages.CreateUsername);
            string username = Console.ReadLine()!;

            Console.Write(OutputMessages.CreatePassword);
            string password = Console.ReadLine()!;

            string token = await authController.Login(username, password);

            return token;
        }

        /// <summary>
        /// This method read the user input and use it to register the user, log it and returns the newly created token with a message.
        /// </summary>
        /// <param name="authController">Authorization Controller which we pass to another method to authorize the login of the User.</param>
        /// <returns>String with successful message and the token with ' / ' separator.</returns>
        private async Task<string> Register(AuthController authController)
        {
            Console.Clear();
            Console.WriteLine(OutputMessages.UsernameRestrictrions);
            Console.Write(OutputMessages.CreateUsername);
            string username = Console.ReadLine()!;

            Console.WriteLine(OutputMessages.PasswordRestrictions);
            Console.Write(OutputMessages.CreatePassword);
            string password = Console.ReadLine()!;

            await authController.RegisterUser(username, password);

            string token = await LoginRegisteredUser(authController, username, password);

            return $"{string.Format(OutputMessages.SuccessfullyRegistered, username)} / {token}";
        }

        /// <summary>
        /// This method login the newly created User and return the token.
        /// </summary>
        /// <param name="authController">Authorization Controller to authorize the login.</param>
        /// <param name="username">The username of the newly created User.</param>
        /// <param name="password">The password of the newly created User.</param>
        /// <returns>The token as a string.</returns>
        private async Task<string> LoginRegisteredUser(AuthController authController, string username, string password)
        {
            string token = await authController.Login(username, password);

            return token;
        }
    }
}

