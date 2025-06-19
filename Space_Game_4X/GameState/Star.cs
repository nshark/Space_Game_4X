using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Game_4X;

public class Star
{
    private static Texture2D[] _starTextures;
    private static Vector2[] _starTextureOrigins;

    [JsonConverter(typeof(Vector2JsonConverter))]
    public Vector2 Position { get; set; }

    //determines which star sprite to use - from 0-3
    public int StarType { get; set; }

    public Planet[] Planets { get; set; }
    public static void InitTextures(Game game)
    {
        _starTextures = new[]
        {
            game.Content.Load<Texture2D>("SpaceSprites/star_large"),
            game.Content.Load<Texture2D>("SpaceSprites/star_medium"),
            game.Content.Load<Texture2D>("SpaceSprites/star_small"),
            game.Content.Load<Texture2D>("SpaceSprites/star_tiny")
        };
        _starTextureOrigins = new[]
        {
            new Vector2(_starTextures[0].Width/2f, _starTextures[0].Height/2f),
            new Vector2(_starTextures[1].Width/2f, _starTextures[1].Height/2f),
            new Vector2(_starTextures[2].Width/2f, _starTextures[2].Height/2f),
            new Vector2(_starTextures[3].Width/2f, _starTextures[3].Height/2f)
        };
    }
    // if -1 is passed as the star type, it is randomized - this is the default value
    public Star(Vector2 position, int starType=-1, Planet[] planets = null)
    {
        if (starType == -1)
        {
            StarType = SpaceGame4X.Rand.Next(0, 3);
        }

        if (planets == null)
        {
            int numOfPlanets = SpaceGame4X.Rand.Next(0, 5);
            if (numOfPlanets != 0)
            {
                Planets = new Planet[numOfPlanets];
                for (int i = 0; i < Planets.Length; i++)
                {
                    Planets[i] = new Planet();
                }
            }
        }

        Position = position;
    }

    public void AddToSpriteBatch(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_starTextures[StarType], Vector2.Multiply(Position + SpaceGame4X.CameraPos, SpaceGame4X.CameraScale) + SpaceGame4X.CameraOffset, null, Color.White, 0f, _starTextureOrigins[StarType], SpaceGame4X.CameraScale, SpriteEffects.None, 0);
    }
}