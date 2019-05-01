using System;
using Tetris.Exceptions;
using Tetris.Interfaces;
using UnityEngine;

namespace Tetris.Model
{
    [CreateAssetMenu(fileName = "NewGrid", menuName = "Tetris/Grid")]
    public class TetrisGridModel : ScriptableObject, ITetrisContainer
    {
        public Vector2Int Size
        {
            get
            {
                return size;
            }
        }

        public Cell[,] Cells
        {
            get
            {
                return cells;
            }
        }

        [SerializeField]
        private Vector2Int size;
        [SerializeField]
        private Cell[,] cells;

        public void InitGrid(Vector2Int size)
        {
            this.size = size;
            cells = GenerateEmptyGrid(size);
        }

        private Cell[,] GenerateEmptyGrid(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new GridGenerationException("At least one of Size vector coordinates are less or equal zero.");

            return new Cell[size.x, size.y];
        }

        public void SpawnElement(ITetrisElement element, Vector2Int position)
        {
            throw new NotImplementedException();
        }
    }
}