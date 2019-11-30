using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid : GameGrid
    {
        private const int SPAWN_ROWS = 2;
        private Tetramino currentTetramino;
        private Vector2Int currentTetraminoPosition;
        private List<Cell> currentTetraminoCells;

        public event Action OnSpawnFailed;

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

            if (ValidateSpawn(newTetramino, startPosition))
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

            if (ValidateFall(currentTetramino, currentTetraminoPosition))
            {
                DeleteTetraminoCells(currentTetramino, currentTetraminoPosition);
                currentTetraminoPosition = currentTetraminoPosition + new Vector2Int(0, -1);
                CreateTetraminoCells(currentTetramino, currentTetraminoPosition);
            }
            else
            {
                currentTetraminoCells.Clear();
                currentTetraminoPosition = Vector2Int.zero;
                currentTetramino = null;
            }
        }

        public void ShiftTetramino(Vector2Int shift)
        {
            if (currentTetramino == null)
                return;

            if (ValidateShift(currentTetramino, currentTetraminoPosition, shift))
            {
                DeleteTetraminoCells(currentTetramino, currentTetraminoPosition);
                currentTetraminoPosition = currentTetraminoPosition + shift;
                CreateTetraminoCells(currentTetramino, currentTetraminoPosition);
            }
        }

        private bool ValidateShift(Tetramino currentTetramino, Vector2Int currentTetraminoPosition, Vector2Int shift)
        {
            Vector2Int tetraminoNextPosition = currentTetraminoPosition + shift;
            for (int i = 0; i < currentTetramino.Size.x; i++)
                for (int j = 0; j < currentTetramino.Size.y; j++)
                {
                    if (currentTetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = tetraminoNextPosition + new Vector2Int(j, -i);
                        if (OutsideOfGrid(position) || OverlapsWithFallenBlocks(position))
                            return false;
                    }
                }
            return true;
        }

        private bool ValidateFall(Tetramino currentTetramino, Vector2Int currentTetraminoPosition)
        {
            Vector2Int tetraminoNextPosition = currentTetraminoPosition + new Vector2Int(0, -1);
            for (int i = 0; i < currentTetramino.Size.x; i++)
                for (int j = 0; j < currentTetramino.Size.y; j++)
                {
                    if (currentTetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = tetraminoNextPosition + new Vector2Int(j, -i);
                        if (OutsideOfGrid(position) || OverlapsWithFallenBlocks(position))
                            return false;
                    }
                }
            return true;
        }

        private bool ValidateSpawn(Tetramino newTetramino, Vector2Int startPosition)
        {
            for (int i = 0; i < newTetramino.Size.x; i++)
                for (int j = 0; j < newTetramino.Size.y; j++)
                {
                    if (newTetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = startPosition + new Vector2Int(j, -i);
                        if (OverlapsWithFallenBlocks(position))
                            return false;
                    }
                }
            return true;
        }

        private void CreateTetraminoCells(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            currentTetraminoCells = new List<Cell>();
            for (int i = 0; i < tetramino.Size.x; i++)
                for (int j = 0; j < tetramino.Size.y; j++)
                {
                    if (tetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = tetraminoPosition + new Vector2Int(j, -i);
                        currentTetraminoCells.Add(CreateCell(position, tetramino.Color));
                    }
                }
        }

        private void DeleteTetraminoCells(Tetramino tetramino, Vector2Int tetraminoPosition)
        {
            for (int i = 0; i < tetramino.Size.x; i++)
                for (int j = 0; j < tetramino.Size.y; j++)
                {
                    if (tetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = tetraminoPosition + new Vector2Int(j, -i);
                        currentTetraminoCells.Remove(Cells[position.x, position.y]);
                        DeleteCell(position);
                    }
                }
            if (currentTetraminoCells.Count > 0)
                throw new InvalidOperationException();
        }

        private bool OverlapsWithFallenBlocks(Vector2Int position)
        {
            var cellAtPosition = Cells[position.x, position.y];
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
