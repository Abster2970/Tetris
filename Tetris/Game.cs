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

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                _gameBoard.SpawnShape();
            }

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

            bool validCoords = _gameBoard.CheckShapeCoords(shapeCopy);
            if (validCoords)
            {
                _gameBoard.CurrentShape.UpdateShape(shapeCopy);
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
