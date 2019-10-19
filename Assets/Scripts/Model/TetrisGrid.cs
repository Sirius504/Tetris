using Tetris.Exceptions;
using UnityEngine;
using Zenject;

namespace Tetris.Model
{
    public class TetrisGrid
    {
        public Vector2Int Size { get; }
        public Cell[,] Cells { get; }

        [Inject]
        public TetrisGrid(Vector2Int size)
        {
            this.Size = size;
            Cells = GenerateEmptyGrid(size);
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new GridGenerationException("At least one of Size vector components is less or equal zero.");

            return new Cell[size.x, size.y];
        }
    }
}