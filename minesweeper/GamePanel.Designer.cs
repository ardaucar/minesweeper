namespace minesweeper
{
    partial class GamePanel
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
            this.components = new System.ComponentModel.Container();
            this.restartButton = new System.Windows.Forms.Button();
            this.mineCount = new System.Windows.Forms.Label();
            this.timeElapsed = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.infoMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // restartButton
            // 
            this.restartButton.AccessibleName = "";
            this.restartButton.BackColor = System.Drawing.Color.Green;
            this.restartButton.ForeColor = System.Drawing.Color.White;
            this.restartButton.Location = new System.Drawing.Point(309, 9);
            this.restartButton.Margin = new System.Windows.Forms.Padding(0);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(172, 32);
            this.restartButton.TabIndex = 0;
            this.restartButton.Text = "Oyunu Başlat";
            this.restartButton.UseVisualStyleBackColor = false;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // mineCount
            // 
            this.mineCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mineCount.AutoSize = true;
            this.mineCount.BackColor = System.Drawing.Color.Black;
            this.mineCount.ForeColor = System.Drawing.Color.Red;
            this.mineCount.Location = new System.Drawing.Point(196, 9);
            this.mineCount.Margin = new System.Windows.Forms.Padding(0);
            this.mineCount.Name = "mineCount";
            this.mineCount.Padding = new System.Windows.Forms.Padding(8, 9, 12, 9);
            this.mineCount.Size = new System.Drawing.Size(97, 31);
            this.mineCount.TabIndex = 1;
            this.mineCount.Text = "Mayın Sayısı: 0";
            this.mineCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeElapsed
            // 
            this.timeElapsed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.timeElapsed.BackColor = System.Drawing.Color.Black;
            this.timeElapsed.ForeColor = System.Drawing.Color.Red;
            this.timeElapsed.Location = new System.Drawing.Point(493, 9);
            this.timeElapsed.Margin = new System.Windows.Forms.Padding(0);
            this.timeElapsed.Name = "timeElapsed";
            this.timeElapsed.Padding = new System.Windows.Forms.Padding(12, 9, 8, 9);
            this.timeElapsed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeElapsed.Size = new System.Drawing.Size(120, 31);
            this.timeElapsed.TabIndex = 2;
            this.timeElapsed.Text = "Geçen Süre: 0";
            this.timeElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 1000;
            // 
            // infoMessage
            // 
            this.infoMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoMessage.BackColor = System.Drawing.SystemColors.Control;
            this.infoMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.infoMessage.Location = new System.Drawing.Point(12, 51);
            this.infoMessage.Name = "infoMessage";
            this.infoMessage.Size = new System.Drawing.Size(776, 362);
            this.infoMessage.TabIndex = 3;
            this.infoMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoMessage.Visible = false;
            // 
            // GamePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.infoMessage);
            this.Controls.Add(this.timeElapsed);
            this.Controls.Add(this.mineCount);
            this.Controls.Add(this.restartButton);
            this.Name = "GamePanel";
            this.Text = "Mayın Tarlası";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Label mineCount;
        private System.Windows.Forms.Label timeElapsed;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label infoMessage;
    }
}

