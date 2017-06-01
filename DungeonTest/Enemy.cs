using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon
{
    class Enemy
    {
        public static Texture2D Texture;
        public Vector2 Position { get; private set; }
        public Vector2 Speed { get; private set; }


        public Enemy(Vector2 position)
        {
            Position = position;
        }

        public void Update()
        {
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
