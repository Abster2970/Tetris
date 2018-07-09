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

        public Game(GameForm gameForm)
        {
            _gameForm = gameForm;
            _gameForm.OnGameFormKeyDown += GameForm_KeyDown;
            _gameForm.OnGameFormLoad += GameForm_Load;
        }

        public void UpdateGameBoard()
        {
            _gameForm.UpdateGameBoard(_gameBoard);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                _gameBoard.CurrentShape.Y--;
            }
            if (e.KeyCode == Keys.A)
            {
                _gameBoard.CurrentShape.X--;
            }
            if (e.KeyCode == Keys.S)
            {
                _gameBoard.CurrentShape.Y++;
            }
            if (e.KeyCode == Keys.D)
            {
                _gameBoard.CurrentShape.X++;
            }
            if (e.KeyCode == Keys.Space)
            {
                _gameBoard.CurrentShape.Rotate();
            }
            if (e.KeyCode == Keys.R)
            {
                _gameBoard.SpawnShape();
            }
            _gameForm.UpdateGameBoard(_gameBoard);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            int width = GameSettings.GameBoardWidth;
            int height = GameSettings.GameBoardHeight;
            int cellSize = GameSettings.CellSize;

            _gameBoard = new GameBoard(width, height, cellSize);
            _gameForm.UpdateGameBoard(_gameBoard);
        }
    }
}
