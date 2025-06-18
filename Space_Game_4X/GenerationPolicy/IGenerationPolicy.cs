using Microsoft.Xna.Framework;

namespace Space_Game_4X;

interface IGenerationPolicy
{
    public Star[] GenerateStarfield();
}