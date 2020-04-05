using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spire.Interfaces;

namespace Spire
{
  class GameObject : IMoving, IGameDrawable
  {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Gravity { get; set; } = 0.1f;
    public int MoveSteps { get; set; } = 5;
    public Vector2 Size { get; set; } = new Vector2(32.0f, 32.0f);
    public Texture2D Texture { get; set; }

    protected bool OnFloor { get; private set; }

    public void Move(IStaticCollidable collidable)
    {
      // Add gravity
      Velocity += new Vector2(0.0f, Gravity);

      // Iterate position
      Vector2 moveStep = Velocity / MoveSteps;
      for (int i = 0; i < MoveSteps; ++i)
      {
        // Iterate horizontal position until we hit a wall
        Vector2 newPos = new Vector2(Position.X + moveStep.X, Position.Y);
        if (!collidable.Intersects(GetRectForPosition(newPos)))
          Position = newPos;

        // Iterate vertical position until we hit a floor/ceiling
        newPos = new Vector2(Position.X, Position.Y + moveStep.Y);
        if (!collidable.Intersects(GetRectForPosition(newPos)))
        {
          OnFloor = false;
          Position = newPos;
        }
        else
        {
          if (Velocity.Y > 0.0f)
            OnFloor = true;
          Velocity = new Vector2(Velocity.X, 0.0f);
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, Position);
    }

    public virtual void Update(KeyboardState keyboardState, IStaticCollidable collidable)
    {
      Move(collidable);
    }

    private System.Drawing.RectangleF GetRectForPosition(Vector2 pos)
      => new System.Drawing.RectangleF(pos.X, pos.Y, Size.X, Size.Y);
  }
}
