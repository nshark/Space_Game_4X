using System;
using Microsoft.Xna.Framework;

namespace Space_Game_4X;

public class RandomRetryGenerationPolicy : IGenerationPolicy
{
    //Consts
    private const int NumberOfStars = 100;
    private const int StarfieldWidth = 5000;
    private const int StarfieldHeight = 5000;
    
    private const int MaxRetries = 1000;
    private const int MinimumDistanceBetweenStars = 100;
    private const int MaximumDistanceBetweenStars = 500;
    
    public Star[] GenerateStarfield()
    {
        Star[] stars = new Star[NumberOfStars];
        for (int i = 0; i < NumberOfStars; i++)
        {
            bool placedStar = false;
            for (int j = 0; j < MaxRetries; j++)
            {
                stars[i] = new Star(new Vector2(SpaceGame4X.Rand.Next(-1 * StarfieldWidth / 2, StarfieldWidth / 2),
                    SpaceGame4X.Rand.Next(-1 * StarfieldHeight / 2, StarfieldHeight / 2)));

                if (i == 0)
                {
                    // the first star is always placed successfully on the first try
                    placedStar = true;
                    break;
                }
                
                bool foundMinDistanceNeighbor = false;
                bool foundMaxDistanceNeighbor = false;
                for (int k = 0; k < i; k++)
                {
                    Vector2 distance = stars[i].Position - stars[k].Position;
                    float distanceSquared = distance.LengthSquared();
                    if (distanceSquared < MinimumDistanceBetweenStars*MinimumDistanceBetweenStars)
                    {
                        foundMinDistanceNeighbor = true;
                        break;
                    }

                    if (distanceSquared < MaximumDistanceBetweenStars*MaximumDistanceBetweenStars)
                    {
                        foundMaxDistanceNeighbor = true;
                    }
                }

                if (!foundMinDistanceNeighbor && foundMaxDistanceNeighbor)
                {
                    placedStar = true;
                    break;
                }
            }

            if (!placedStar)
            {
                // might be better to throw an exception?
                Console.Error.WriteLine("Exceeded maximum number of retries for random star generation");
            }
        }

        return stars;
    }
}