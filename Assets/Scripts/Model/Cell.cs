using UnityEngine;

namespace Tetris.Model
{
    public class Cell
    {
        [SerializeField]
        private Color color;

        public Cell(Color color)
        {
            this.color = color;
        }
    }
}