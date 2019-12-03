using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid : GameGrid
    {
        private const int SPAWN_ROWS = 2;
        private Tetramino currentTetramino;
        private Vector2Int currentTetraminoPosition;
        private List<Mino> currentTetraminoCells;

        public event Action OnSpawnFailed;
        public event Action<HashSet<int>> OnTetraminoReleased;
        public event Action OnLineCleared;

        public TetrisGrid(Vector2Int size) : base(size)
        {

        }

        public void SpawnTetramino(Tetramino newTetramino)
        {
            if (currentTetramino != null)
                throw new InvalidOperationException("Attempt to spawn new tetramino while previous one still in active state.");
            // Tetramino spawns at top center of grid, in two hidden spawn rows, at center,
            // shifted to left if size.x of tetramino's bounding box is uneven
            int startPositionX = Size.x / 2 - (newTetramino.Size.x / 2 + newTetramino.Size.x % 2);
            // Tetraminos processing starts from top left corner of their bounding box
            Vector2Int startPosition = new Vector2Int(startPositionX, Size.y - 1);

            if (ValidateTetramino(newTetramino, startPosition, (minoXY) => !OverlapsWithFallenBlocks(minoXY)))
            {
                CreateTetraminoCells(newTetramino, startPosition);
                currentTetramino = newTetramino;
                currentTetraminoPosition = startPosition;
            }
            else
                OnSpawnFailed?.Invoke();
        }

        public void ApplyGravity()
        {
            if (currentTetramino == null)
                return;

            var newTetraminoPosition = currentTetraminoPosition + new Vector2Int(0, -1);
            if (ValidateTetramino(currentTetramino, newTetraminoPosition, MinoInValidPosition))
            {
                DeleteTetraminoCells(currentTetramino, currentTetraminoPosition);
                currentTetraminoPosition = newTetraminoPosition;
                CreateTetraminoCells(currentTetramino, currentTetraminoPosition);
            }
            else
            {
                HashSet<int> linesAffected = GetLinesOccupiedByTetramino(currentTetramino, currentTetraminoPosition);
                var filledLines = GetFilledLines(linesAffected);
                if (filledLines.Count > 0)
                {
                    ClearLines(filledLines);
                    int topLine = filledLines.Max();
                    PushRowsDown(topLine + 1, filledLines.Count);
                }
                ReleaseCurrentTetramino();
                OnTetraminoReleased?.Invoke(linesAffected);
            }
        }

        private void PushRowsDown(int startFrom, int distance)
        {
            for (int j = startFrom; j < Cells.GetLength(1); j++)
                for (int i = 0; i < Cells.GetLength(0); i++)
                {
                    Cells[i, j - distance] = Cells[i, j];
                    Cells[i, j] = null;
                    OnCellDeleted?.Invoke(new Vector2Int(i, j));
                    if (Cells[i, j - distance] != null)
                        OnCellCreated?.Invoke(new Vector2Int(i, j - distance), Cells[i, j - distance]);
                }
        }

        public HashSet<int> GetFilledLines(HashSet<int> linesIndices)
        {
            HashSet<int> result = new HashSet<int>();
            foreach (int j in linesIndices)
            {
                bool filled = true;
                for (int i = 0; i < Cells.GetLength(0); i++)
                    filled &= Cells[i, j] != null;
                if (filled)
                    result.Add(j);
            }
            return result;
        }

        private HashSet<int> GetLinesOccupiedByTetramino(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            var result = new HashSet<int>();
            foreach (var minoPosition in GetMinosPositionsInGrid(tetramino, tetraminoPosition))
                result.Add(minoPosition.y);
            return result;
        }

        private void ReleaseCurrentTetramino()
        {
            currentTetraminoCells.Clear();
            currentTetraminoPosition = Vector2Int.zero;
            currentTetramino = null;
        }

        private void ClearLines(HashSet<int> linesFilled)
        {
            foreach (int j in linesFilled)
                for (int i = 0; i < Cells.GetLength(0); i++)
                    DeleteCell(new Vector2Int(i, j));
        }

        public void ShiftTetramino(Vector2Int shift)
        {
            if (currentTetramino == null)
                return;

            Vector2Int newTetraminoPosition = currentTetraminoPosition + shift;
            if (ValidateTetramino(currentTetramino, newTetraminoPosition, MinoInValidPosition))
            {
                DeleteTetraminoCells(currentTetramino, currentTetraminoPosition);
                currentTetraminoPosition = newTetraminoPosition;
                CreateTetraminoCells(currentTetramino, currentTetraminoPosition);
            }
        }

        public void RotateTetramino(RotationEnum rotation)
        {
            if (currentTetramino == null)
                return;

            var newTetramino = currentTetramino.GetTetraminoRotated(rotation);
            if (ValidateTetramino(newTetramino, currentTetraminoPosition, MinoInValidPosition))
            {
                DeleteTetraminoCells(currentTetramino, currentTetraminoPosition);
                currentTetramino = newTetramino;
                CreateTetraminoCells(currentTetramino, currentTetraminoPosition);
            }
        }

        private bool ValidateTetramino(Tetramino tetramino, Vector2Int tetraminoPosition, Func<Vector2Int, bool> correctMinoPredicate)
        {
            foreach (var minoPosition in GetMinosPositionsInGrid(tetramino, tetraminoPosition))
            {
                if (!correctMinoPredicate(minoPosition))
                    return false;
            }
            return true;
        }

        private void CreateTetraminoCells(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            currentTetraminoCells = new List<Mino>();
            foreach (var minoPosition in GetMinosPositionsInGrid(tetramino, tetraminoPosition))
                currentTetraminoCells.Add(CreateCell(minoPosition, tetramino.Color));
        }

        private void DeleteTetraminoCells(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            foreach (var minoPosition in GetMinosPositionsInGrid(tetramino, tetraminoPosition))
            {
                currentTetraminoCells.Remove(Cells[minoPosition.x, minoPosition.y]);
                DeleteCell(minoPosition);
            }

            if (currentTetraminoCells.Count > 0)
                throw new InvalidOperationException();
        }

        public List<Vector2Int> GetMinosPositionsInGrid(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            var result = new List<Vector2Int>();
            for (int j = 0; j < tetramino.Size.y; j++)
                for (int i = 0; i < tetramino.Size.x; i++)
                {
                    if (tetramino.Matrix[i, j] != 0)
                    {
                        result.Add(tetraminoPosition + new Vector2Int(i, -j));
                    }
                }
            return result;
        }

        private bool MinoInValidPosition(Vector2Int minoXY)
        {
            if (OutsideOfGrid(minoXY))
                return false;
            if (OverlapsWithFallenBlocks(minoXY))
                return false;
            return true;
        }

        private bool OverlapsWithFallenBlocks(Vector2Int position)
        {
            Mino cellAtPosition = null;
            cellAtPosition = Cells[position.x, position.y];
            return (cellAtPosition != null && !currentTetraminoCells.Contains(cellAtPosition));
        }

        private bool OutsideOfGrid(Vector2Int position)
        {
            return position.x < 0
                || position.x >= Size.x
                || position.y < 0
                || position.y >= Size.y;
        }

        private bool AbovePlayfield(Vector2Int position)
        {
            return position.y > Size.y - 1 - SPAWN_ROWS;
        }
    }
}
