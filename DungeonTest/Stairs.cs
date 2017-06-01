using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon
{
    class Stairs
    {
        public static Texture2D Texture;
        public Rectangle Rectangle { get; private set; }
        public Vector2 position;

        public Stairs(Rectangle rectangle)
        {
            Rectangle = rectangle;
            position = new Vector2(rectangle.Left, rectangle.Top);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public void Collision()
        {
            /*if (Rectangle.IsTouchingTop())
            {
                position = new Vector2(position.X, position.Y + 2);
            }
            if (Rectangle.IsTouchingLeft())
            {
                position = new Vector2(position.X + 2, position.Y);
            }
            if (Rectangle.IsTouchingRight())
            {
                position = new Vector2(position.X - 2, position.Y);
            }
            if (Rectangle.IsTouchingBottom())
            {
                position = new Vector2(position.X, position.Y - 2);
            }*/

        }

    }
}
