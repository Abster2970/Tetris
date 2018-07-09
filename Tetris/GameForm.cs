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

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, gameBoardPanel, new object[] { true });
        }

        public void UpdateGameBoard(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;

            gameBoardPanel.Width = _gameBoard.Width * _gameBoard.CellSize + 3;
            gameBoardPanel.Height = _gameBoard.Height * _gameBoard.CellSize + 3;
            gameBoardPanel.Invalidate();
        }

        private void GameBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_gameBoard != null)
                DrawGameBoard(e);
            DrawGrid(e);
        }

        private void DrawGameBoard(PaintEventArgs e)
        {
            foreach (var shape in _gameBoard.Shapes)
            {
                int cellSize = _gameBoard.CellSize;
                int shapeX = shape.X * cellSize;
                int shapeY = shape.Y * cellSize;

                foreach (var shapePart in shape.ShapeParts)
                {
                    int offsetX = shapePart.X * cellSize;
                    int offsetY = shapePart.Y * cellSize;

                    int shapePartX = shapeX + offsetX;
                    int shapePartY = shapeY + offsetY;
                    
                    e.Graphics.FillRectangle(new SolidBrush(shape.Color), shapePartX, shapePartY, cellSize, cellSize);
                }
            }
        }

        public void DrawGrid(PaintEventArgs e)
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
    }
}