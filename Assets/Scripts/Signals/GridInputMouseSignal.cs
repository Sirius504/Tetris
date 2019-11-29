using UnityEngine;

namespace Tetris.Signals
{
    public class GridInputMouseSignal
    {
        public Vector2Int Position { get; }

        public GridInputMouseSignal(Vector2Int position)
        {
            Position = position;
        }
    }
}
