using System;
using UnityEngine;

namespace Tetris.Model
{
    public class TetrisGrid : GameGrid
    {
        private Tetramino currentTetramino;

        public TetrisGrid(Vector2Int size) : base(size)
        {

        }

        public void SpawnTetramino(Tetramino newTetramino)
        {
            if (currentTetramino != null)
                throw new InvalidOperationException("Attempt to spawn new tetramino while previous one still in active state.");

            
        }
    } 
}
