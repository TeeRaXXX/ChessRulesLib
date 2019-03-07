using System;

namespace ChessLib
{
    class FigureMoving
    {
        public Figure Figure { get; private set; }
        public Square MoveFrom { get; private set; }
        public Square MoveTo { get; private set; }
        public Figure Promotion { get; private set; }

        public FigureMoving(FigureOnSquare figureOnSquare, Square moveTo, Figure promotion = Figure.None)
        {
            Figure = figureOnSquare.Figure;
            MoveFrom = figureOnSquare.Square;
            MoveTo = moveTo;
            Promotion = promotion;
        }

        public FigureMoving(string move)    //  Pe2e4   Pe7e8Q
        {
            Figure = (Figure) move[0];
            MoveFrom = new Square(move.Substring(1, 2));
            MoveTo = new Square(move.Substring(3, 2));
            Promotion = (move.Length == 6) ? (Figure)move[5] : Figure.None;
        }

        public int DeltaX { get { return MoveTo.X - MoveFrom.X; } }
        public int DeltaY { get { return MoveTo.Y - MoveFrom.Y; } }

        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }

        public int SignX { get { return Math.Sign(DeltaX); } }
        public int SignY { get { return Math.Sign(DeltaY); } }

        public override string ToString()
        {
            string text = (char)Figure + MoveFrom.Name + MoveTo.Name;
            if (Promotion != Figure.None)
            {
                text += (char)Promotion;
            }
            return text;
        }
    }
}
