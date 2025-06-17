using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Game_4X;

public class SpaceGame4X : Game
{
    //Consts
    private const int NumberOfStars = 100;
    
    
    public static Random rand = new Random();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Star[] _stars;
    public SpaceGame4X()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _stars = new[] { new Star(new Vector2(50, 50)) };
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Star.InitTextures(this);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        foreach (var star in _stars)
        {
            star.AddToSpriteBatch(_spriteBatch);
        }
        _spriteBatch.End();
        // TODO: Add your drawing code here
        
        base.Draw(gameTime);
    }
}