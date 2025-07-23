using System.Windows;
using System.Windows.Controls;

namespace ChessBoardWPF
{
    public partial class PromotionDialog : Window
    {
        public string SelectedPieceType { get; private set; }

        public PromotionDialog(string color)
        {
            InitializeComponent();
            DataContext = new PromotionViewModel(color);
        }

        private void PieceButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedPieceType = (sender as Button)?.Tag.ToString();
            DialogResult = true;
            Close();
        }
    }

    public class PromotionViewModel
    {
        #region constants
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

        public string QueenImageUrl { get; }
        public string RookImageUrl { get; }
        public string BishopImageUrl { get; }
        public string KnightImageUrl { get; }

        public PromotionViewModel(string color)
        {
            if (color == "White")
            {
                QueenImageUrl = WhiteQueen;
                RookImageUrl = WhiteRook;
                BishopImageUrl = WhiteBishop;
                KnightImageUrl = WhiteKnight;
            }
            else
            {
                QueenImageUrl = BlackQueen;
                RookImageUrl = BlackRook;
                BishopImageUrl = BlackBishop;
                KnightImageUrl = BlackKnight;
            }
        }
    }
}