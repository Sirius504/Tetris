using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid : GameGrid
    {
        private const int SPAWN_ROWS = 2;
        private Tetramino currentTetramino;
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

            bool spawnSucceded = false;
            spawnSucceded = ValidateSpawn(newTetramino, startPosition);
            if (spawnSucceded)
                SpawnTetraminoCells(startPosition, newTetramino);
            else
                OnSpawnFailed?.Invoke();
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

        private void SpawnTetraminoCells(Vector2Int startPosition, Tetramino tetramino)
        {
            currentTetraminoCells = new List<Cell>();
            for (int i = 0; i < tetramino.Size.x; i++)
                for (int j = 0; j < tetramino.Size.y; j++)
                {
                    if (tetramino.Matrix[i, j] != 0)
                    {
                        Vector2Int position = startPosition + new Vector2Int(j, -i);
                        currentTetraminoCells.Add(CreateCell(position, tetramino.Color));
                    }
                }
        }

        private bool OverlapsWithFallenBlocks(Vector2Int position)
        {
            var cellAtPosition = Cells[position.x, position.y];
            return (cellAtPosition != null /*&& !currentTetraminoCells.Contains(cellAtPosition)*/);
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
