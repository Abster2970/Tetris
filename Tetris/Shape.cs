using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<ShapePart> ShapeParts => _shapeParts;

        private List<ShapePart> _shapeParts;

        public Shape()
        {
            _shapeParts = new List<ShapePart>();
        }

        // * (-1, -1) * (0, -1) * (1, -1)
        // * (-1,  0) ^ (0,  0) * (1,  0)
        // * (-1,  1) * (0,  1) * (1,  1)
        public void Rotate()
        {
            foreach (var shapePart in _shapeParts)
            {
                if (!shapePart.IsPivotal)
                {
                    int x = shapePart.X;
                    int y = shapePart.Y;

                    if (x < 0 && y < 0)
                    {
                        shapePart.X = -x;
                    }
                    if (x == 0 && y < 0)
                    {
                        shapePart.X = -y;
                        shapePart.Y = 0;
                    }
                    if (x > 0 && y < 0)
                    {
                        shapePart.Y = -y;
                    }
                    if (x < 0 && y == 0)
                    {
                        shapePart.X = 0;
                        shapePart.Y = x;
                    }
                    if (x > 0 && y == 0)
                    {
                        shapePart.X = 0;
                        shapePart.Y = x;
                    }
                    if (x < 0 && y > 0)
                    {
                        shapePart.Y = -y;
                    }
                    if (x == 0 && y > 0)
                    {
                        shapePart.X = -y;
                        shapePart.Y = 0;
                    }
                    if (x > 0 && y > 0)
                    {
                        shapePart.X = -x;
                    }
                }
            }
        }

        public void MoveRight()
        {
            X++;
        }

        public void MoveLeft()
        {
            X--;
        }

        public void MoveDown()
        {
            Y++;
        }

        public void SetShapeParts(IEnumerable<ShapePart> shapeParts)
        {
            _shapeParts = new List<ShapePart>(shapeParts);
        }

        public void SetShapePartsFree()
        {
            foreach (var shapePart in ShapeParts)
            {
                shapePart.X = this.X + shapePart.X;
                shapePart.Y = this.Y + shapePart.Y;
            }
        }

        public void SetColor(Color color)
        {
            foreach (var shapePart in _shapeParts)
            {
                shapePart.Color = color;
            }
        }
    }
}
