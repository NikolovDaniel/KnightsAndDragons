using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.DTOs;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.UI.Utilities.Messages;
using System.Text;

namespace KnightsAndDragons.UI.Controllers;

public class AuthController
{
    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        this._userService = userService;
    }

    /// <summary>
    /// Method used to pass user input data to the User Repository.
    /// </summary>
    /// <param name="username">Inputted username.</param>
    /// <param name="password">Inputted password.</param>
    /// <returns>Message about the creation of the new User.</returns>
    public async Task<string> RegisterUser(string username, string password)
    {
        await this._userService.CreateUser(username, password);

        return OutputMessages.CreatedUser;
    }

    /// <summary>
    /// Method to Log in the User.
    /// </summary>
    /// <param name="username">Parameter passed to another method, to validate the User.</param>
    /// <param name="password">Parameter passed to another method, to validate the User.</param>
    /// <returns>The User's newly created token.</returns>
    public async Task<string> Login(string username, string password)
    {
        var token = await _userService.LoginUser(username, password);

        return token!;
    }

    /// <summary>
    /// Method to Logout the User.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws an error for invalid Token.</exception>
    public async Task<string> Logout(string token)
    {
        await _userService.RemoveUserTokenAndSaveDbChanges(token);

        return OutputMessages.SuccessfullyLoggedOut;
    }

    /// <summary>
    /// Method to delete the token and save changes by calling SaveChanges through the method of userService.
    /// </summary>
    /// <param name="token">Token to retrieve the currently logged in User.</param>
    public async Task RemoveUserToken(string token)
    {
        await _userService.RemoveUserTokenAndSaveDbChanges(token);
    }
}

