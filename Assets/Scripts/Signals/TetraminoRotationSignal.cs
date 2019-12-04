using Tetris.Model.Enumerators;

namespace Tetris.Signals
{
    public class TetraminoRotationSignal
    {
        public RotationDirectionEnum Rotation { get; }

        public TetraminoRotationSignal(RotationDirectionEnum rotation)
        {
            Rotation = rotation;
        }
    } 
}
