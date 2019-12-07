using Tetris.Model.Enumerators;

namespace Tetris.Model
{
    public class Mino
    {
        public MinoColorsEnum Color { get; }

        public Mino(MinoColorsEnum color)
        {
            Color = color;
        }
    }
}