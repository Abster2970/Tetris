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
        public List<ShapePart> ShapeParts => _shapeParts;
        public Shape CurrentShape => _currentShape;
        
        private int _width;
        private int _height;
        private int _cellSize;
        private List<ShapePart> _shapeParts;
        private Shape _currentShape;

        public GameBoard(int width, int height, int cellSize)
        {
            _shapeParts = new List<ShapePart>();
            _width = width;
            _height = height;
            _cellSize = cellSize;
        }

        public void SpawnShape()
        {
            if (_currentShape != null)
            {
                _currentShape.SetShapePartsFree();
                _shapeParts.AddRange(_currentShape.ShapeParts);
            }
            _currentShape = ShapeManager.GetRandomShape();
        }

        public CollisionType CheckShapeCollision(Shape shape)
        {
            if (CollideWithSideWalls(shape))
            {
                return CollisionType.SideWall;
            }

            if (CollideWithShapeParts(_currentShape) && CollideWithTop(_currentShape))
            {
                return CollisionType.GameOver;
            }

            if (CollideWithShapeParts(shape) || CollideWithBottom(shape))
            {
                return CollisionType.ShapePartOrBottom;
            }

            return CollisionType.None;
        }

        private bool CollideWithSideWalls(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                int offsetX = shapePart.X;
                int offsetY = shapePart.Y;

                int x = shape.X + offsetX;
                int y = shape.Y + offsetY;

                if (x < 0 || x >= _width)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollideWithBottom(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                var shapePartY = shape.Y + shapePart.Y;

                if (shapePartY >= _height)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollideWithTop(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                var shapePartY = shape.Y + shapePart.Y;

                if (shapePartY < 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CollideWithShapeParts(Shape shape)
        {
            foreach (var otherShapePart in _shapeParts)
            {
                foreach (var shapePart in shape.ShapeParts)
                {
                    var shapePartX = shape.X + shapePart.X;
                    var shapePartY = shape.Y + shapePart.Y;

                    if (shapePartX == otherShapePart.X && shapePartY == otherShapePart.Y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int RemoveFullRows()
        {
            List<int> rowsToRemove = new List<int>();

            for (int i = 0; i < _height; i++)
            {
                int count = 0;

                for (int j = 0; j < _width; j++)
                {
                    int x = j;
                    int y = i;

                    if (_shapeParts.Any(sp => sp.X == x && sp.Y == y))
                    {
                        count++;
                    }
                }

                if (count >= _width)
                {
                    rowsToRemove.Add(i);
                }
            }

            foreach (var row in rowsToRemove)
            {
                _shapeParts.RemoveAll(x => x.Y == row);

                foreach (var affectedShapePart in _shapeParts.Where(x => x.Y < row))
                {
                    affectedShapePart.Y++;
                }
            }

            return rowsToRemove.Count;
        }
    }
}
