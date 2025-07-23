using ChessBoardWPF.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessBoardWPF
{
    public abstract class ChessPiece
    {
        public Coord Coord { get; set; }
        public string Type { get; protected set; }
        public string Color { get; }

        protected ChessPiece(Coord coord, string type, string color)
        {
            Coord = coord;
            Type = type;
            Color = color;
        }

        public abstract List<Coord> GetPossibleMoves(List<ChessPiece> pieces, Coord enPassantCoord);

        public bool AvailableForEnPassant(Coord previousCoord)
        {
            if (this is Pawn && Math.Abs(Coord.Y - previousCoord.Y) == 2)
                return true;
            return false;
        }

        protected void AddMoveIfValid(List<Coord> moves, Coord newCoord, List<ChessPiece> pieces)
        {
            if (newCoord.X >= 0 && newCoord.X < 8 && newCoord.Y >= 0 && newCoord.Y < 8)
            {
                var targetPiece = pieces.FirstOrDefault(p => p.Coord.Equals(newCoord));
                if (targetPiece == null)
                {
                    moves.Add(newCoord);
                }
                else if (!targetPiece.Color.Equals(Color))
                {
                    moves.Add(newCoord);
                }
            }
        }

        protected void AddAttackMoveIfValid(List<Coord> moves, Coord newCoord, List<ChessPiece> pieces)
        {
            if (newCoord.X >= 0 && newCoord.X < 8 && newCoord.Y >= 0 && newCoord.Y < 8)
            {
                var targetPiece = pieces.FirstOrDefault(p => p.Coord.Equals(newCoord));
                if (targetPiece != null && !targetPiece.Color.Equals(Color))
                {
                    moves.Add(newCoord);
                }
            }
        }

        protected void ProcessDirection(List<Coord> moves, List<ChessPiece> pieces, int xStep, int yStep, bool continuous = true)
        {
            int x = Coord.X + xStep;
            int y = Coord.Y + yStep;

            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                Coord newCoord = new Coord(x, y);
                var targetPiece = pieces.FirstOrDefault(p => p.Coord.Equals(newCoord));

                if (targetPiece == null)
                {
                    moves.Add(newCoord);
                }
                else
                {
                    if (!targetPiece.Color.Equals(Color))
                    {
                        moves.Add(newCoord);
                    }
                    break;
                }

                if (!continuous) break;

                x += xStep;
                y += yStep;
            }
        }
    }
}