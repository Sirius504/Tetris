using Tetris.Model.Enumerators;

namespace Tetris.Signals
{
    public class TetraminoLetterSignal
    {
        public TetraminoTypeEnum TetraminoType { get; }

        public TetraminoLetterSignal(TetraminoTypeEnum type)
        {
            TetraminoType = type;
        }
    }
}
