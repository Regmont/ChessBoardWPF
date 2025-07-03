using ChessBoardWPF.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ChessBoardWPF
{
    public partial class MainWindow : Window
    {
        List<ChessPiece> pieces;
        bool inCheck;

        public MainWindow()
        {
            InitializeComponent();

            pieces = new List<ChessPiece>();
            inCheck = false;

            var handler = new ChessBoardHandler(FieldCanvas, pieces);
            handler.SetChessBoard();

            ChessPiece currentPiece = pieces.Where(x => x.Coord.X == 0 && x.Coord.Y == 1).FirstOrDefault();

            if (currentPiece != null)
            {
                MovePiece(currentPiece, handler, new Coord(0, 3));
            }
        }

        private void MovePiece(ChessPiece piece, ChessBoardHandler handler, Coord newPos)
        {
            if (!inCheck)
            {
                //проверка на съедание фигуры
                pieces.Where(x => x == piece).First().Coord = newPos;
            }

            handler.PrintChessBoard(pieces);
        }
    }
}