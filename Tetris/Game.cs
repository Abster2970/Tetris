﻿using System;
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
            _gameForm.OnNewGameClick += GameForm_OnNewGameClick;
            _gameTimer = new Timer();
            _gameTimer.Interval = 700;
            _gameTimer.Tick += GameTimer_Tick;
        }

        private void GameForm_OnNewGameClick(object sender, EventArgs e)
        {
            _score = 0;
            _gameBoard.Clear();
            _gameTimer.Start();
            GameTimer_Tick(sender, e);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            var shape = _gameBoard.CurrentShape;
            TryMoveDown(shape);

            UpdateGameState();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            var shape = _gameBoard.CurrentShape;
            
            switch (e.KeyCode)
            {
                case Keys.A:
                    TryMoveLeft(shape);
                    break;
                case Keys.S:
                    TryMoveDown(shape);
                    break;
                case Keys.D:
                    TryMoveRight(shape);
                    break;
                case Keys.Space:
                    TryRotate(shape);
                    break;
                default:
                    return;
            }

            UpdateGameState();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            int width = GameSettings.GameBoardWidth;
            int height = GameSettings.GameBoardHeight;

            _gameBoard = new GameBoard(width, height);
            _gameForm.UpdateGameBoard(_gameBoard);
            ScoreManager.Load();
        }

        private void TryRotate(Shape shape)
        {
            if (_gameBoard.CanRotate(shape))
                shape.Rotate();
        }

        private void TryMoveRight(Shape shape)
        {
            if (_gameBoard.CanMoveRight(shape))
                shape.X++;
        }

        private void TryMoveDown(Shape shape)
        {
            if (_gameBoard.CanMoveDown(shape))
            {
                shape.Y++;
            }
            else
            {
                SpawnShape();
                RemoveFullRows();
            }
        }

        private void TryMoveLeft(Shape shape)
        {
            if (_gameBoard.CanMoveLeft(shape))
                shape.X--;
        }
        
        private void UpdateGameState()
        {
            var shape = _gameBoard.CurrentShape;

            _gameForm.UpdateGameBoard(_gameBoard);
            _gameForm.UpdateScore(_score);

            if (_gameBoard.CollideWithTop(shape) && !_gameBoard.CanMoveDown(shape) ||
                _gameBoard.CollideWithShapeParts(shape))
            {
                GameOver();
            }
        }

        private void SpawnShape()
        {
            _gameBoard.SpawnShape();
        }

        private void RemoveFullRows()
        {
            int removedRows = _gameBoard.RemoveFullRows();
            if (removedRows > 0)
            {
                _score += (int)Math.Pow(10, removedRows);
            }
        }

        private void GameOver()
        {
            _gameTimer.Stop();
            _gameForm.GameOver();
            ScoreManager.AddScore(_score);
            ScoreManager.Save();
        }
    }
}
