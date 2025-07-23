using System.Collections.Generic;

namespace ChessBoardWPF.Classes.Pieces
{
    class Queen : ChessPiece
    {
        public Queen(Coord coord, string color) : base(coord, "Queen", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    ProcessDirection(moves, pieces, x, y);
                }
            }

            return moves;
        }
    }
}