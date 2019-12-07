using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class Tetramino
    {
        public TetraminoTypeEnum Type { get; }
        public int[,] Matrix { get; }
        public Vector2Int Size { get; }
        public Vector2 Pivot { get; }
        public RotationStateEnum CurrentRotationState { get; }
        public MinoColorsEnum Color { get; }

        private readonly int[,] clockwiseRotationMatrix =
        {
            { 0, 1},
            {-1, 0}
        };
        private readonly int[,] counterClockwiseRotationMatrix =
        {
            {0, -1},
            {1,  0}
        };

        public Tetramino(TetraminoTypeEnum type, int[,] matrix, Vector2 pivot, RotationStateEnum currentRotation, MinoColorsEnum color)
        {
            Type = type;
            Matrix = matrix;
            Pivot = pivot;
            CurrentRotationState = currentRotation;
            Color = color;
            Size = new Vector2Int(Matrix.GetLength(0), Matrix.GetLength(1));
        }

        public Tetramino GetTetraminoRotated(RotationDirectionEnum rotation)
        {
            int[,] rotationMatrix = GetRotationMatrix(rotation);
            var minosRelativeCoordinates = AddPivot(GetMinosCoordinates(), -Pivot);
            var rotatedCoordintes = new List<Vector2>();
            foreach (var coordinate in minosRelativeCoordinates)
            {
                float newX = rotationMatrix[0, 0] * coordinate.x + rotationMatrix[1, 0] * coordinate.y;
                float newY = rotationMatrix[0, 1] * coordinate.x + rotationMatrix[1, 1] * coordinate.y;
                rotatedCoordintes.Add(new Vector2(newX, newY));
            }
            var newMinosCoordinates = AddPivot(rotatedCoordintes, Pivot);

            var newMatrix = CreateTetraminoMatrix(newMinosCoordinates);
            var newRotation = CurrentRotationState.AddRotation(rotation);
            return new Tetramino(Type, newMatrix, Pivot, newRotation, Color);
        }

        private int[,] GetRotationMatrix(RotationDirectionEnum rotation)
        {
            switch (rotation)
            {
                case RotationDirectionEnum.Clockwise:
                    return clockwiseRotationMatrix;
                case RotationDirectionEnum.Counterclockwise:
                    return counterClockwiseRotationMatrix;
                default:
                    throw new InvalidOperationException();
            }
        }

        private int[,] CreateTetraminoMatrix(List<Vector2> newMinosCoordinates)
        {
            int[,] newMatrix = new int[Matrix.GetLength(0), Matrix.GetLength(1)];

            foreach (var mino in newMinosCoordinates)
                newMatrix[Mathf.RoundToInt(mino.x), Mathf.RoundToInt(mino.y)] = 1;
            return newMatrix;
        }

        public List<Vector2> GetMinosCoordinates()
        {
            List<Vector2> result = new List<Vector2>();
            for (int j = 0; j < Matrix.GetLength(1); j++)
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    if (Matrix[i, j] == 1)
                        result.Add(new Vector2(i, j));
                }
            return result;
        }

        private List<Vector2> AddPivot(List<Vector2> coordinates, Vector2 pivot)
        {
            List<Vector2> result = new List<Vector2>();
            foreach (var coordinate in coordinates)
            {
                result.Add(coordinate + pivot);
            }
            return result;
        }
    }
}
