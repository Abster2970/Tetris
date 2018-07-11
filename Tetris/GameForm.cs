using System;
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
            this.KeyPreview = true;

            gameBoardPanel.Paint += GameBoardPanel_Paint;
            gameBoardPanel.Width = GameSettings.GameBoardWidth * GameSettings.CellSize + 3;
            gameBoardPanel.Height = GameSettings.GameBoardHeight * GameSettings.CellSize + 3;
            gameBoardPanel.BorderStyle = BorderStyle.FixedSingle;
            gameBoardPanel.BackColor = Color.FromArgb(36, 36, 38);
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, gameBoardPanel, new object[] { true });

            nextShapePreviewPanel.Paint += NextShapePreviewPanel_Paint;
            nextShapePreviewPanel.Width = GameSettings.PreviewWidth * GameSettings.CellSize + 3;
            nextShapePreviewPanel.Height = GameSettings.PreviewHeight * GameSettings.CellSize + 3;
            nextShapePreviewPanel.BorderStyle = BorderStyle.FixedSingle;
            nextShapePreviewPanel.BackColor = Color.FromArgb(36, 36, 38);
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, nextShapePreviewPanel, new object[] { true });
        }

        public void UpdateGameBoard(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            
            gameBoardPanel.Invalidate();
            nextShapePreviewPanel.Invalidate();
        }

        public void UpdateScore(int score)
        {
            scoreLabel.Text = score.ToString();

            int bestScore = ScoreManager.GetMax();
            if (score > bestScore)
            {
                scoreLabel.Text += " (New record!)";
                bestScoreLabel.Text = score.ToString();
            }
            else
            {
                bestScoreLabel.Text = bestScore.ToString();
            }
        }

        private void NextShapePreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_gameBoard?.NextShape != null)
            {
                DrawNextShapePreview(e);
            }
            DrawPreviewGrid(e);
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

        private void DrawNextShapePreview(PaintEventArgs e)
        {
            int cellSize = GameSettings.CellSize;
            
            foreach (var shapePart in _gameBoard.NextShape.ShapeParts)
            {
                int shapePartX = (shapePart.X + GameSettings.PreviewWidth / 2) * cellSize;
                int shapePartY = (shapePart.Y + GameSettings.PreviewHeight / 2 - 1) * cellSize;

                e.Graphics.FillRectangle(new SolidBrush(shapePart.Color), shapePartX, shapePartY, cellSize, cellSize);
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

                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(40, 0, 0, 0), 1),
                        new Rectangle(x, y, cellSize, cellSize));
                }
            }
        }

        private void DrawGameBoard(PaintEventArgs e)
        {
            int cellSize = GameSettings.CellSize;
            
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

        private void DrawGrid(PaintEventArgs e)
        {
            if (_gameBoard == null) return;

            for (int i = 0; i < GameSettings.GameBoardHeight; i++)
            {
                for (int j = 0; j < GameSettings.GameBoardWidth; j++)
                {
                    int cellSize = GameSettings.CellSize;
                    int x = j * cellSize;
                    int y = i * cellSize;

                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(40, 0, 0, 0), 1),
                        new Rectangle(x, y, cellSize, cellSize));
                }
            }
        }

        public event EventHandler<KeyEventArgs> OnGameFormKeyDown;
        public event EventHandler OnGameFormLoad;
        public event EventHandler OnNewGameClick;

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            OnGameFormKeyDown(sender, e);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            OnGameFormLoad(sender, e);
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            OnNewGameClick(sender, e);
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
