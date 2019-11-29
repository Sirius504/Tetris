using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;
using Zenject;

namespace Tetris.Model
{
    internal class TetraminoFactory : IFactory<TetraminoType, Tetramino>
    {
        private struct TetraminoInfo
        {
            public int[,] matrix;
            public Vector2 pivot;
        }

        private readonly Dictionary<TetraminoType, TetraminoInfo> tetraminoInfos =
            new Dictionary<TetraminoType, TetraminoInfo>()
            {
                {
                    TetraminoType.I, new TetraminoInfo()
                    {
                        matrix = new int[4, 4]
                        {
                            {0, 0, 0, 0},
                            {1, 1, 1, 1},
                            {0, 0, 0, 0},
                            {0, 0, 0, 0},
                        },
                        pivot = new Vector2(1.5f, 1.5f)
                    }
                },

                {
                    TetraminoType.J, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 0, 0},
                            {1, 1, 1},
                            {0, 0, 0}
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                {
                    TetraminoType.L, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 0, 1,},
                            {1, 1, 1,},
                            {0, 0, 0,},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                {
                    TetraminoType.O, new TetraminoInfo()
                    {
                        matrix = new int[3, 4]
                        {
                            {0, 1, 1, 0},
                            {0, 1, 1, 0},
                            {0, 0, 0, 0},
                        },
                        pivot = new Vector2(1.5f, 0.5f)
                    }
                },

                {
                    TetraminoType.S, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 1},
                            {1, 1, 0},
                            {0, 0, 0},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                {
                    TetraminoType.T, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {0, 1, 0},
                            {1, 1, 1},
                            {0, 0, 0},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                {
                    TetraminoType.Z, new TetraminoInfo()
                    {
                        matrix = new int[3, 3]
                        {
                            {1, 1, 0},
                            {0, 1, 1},
                            {0, 0, 0},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },
            };


        public Tetramino Create(TetraminoType tetraminoType)
        {
            var tetraminoInfo = tetraminoInfos[tetraminoType];
            return new Tetramino(tetraminoInfo.matrix, tetraminoInfo.pivot);
        }
    }
}
