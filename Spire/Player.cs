using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spire.Interfaces;

namespace Spire
{
  class Player : GameObject
  {
    private const float Speed = 3.0f;

    public Player(ContentManager content)
    {
      Texture = content.Load<Texture2D>("char");
      Size = new Vector2(Texture.Width, Texture.Height);
    }

    public override void Update(KeyboardState keyboardState, IStaticCollidable collidable)
    {
      // Do left/right movement
      if (keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Right))
        Velocity = new Vector2(-Speed, Velocity.Y);
      else if (keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left))
        Velocity = new Vector2(Speed, Velocity.Y);
      else
        Velocity = new Vector2(0.0f, Velocity.Y);

      // Jumping
      if (keyboardState.IsKeyDown(Keys.Up) && OnFloor)
        Velocity -= new Vector2(0.0f, 5.0f);

      base.Update(keyboardState, collidable);
    }
  }
}
