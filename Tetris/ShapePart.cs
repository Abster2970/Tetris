using System.Drawing;

namespace Tetris
{
    public class ShapePart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPivotal { get; set; }
        public Color Color { get; set; }
    }
}