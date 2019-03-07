
using System;

namespace ChessLib
{
    class Moves
    {
        private FigureMoving _fm;
        private Board _board;

        public Moves(Board board)
        {
            _board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            _fm = fm;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        private bool CanMoveFrom()
        {
            return _fm.MoveFrom.OnBoard() && _fm.Figure.GetColor() == _board.MoveColor;
        }

        private bool CanMoveTo()
        {
            return _fm.MoveTo.OnBoard() && 
                _fm.MoveFrom != _fm.MoveTo &&
                _board.GetFigureAt(_fm.MoveTo).GetColor() != _board.MoveColor;
        }

        private bool CanFigureMove()
        {
            switch (_fm.Figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return CanStraightMove();

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return (_fm.SignX == 0 || _fm.SignY == 0) && CanStraightMove();

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return (_fm.SignX != 0 && _fm.SignY != 0) && CanStraightMove();

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPownMove();

                default: return false;
            }
        }

        private bool CanKingMove()
        {
            if (_fm.AbsDeltaX <= 1 && _fm.AbsDeltaY <= 1)
            {
                return true;
            }
            return false;
        }

        private bool CanKnightMove()
        {
            if (_fm.AbsDeltaX == 1 && _fm.AbsDeltaY == 2)
            {
                return true;
            }
            if (_fm.AbsDeltaX == 2 && _fm.AbsDeltaY == 1)
            {
                return true;
            }
            return false;
        }

        private bool CanPownMove()
        {
            if (_fm.MoveFrom.Y < 1 || _fm.MoveFrom.Y > 6)
            {
                return false;
            }
            int stepY = _fm.Figure.GetColor() == Color.White ? 1 : -1;
            return CanPownGo(stepY) || CanPownJump(stepY) || CanPowmEat(stepY);
        }

        private bool CanPownGo(int stepY)
        {
            if (_board.GetFigureAt(_fm.MoveTo) == Figure.None)
            {
                if (_fm.DeltaX == 0)
                {
                    if (_fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanPownJump(int stepY)
        {
            if (_board.GetFigureAt(_fm.MoveTo) == Figure.None)
            {
                if (_fm.DeltaX == 0)
                {
                    if (_fm.DeltaY == 2 * stepY)
                    {
                        if (_fm.MoveFrom.Y == 1 || _fm.MoveFrom.Y == 6)
                        {
                            if (_board.GetFigureAt(new Square(_fm.MoveFrom.X, _fm.MoveFrom.Y + stepY)) == Figure.None)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CanPowmEat(int stepY)
        {
            if (_board.GetFigureAt(_fm.MoveTo) != Figure.None)
            {
                if (_fm.AbsDeltaX == 1)
                {
                    if (_fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanStraightMove()
        {
            Square at = _fm.MoveFrom;
            do
            {
                at = new Square(at.X + _fm.SignX, at.Y + _fm.SignY);
                if (at == _fm.MoveTo)
                {
                    return true;
                }
            } while (at.OnBoard() && _board.GetFigureAt(at) == Figure.None);

            return false;
        }
    }
}
