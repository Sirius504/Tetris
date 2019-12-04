using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;
using Zenject;

namespace Tetris.Model
{
    public class TetraminoFactory : IFactory<TetraminoTypeEnum, Tetramino>
    {
        private struct TetraminoInfo
        {
            public int[,] matrix;
            public Vector2 pivot;
            public CellColorsEnum color;
        }

        // Here tetraminos written in transposed way so tetraminos store correctly
        // and when X and Y values passed as indices you get correct value
        private readonly Dictionary<TetraminoTypeEnum, TetraminoInfo> tetraminoInfos =
            new Dictionary<TetraminoTypeEnum, TetraminoInfo>()
            {
                {
                    TetraminoTypeEnum.I, new TetraminoInfo()
                    {
                        matrix = new int[4, 4]
                        {
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                            {0, 1, 0, 0},
                        },
                        pivot = new Vector2(1.5f, 1.5f),
                        color = CellColorsEnum.Cyan
                    }
                },

                {
                    TetraminoTypeEnum.J, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 1, 0},
                            {0, 1, 0},
                            {0, 1, 0}
                        },
                        pivot = new Vector2(1f, 1f),
                        color = CellColorsEnum.Blue
                    }
                },

                {
                    TetraminoTypeEnum.L, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0,},
                            {0, 1, 0,},
                            {1, 1, 0,},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = CellColorsEnum.Orange
                    }
                },

                {
                    TetraminoTypeEnum.O, new TetraminoInfo()
                    {
                        matrix = new int[4, 3]
                        {
                            {0, 0, 0 },
                            {1, 1, 0 },
                            {1, 1, 0 },
                            {0, 0, 0 },
                        },
                        pivot = new Vector2(1.5f, 0.5f),
                        color = CellColorsEnum.Yellow
                    }
                },

                {
                    TetraminoTypeEnum.S, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0},
                            {1, 1, 0},
                            {1, 0, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = CellColorsEnum.Green
                    }
                },

                {
                    TetraminoTypeEnum.T, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0},
                            {1, 1, 0},
                            {0, 1, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = CellColorsEnum.Magenta
                    }
                },

                {
                    TetraminoTypeEnum.Z, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 0, 0},
                            {1, 1, 0},
                            {0, 1, 0},
                        },
                        pivot = new Vector2(1f, 1f),
                        color = CellColorsEnum.Red
                    }
                },
            };


        public Tetramino Create(TetraminoTypeEnum tetraminoType)
        {
            var tetraminoInfo = tetraminoInfos[tetraminoType];
            return new Tetramino(tetraminoType, tetraminoInfo.matrix, tetraminoInfo.pivot, RotationStateEnum._0, tetraminoInfo.color);
        }
    }
}
