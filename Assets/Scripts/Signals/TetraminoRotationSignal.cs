using Tetris.Model.Enumerators;

namespace Tetris.Signals
{
    public class TetraminoRotationSignal
    {
        public RotationEnum Rotation { get; }

        public TetraminoRotationSignal(RotationEnum rotation)
        {
            Rotation = rotation;
        }
    } 
}
