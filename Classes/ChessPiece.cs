namespace ChessBoardWPF
{
    public class ChessPiece
    {
        public Coord Coord { get; set; }
        public string Type { get; set; }
        public string Color { get; }

        public ChessPiece(Coord coord, string type, string color)
        {
            Coord = coord;
            Type = type;
            Color = color;
        }
    }
}