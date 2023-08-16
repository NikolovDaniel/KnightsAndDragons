using KnightsAndDragons.UI.Views;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.UI.Utilities.Messages;
using KnightsAndDragons.UI.Controllers;

namespace KnightsAndDragons.UI
{
    public class Game : IGame
    {
        private SerializerController serializeController;
        private Controller controller;
        private AuthController authController;
        private LoggedInView loggedInView;
        private StartMenuView startMenuView;
        private string token = null!;

        public Game(IDragonService dragonService, IKnightService knightService, IUserService userService, IJsonSerializerServices serializer, IJsonDeserializerServices deserializer)
        {
            this.serializeController = new SerializerController(serializer, deserializer);
            this.controller = new Controller(dragonService, knightService, userService);
            this.authController = new AuthController(userService);
            this.loggedInView = new LoggedInView(userService);
            this.startMenuView = new StartMenuView();
        }

        /// <summary>
        /// This method starts the Game and shows the Views for the User.
        /// </summary>
        public async Task Start()
        {
            // Event handler for when the User exit the console application.
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(Current_DomainProcess);

            /// Deserialize a JSON file and populate the DB. Do not delete the files, because it will throw an exception.
            this.serializeController.Deserialize();

            while (true)
            {
                try
                {
                    if (this.token == null) // Here we present the Logging View for the User
                    {

                        string result = await startMenuView.CreateStartMenuView(authController);
                        string[] input = result.Split('/', StringSplitOptions.RemoveEmptyEntries).ToArray();

                        if (input.Length == 1) // If Login is chosen we set the token and let the User log in
                        {
                            SetToken(input[0]);
                            Console.Clear();
                            Console.WriteLine(OutputMessages.SuccessfullyLoggedIn);
                        }
                        else // If Register is chosen we set the token and log the User.
                        {
                            SetToken(input[1].Trim());
                            Console.Clear();
                            Console.WriteLine(input[0]);
                        }

                        continue;
                    }
                    else // Here we present the logged in view
                    {
                        string result = await loggedInView.CreateLoggedInView(token, controller, authController);
                        string[] input = result.Split('/', StringSplitOptions.RemoveEmptyEntries).ToArray();

                        if (input.Length == 1) // Other options 
                        {
                            Console.Clear();
                            Console.WriteLine(input[0]);
                        }
                        else // If user choose Logout
                        {
                            this.token = null!;
                            Console.Clear();
                            Console.WriteLine(input[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);

                    if (ex.Message == ExceptionMessages.ExitingTheGame)
                    {
                        this.serializeController.Serialize();
                        Environment.Exit(0);
                    }

                    continue;
                }
            }
        }

        /// <summary>
        /// Setting the token for the current User.
        /// </summary>
        /// <param name="token">Token to validate the User.</param>
        private void SetToken(string token)
        {
            this.token = token;
        }

        /// <summary>
        /// Sets the token to null for the User in the Database.
        /// </summary>
        private async void Current_DomainProcess(object sender, EventArgs e)
        {
            await this.authController.RemoveUserToken(this.token);
        }
    }
}