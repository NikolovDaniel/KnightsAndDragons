namespace KnightsAndDragons.UI.Utilities.Messages
{
    public class OutputMessages
    {
        public const string CharacterDetails = " --- Name: {0}, Attack Power: {1}, Health: {2}, Level: {3}, Experience: {4}.";
        public const string SuccessfullyAddedKnight = "You have successfully created a Knight.";
        public const string SuccessfullyAddedDragon = "You have successfully created a Dragon.";
        public const string SuccessfullyRemovedKnight = "You have successfully removed a Knight.";
        public const string SuccessfullyRemovedDragon = "You have successfully removed a Dragon.";
        public const string UserHasNoKnights = " --- You have no Knights created.";
        public const string UserHasNoDragons = " --- You have no Dragons created.";
        public const string CreatedUser = "You have successfully created an account. Please log in!";
        public const string SuccessfullyLoggedIn = "You have successfully logged in!";
        public const string SuccessfullyLoggedOut = "You have successfully logged out!\n";
        public const string SuccessfullyRegistered = "You have successfully registered new account with the username: {0}!" +
                                                     "\nRedirecting you to the dashboard...";
        public const string StartMenuText = "[1]. Login." +
                                            "\n[2]. Register." +
                                            "\n[3]. Exit the game!!!" +
                                            "\n - Please choose an option by pressing a number: ";
        public const string MainMenuText = "\nLogged in: {0}, Dragons: {1}, Knights: {2}" +
                                           "\n--- [1]. List all Dragons and Knights." +
                                           "\n--- [2]. Create character." +
                                           "\n--- [3]. Remove character." +
                                           "\n--- [4]. Logout." +
                                           "\n--- Please choose an option by pressing a number: ";
        public const string ChooseType = "Please select what character type you want to create by pressing the correct number: [1]. Dragon, [2]. Knight. Pressed key: ";
        public const string ChooseName = "\nPlease choose a Name for your character: ";
        public const string ChooseNameToDelete = "\nPlease choose the name of the character you want to delete: ";
        public const string ChooseTypeToDelete = "\nPlease choose the type of the character you want to delete: ";
        public const string CharacterToDelete = "\n --- You can choose a character from above to delete.";
        public const string UsernameRestrictrions = " * Username should be at least 5 characters long and maximum 100.";
        public const string CreateUsername = "Create username: ";
        public const string PasswordRestrictions = " * Password must contain one number, one big letter and minimum length of 8 characters.";
        public const string CreatePassword = "\nChoose a password: ";
    }
}

