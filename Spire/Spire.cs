using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Spire
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Spire : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    Level level;
    ICollection<GameObject> gameObjects = new List<GameObject>();

    SpriteFont font;

    SoundEffect weird;

    public Spire()
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

      level = new Level(Content);
      gameObjects.Add(new Player(Content) { Position = new Vector2(100, 100) });

      font = Content.Load<SpriteFont>("RubberBiscuit");

      weird = Content.Load<SoundEffect>("weird");

      var instance = weird.CreateInstance();
      instance.Play();
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
      foreach (var obj in gameObjects)
      {
        obj.Update(state, level);
      }

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

      spriteBatch.Begin();
      level.Draw(spriteBatch);
      foreach (var obj in gameObjects)
      {
        obj.Draw(spriteBatch);
      }
      spriteBatch.DrawString(font, "Font test", new Vector2(50, 50), Color.White);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
