using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Dungeon
{
    class Tile
    {
        public static Texture2D Texture;
        public Rectangle Rectangle { get; protected set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }

    class ContentTile : Tile
    {
        public ContentTile(Rectangle rec)
        {
            Rectangle = rec;
        }
    }
}
