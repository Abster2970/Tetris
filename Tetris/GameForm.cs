﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GameForm : Form
    {
        private GameBoard _gameBoard;

        public GameForm()
        {
            InitializeComponent();
            
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gameBoardPanel.Paint += GameBoardPanel_Paint;
            gameBoardPanel.BorderStyle = BorderStyle.FixedSingle;
            gameBoardPanel.BackColor = Color.White;

            shapePreviewPanel.Width = GameSettings.PreviewWidth * GameSettings.CellSize;
            shapePreviewPanel.Height = GameSettings.PreviewHeight * GameSettings.CellSize;
            shapePreviewPanel.BackColor = Color.Bisque;
            shapePreviewPanel.Paint += ShapePreviewPanel_Paint;

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, gameBoardPanel, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, shapePreviewPanel, new object[] { true });
        }

        public void UpdateGameBoard(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;

            gameBoardPanel.Width = _gameBoard.Width * _gameBoard.CellSize + 3;
            gameBoardPanel.Height = _gameBoard.Height * _gameBoard.CellSize + 3;
            gameBoardPanel.Invalidate();
            shapePreviewPanel.Invalidate();
        }

        public void UpdateScore(int score)
        {
            scoreLabel.Text = score.ToString();
        }

        private void GameBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_gameBoard != null)
            {
                DrawGameBoard(e);
            }
            DrawGrid(e);
        }

        private void ShapePreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_gameBoard != null)
            {
                DrawPreview(e);
            }
            DrawPreviewGrid(e);
        }

        private void DrawGameBoard(PaintEventArgs e)
        {
            int cellSize = _gameBoard.CellSize;

            if (_gameBoard.ShapeParts != null)
            {
                foreach (var shapePart in _gameBoard.ShapeParts)
                {
                    int shapePartX = shapePart.X * cellSize;
                    int shapePartY = shapePart.Y * cellSize;

                    e.Graphics.FillRectangle(new SolidBrush(shapePart.Color), shapePartX, shapePartY, cellSize, cellSize);
                }
            }

            var currentShape = _gameBoard.CurrentShape;
            if (currentShape != null)
            {
                int shapeX = currentShape.X * cellSize;
                int shapeY = currentShape.Y * cellSize;

                foreach (var shapePart in currentShape.ShapeParts)
                {
                    int offsetX = shapePart.X * cellSize;
                    int offsetY = shapePart.Y * cellSize;

                    int shapePartX = shapeX + offsetX;
                    int shapePartY = shapeY + offsetY;

                    e.Graphics.FillRectangle(new SolidBrush(shapePart.Color), shapePartX, shapePartY, cellSize, cellSize);
                }
            }
        }

        private void DrawPreview(PaintEventArgs e)
        {
            int cellSize = _gameBoard.CellSize;

            var currentShape = _gameBoard.CurrentShape;
            if (currentShape != null)
            {
                int shapeX = GameSettings.PreviewWidth / 2 * cellSize;
                int shapeY = GameSettings.PreviewHeight / 2 * cellSize;

                foreach (var shapePart in currentShape.ShapeParts)
                {
                    int offsetX = shapePart.X * cellSize;
                    int offsetY = shapePart.Y * cellSize;

                    int shapePartX = shapeX + offsetX;
                    int shapePartY = shapeY + offsetY;

                    e.Graphics.FillRectangle(new SolidBrush(shapePart.Color), shapePartX, shapePartY, cellSize, cellSize);
                }
            }
        }

        private void DrawGrid(PaintEventArgs e)
        {
            for (int i = 0; i < _gameBoard.Height; i++)
            {
                for (int j = 0; j < _gameBoard.Width; j++)
                {
                    int cellSize = _gameBoard.CellSize;
                    int x = j * cellSize;
                    int y = i * cellSize;

                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 0, 0, 0), 1),
                        new Rectangle(x, y, cellSize, cellSize));
                }
            }
        }

        private void DrawPreviewGrid(PaintEventArgs e)
        {
            for (int i = 0; i < GameSettings.PreviewHeight; i++)
            {
                for (int j = 0; j < GameSettings.PreviewWidth; j++)
                {
                    int cellSize = GameSettings.CellSize;
                    int x = j * cellSize;
                    int y = i * cellSize;

                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 0, 0, 0), 1),
                        new Rectangle(x, y, cellSize, cellSize));
                }
            }
        }

        public event EventHandler<KeyEventArgs> OnGameFormKeyDown;
        public event EventHandler OnGameFormLoad;

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            OnGameFormKeyDown(sender, e);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            OnGameFormLoad(sender, e);
        }

        public void GameOver()
        {
            var result = MessageBox.Show("Game Over");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                // Closes the parent form.

                this.Close();

            }
        }
    }
}
