using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tetris.Model
{
    public class FilledLinesCleaner 
    {
        private readonly GameGrid grid;
        private readonly TetraminoController tetraminoController;

        public event Action OnLineCleared;

        public FilledLinesCleaner(GameGrid grid, TetraminoController tetraminoController)
        {
            this.grid = grid;
            this.tetraminoController = tetraminoController;
            tetraminoController.OnTetraminoReleased += ClearLinesIfFilled;
        }

        private void ClearLinesIfFilled(HashSet<int> lines)
        {
            var filledLines = GetFilledLines(lines);
            if (filledLines.Count > 0)
            {
                ClearLines(filledLines);
                int topLine = filledLines.Max();
                PushRowsDown(topLine + 1, filledLines.Count);
            }
        }

        private void PushRowsDown(int startFrom, int distance)
        {
            for (int j = startFrom; j < grid.Size.y; j++)
                for (int i = 0; i < grid.Size.x; i++)
                {
                    Vector2Int from = new Vector2Int(i, j);
                    Vector2Int to = new Vector2Int(i, j - distance);
                    grid.ReplaceMino(to, grid.GetCell(from));
                    grid.DeleteMino(from);
                }
        }

        public HashSet<int> GetFilledLines(HashSet<int> linesIndices)
        {
            HashSet<int> result = new HashSet<int>();
            foreach (int j in linesIndices)
            {
                bool filled = true;
                for (int i = 0; i < grid.Size.x; i++)
                    filled &= grid.GetCell(i, j) != null;
                if (filled)
                    result.Add(j);
            }
            return result;
        }


        private void ClearLines(HashSet<int> linesFilled)
        {
            foreach (int j in linesFilled)
            {
                for (int i = 0; i < grid.Size.x; i++)
                    grid.DeleteMino(i, j);
                OnLineCleared?.Invoke();
            }
        }
    }
}
