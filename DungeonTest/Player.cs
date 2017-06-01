using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dungeon
{
    public class Player
    {
        public Texture2D Texture;
        private Vector2 speed;
        private Rectangle rectangle;
        private bool[] touchingWall;
        private int[] touchingWallTime;

        public Vector2 Position { get; private set; }

        public Player()
        {
            Position = new Vector2(25, 25);

            touchingWall = new Boolean[4];
            for (int i = 0; i < 3; i++)
                touchingWall[i] = false;

            touchingWallTime = new Int32[4];
            for (int i = 0; i < 3; i++)
                touchingWallTime[i] = 0;
        }

        public void Update(GameTime gameTime)
        {
            KeyBoardMove(gameTime);
            //GamePadMove();
            Position += speed;
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        private void KeyBoardMove(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W) && !touchingWall[0])
            {
                speed.Y = -CalculateSpeed(gameTime);
                speed.X = 0;
            }
            else if (keyboard.IsKeyDown(Keys.A) && !touchingWall[1])
            {
                speed.X = -CalculateSpeed(gameTime);
                speed.Y = 0;
            }
            else if (keyboard.IsKeyDown(Keys.D) && !touchingWall[2])
            {
                speed.X = CalculateSpeed(gameTime);
                speed.Y = 0;
            }
            else if (keyboard.IsKeyDown(Keys.S) && !touchingWall[3])
            {
                speed.Y = CalculateSpeed(gameTime);
                speed.X = 0;
            }
            else speed = new Vector2(0, 0);          
        }

        private float CalculateSpeed(GameTime gameTime)
        {
            return 3;//(float)gameTime.ElapsedGameTime.TotalMilliseconds / 8;
        }

        private void GamePadMove()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.ThumbSticks.Left.X != 0)
            {
                speed.X = gamePadState.ThumbSticks.Left.X * 1.2f;
                speed.Y = 0;
            }
            else if (gamePadState.ThumbSticks.Left.Y != 0)
            {
                speed.Y = -gamePadState.ThumbSticks.Left.Y * 1.2f;
                speed.X = 0;
            }
            else speed = new Vector2(0, 0);
        }

        public void Collision(int xoffset, int yoffset, Rectangle stairsRec)
        {
            if (rectangle.IsTouchingTop())
            {
                Position = new Vector2(Position.X, Position.Y + 3);
                touchingWall[0] = true;
            }
            if (rectangle.IsTouchingLeft())
            {
                Position = new Vector2(Position.X + 3, Position.Y);
                touchingWall[1] = true;
            }
            if (rectangle.IsTouchingRight())
            {
                Position = new Vector2(Position.X - 3, Position.Y);
                touchingWall[2] = true;
            }
            if (rectangle.IsTouchingBottom())
            {
                Position = new Vector2(Position.X, Position.Y - 3);
                touchingWall[3] = true;
            }

            if (Position.X < 25)
                Position = new Vector2(25, Position.Y);

            if (Position.X > xoffset - rectangle.Width)
                Position = new Vector2(xoffset - rectangle.Width, Position.Y);

            if (Position.Y < 25)
                Position = new Vector2(Position.X, 25);

            if (Position.Y > yoffset - rectangle.Height)
                Position = new Vector2(Position.X, yoffset - rectangle.Height);

            if (rectangle.IsStairsTouching(stairsRec))
            {
                GameState.IsCleared = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (touchingWall[i])
                    touchingWallTime[i]++;
                if (touchingWallTime[i] >= 20)
                {
                    touchingWall[i] = false;
                    touchingWallTime[i] = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, rectangle, Color.White);
        }
    }
}
