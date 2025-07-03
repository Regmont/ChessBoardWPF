using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

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

        private Canvas FieldCanvas;
        List<ChessPiece> pieces;

        public ChessBoardHandler(Canvas fieldCanvas, List<ChessPiece> pieces)
        {
            FieldCanvas = fieldCanvas;
            this.pieces = pieces;
        }

        public void SetChessBoard()
        {
            pieces.Add(new ChessPiece(new Coord(0, 0), "Rook", "Black"));
            pieces.Add(new ChessPiece(new Coord(7, 0), "Rook", "Black"));
            pieces.Add(new ChessPiece(new Coord(1, 0), "Knight", "Black"));
            pieces.Add(new ChessPiece(new Coord(6, 0), "Knight", "Black"));
            pieces.Add(new ChessPiece(new Coord(2, 0), "Bishop", "Black"));
            pieces.Add(new ChessPiece(new Coord(5, 0), "Bishop", "Black"));
            pieces.Add(new ChessPiece(new Coord(3, 0), "Queen", "Black"));
            pieces.Add(new ChessPiece(new Coord(4, 0), "King", "Black"));

            pieces.Add(new ChessPiece(new Coord(0, 7), "Rook", "White"));
            pieces.Add(new ChessPiece(new Coord(7, 7), "Rook", "White"));
            pieces.Add(new ChessPiece(new Coord(1, 7), "Knight", "White"));
            pieces.Add(new ChessPiece(new Coord(6, 7), "Knight", "White"));
            pieces.Add(new ChessPiece(new Coord(2, 7), "Bishop", "White"));
            pieces.Add(new ChessPiece(new Coord(5, 7), "Bishop", "White"));
            pieces.Add(new ChessPiece(new Coord(3, 7), "Queen", "White"));
            pieces.Add(new ChessPiece(new Coord(4, 7), "King", "White"));

            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new ChessPiece(new Coord(i, 1), "Pawn", "Black"));
                pieces.Add(new ChessPiece(new Coord(i, 6), "Pawn", "White"));
            }

            PrintChessBoard(pieces);
        }

        public void PrintChessBoard(List<ChessPiece> pieces)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = 60;
                    rect.Height = 60;
                    rect.Stroke = Brushes.Black;
                    rect.Fill = (i + j) % 2 == 0 ? Brushes.White : Brushes.Gray;

                    Canvas.SetLeft(rect, j * 60);
                    Canvas.SetTop(rect, i * 60);

                    FieldCanvas.Children.Add(rect);
                }
            }

            foreach (ChessPiece piece in pieces)
            {
                string url = string.Empty;

                switch (piece.Type)
                {
                    case "Rook":
                        url = piece.Color == "Black" ? BlackRook : WhiteRook;
                        break;
                    case "Knight":
                        url = piece.Color == "Black" ? BlackKnight : WhiteKnight;
                        break;
                    case "Bishop":
                        url = piece.Color == "Black" ? BlackBishop : WhiteBishop;
                        break;
                    case "Queen":
                        url = piece.Color == "Black" ? BlackQueen : WhiteQueen;
                        break;
                    case "King":
                        url = piece.Color == "Black" ? BlackKing : WhiteKing;
                        break;
                    case "Pawn":
                        url = piece.Color == "Black" ? BlackPawn : WhitePawn;
                        break;

                }

                AddPieceToCell(piece.Coord.Y, piece.Coord.X, url);
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