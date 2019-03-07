
using System;
using System.Collections.Generic;

namespace ChessLib
{
    struct Square
    {
        public static Square None = new Square(-1, -1);
        public int X { get; private set; }
        public int Y { get; private set; }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Square(string e2)
        {
            if (e2.Length == 2 &&
                e2[0] >= 'a' && e2[0] <= 'h' &&
                e2[1] >= '1' && e2[1] <= '8')
            {
                X = e2[0] - 'a';
                Y = e2[1] - '1';
            }
            else
            {
                this = None;
            }
        }

        public bool OnBoard()
        {
            return X >= 0 && X < 8 &&
                   Y >= 0 && Y < 8;
        }

        public string Name { get { return ((char)('a' + X)).ToString() + (Y + 1).ToString(); } }

        public static bool operator == (Square a, Square b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator != (Square a, Square b)
        {
            return !(a == b);
        }

        public static IEnumerable<Square> YieldSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }
    }
}
