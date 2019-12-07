namespace Tetris.Model.Enumerators
{
    public enum RotationStateEnum
    {
        _0 = 0, // spawn state.
        R  = 1, // state resulting from a clockwise rotation ("right") from spawn.
        _2 = 2, // state resulting from 2 successive rotations in either direction from spawn.
        L  = 3  // state resulting from a counter-clockwise ("left") rotation from spawn.
    }
}
