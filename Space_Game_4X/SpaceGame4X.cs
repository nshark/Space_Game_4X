using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Game_4X;

public class SpaceGame4X : Game
{
    //Consts
    private const float CameraSpeed = 0.1f;
    public static Vector2 CameraPos = new Vector2();
    public static float CameraScale = 1;
    public static Random Rand = new Random();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Star[] _stars;
    private int _scrollLastFrame = 0;
    public SpaceGame4X()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _stars = GenerationPolicy.GenerateStarfield();
        _scrollLastFrame = Mouse.GetState().ScrollWheelValue;
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
        KeyboardState kb = Keyboard.GetState();
        MouseState m = Mouse.GetState();
        if (kb.IsKeyDown(Keys.W))
        {
            CameraPos.Y += gameTime.ElapsedGameTime.Milliseconds * CameraSpeed;
        }
        if (kb.IsKeyDown(Keys.S))
        {
            CameraPos.Y -= gameTime.ElapsedGameTime.Milliseconds * CameraSpeed;
        }
        if (kb.IsKeyDown(Keys.A))
        {
            CameraPos.X += gameTime.ElapsedGameTime.Milliseconds * CameraSpeed;
        }
        if (kb.IsKeyDown(Keys.D))
        {
            CameraPos.X -= gameTime.ElapsedGameTime.Milliseconds * CameraSpeed;
        }

        int scrollDifference = m.ScrollWheelValue - _scrollLastFrame;
        if (scrollDifference != 0)
        {
            float zoomChange = 0.1f * (scrollDifference / 120f);
            CameraScale = MathHelper.Clamp(CameraScale + zoomChange, 0.1f, 10);
        }

        _scrollLastFrame = m.ScrollWheelValue;
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