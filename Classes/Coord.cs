using System.Windows;

namespace ChessBoardWPF
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord(Coord other)
        {
            X = other.X;
            Y = other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Coord))
                return false;

            Coord coord = (Coord)obj;
            return X == coord.X && Y == coord.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public static Coord PointToCoord(Point p, int canvasSize)
        {
            return new Coord((int)(p.X / canvasSize), (int)(p.Y / canvasSize));
        }
    }
}