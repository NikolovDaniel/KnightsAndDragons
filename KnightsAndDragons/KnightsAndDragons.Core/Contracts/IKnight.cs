namespace KnightsAndDragons.Core.Contracts
{
    public interface IKnight
    {
        int Id { get; }
        int UserId { get; }
        string Name { get; }
        int AttackPower { get; }
        int Health { get; }
        int Mana { get; }
        int Level { get; }
        int Experience { get; }
        int Gold { get; }
    }
}