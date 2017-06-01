using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon
{
    class Map
    {
        public List<ContentTile> Maze = new List<ContentTile>();
        public static bool[,] CanEntry;

        public int Width { get; private set; }

        public int Height { get; private set; }

        private const int W = 16, H = 10;

        public void Put(int d, int size)
        {
            CanEntry = new Boolean[1000, 1500];
            for (int i = 0; i < 1024; i++)
                for (int j = 0; j < 768; j++)
                    CanEntry[j, i] = true;

            switch (d)
            {
                case 0:
                    TopPut(size);
                    break;
                case 1:
                    RightPut(size);
                    break;
                case 2:
                    BottomPut(size);
                    break;
                case 3:
                    LeftPut(size);
                    break;
            }
            Width = (W + 1) * (size * 2);
            Height = (H + 1) * (size * 2);

        }
            
        public int[,] Direction;
        

        public Map()
        {
            Direction = new Int32[50, 50];
        }

        public void Update()
        {
        }

        void TopPut(int size)
        {
            var rand = new Random();
            int d;
            //上0, 右1, 下2, 左3

            for (int y = 1; y <= H; y++)
            {
                for (int x = 1; x <= W; x++)
                {
                    while (true)
                    {
                        d = rand.Next(4);
                        if (y != 1)
                        {
                            if (d == 0)
                                continue;
                        }
                        if (x >= 2)
                        {
                            if (Direction[y, (x - 1)] == 1 && d == 3)
                                continue;

                        }
                        Direction[y, x] = d;
                        GenerateTile(x, y, d, size);
                        
                        break;
                    }
                }
            }
        }

        void RightPut(int size)
        {
            var rand = new Random();
            int d;
            //上0, 右1, 下2, 左3

            for (int x = W; x >= 1; x--)
            {
                for (int y = 1; y <= H; y++)
                {
                    while (true)
                    {
                        d = rand.Next(4);
                        if (x != W)
                        {
                            if (d == 1)
                                continue;
                        }

                        if (y >= 2)
                        {
                            if (d == 0 && Direction[(y - 1), x] == 2)
                                continue;
                        }
                        Direction[y, x] = d;
                        GenerateTile(x, y, d, size);

                        break;
                    }
                }
            }
        }

        void BottomPut(int size)
        {
            var rand = new Random();
            int d;
            //上0, 右1, 下2, 左3

            for (int x = 1; x <= W; x++)
            {
                for (int y = H; y >= 1; y--)
                {
                    while (true)
                    {
                        d = rand.Next(4);
                        if (y != H)
                        {
                            if (d == 2)
                                continue;
                        }
                        if (x >= 2)
                        {
                            if (Direction[y, (x - 1)] == 1 && d == 3)
                                continue;
                        }
                        Direction[y, x] = d;
                        GenerateTile(x, y, d, size);

                        break;
                    }
                }
            }
        }

        void LeftPut(int size)
        {
            var rand = new Random();
            int d;
            //上0, 右1, 下2, 左3

            for (int x = 1; x <= W; x++)
            {
                for (int y = 1; y <= H; y++)
                {
                    while (true)
                    {
                        d = rand.Next(4);
                        if (x != 1)
                        {
                            if (d == 3)
                                continue;
                        }
                        if (y >= 2)
                        {
                            if (Direction[(y- 1), x - 1] == 2 && d == 0)
                                continue;
                        }
                        Direction[y, x] = d;
                        GenerateTile(x, y, d, size);

                        break;
                    }
                }
            }
        }

        private void GenerateTile(int x, int y, int d, int size)
        {

            Maze.Add(new ContentTile(new Rectangle(x * (size * 2), y * (size * 2), size, size)));

            for (int i = x * (size * 2); i <= x * (size * 2) + size; i++)
                for (int j = y * (size * 2); j <= y * (size * 2) + size; j++)
                    CanEntry[j, i] = false;

            //CanEntry[(2 * y - 1), (2 * x - 1)] = false;

            switch (d)
            {
                case 0:
                    Maze.Add(new ContentTile(new Rectangle(x * (size * 2), y * (size * 2) - size, size, size)));

                    for (int i = x * (size * 2); i <= x * (size * 2) + size; i++)
                        for (int j = y * (size * 2) - size; j <= y * (size * 2); j++)
                            CanEntry[j, i] = false;
                    break;

                case 1:
                    Maze.Add(new ContentTile(new Rectangle(x * (size * 2) + size, y * (size * 2), size, size)));

                    for (int i = x * (size * 2) + size; i <= x * (size * 2) + (2 * size); i++)
                        for (int j = y * (size * 2); j <= y * (size * 2) + size; j++)
                            CanEntry[j, i] = false;
                    break;

                case 2:
                    Maze.Add(new ContentTile(new Rectangle(x * (size * 2), y * (size * 2) + size, size, size)));

                    for (int i = x * (size * 2); i <= x * (size * 2) + size; i++)
                        for (int j = y * (size * 2) + size; j <= y * (size * 2) + (size * 2); j++)
                            CanEntry[j, i] = false;
                    break;

                case 3:
                    Maze.Add(new ContentTile(new Rectangle(x * (size * 2) - size, y * (size * 2), size, size)));

                    for (int i = x * (size * 2) - size; i <= x * (size * 2); i++)
                        for (int j = y * (size * 2); j <= y * (size * 2) + size; j++)
                            CanEntry[j, i] = false;
                    break;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tiles in Maze)
                tiles.Draw(spriteBatch);
        }
    }
}
