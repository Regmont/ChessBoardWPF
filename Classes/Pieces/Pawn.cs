using System.Collections.Generic;
using System.Linq;

namespace ChessBoardWPF.Classes.Pieces
{
    class Pawn : ChessPiece
    {
        public Pawn(Coord coord, string color) : base(coord, "Pawn", color) { }

        public override List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord)
        {
            List<Coord> moves = new List<Coord>();
            int direction = Color == "White" ? -1 : 1;

            Coord forward = new Coord(Coord.X, Coord.Y + direction);
            if (pieces.FirstOrDefault(p => p.Coord.Equals(forward)) == null)
            {
                moves.Add(forward);

                if ((Color == "White" && Coord.Y == 6) || (Color == "Black" && Coord.Y == 1))
                {
                    Coord doubleForward = new Coord(Coord.X, Coord.Y + 2 * direction);
                    if (pieces.FirstOrDefault(p => p.Coord.Equals(doubleForward)) == null)
                    {
                        moves.Add(doubleForward);
                    }
                }
            }

            AddAttackMoveIfValid(moves, new Coord(Coord.X - 1, Coord.Y + direction), pieces);
            AddAttackMoveIfValid(moves, new Coord(Coord.X + 1, Coord.Y + direction), pieces);

            if (enPassantCoord != null)
                GetEnPassantMove(enPassantCoord, moves);

            return moves;
        }

        public void GetEnPassantMove(Coord enPassantCoord, List<Coord> moves)
        {
            if (Color.Equals("White") && Coord.Y == 3)
            {
                if (Coord.X > 0)
                {
                    Coord leftCheck = new Coord(Coord.X - 1, Coord.Y - 1);
                    if (leftCheck.Equals(enPassantCoord))
                    {
                        moves.Add(leftCheck);
                        return;
                    }
                }

                if (Coord.X < 7)
                {
                    Coord rightCheck = new Coord(Coord.X + 1, Coord.Y - 1);
                    if (rightCheck.Equals(enPassantCoord))
                    {
                        moves.Add(rightCheck);
                        return;
                    }
                }
            }

            if (Color.Equals("Black") && Coord.Y == 4)
            {
                if (Coord.X > 0)
                {
                    Coord leftCheck = new Coord(Coord.X - 1, Coord.Y + 1);
                    if (leftCheck.Equals(enPassantCoord))
                    {
                        moves.Add(leftCheck);
                        return;
                    }
                }

                if (Coord.X < 7)
                {
                    Coord rightCheck = new Coord(Coord.X + 1, Coord.Y + 1);
                    if (rightCheck.Equals(enPassantCoord))
                    {
                        moves.Add(rightCheck);
                        return;
                    }
                }
            }
        }
    }
}

//Будет добавлен функционал превращения пешки