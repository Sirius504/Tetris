using System;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class GameGrid
    {
        public Vector2Int Size { get; private set; }
        public Cell[,] Cells { get; }

        public GameGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new ArgumentOutOfRangeException("At least one of Size vector components is less or equal zero.");
            Size = size;
            Cells = GenerateEmptyGrid(size);
        }

        public void CreateCell(Vector2Int position, CellColorsEnum color)
        {
            CheckIfArgumentsFallWithinSize(position);

            if (Cells[position.x, position.y] != null)
                throw new ArgumentException("Existing cell found at passed coordinates.");

            Cells[position.x, position.y] = new Cell(color);
        }

        private void CheckIfArgumentsFallWithinSize(Vector2Int argument)
        {
            if ((argument.x < 0 || argument.x >= Cells.Length)
                && (argument.y < 0 || argument.y >= Cells.Length))
            throw new ArgumentOutOfRangeException("Passed coordinates are out of grid range.");
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            var colorValues = (CellColorsEnum[])Enum.GetValues(typeof(CellColorsEnum));
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