using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLib
{
    class Board
    {
        public string Fen { get; private set; }
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public const short BOARD_SIZE = 8;
        public const char EMPTY_SQUARE = '.';

        private Figure[,] _figures;

        public Board(string fen)
        {
            Fen = fen;
            _figures = new Figure[BOARD_SIZE, BOARD_SIZE];
            Init();
        }

        private void Init()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            //0                                           1 2    3 4 5
            string[] parts = Fen.Split();
            if (parts.Length != 6)
            {
                return;
            }
            InitFigures(parts[0]);
            MoveColor = parts[1] == "b" ? Color.Black : Color.White;
            MoveNumber = int.Parse(parts[5]);
        }

        private void InitFigures(string data)
        {
            //  Replace from /8/ to /11111111/
            for (int j = 8; j >= 2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", EMPTY_SQUARE.ToString());
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    _figures[x, y] = lines[7 - y][x] == '.' ? Figure.None : (Figure)lines[7 - y][x];
                }
            }
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (var square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }

        private void GenerateFen()
        {
            Fen = FenFigures() + " " +
                   (MoveColor == Color.White ? "w" : "b") +
                   " - - 0 " + MoveNumber.ToString();
        }

        private string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(_figures[x, y] == Figure.None ? '1' : (char)_figures[x, y]);
                }
                if (y > 0)
                {
                    sb.Append('/');
                }
            }
            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }

            return sb.ToString();
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return _figures[square.X, square.Y];
            }
            return Figure.None;
        }

        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                _figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Fen);
            next.SetFigureAt(fm.MoveFrom, Figure.None);
            next.SetFigureAt(fm.MoveTo, fm.Promotion == Figure.None ? fm.Figure : fm.Promotion);
            if (MoveColor == Color.Black)
            {
                next.MoveNumber++;
            }
            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();
            return next;
        }

        private bool CanEatKing()
        {
            Square enemyKing = FindEnemyKing();
            Moves moves = new Moves(this);
            foreach (var fs in YieldFigures())
            {
                FigureMoving fm = new FigureMoving(fs, enemyKing);
                if (moves.CanMove(fm))
                {
                    return true;
                }
            }
            return false;
        }

        private Square FindEnemyKing()
        {
            Figure enemyKing = MoveColor == Color.Black ? Figure.WhiteKing : Figure.BlackKing;
            foreach (var square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == enemyKing)
                {
                    return square;
                }
            }
            return Square.None;
        }

        public bool IsCheck()
        {
            Board after = new Board(Fen);
            after.MoveColor = MoveColor.FlipColor();
            return after.CanEatKing();
        }

        public bool IsCheckAfterMove(FigureMoving fm)
        {
            Board after = Move(fm);
            return after.CanEatKing();
        }
    }
}
