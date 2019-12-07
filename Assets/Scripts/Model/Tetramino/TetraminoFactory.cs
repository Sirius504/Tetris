using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;
using Zenject;

namespace Tetris.Model
{
    public class TetraminoFactory : IFactory<TetraminoTypeEnum, Tetramino>
    {
        private struct TetraminoData
        {
            public int[,] matrix;
            public Vector2 pivot;
            public MinoColorsEnum color;
        }

        // Here tetraminos written in transposed way so tetraminos store correctly
        // and when X and Y values passed as indices you get correct value
        private readonly Dictionary<TetraminoTypeEnum, TetraminoData> tetraminoDatas =
            new Dictionary<TetraminoTypeEnum, TetraminoData>()
            {
                {
                    TetraminoTypeEnum.I, new TetraminoData()
                    {
                        matrix = new int[4, 4]
                        {
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                        },
                        pivot = new Vector2(1.5f, 1.5f),
                        color = MinoColorsEnum.Cyan
                    }
                },

                {
                    TetraminoTypeEnum.J, new TetraminoData()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 1, 0},
                            {0, 1, 0},
                            {0, 1, 0}
                        },
                        pivot = new Vector2(1f, 1f),
                        color = MinoColorsEnum.Blue
                    }
                },

                {
                    TetraminoTypeEnum.L, new TetraminoData()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0,},
                            {0, 1, 0,},
                            {1, 1, 0,},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = MinoColorsEnum.Orange
                    }
                },

                {
                    TetraminoTypeEnum.O, new TetraminoData()
                    {
                        matrix = new int[4, 3]
                        {
                            {0, 0, 0 },
                            {1, 1, 0 },
                            {1, 1, 0 },
                            {0, 0, 0 },
                        },
                        pivot = new Vector2(1.5f, 0.5f),
                        color = MinoColorsEnum.Yellow
                    }
                },

                {
                    TetraminoTypeEnum.S, new TetraminoData()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0},
                            {1, 1, 0},
                            {1, 0, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = MinoColorsEnum.Green
                    }
                },

                {
                    TetraminoTypeEnum.T, new TetraminoData()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0},
                            {1, 1, 0},
                            {0, 1, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = MinoColorsEnum.Magenta
                    }
                },

                {
                    TetraminoTypeEnum.Z, new TetraminoData()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 0, 0},
                            {1, 1, 0},
                            {0, 1, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = MinoColorsEnum.Red
                    }
                },
            };


        public Tetramino Create(TetraminoTypeEnum tetraminoType)
        {
            var tetraminoData = tetraminoDatas[tetraminoType];
            return new Tetramino(tetraminoType, tetraminoData.matrix, tetraminoData.pivot, RotationStateEnum._0, tetraminoData.color);
        }
    }
}
