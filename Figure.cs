
namespace ChessLib
{
    enum Figure
    {
        None,

        WhiteKing = 'K',
        WhiteQueen = 'Q',
        WhiteRook = 'R',
        WhiteBishop = 'B',
        WhiteKnight = 'N',
        WhitePawn = 'P',

        BlackKing = 'k',
        BlackQueen = 'q',
        BlackRook = 'r',
        BlackBishop = 'b',
        BlackKnight = 'n',
        BlackPawn = 'p',
    }

    static class FigureMetods
    {
        public static Color GetColor(this Figure figure)
        {
            if (figure == Figure.None)
            {
                return Color.None;
            }
            return (figure == Figure.WhiteKing ||
                    figure == Figure.WhiteQueen ||
                    figure == Figure.WhiteRook ||
                    figure == Figure.WhiteBishop ||
                    figure == Figure.WhiteKnight ||
                    figure == Figure.WhitePawn)
                ? Color.White : Color.Black;
        }
    }
}
