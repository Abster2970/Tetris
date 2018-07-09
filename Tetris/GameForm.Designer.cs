namespace Tetris
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameBoardPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gameBoardPanel
            // 
            this.gameBoardPanel.Location = new System.Drawing.Point(12, 12);
            this.gameBoardPanel.Name = "gameBoardPanel";
            this.gameBoardPanel.Size = new System.Drawing.Size(260, 326);
            this.gameBoardPanel.TabIndex = 0;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 350);
            this.Controls.Add(this.gameBoardPanel);
            this.Name = "GameForm";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gameBoardPanel;
    }
}

