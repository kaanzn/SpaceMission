using SpaceMission.Enums;

namespace SpaceMission.Core;

internal class Spaceship
{
    public SpaceshipType _type;
    private readonly string _name =  "p";
    public short Health { get; private set; } = 100;
    public int Credits = 0;

    public Spaceship(SpaceshipType type, string name)
    {
        _type = type;
        _name = name;
    }
}