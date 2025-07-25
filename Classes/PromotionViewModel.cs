namespace ChessBoardWPF.Classes
{
    internal class PromotionViewModel
    {
        public string QueenImageUrl { get; }
        public string RookImageUrl { get; }
        public string BishopImageUrl { get; }
        public string KnightImageUrl { get; }

        public PromotionViewModel(string color)
        {
            QueenImageUrl = PieceImages.GetPieceImageUrl("Queen", color);
            RookImageUrl = PieceImages.GetPieceImageUrl("Rook", color);
            BishopImageUrl = PieceImages.GetPieceImageUrl("Bishop", color);
            KnightImageUrl = PieceImages.GetPieceImageUrl("Knight", color);
        }
    }
}