using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class Tetramino
    {
        public int[,] Matrix { get; }
        public Vector2Int Size { get; }
        public Vector2 Pivot { get; }
        public CellColorsEnum Color { get; }

        public Tetramino(int[,] matrix, Vector2 pivot, CellColorsEnum color)
        {
            Matrix = matrix;
            Pivot = pivot;
            Color = color;
            Size = new Vector2Int(Matrix.GetLength(0), Matrix.GetLength(1));
        }
    }
}