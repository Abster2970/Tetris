using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameBoard
    {
        public int Width => _width;
        public int Height => _height;
        public int CellSize => _cellSize;
        public List<Shape> Shapes => _shapes;
        public Shape CurrentShape => Shapes.Last();
        
        private int _width;
        private int _height;
        private int _cellSize;
        private List<Shape> _shapes;

        public GameBoard(int width, int height, int cellSize)
        {
            _shapes = new List<Shape>();
            _width = width;
            _height = height;
            _cellSize = cellSize;
        }

        public void SpawnShape()
        {
            var newShape = ShapeManager.GetRandomShape();
            _shapes.Add(newShape);
        }
    }
}
