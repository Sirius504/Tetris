using System;

namespace Tetris.Exceptions
{
    public class GridGenerationException : Exception
    {
        public GridGenerationException()
        {
        }

        public GridGenerationException(string message) : base(message)
        {
        }
    }
}
