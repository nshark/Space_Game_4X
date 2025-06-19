using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Space_Game_4X.Components;
using Space_Game_4X.Screens;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Space_Game_4X;

public class SpaceGame4X : Game
{
    //Consts
    private const float CameraSpeed = 0.1f;
    
    public static Vector2 CameraPos = new Vector2();
    public static float CameraScale = 1;
    public static Random Rand = new Random();
    private GraphicsDeviceManager _graphics;
    private HUD _screen;
    private GumService Gum => GumService.Default;
    private SpriteBatch _spriteBatch;
    private GameState _gameState = new GameState();
    private int _scrollLastFrame = 0;
    public static Vector2 CameraOffset = Vector2.Zero;
    private readonly IGenerationPolicy _generationPolicy = new RandomRetryGenerationPolicy();
    public SpaceGame4X()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _gameState.Stars = _generationPolicy.GenerateStarfield();
        _scrollLastFrame = Mouse.GetState().ScrollWheelValue;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Gum.Initialize(this, "GumUI/gumUI.gumx");
        _screen = new HUD();
        _screen.TurnMenuInstance.Click += OnEndTurn;
        _screen.TurnMenuInstance.ButtonCategoryState = TurnMenu.ButtonCategory.Enabled;
        _screen.AddToRoot();
        base.Initialize();
    }

    private void OnEndTurn(object sender, EventArgs e)
    {
        _screen.TurnMenuInstance.CounterNumText = (int.Parse(_screen.TurnMenuInstance.CounterNumText) + 1).ToString();
        _gameState.Save("autosave_" + _screen.TurnMenuInstance.CounterNumText + ".json");
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Star.InitTextures(this);
        Planet.InitTypes();
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        Gum.Update(gameTime);
        if (Math.Abs(_graphics.GraphicsDevice.Viewport.Width / 2f - CameraOffset.X) > 0.001 ||
            Math.Abs(_graphics.GraphicsDevice.Viewport.Height / 2f - CameraOffset.Y) > 0.001)
        {
            CameraOffset.X = _graphics.GraphicsDevice.Viewport.Width / 2f;
            CameraOffset.Y = _graphics.GraphicsDevice.Viewport.Height / 2f;
        }
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
        foreach (var star in _gameState.Stars)
        {
            star.AddToSpriteBatch(_spriteBatch);
        }
        _spriteBatch.End();
        // TODO: Add your drawing code here
        Gum.Draw();
        base.Draw(gameTime);
    }
}