using Tetris.Model;
using UnityEngine;

namespace Tetris.Interfaces
{
    public interface ITetrisContainer
    {
        Vector2Int Size { get; }

        Cell[,] Cells { get; }        

        void SpawnElement(ITetrisElement element, Vector2Int position);
    }
}