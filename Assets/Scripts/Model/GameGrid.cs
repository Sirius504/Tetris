using System;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class GameGrid
    {
        public Vector2Int Size { get; }
        private Mino[,] Grid { get; }

        public Action<Vector2Int, Mino> OnMinoAdded;
        public Action<Vector2Int> OnMinoDeleted;

        public GameGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new ArgumentOutOfRangeException("size", size, "At least one of Size vector components is less or equal zero.");
            Size = size;
            Grid = new Mino[size.x, size.y];
        }

        public Mino GetCell(Vector2Int position)
        {
            return GetCell(position.x, position.y);
        }

        public Mino GetCell(int x, int y)
        {
            CheckIfPositionValid(x, y);
            return Grid[x, y];
        }

        public Mino CreateMino(Vector2Int position, MinoColorsEnum color)
        {
            return CreateMino(position.x, position.y, color);
        }

        public Mino CreateMino(int x, int y, MinoColorsEnum color)
        {
            CheckIfPositionValid(x, y);

            if (Grid[x, y] != null)
                throw new ArgumentException($"Existing cell found at passed coordinates ({x}, {y}).");

            var result = new Mino(color);
            Grid[x, y] = result;
            OnMinoAdded?.Invoke(new Vector2Int(x, y), result);
            return result;
        }

        public void PlaceMino(Vector2Int position, Mino mino)
        {
            PlaceMino(position.x, position.y, mino);
        }

        public void PlaceMino(int x, int y, Mino mino)
        {
            CheckIfPositionValid(x, y);

            if (mino == null)
                return;

            if (Grid[x, y] != null)
                throw new ArgumentException($"Existing cell found at passed coordinates ({x}, {y}).");

            Grid[x, y] = mino;
            OnMinoAdded?.Invoke(new Vector2Int(x, y), mino);            
        }

        public void ReplaceMino(Vector2Int position, Mino mino)
        {
            ReplaceMino(position.x, position.y, mino);
        }        

        public void ReplaceMino(int x, int y, Mino mino)
        {
            CheckIfPositionValid(x, y);
                        
            if (mino == null)
            {
                DeleteMino(x, y);
                return;
            }
            if (Grid[x, y] != null)
                DeleteMino(x, y);

            Grid[x, y] = mino;
            OnMinoAdded?.Invoke(new Vector2Int(x, y), mino);
        }

        internal void DeleteMino(Vector2Int position)
        {
            DeleteMino(position.x, position.y);
        }

        internal void DeleteMino(int x, int y)
        {
            CheckIfPositionValid(x, y);

            if (Grid[x, y] == null)
                return;
            Grid[x, y] = null;
            OnMinoDeleted?.Invoke(new Vector2Int(x, y));
        }

        public bool WithinGrid(Vector2Int position)
        {
            return WithinGrid(position.x, position.y);
        }

        public bool WithinGrid(int x, int y)
        {
            return (x >= 0
                && x < Size.x
                && y >= 0
                && y < Size.y);
        }

        private void CheckIfPositionValid(Vector2Int position)
        {
            CheckIfPositionValid(position.x, position.y);
        }

        private void CheckIfPositionValid(int x, int y)
        {
            if (!WithinGrid(x, y))
                throw new ArgumentOutOfRangeException("position", "Passed coordinates are out of grid range.");
        }
    }
}