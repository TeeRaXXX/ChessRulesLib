using System;
using ChessLib;

namespace DemoChessLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine(chess.Fen);
                Console.WriteLine(ChessToASCII(chess));
                foreach (var move_ in chess.GetAllMoves())
                {
                    Console.Write(move_ + '\t');
                }
                Console.WriteLine();
                Console.Write("> ");
                string move = Console.ReadLine();
                if (move == "")
                {
                    break;
                }
                chess = chess.Move(move);
            }
        }

        private static string ChessToASCII(Chess chess)
        {
            string text = "  +-----------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }
                text += "|\n";
            }
            text += "  +-----------------+\n";
            text += "    a b c d e f g h\n";

            return text;
        }
    }
}
