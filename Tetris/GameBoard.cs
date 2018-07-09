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

        public bool CheckShapeCoords(Shape shape)
        {
            if (CollideWithWalls(shape)) return false;
            if (CollideWithShapes(shape)) return false;

            return true;
        }

        private bool CollideWithWalls(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                int offsetX = shapePart.X;
                int offsetY = shapePart.Y;

                int x = shape.X + offsetX;
                int y = shape.Y + offsetY;

                if (x < 0 || x >= _width || y >= _height)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollideWithShapes(Shape shape)
        {
            var otherShapes = _shapes.Where(x => x.Id != shape.Id);

            foreach (var otherShape in otherShapes)
            {
                foreach (var otherShapePart in otherShape.ShapeParts)
                {
                    var otherShapePartX = otherShape.X + otherShapePart.X;
                    var otherShapePartY = otherShape.Y + otherShapePart.Y;

                    foreach (var shapePart in shape.ShapeParts)
                    {
                        var shapePartX = shape.X + shapePart.X;
                        var shapePartY = shape.Y + shapePart.Y;

                        if (shapePartX == otherShapePartX && shapePartY == otherShapePartY)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void RemoveFullRows()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    
                }
            }
        }
    }
}
