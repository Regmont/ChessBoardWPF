using ChessBoardWPF.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessBoardWPF.Classes
{
    class ChessBoardHandler
    {
        private const int Size = 8;

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
                        rect.Fill = Brushes.LightBlue;
                    else if (selectedPiece != null && selectedPiece.GetPossibleMoves(pieces, enPassantCoord).Any(m => m.X == x && m.Y == y))
                        rect.Fill = Brushes.LightGreen;
                    else
                        rect.Fill = (x + y) % 2 == 0 ? Brushes.White : Brushes.Gray;

                    Canvas.SetLeft(rect, x * 60);
                    Canvas.SetTop(rect, y * 60);

                    FieldCanvas.Children.Add(rect);
                }
            }

            foreach (ChessPiece piece in pieces)
            {
                string imageUrl = PieceImages.GetPieceImageUrl(piece.Type, piece.Color);
                AddPieceToCell(piece.Coord, imageUrl);
            }
        }

        private void AddPieceToCell(Coord coord, string imageUrl)
        {
            Image pieceImage = new Image();
            pieceImage.Source = new BitmapImage(new Uri(imageUrl));
            pieceImage.Width = 40;
            pieceImage.Height = 40;

            Canvas.SetLeft(pieceImage, coord.X * 60 + 10);
            Canvas.SetTop(pieceImage, coord.Y * 60 + 10);

            FieldCanvas.Children.Add(pieceImage);
        }

        public void PromotePawn(Pawn pawn)
        {
            var dialog = new PromotionDialog(pawn.Color);
            if ((bool)dialog.ShowDialog())
            {
                ChessPiece newPiece;
                switch (dialog.SelectedPieceType)
                {
                    case "Queen":
                        newPiece = new Queen(pawn.Coord, pawn.Color);
                        break;
                    case "Rook":
                        newPiece = new Rook(pawn.Coord, pawn.Color);
                        break;
                    case "Bishop":
                        newPiece = new Bishop(pawn.Coord, pawn.Color);
                        break;
                    case "Knight":
                        newPiece = new Knight(pawn.Coord, pawn.Color);
                        break;
                    default:
                        throw new InvalidOperationException("Неизвестный тип фигуры");
                }

                pieces.Remove(pawn);
                pieces.Add(newPiece);
            }
        }
    }
}