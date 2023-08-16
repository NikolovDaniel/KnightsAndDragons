using KnightsAndDragons.Core.Models;

namespace KnightsAndDragons.Core.Contracts
{
    public interface IUser
    {
        int Id { get; }
        string Username { get; }
        string Password { get; }
        string Token { get; }
        ICollection<Dragon> Dragons { get; }
        ICollection<Knight> Knights { get; }
    }
}
