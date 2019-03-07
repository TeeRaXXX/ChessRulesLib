using System.Collections.Generic;

namespace ChessLib
{
    public class Chess
    {
        public string Fen { get; private set; }

        public const string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        private Board _board;
        private Moves _moves;
        private List<FigureMoving> _allMoves;

        public Chess(string fen = START_FEN)
        {
            Fen = fen;
            _board = new Board(fen);
            _moves = new Moves(_board);
        }

        private Chess(Board board)
        {
            _board = board;
            Fen = board.Fen;
            _moves = new Moves(board);
        }

        public Chess Move(string move)  // P2ee4    Pe7e8Q
        {
            FigureMoving fm = new FigureMoving(move);
            if (!_moves.CanMove(fm))
            {
                return this;
            }
            if (_board.IsCheckAfterMove(fm))
            {
                return this;
            }
            Board nextBoard = _board.Move(fm);
            Chess newChess = new Chess(nextBoard);
            return newChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = _board.GetFigureAt(square);
            return f == Figure.None ? Board.EMPTY_SQUARE : (char)f;
        }

        private void FindAllMoves()
        {
            _allMoves = new List<FigureMoving>();

            foreach (var fs in _board.YieldFigures())
            {
                foreach (var moveTo in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, moveTo);
                    if (_moves.CanMove(fm))
                    {
                        if (!_board.IsCheckAfterMove(fm))
                        {
                            _allMoves.Add(fm);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();

            foreach (var fm in _allMoves)
            {
                list.Add(fm.ToString());
            }
            return list;
        }

        public bool IsCheck()
        {
            return _board.IsCheck();
        }
    }
}
