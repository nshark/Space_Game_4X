using Microsoft.Xna.Framework;

namespace Space_Game_4X;

public class RandomGenerationPolicy : IGenerationPolicy
{
    //Consts
    private const int NumberOfStars = 100;
    private const int StarfieldWidth = 5000;
    private const int StarfieldHeight = 5000;
    
    public Star[] GenerateStarfield()
    {
        Star[] stars = new Star[NumberOfStars];
        for (int i = 0; i < NumberOfStars; i++)
        {
            stars[i] = new Star(new Vector2(SpaceGame4X.Rand.Next(-1 * StarfieldWidth / 2, StarfieldWidth / 2),
                SpaceGame4X.Rand.Next(-1 * StarfieldHeight / 2, StarfieldHeight / 2)));
        }

        return stars;
    }
}