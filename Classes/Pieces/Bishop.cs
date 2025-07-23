using System.Collections.Generic;

namespace ChessBoardWPF.Classes.Pieces
{
    class Bishop : ChessPiece
    {
        public Bishop(Coord coord, string color) : base(coord, "Bishop", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();

            ProcessDirection(moves, pieces, -1, -1);
            ProcessDirection(moves, pieces, 1, -1);
            ProcessDirection(moves, pieces, 1, 1);
            ProcessDirection(moves, pieces, -1, 1);

            return moves;
        }
    }
}