using ChessBoardWPF.Classes;
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
}