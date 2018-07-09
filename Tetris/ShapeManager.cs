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
        public static IReadOnlyCollection<Shape> AvailableShapes => _availableShapes;

        private static List<Shape> _availableShapes;
        private static List<Color> _availableColors;

        static ShapeManager()
        {
            InitializeColors();
            InitializeShapes();
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

        private static void InitializeShapes()
        {
            _availableShapes = new List<Shape>();

            //  *
            //* ^ *
            Shape shape1 = new Shape();
            shape1.AddShapeParts(new List<ShapePart> {
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
            });
            _availableShapes.Add(shape1);

            // *
            // *
            // ^ *
            Shape shape2 = new Shape();
            shape2.AddShapeParts(new List<ShapePart> {
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
            });
            _availableShapes.Add(shape2);

            // ^
            // *
            // *
            // *
            Shape shape3 = new Shape();
            shape3.AddShapeParts(new List<ShapePart> {
                new ShapePart
                {
                    IsPivotal = true,
                    X = 0,
                    Y = 0
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
                },
                new ShapePart
                {
                    X = 0,
                    Y = 3
                }
            });
            _availableShapes.Add(shape3);

            // * ^ * 
            // *   *
            Shape shape4 = new Shape();
            shape4.AddShapeParts(new List<ShapePart> {
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
            });
            _availableShapes.Add(shape4);

            //   * *
            //   ^
            // * *
            Shape shape5 = new Shape();
            shape5.AddShapeParts(new List<ShapePart> {
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
            });
            _availableShapes.Add(shape5);

            //   ^ *
            // * *
            Shape shape6 = new Shape();
            shape6.AddShapeParts(new List<ShapePart> {
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
            });
            _availableShapes.Add(shape6);

            // ^ *
            // * *
            Shape shape7 = new Shape();
            shape7.AddShapeParts(new List<ShapePart> {
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
                    X = 1,
                    Y = 1
                }
            });
            _availableShapes.Add(shape7);
        }

        private static Color GetRandomColor()
        {
            int toSkip = Randomizer.GetRandomNumber(0, _availableColors.Count);
            return _availableColors.Skip(toSkip).First();
        }

        public static Shape GetRandomShape()
        {
            var toSkip = Randomizer.GetRandomNumber(0, _availableShapes.Count);
            var randomShape = _availableShapes.Skip(toSkip).First();
            randomShape.Color = GetRandomColor();

            return randomShape;
        }
    }
}
