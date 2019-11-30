using UnityEngine;

namespace Tetris.Signals
{
    public class TetraminoShiftSignal
    {
        public Vector2Int Shift { get; }

        public TetraminoShiftSignal(Vector2Int shift)
        {
            Shift = shift;
        }
    } 
}
