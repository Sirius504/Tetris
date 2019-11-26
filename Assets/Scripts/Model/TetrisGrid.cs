#define GRID_DEBUG
using System;
using Tetris.Exceptions;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid
    {
        public Vector2Int Size { get; private set; }
        public Cell[,] Cells { get; }

        public TetrisGrid(Vector2Int size)
        {
            Size = size;
            Cells = GenerateEmptyGrid(size);
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new GridGenerationException("At least one of Size vector components is less or equal zero.");
            var colorValues = (CellColorsEnum[])Enum.GetValues(typeof(CellColorsEnum));
            Cell[,] result = new Cell[size.x, size.y];
#if DEBUG
            for (int j = 0; j < size.y; j++)
                for (int i = 0; i < size.x; i++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        CellColorsEnum color = colorValues[UnityEngine.Random.Range(0, colorValues.Length)];
                        result[i, j] = new Cell(color);
                    }
                }
#endif

            return result;
        }
    }
}