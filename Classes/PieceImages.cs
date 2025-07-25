using System;
using System.IO;

namespace ChessBoardWPF.Classes
{
    internal class PieceImages
    {
        private const string AssetsFolder = "Assets/Pieces/";

        public static string GetPieceImageUrl(string type, string color)
        {
            string fileName = $"{color}{type}.png";
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssetsFolder, fileName);
        }
    }
}