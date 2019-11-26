using Tetris.Model.Enumerators;

namespace Tetris.Model
{
    public class Cell
    {
        public CellColorsEnum Color { get; }

        public Cell(CellColorsEnum color)
        {
            Color = color;
        }
    }
}