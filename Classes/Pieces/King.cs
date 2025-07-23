using System.Collections.Generic;

namespace ChessBoardWPF.Classes.Pieces
{
    class King : ChessPiece
    {
        public King(Coord coord, string color) : base(coord, "King", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    AddMoveIfValid(moves, new Coord(Coord.X + x, Coord.Y + y), pieces);
                }
            }

            return moves;
        }
    }
}