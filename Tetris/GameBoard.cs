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
            _currentShape.X = _width / 2;
            var maxY = _currentShape.ShapeParts.Max(x => x.Y);
            _currentShape.Y = -maxY;
        }


        public bool CanMoveDown(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                var shapePartY = shape.Y + shapePart.Y;

                if (shapePartY + 1 >= _height)
                {
                    return false;
                }
            }

            foreach (var otherShapePart in _shapeParts)
            {
                foreach (var shapePart in shape.ShapeParts)
                {
                    var shapePartX = shape.X + shapePart.X;
                    var shapePartY = shape.Y + shapePart.Y;

                    if (shapePartX == otherShapePart.X && shapePartY + 1 == otherShapePart.Y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CanMoveRight(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                int shapePartX = shape.X + shapePart.X;

                if (shapePartX + 1 >= _width)
                {
                    return false;
                }
            }

            foreach (var otherShapePart in _shapeParts)
            {
                foreach (var shapePart in shape.ShapeParts)
                {
                    var shapePartX = shape.X + shapePart.X;
                    var shapePartY = shape.Y + shapePart.Y;

                    if (shapePartX + 1 == otherShapePart.X && shapePartY == otherShapePart.Y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CanMoveLeft(Shape shape)
        {
            foreach (var shapePart in shape.ShapeParts)
            {
                int shapePartX = shape.X + shapePart.X;

                if (shapePartX - 1 < 0)
                {
                    return false;
                }
            }

            foreach (var otherShapePart in _shapeParts)
            {
                foreach (var shapePart in shape.ShapeParts)
                {
                    var shapePartX = shape.X + shapePart.X - 1;
                    var shapePartY = shape.Y + shapePart.Y;

                    if (shapePartX == otherShapePart.X && shapePartY == otherShapePart.Y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CanRotate(Shape shape)
        {
            Shape shapeCopy = new Shape();

            List<ShapePart> shapeParts = new List<ShapePart>();
            foreach (var shapePart in shape.ShapeParts)
            {
                ShapePart shapePartCopy = new ShapePart();
                shapePartCopy.X = shapePart.X;
                shapePartCopy.Y = shapePart.Y;
                shapePartCopy.IsPivotal = shapePart.IsPivotal;
                shapeParts.Add(shapePartCopy);
            }

            shapeCopy.SetShapeParts(shapeParts);
            shapeCopy.X = shape.X;
            shapeCopy.Y = shape.Y;

            shapeCopy.Rotate();

            foreach (var shapePart in shapeCopy.ShapeParts)
            {
                int shapePartX = shape.X + shapePart.X;
                int shapePartY = shape.Y + shapePart.Y;

                if (shapePartX < 0 || shapePartX >= _width || shapePartY >= _height)
                {
                    return false;
                }
            }

            foreach (var otherShapePart in _shapeParts)
            {
                foreach (var shapePart in shapeCopy.ShapeParts)
                {
                    var shapePartX = shape.X + shapePart.X;
                    var shapePartY = shape.Y + shapePart.Y;

                    if (shapePartX == otherShapePart.X && shapePartY == otherShapePart.Y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CollideWithTop(Shape shape)
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

        public bool CollideWithShapeParts(Shape shape)
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

        public void Clear()
        {
            _shapeParts.Clear();
            _currentShape = null;
            SpawnShape();
        }
    }
}
