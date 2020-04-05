using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spire.Interfaces;
using System.Drawing;

namespace Spire
{
  class Level : IStaticCollidable, IGameDrawable
  {
    public Texture2D Texture { get; set; }

    string[] map = new string[]
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

    public Level(ContentManager content)
    {
      Texture = content.Load<Texture2D>("block");
    }

    public bool Intersects(RectangleF rect)
    {
      var tileRect = new System.Drawing.RectangleF(0.0f, 0.0f, 32.0f, 32.0f);
      for (int y = 0; y < map.Length; ++y)
      {
        for (int x = 0; x < map[y].Length; ++x)
        {
          char tile = map[y][x];
          if (tile == '#')
          {
            tileRect.X = x * 32.0f;
            tileRect.Y = y * 32.0f;
            if (tileRect.IntersectsWith(rect))
              return true;
          }
        }
      }
      return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      for (int y = 0; y < map.Length; ++y)
      {
        for (int x = 0; x < map[y].Length; ++x)
        {
          char block = map[y][x];

          if (block == '#')
          {
            spriteBatch.Draw(Texture, new Vector2(x * 32, y * 32));
          }
        }
      }
    }
  }
}
