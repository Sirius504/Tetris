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
            public bool[,] matrix;
            public Vector2 pivot;
        }

        private readonly Dictionary<TetraminoType, TetraminoInfo> tetraminoInfos =
            new Dictionary<TetraminoType, TetraminoInfo>()
            {
                { TetraminoType.I, new TetraminoInfo()
                    {
                        matrix = new bool[4, 4]
                        {
                            {false,  false, false, false},
                            {true,  true, true, true},
                            {false,  false, false, false},
                            {false,  false, false, false},
                        },
                        pivot = new Vector2(1.5f, 1.5f)
                    }
                },

                { TetraminoType.J, new TetraminoInfo()
                    {
                        matrix = new bool[3, 3]
                        {
                            {true,  false, false},
                            {true,  true, true},
                            {false,  false, false}
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                { TetraminoType.L, new TetraminoInfo()
                    {
                        matrix = new bool[3, 3]
                        {
                            {false,  false, true,},
                            {true,  true, true,},
                            {false,  false, false,},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                { TetraminoType.O, new TetraminoInfo()
                    {
                        matrix = new bool[3, 4]
                        {
                            {false,  false, false, false},
                            {false,  false, false, false},
                            {false,  false, false, false},
                        },
                        pivot = new Vector2(1.5f, 0.5f)
                    }
                },

                { TetraminoType.S, new TetraminoInfo()
                    {
                        matrix = new bool[3, 3]
                        {
                            {false,  true, true},
                            {true,  true, false},
                            {false,  false, false},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                { TetraminoType.T, new TetraminoInfo()
                    {
                        matrix = new bool[3, 3]
                        {
                            {false,  true, false},
                            {true,  true, true},
                            {false,  false, false},
                        },
                        pivot = new Vector2(1f, 1f)
                    }
                },

                { TetraminoType.Z, new TetraminoInfo()
                    {
                        matrix = new bool[3, 3]
                        {
                            {true,  true, false},
                            {false,  true, true},
                            {false,  false, false},
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
