using Tetris.Exceptions;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid
    {
        public Vector2Int Size { get; } = new Vector2Int(10, 20); 
        public Cell[,] Cells { get; }

        public TetrisGrid()
        {
            Cells = GenerateEmptyGrid(Size);
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new GridGenerationException("At least one of Size vector components is less or equal zero.");

            return new Cell[size.x, size.y];
        }
    }
}