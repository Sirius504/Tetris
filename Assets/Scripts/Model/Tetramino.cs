using UnityEngine;

namespace Tetris.Model
{
    public class Tetramino
    {
        private int[,] matrix;
        private Vector2 pivot;

        public Tetramino(int[,] matrix, Vector2 pivot)
        {
            this.matrix = matrix;
            this.pivot = pivot;
        }
    }
}