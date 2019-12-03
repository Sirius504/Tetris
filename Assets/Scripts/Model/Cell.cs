using Tetris.Model.Enumerators;

namespace Tetris.Model
{
    public class Mino
    {
        public CellColorsEnum Color { get; }

        public Mino(CellColorsEnum color)
        {
            Color = color;
        }
    }
}