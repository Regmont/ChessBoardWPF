using System.Collections.Generic;

namespace ChessBoardWPF.Classes.Pieces
{
    class Rook : ChessPiece
    {
        public Rook(Coord coord, string color) : base(coord, "Rook", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();

            ProcessDirection(moves, pieces, -1, 0);
            ProcessDirection(moves, pieces, 1, 0);
            ProcessDirection(moves, pieces, 0, -1);
            ProcessDirection(moves, pieces, 0, 1);

            return moves;
        }
    }
}