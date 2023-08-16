namespace KnightsAndDragons.Core.Contracts
{
    public interface IDragon
    {
        int Id { get; }
        int UserId { get; }
        string Name { get; }
        int AttackPower { get; }
        int Health { get; }
        int Level { get; }
        int Experience { get; }
        int Gold { get; }
    }
}
