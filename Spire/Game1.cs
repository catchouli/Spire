using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spire
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    Texture2D playerTexture;
    Texture2D blockTexture;

    string[] lvl = new string[]
    {
      "#########################",
      "#                       #",
      "#                       #",
      "#                       #",
      "#                       #",
      "#                       #",
      "#                       #",
      "#  ###                  #",
      "#                       #",
      "#                       #",
      "#          #########    #",
      "#                       #",
      "#                       #",
      "#                       #",
      "#########################",
    };

    Vector2 playerPos = new Vector2(100, 100);
    Vector2 playerVel = Vector2.Zero;
    bool onFloor = false;

    bool CheckLevelCollisions(System.Drawing.RectangleF boundingBox)
    {
      for (int y = 0; y < lvl.Length; ++y)
      {
        for (int x = 0; x < lvl[y].Length; ++x)
        {
          char tile = lvl[y][x];
          if (tile == '#')
          {
            var tileRect = new System.Drawing.RectangleF(x * 32, y * 32, 32, 32);
            if (tileRect.IntersectsWith(boundingBox))
              return true;
          }
        }
      }
      return false;
    }

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
      playerTexture = Content.Load<Texture2D>("char");
      blockTexture = Content.Load<Texture2D>("block");
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      KeyboardState state = Keyboard.GetState();

      // TODO: Add your update logic here
      playerVel += new Vector2(0.0f, 0.1f);
      if (state.IsKeyDown(Keys.Left) && !state.IsKeyDown(Keys.Right))
        playerVel.X = -3.0f;
      else if (state.IsKeyDown(Keys.Right) && !state.IsKeyDown(Keys.Left))
        playerVel.X = 3.0f;
      else
        playerVel.X = 0.0f;

      if (state.IsKeyDown(Keys.Up) && onFloor)
        playerVel -= new Vector2(0.0f, 5.0f);

      int steps = 5;
      Vector2 playerStep = playerVel / steps;

      Func<Vector2, System.Drawing.RectangleF> playerRect = pos => new System.Drawing.RectangleF(pos.X, pos.Y, 32, 64);

      for (int i = 0; i < steps; ++i)
      {
        Vector2 newPlayerPos = new Vector2(playerPos.X + playerStep.X, playerPos.Y);
        if (!CheckLevelCollisions(playerRect(newPlayerPos)))
          playerPos = newPlayerPos;

        newPlayerPos = new Vector2(playerPos.X, playerPos.Y + playerStep.Y);
        if (!CheckLevelCollisions(playerRect(newPlayerPos)))
        {
          onFloor = false;
          playerPos = newPlayerPos;
        }
        else
        {
          if (playerVel.Y > 0.0f)
            onFloor = true;
          playerVel.Y = 0.0f;
        }
      }

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here
      spriteBatch.Begin();
      for (int y = 0; y < lvl.Length; ++y)
      {
        for (int x = 0; x < lvl[y].Length; ++x)
        {
          char block = lvl[y][x];

          if (block == '#')
          {
            spriteBatch.Draw(blockTexture, new Vector2(x * 32, y * 32));
          }
        }
      }
      spriteBatch.Draw(playerTexture, playerPos);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
