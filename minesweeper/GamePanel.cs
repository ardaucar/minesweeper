using minesweeper.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public partial class GamePanel : System.Windows.Forms.Form
    {
        private Minesweeper game;

        public GamePanel()
        {
            InitializeComponent();
            this.Size = new Size(64 * 9 + Minesweeper.paddingX * 2, 64 * 9 + Minesweeper.paddingX * 2);
            this.SizeFromClientSize(this.Size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.game = new Minesweeper(this);
            this.Icon = Properties.Resources.logo;
            restartButton.Location = new Point(this.Size.Width / 2 - restartButton.Size.Width / 2, 12);
            mineCount.Location = new Point(Minesweeper.paddingX, 12);
            timeElapsed.Location = new Point(this.Size.Width - (Minesweeper.paddingX + timeElapsed.Size.Width), 12);

            mineCount.Text = "Mayın Sayısı: 0";
            timeElapsed.Text = "Geçen Süre: 0";
            infoMessage.Text = "Mayın Tarlasına hoş geldiniz,\n oyunu başlatmak için yukarıdaki\n 'Oyunu Başlat' butonuna tıklayınız";
            infoMessage.Visible = true;
        }

        private void restartButton_Click(object sender, EventArgs e) => this.game.onRestartClick();
        public Label getMineCountLabel() => this.mineCount;
        public Label getTimeElapsedLabel() => this.timeElapsed;
        public Timer getTimer() => this.gameTimer;
        public Button getRestartButton() => this.restartButton;
        public Label getInfoMessageLabel() => this.infoMessage;
    }
}
