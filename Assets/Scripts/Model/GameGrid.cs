using System;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class GameGrid
    {
        public Vector2Int Size { get; }
        public Cell[,] Cells { get; }

        public GameGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new ArgumentOutOfRangeException("size", size, "At least one of Size vector components is less or equal zero.");
            Size = size;
            Cells = GenerateEmptyGrid(size);
        }

        public Cell CreateCell(Vector2Int position, CellColorsEnum color)
        {
            CheckIfArgumentsFallWithinSize(position);

            if (Cells[position.x, position.y] != null)
                throw new ArgumentException($"Existing cell found at passed coordinates {position}.");

            var result = new Cell(color);
            Cells[position.x, position.y] = result; 
            return result;
        }

        private void CheckIfArgumentsFallWithinSize(Vector2Int argument)
        {
            if ((argument.x < 0 || argument.x >= Cells.GetLength(0))
                || (argument.y < 0 || argument.y >= Cells.GetLength(1)))
                throw new ArgumentOutOfRangeException("argument", argument, "Passed coordinates are out of grid range.");
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            Cell[,] result = new Cell[size.x, size.y];
            return result;
        }

        internal void DeleteCell(Vector2Int position)
        {
            CheckIfArgumentsFallWithinSize(position);

            if (Cells[position.x, position.y] == null)
                return;
            Cells[position.x, position.y] = null;
        }
    }
}