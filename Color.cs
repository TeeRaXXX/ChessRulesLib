
namespace ChessLib
{
    enum Color
    {
        None,
        White,
        Black
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            if (color == Color.Black)
            {
                return Color.White;
            }
            return Color.None;
        }
    }
}
