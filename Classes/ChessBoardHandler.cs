using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessBoardWPF.Classes.Pieces;
using System.Linq;

namespace ChessBoardWPF.Classes
{
    class ChessBoardHandler
    {
        #region constants
        private const int Size = 8;

        private const string BlackBishop = "https://upload.wikimedia.org/wikipedia/commons/8/81/Chess_bdt60.png";
        private const string WhiteBishop = "https://upload.wikimedia.org/wikipedia/commons/9/9b/Chess_blt60.png";
        private const string WhiteKing = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Chess_klt60.png";
        private const string BlackKing = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Chess_kdt60.png";
        private const string WhiteKnight = "https://upload.wikimedia.org/wikipedia/commons/2/28/Chess_nlt60.png";
        private const string BlackKnight = "https://upload.wikimedia.org/wikipedia/commons/f/f1/Chess_ndt60.png";
        private const string WhitePawn = "https://upload.wikimedia.org/wikipedia/commons/0/04/Chess_plt60.png";
        private const string BlackPawn = "https://upload.wikimedia.org/wikipedia/commons/c/cd/Chess_pdt60.png";
        private const string WhiteQueen = "https://upload.wikimedia.org/wikipedia/commons/4/49/Chess_qlt60.png";
        private const string BlackQueen = "https://upload.wikimedia.org/wikipedia/commons/a/af/Chess_qdt60.png";
        private const string WhiteRook = "https://upload.wikimedia.org/wikipedia/commons/5/5c/Chess_rlt60.png";
        private const string BlackRook = "https://upload.wikimedia.org/wikipedia/commons/a/a0/Chess_rdt60.png";
        #endregion

        public Canvas FieldCanvas;
        List<ChessPiece> pieces;

        public ChessBoardHandler(Canvas fieldCanvas, List<ChessPiece> pieces)
        {
            FieldCanvas = fieldCanvas;
            this.pieces = pieces;
        }

        public void SetChessBoard()
        {
            pieces.Add(new Rook(new Coord(0, 0), "Black"));
            pieces.Add(new Rook(new Coord(7, 0), "Black"));
            pieces.Add(new Knight(new Coord(1, 0), "Black"));
            pieces.Add(new Knight(new Coord(6, 0), "Black"));
            pieces.Add(new Bishop(new Coord(2, 0), "Black"));
            pieces.Add(new Bishop(new Coord(5, 0), "Black"));
            pieces.Add(new Queen(new Coord(3, 0), "Black"));
            pieces.Add(new King(new Coord(4, 0), "Black"));

            pieces.Add(new Rook(new Coord(0, 7), "White"));
            pieces.Add(new Rook(new Coord(7, 7), "White"));
            pieces.Add(new Knight(new Coord(1, 7), "White"));
            pieces.Add(new Knight(new Coord(6, 7), "White"));
            pieces.Add(new Bishop(new Coord(2, 7), "White"));
            pieces.Add(new Bishop(new Coord(5, 7), "White"));
            pieces.Add(new Queen(new Coord(3, 7), "White"));
            pieces.Add(new King(new Coord(4, 7), "White"));

            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(new Coord(i, 1), "Black"));
                pieces.Add(new Pawn(new Coord(i, 6), "White"));
            }

            PrintChessBoard(pieces, null, null);
        }

        public void PrintChessBoard(List<ChessPiece> pieces, ChessPiece selectedPiece, Coord enPassantCoord)
        {
            FieldCanvas.Children.Clear();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = 60,
                        Height = 60,
                        Stroke = Brushes.Black
                    };

                    if (selectedPiece != null && selectedPiece.Coord.X == x && selectedPiece.Coord.Y == y)
                    {
                        rect.Fill = Brushes.LightBlue;
                    }
                    else if (selectedPiece != null && selectedPiece.GetPossibleMoves(pieces, enPassantCoord).Any(m => m.X == x && m.Y == y))
                    {
                        rect.Fill = Brushes.LightGreen;
                    }
                    else
                    {
                        rect.Fill = (x + y) % 2 == 0 ? Brushes.White : Brushes.Gray;
                    }

                    Canvas.SetLeft(rect, x * 60);
                    Canvas.SetTop(rect, y * 60);

                    FieldCanvas.Children.Add(rect);
                }
            }

            foreach (ChessPiece piece in pieces)
            {
                string imageUrl = GetPieceImageUrl(piece);
                AddPieceToCell(piece.Coord.Y, piece.Coord.X, imageUrl);
            }
        }

        private string GetPieceImageUrl(ChessPiece piece)
        {
            if (piece.Color == "Black")
            {
                switch (piece.Type)
                {
                    case "Rook": return BlackRook;
                    case "Knight": return BlackKnight;
                    case "Bishop": return BlackBishop;
                    case "Queen": return BlackQueen;
                    case "King": return BlackKing;
                    case "Pawn": return BlackPawn;
                    default: throw new ArgumentException("Unknown black piece type");
                }
            }
            else
            {
                switch (piece.Type)
                {
                    case "Rook": return WhiteRook;
                    case "Knight": return WhiteKnight;
                    case "Bishop": return WhiteBishop;
                    case "Queen": return WhiteQueen;
                    case "King": return WhiteKing;
                    case "Pawn": return WhitePawn;
                    default: throw new ArgumentException("Unknown white piece type");
                }
            }
        }

        private void AddPieceToCell(int row, int col, string imageUrl)
        {
            Image pieceImage = new Image();
            pieceImage.Source = new BitmapImage(new Uri(imageUrl));
            pieceImage.Width = 40;
            pieceImage.Height = 40;

            Canvas.SetLeft(pieceImage, col * 60 + 10);
            Canvas.SetTop(pieceImage, row * 60 + 10);

            FieldCanvas.Children.Add(pieceImage);
        }
    }
}