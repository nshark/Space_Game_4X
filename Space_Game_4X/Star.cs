using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Game_4X;

public class Star
{
    private static Texture2D[] _starTextures;
    private static Vector2[] _starTextureOrigins;
    
    private Vector2 pos;
    //determines which star sprite to use - from 0-3
    private int _starType;

    public Vector2 Position
    {
        get => pos;
    }
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
    public Star(Vector2 position, int starType=-1)
    {
        if (starType == -1)
        {
            _starType = SpaceGame4X.Rand.Next(0, 3);
        }

        pos = position;
    }

    public void AddToSpriteBatch(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_starTextures[_starType], Vector2.Multiply(pos + SpaceGame4X.CameraPos, SpaceGame4X.CameraScale) + SpaceGame4X.CameraOffset, null, Color.White, 0f, _starTextureOrigins[_starType], SpaceGame4X.CameraScale, SpriteEffects.None, 0);
    }
}