using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model
{
    public class WallKicksData
    {
        private struct WallKicksInfo
        {
            public RotationStateEnum fromState;
            public List<Vector2Int> clockwiseWallKicks;
            public List<Vector2Int> counterClockwiseWallKicks;
        }

        #region KicksDataJLSTZ
        private static Dictionary<RotationStateEnum, WallKicksInfo> WallKicksJLSTZ =
            new Dictionary<RotationStateEnum, WallKicksInfo>()
            {
                {
                    RotationStateEnum._0, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum._0,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int(-1,  1),
                            new Vector2Int( 0, -2),
                            new Vector2Int(-1, -2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int(0,  0),
                            new Vector2Int(1,  0),
                            new Vector2Int(1,  1),
                            new Vector2Int(0, -2),
                            new Vector2Int(1, -2),
                        }
                    }
                },

                {
                    RotationStateEnum.R, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum.R,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int(0,  0),
                            new Vector2Int(1,  0),
                            new Vector2Int(1, -1),
                            new Vector2Int(0,  2),
                            new Vector2Int(1,  2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int( 1,  0),
                            new Vector2Int( 1, -1),
                            new Vector2Int( 0, -2),
                            new Vector2Int(-1, -2),
                        }
                    }
                },

                {
                    RotationStateEnum._2, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum._2,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int(0,  0),
                            new Vector2Int(1,  0),
                            new Vector2Int(1,  1),
                            new Vector2Int(0, -2),
                            new Vector2Int(1, -2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int(-1,  1),
                            new Vector2Int( 0, -2),
                            new Vector2Int(-1, -2),
                        }
                    }
                },

                {
                    RotationStateEnum.L, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum.L,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int(-1, -1),
                            new Vector2Int( 0,  2),
                            new Vector2Int(-1,  2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int(-1, -1),
                            new Vector2Int( 0,  2),
                            new Vector2Int(-1,  2),
                        }
                    }
                },
            };
        #endregion

        #region KicksDataI
        private static Dictionary<RotationStateEnum, WallKicksInfo> WallKicksI =
            new Dictionary<RotationStateEnum, WallKicksInfo>()
            {
                {
                    RotationStateEnum._0, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum._0,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-2,  0),
                            new Vector2Int( 1,  0),
                            new Vector2Int(-2, -1),
                            new Vector2Int( 1,  2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int( 2,  0),
                            new Vector2Int(-1,  2),
                            new Vector2Int( 2, -1),
                        }
                    }
                },

                {
                    RotationStateEnum.R, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum.R,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int( 2, -0),
                            new Vector2Int(-1,  2),
                            new Vector2Int( 2, -1),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int( 2,  0),
                            new Vector2Int(-1, -0),
                            new Vector2Int( 2,  1),
                            new Vector2Int(-1, -2),
                        }
                    }
                },

                {
                    RotationStateEnum._2, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum._2,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int( 2,  0),
                            new Vector2Int(-1,  0),
                            new Vector2Int( 2,  1),
                            new Vector2Int(-1, -2),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int( 1,  0),
                            new Vector2Int(-2,  0),
                            new Vector2Int( 1, -2),
                            new Vector2Int(-2,  1),
                        }
                    }
                },

                {
                    RotationStateEnum.L, new WallKicksInfo()
                    {
                        fromState = RotationStateEnum.L,
                        clockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int( 0,  0),
                            new Vector2Int( 1,  0),
                            new Vector2Int(-2, -0),
                            new Vector2Int( 1, -2),
                            new Vector2Int(-2,  1),
                        },
                        counterClockwiseWallKicks = new List<Vector2Int>()
                        {
                            new Vector2Int(-0,  0),
                            new Vector2Int(-2,  0),
                            new Vector2Int( 1,  0),
                            new Vector2Int(-2, -1),
                            new Vector2Int( 1,  2),
                        }
                    }
                },
            };
        #endregion


        public static List<Vector2Int> Get(TetraminoTypeEnum type, RotationStateEnum currentState, RotationDirectionEnum direction)
        {
            WallKicksInfo wallKicks;
            switch (type)
            {
                case TetraminoTypeEnum.O:
                    return new List<Vector2Int>() { new Vector2Int(0, 0) };
                case TetraminoTypeEnum.I:
                    wallKicks = WallKicksI[currentState];
                    break;
                case TetraminoTypeEnum.L:
                case TetraminoTypeEnum.J:
                case TetraminoTypeEnum.Z:
                case TetraminoTypeEnum.S:
                case TetraminoTypeEnum.T:
                    wallKicks = WallKicksJLSTZ[currentState];
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (direction)
            {
                case RotationDirectionEnum.Clockwise:
                    return wallKicks.clockwiseWallKicks;
                case RotationDirectionEnum.Counterclockwise:
                    return wallKicks.counterClockwiseWallKicks;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
