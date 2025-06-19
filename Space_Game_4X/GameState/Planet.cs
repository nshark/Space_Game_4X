using System;

namespace Space_Game_4X;

public class Planet
{
    private static WeightedRandom Types = new WeightedRandom();

    public static void InitTypes()
    {
        Types.AddItem("Barren", 5);
        Types.AddItem("Toxic", 5);
        Types.AddItem("Continental", 2.5);
        Types.AddItem("Gaia", 0.1);
    }
    public string Type;
    public int Size;
    public Planet()
    {
        Size = SpaceGame4X.Rand.Next(5, 15);
        Type = Types.GetRandomItem();
    }

    public Planet(string type, int size)
    {
        Type = type;
        Size = size;
    }
}