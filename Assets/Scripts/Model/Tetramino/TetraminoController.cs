using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class TetraminoController
    {
        public class TetraminoGridData
        {
            public Tetramino Tetramino { get; set; }
            public Vector2Int Position { get; set; }
            public List<Mino> Minos { get; set; }

            public TetraminoGridData(Tetramino tetramino, Vector2Int position)
            {
                Tetramino = tetramino;
                Position = position;
                Minos = new List<Mino>();
            }

            public List<Vector2Int> GetMinosGridPositions()
            {
                var result = new List<Vector2Int>();
                foreach (var relativeMinoCoordinate in Tetramino.GetMinosCoordinates())
                {
                    int gridX = Position.x + Mathf.RoundToInt(relativeMinoCoordinate.x);
                    int gridY = Position.y - Mathf.RoundToInt(relativeMinoCoordinate.y);
                    result.Add(new Vector2Int(gridX, gridY));
                }
                return result;
            }
        }

        private TetraminoGridData currentTetramino;
        private const int SPAWN_ROWS = 2;
        private GameGrid grid;
        
        public event Action OnSpawnFailed;
        public event Action<HashSet<int>> OnTetraminoReleased;


        public TetraminoController(GameGrid grid)
        {
            this.grid = grid;
        }


        public void Spawn(Tetramino newTetramino)
        {
            if (currentTetramino != null)
                throw new InvalidOperationException("Attempt to spawn new tetramino while previous one still in active state.");
            Vector2Int startPosition = GetTetraminoStartPosition(newTetramino);
            var newTetraminoInfo = new TetraminoGridData(newTetramino, startPosition);

            if (ValidateTetramino(newTetraminoInfo, MinoInValidPosition))
                ApplyNewTetramino(newTetraminoInfo);
            else
                OnSpawnFailed?.Invoke();
        }

        public void ApplyGravity()
        {
            if (currentTetramino == null)
                return;

            var newTetraminoPosition = currentTetramino.Position + new Vector2Int(0, -1);
            var newTetramino = new TetraminoGridData(currentTetramino.Tetramino, newTetraminoPosition);
            if (ValidateTetramino(newTetramino, MinoInValidPosition))
                ApplyNewTetramino(newTetramino);
            else
            {
                ReleaseCurrentTetramino();
            }
        }

        public void Shift(Vector2Int shift)
        {
            if (currentTetramino == null)
                return;

            Vector2Int newTetraminoPosition = currentTetramino.Position + shift;
            var newTetramino = new TetraminoGridData(currentTetramino.Tetramino, newTetraminoPosition);
            if (ValidateTetramino(newTetramino, MinoInValidPosition))
                ApplyNewTetramino(newTetramino);
        }

        public void Rotate(RotationDirectionEnum rotation)
        {
            if (currentTetramino == null)
                return;

            var newTetramino = currentTetramino.Tetramino.GetTetraminoRotated(rotation);
            var newTetraminoInfo = new TetraminoGridData(newTetramino, Vector2Int.zero);
            var wallKicks = WallKicksData.Get(currentTetramino.Tetramino.Type, currentTetramino.Tetramino.CurrentRotationState, rotation);
            int wallKickIndex = 0;
            bool valid = false;
            do
            {
                newTetraminoInfo.Position = currentTetramino.Position + wallKicks[wallKickIndex];                
                valid = ValidateTetramino(newTetraminoInfo, MinoInValidPosition);
                wallKickIndex++;
            }
            while (!valid && wallKickIndex < wallKicks.Count);

            if (valid)
                ApplyNewTetramino(newTetraminoInfo);
        }
        
        private HashSet<int> GetLinesOccupiedByTetramino(TetraminoGridData tetramino)
        {
            var result = new HashSet<int>();
            foreach (var minoPosition in tetramino.GetMinosGridPositions())
                result.Add(minoPosition.y);
            return result;
        }

        private void ReleaseCurrentTetramino()
        {
            HashSet<int> linesAffected = GetLinesOccupiedByTetramino(currentTetramino);
            currentTetramino = null;
            OnTetraminoReleased?.Invoke(linesAffected);
        }

        private Vector2Int GetTetraminoStartPosition(Tetramino newTetramino)
        {
            // Tetramino spawns at top center of grid, in two hidden spawn rows, at center,
            // shifted to left if size.x of tetramino's bounding box is uneven
            int startPositionX = grid.Size.x / 2 - (newTetramino.Size.x / 2 + newTetramino.Size.x % 2);
            // Tetraminos processing starts from top left corner of their bounding box
            Vector2Int startPosition = new Vector2Int(startPositionX, grid.Size.y - 1);
            return startPosition;
        }

        private bool ValidateTetramino(TetraminoGridData tetraminoGridInfo, Func<Vector2Int, bool> correctMinoPredicate)
        {
            foreach (var minoPosition in tetraminoGridInfo.GetMinosGridPositions())
            {
                if (!correctMinoPredicate(minoPosition))
                    return false;
            }
            return true;
        }

        private void ApplyNewTetramino(TetraminoGridData newTetramino)
        {
            if (newTetramino == null)
                throw new ArgumentNullException("newTetramino", "New tetramino is null");

            if (newTetramino.Equals(currentTetramino))
                return;

            RemoveTetraminoFromGrid(currentTetramino);
            PutTetraminoInGrid(newTetramino);
            currentTetramino = newTetramino;
        }

        private void PutTetraminoInGrid(TetraminoGridData tetraminoGridInfo)
        {
            if (tetraminoGridInfo == null)
                throw new ArgumentNullException("tetraminoGridInfo", "Can't spawn \"null\" tetramino");

            foreach (var minoPosition in tetraminoGridInfo.GetMinosGridPositions())
            {
                var newMino = grid.CreateMino(minoPosition, tetraminoGridInfo.Tetramino.Color);
                tetraminoGridInfo.Minos.Add(newMino);
            }
        }

        private void RemoveTetraminoFromGrid(TetraminoGridData tetraminoGridData)
        {
            if (tetraminoGridData == null)
                return;

            foreach (var minoPosition in tetraminoGridData.GetMinosGridPositions())
            {
                tetraminoGridData.Minos.Remove(grid.GetCell(minoPosition));
                grid.DeleteMino(minoPosition);
            }

            if (tetraminoGridData.Minos.Count > 0)
                throw new InvalidOperationException();
        }

        private bool MinoInValidPosition(Vector2Int minoXY)
        {
            if (!grid.WithinGrid(minoXY))
                return false;
            if (OverlapsWithFallenBlocks(minoXY))
                return false;
            return true;
        }

        private bool OverlapsWithFallenBlocks(Vector2Int position)
        {
            Mino cellAtPosition = grid.GetCell(position);
            return (cellAtPosition != null && !currentTetramino.Minos.Contains(cellAtPosition));
        }

        private bool AbovePlayfield(Vector2Int position)
        {
            return position.y > grid.Size.y - 1 - SPAWN_ROWS;
        }
    }
}
