using Tetris.Model.Enumerators;

namespace Tetris.Signals
{
    public class TetraminoLetterSignal
    {
        public TetraminoType TetraminoType { get; }

        public TetraminoLetterSignal(TetraminoType type)
        {
            TetraminoType = type;
        }
    }
}
