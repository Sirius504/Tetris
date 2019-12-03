using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class Tetramino
    {
        public int[,] Matrix { get; }
        public Vector2Int Size { get; }
        public Vector2 Pivot { get; }
        public CellColorsEnum Color { get; }

        private readonly int[,] clockwiseRotationMatrix =
        {
            {0,  1},
            {-1, 0}
        };
        private readonly int[,] counterClockwiseRotationMatrix =
        {
            {0, -1},
            {1,  0}
        };

        public Tetramino(int[,] matrix, Vector2 pivot, CellColorsEnum color)
        {
            Matrix = matrix;
            Pivot = pivot;
            Color = color;
            Size = new Vector2Int(Matrix.GetLength(0), Matrix.GetLength(1));
        }

        public Tetramino GetTetraminoRotated(RotationEnum rotation)
        {
            switch (rotation)
            {
                case RotationEnum.Clockwise:
                    return GetRotatedTetramino(clockwiseRotationMatrix);
                case RotationEnum.Counterclockwise:
                    return GetRotatedTetramino(counterClockwiseRotationMatrix);
                default:
                    throw new InvalidOperationException();
            }
        }

        private Tetramino GetRotatedTetramino(int[,] rotationMatrix)
        {
            var coordinates = GetMinosCoordinates();
            var relativeCoordinates = AddPivot(coordinates, -Pivot);
            var rotatedCoordintes = new List<Vector2>();
            foreach (var coordinate in relativeCoordinates)
            {
                float newX = rotationMatrix[0, 0] * coordinate.x + rotationMatrix[1, 0] * coordinate.y;
                float newY = rotationMatrix[0, 1] * coordinate.x + rotationMatrix[1, 1] * coordinate.y;
                rotatedCoordintes.Add(new Vector2(newX, newY));
            }
            var newMinosCoordinates = AddPivot(rotatedCoordintes, Pivot);

            var newMatrix = CreateTetraminoMatrix(newMinosCoordinates);
            return new Tetramino(newMatrix, Pivot, Color);
        }

        private int[,] CreateTetraminoMatrix(List<Vector2> newMinosCoordinates)
        {
            int[,] newMatrix = new int[Matrix.GetLength(0), Matrix.GetLength(1)];

            foreach (var mino in newMinosCoordinates)
                newMatrix[Mathf.RoundToInt(mino.x), Mathf.RoundToInt(mino.y)] = 1;
            return newMatrix;
        }

        private List<Vector2> GetMinosCoordinates()
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