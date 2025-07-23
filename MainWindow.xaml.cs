using ChessBoardWPF.Classes;
using ChessBoardWPF.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ChessBoardWPF
{
    public partial class MainWindow : Window
    {
        ChessBoardHandler handler;
        List<ChessPiece> pieces;
        bool pieceSelected;
        string currentPlayerColor;
        ChessPiece selectedPiece;
        Coord EnPassantCoord;

        public MainWindow()
        {
            InitializeComponent();

            pieces = new List<ChessPiece>();

            handler = new ChessBoardHandler(FieldCanvas, pieces);
            handler.SetChessBoard();

            pieceSelected = false;
            currentPlayerColor = "White";
            EnPassantCoord = null;
        }

        private ChessPiece SelectPiece(Coord selectedCoord)
        {
            ChessPiece selectedPiece = pieces.FirstOrDefault(x => x.Coord.Equals(selectedCoord));

            if (selectedPiece != null && selectedPiece.Color.Equals(currentPlayerColor) && selectedPiece.GetPossibleMoves(pieces, EnPassantCoord).Count() > 0)
            {
                pieceSelected = true;
                return selectedPiece;
            }

            return null;
        }

        private bool MovePiece(ChessPiece piece, Coord newPos)
        {
            if (piece.GetPossibleMoves(pieces, EnPassantCoord).Contains(newPos))
            {
                ChessPiece pieceToRemove = pieces.FirstOrDefault(x => x.Coord.Equals(newPos));

                if (pieceToRemove != null)
                {
                    pieces.Remove(pieceToRemove);
                }
                else if (piece is Pawn && !piece.Coord.X.Equals(newPos.X))
                {
                    if (piece.Color.Equals("White"))
                        pieceToRemove = pieces.First(x => x.Coord.Equals(new Coord(newPos.X, newPos.Y + 1)));
                    else
                        pieceToRemove = pieces.First(x => x.Coord.Equals(new Coord(newPos.X, newPos.Y - 1)));

                    pieces.Remove(pieceToRemove);
                }

                piece.Coord = newPos;
                pieceSelected = false;
                EnPassantCoord = null;

                return true;
            }

            return false;
        }

        private void FieldCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                pieceSelected = false;
                selectedPiece = null;
                handler.PrintChessBoard(pieces, null, null);

                return;
            }

            Point position = e.GetPosition(handler.FieldCanvas);
            Coord selectedCoord = Coord.PointToCoord(position, (int)(FieldCanvas.ActualWidth / 8));

            if (pieceSelected)
            {
                Coord previousCoord = new Coord(selectedPiece.Coord);

                if (MovePiece(selectedPiece, selectedCoord))
                {
                    if (selectedPiece.AvailableForEnPassant(previousCoord))
                    {
                        EnPassantCoord = new Coord(previousCoord.X, (previousCoord.Y + selectedPiece.Coord.Y) / 2);
                    }

                    if (selectedPiece is Pawn && (selectedPiece.Coord.Y == 0 || selectedPiece.Coord.Y == 7))
                        PromotePawn((Pawn)selectedPiece);

                    handler.PrintChessBoard(pieces, null, EnPassantCoord);
                    currentPlayerColor = currentPlayerColor.Equals("White") ? "Black" : "White";
                }
            }
            else
            {
                selectedPiece = SelectPiece(selectedCoord);

                if (selectedPiece != null)
                    handler.PrintChessBoard(pieces, selectedPiece, EnPassantCoord);
            }
        }

        private void PromotePawn(Pawn pawn)
        {
            var dialog = new PromotionDialog(pawn.Color);
            if (dialog.ShowDialog() == true)
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