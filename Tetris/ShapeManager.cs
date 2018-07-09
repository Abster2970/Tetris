using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class ShapeManager
    {
        private static List<Color> _availableColors;

        static ShapeManager()
        {
            InitializeColors();
        }

        private static void InitializeColors()
        {
            _availableColors = new List<Color>
            {
                Color.Red,
                Color.BlueViolet,
                Color.DeepSkyBlue,
                Color.Green,
                Color.Orange
            };
        }

        private static List<ShapePart> GetRandomShapeParts()
        {
            int randomShapeNumber = Randomizer.GetRandomNumber(0, 7);

            switch (randomShapeNumber)
            {
                case 0:
                    //  *
                    //* ^ *
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = -1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = -1
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = 0
                        }
                    };
                case 1:
                    // *
                    // *
                    // ^ *
                    return new List<ShapePart>
                    {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = -1
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = -2
                        }
                    };
                case 2:
                    // *
                    // ^
                    // *
                    // *
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = -1
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = 1
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = 2
                        }
                    };
                case 3:
                    // * ^ * 
                    // *   *
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = -1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = -1,
                            Y = 1
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = 1
                        }
                    };
                case 4:
                    //   * *
                    //   ^
                    // * *
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = -1
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = -1
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = 1
                        },
                        new ShapePart
                        {
                            X = -1,
                            Y = 1
                        }
                    };
                case 5:
                    //   ^ *
                    // * *
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            X = 0,
                            Y = 1
                        },
                        new ShapePart
                        {
                            X = -1,
                            Y = 1
                        }
                    };
                case 6:
                    // ^ ^
                    // ^ ^
                    return new List<ShapePart> {
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 0
                        },
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 1,
                            Y = 0
                        },
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 0,
                            Y = 1
                        },
                        new ShapePart
                        {
                            IsPivotal = true,
                            X = 1,
                            Y = 1
                        }
                    };
            }

            return null;
        }

        private static Color GetRandomColor()
        {
            int toSkip = Randomizer.GetRandomNumber(0, _availableColors.Count);
            return _availableColors.Skip(toSkip).First();
        }

        public static Shape GetRandomShape()
        {
            var randomShapeParts = GetRandomShapeParts();

            var randomShape = new Shape();
            randomShape.Id = Randomizer.GetRandomNumber(0, int.MaxValue);
            randomShape.SetShapeParts(randomShapeParts);
            randomShape.Color = GetRandomColor();
            randomShape.X = GameSettings.GameBoardWidth / 2;

            return randomShape;
        }
    }
}