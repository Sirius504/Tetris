using UnityEngine;

namespace Tetris.Interfaces
{
    public interface ITetrisElement
    {
        bool WasSpawned { get; }

        Vector2 Size { get; }


        ITetrisContainer GetContainer();
    }
}
