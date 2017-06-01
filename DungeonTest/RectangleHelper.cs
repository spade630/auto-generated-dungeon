using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Dungeon
{
    static class RectangleHelper
    {
        public static bool IsTouchingTop(this Rectangle r)
        {
            for (int x = r.Left; x <= r.Right; x++)
            {
                if (!Map.CanEntry[r.Top, x])
                    return true;
            }

            return false;
        }

        public static bool IsTouchingBottom(this Rectangle r)
        {
            for (int x = r.Left; x <= r.Right; x++)
            {
                if (!Map.CanEntry[r.Bottom, x])
                    return true;
            }

            return false;
            //return (!(Map.CanEntry[r.Bottom, r.Left]) || !(Map.CanEntry[r.Bottom, r.Right]));
        }

        public static bool IsTouchingLeft(this Rectangle r)
        {
            for (int y = r.Top; y <= r.Bottom; y++)
            {
                if (!Map.CanEntry[y, r.Left])
                    return true;
            }

            return false;
            //return (!(Map.CanEntry[r.Top, r.Left]) || !(Map.CanEntry[r.Bottom, r.Left]));
        }

        public static bool IsTouchingRight(this Rectangle r)
        {
            for (int y = r.Top; y <= r.Bottom; y++)
            {
                if (!Map.CanEntry[y, r.Right])
                    return true;
            }

            return false;
            //return (!(Map.CanEntry[r.Top, r.Right]) || !(Map.CanEntry[r.Bottom, r.Right]));
        }

        public static bool IsStairsTouching(this Rectangle playerRec, Rectangle stairsRec)
        {
            return (playerRec.Left <= stairsRec.Right &&
                    playerRec.Right >= stairsRec.Left &&
                    playerRec.Top <= stairsRec.Bottom &&
                    playerRec.Bottom >= stairsRec.Top);
        }

    }
}
