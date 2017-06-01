using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dungeon
{
    class GameState
    {
        public List<Enemy> enemies = new List<Enemy>();
        Random rand = new Random();
        Map map;
        Player player;
        Stairs stairs;
        public static bool IsCleared;

        public GameState()
        {
            IsCleared = false;
        }

        public void Initialize(ContentManager Content)
        {
            map = new Map();
            player = new Player();
            int px = rand.Next(50, 900);
            int py = rand.Next(50, 600);
            px = px / 50;
            py = py / 50;
            stairs = new Stairs(new Rectangle(px * 50 - 25, py * 50 - 25, 25, 25));
            Tile.Texture = Content.Load<Texture2D>(@"Dungeon");
            player.Texture = Content.Load<Texture2D>(@"Player");
            Stairs.Texture = Content.Load<Texture2D>(@"DownStairs");

            map.Put(2, 25);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            player.Collision(map.Width, map.Height, stairs.Rectangle);
            stairs.Collision();

            if(IsCleared)
                Clear();
        }

        private void Clear()
        {
            map.Maze.Clear();
            int d = rand.Next(4);
            map.Put(d, 25);
            int px = rand.Next(50, 900);
            int py = rand.Next(50, 600);
            px = px / 50;
            py = py / 50;
            stairs = new Stairs(new Rectangle(px * 50 - 25, py * 50 - 25, 25, 25));
            IsCleared = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            stairs.Draw(spriteBatch);

            const int size = 25;
            for (int i = 0; i < 35; i++)
            {
                spriteBatch.Draw(Tile.Texture, new Rectangle(i * size, 0, size, size), Color.White);
                spriteBatch.Draw(Tile.Texture, new Rectangle(i * size, 550, size, size), Color.White);
            }

            for (int i = 0; i < 23; i++)
            {
                spriteBatch.Draw(Tile.Texture, new Rectangle(0, i * size, size, size), Color.White);
                spriteBatch.Draw(Tile.Texture, new Rectangle(850, i * size, size, size), Color.White);
            }
        }
    }
}
