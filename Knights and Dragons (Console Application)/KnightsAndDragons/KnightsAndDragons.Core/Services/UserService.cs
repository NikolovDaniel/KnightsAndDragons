using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.DTOs;

namespace KnightsAndDragons.Core.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public Task CreateUser(string username, string password)
        {
            return _userRepository.CreateUser(username, password);
        }

        public ExportUserDto GetUserWithToken(string token)
        {
            return _userRepository.GetUserWithToken(token);
        }

        public Task<string> LoginUser(string username, string password)
        {
            return _userRepository.LoginUser(username, password);
        }

        public Task RemoveUserTokenAndSaveDbChanges(string token)
        {
            return _userRepository.RemoveUserTokenAndSaveDbChanges(token);
        }
    }
}

