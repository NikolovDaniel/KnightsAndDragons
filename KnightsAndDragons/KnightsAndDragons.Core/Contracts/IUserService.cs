using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IUserService
    {
        Task<string> LoginUser(string username, string password);
        Task CreateUser(string username, string password);
        ExportUserDto GetUserWithToken(string token);
        Task RemoveUserTokenAndSaveDbChanges(string token);
    }
}

