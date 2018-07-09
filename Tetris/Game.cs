using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class Game
    {
        private GameForm _gameForm;
        private GameBoard _gameBoard;
        private Timer _gameTimer;
        private int _score;

        public Game(GameForm gameForm)
        {
            _gameForm = gameForm;
            _gameForm.OnGameFormKeyDown += GameForm_KeyDown;
            _gameForm.OnGameFormLoad += GameForm_Load;
            _gameTimer = new Timer();
            _gameTimer.Interval = 500;
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();
        }

        private void UpdateGameState(Shape shape)
        {
            var collision = _gameBoard.CheckShapeCollision(shape);
            if (collision == CollisionType.None)
            {
                _gameBoard.CurrentShape.UpdateShape(shape);
            }

            if (collision == CollisionType.ShapePartOrBottom)
            {
                _gameBoard.SpawnShape();
                int removedRows = _gameBoard.RemoveFullRows();
                if (removedRows > 0)
                {
                    _score += 10;
                    _gameForm.UpdateScore(_score);
                }
            }

            if (collision == CollisionType.GameOver)
            {
                GameOver();
            }

            _gameForm.UpdateGameBoard(_gameBoard);
            _gameForm.UpdateScore(_score);
        }

        private void GameOver()
        {
            _gameTimer.Stop();
            _gameForm.GameOver();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            var shapeCopy = _gameBoard.CurrentShape.Copy();
            shapeCopy.Y++;

            UpdateGameState(shapeCopy);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            var shapeCopy = _gameBoard.CurrentShape.Copy();

            if (e.KeyCode == Keys.A)
            {
                shapeCopy.X--;
            }
            if (e.KeyCode == Keys.S)
            {
                shapeCopy.Y++;
            }
            if (e.KeyCode == Keys.D)
            {
                shapeCopy.X++;
            }
            if (e.KeyCode == Keys.Space)
            {
                shapeCopy.Rotate();
            }

            UpdateGameState(shapeCopy);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            int width = GameSettings.GameBoardWidth;
            int height = GameSettings.GameBoardHeight;
            int cellSize = GameSettings.CellSize;

            _gameBoard = new GameBoard(width, height, cellSize);
            _gameBoard.SpawnShape();
            _gameForm.UpdateGameBoard(_gameBoard);
        }
    }
}
