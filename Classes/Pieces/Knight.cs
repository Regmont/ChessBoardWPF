using System.Collections.Generic;

namespace ChessBoardWPF.Classes.Pieces
{
    class Knight : ChessPiece
    {
        public Knight(Coord coord, string color) : base(coord, "Knight", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();

            int[] xSteps = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] ySteps = { -1, -2, -2, -1, 1, 2, 2, 1 };

            for (int i = 0; i < 8; i++)
            {
                AddMoveIfValid(moves, new Coord(Coord.X + xSteps[i], Coord.Y + ySteps[i]), pieces);
            }

            return moves;
        }
    }
}