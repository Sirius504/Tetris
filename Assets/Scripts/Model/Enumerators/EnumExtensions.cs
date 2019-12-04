using System;

namespace Tetris.Model.Enumerators
{
    public static class EnumExtensions
    {
        public static RotationStateEnum AddRotation(this RotationStateEnum state, RotationDirectionEnum rotation)
        {
            int length = Enum.GetValues(typeof(RotationStateEnum)).Length;
            int newValue = ((int)state + (int)rotation) % length;
            if (newValue < 0)
                newValue += length;
            return (RotationStateEnum)newValue;
        }
    }
}
