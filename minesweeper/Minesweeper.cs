using System;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Media;

namespace minesweeper
{
    public enum GameStatus
    {
        PREGAME,
        MIDGAME,
        ENDGAME
    }

    public class Minesweeper
    {
        public readonly GamePanel panel;
        public bool isVictory = false;

        public const int paddingTop = 60;
        public const int paddingX = 64;

        private readonly Timer gameTimer;
        private readonly Button restartButton;
        private readonly Label mineCount;
        private readonly Label timeElapsed;
        private readonly Label infoMessage;

        private Tile[,] gameboard;
        private GameStatus gameStatus = GameStatus.PREGAME;
        private long secondsElapsed = 0;
        private int mineMax = 0;
        private int mineRemaining = 0;

        public Minesweeper (GamePanel panel)
        {
            this.panel = panel;
            this.gameTimer = panel.getTimer();
            this.restartButton = panel.getRestartButton();
            this.mineCount = panel.getMineCountLabel();
            this.timeElapsed = panel.getTimeElapsedLabel();
            this.infoMessage = panel.getInfoMessageLabel();
        }

        public Tile getTile(int x, int y)
        {
            return this.inBoundries(x, y) ? this.gameboard[y, x] : null;
        }
        
        public int getRemainingMine() => this.mineRemaining;
        public Bitmap getDangerousImage(Nullable<short> value)
        {
            switch (value)
            {
                case 1:
                    return Properties.Resources._1;
                case 2:
                    return Properties.Resources._2;
                case 3:
                    return Properties.Resources._3;
                case 4:
                    return Properties.Resources._4;
                case 5:
                    return Properties.Resources._5;
                case 6:
                    return Properties.Resources._6;
                case 7:
                    return Properties.Resources._7;
                case 8:
                    return Properties.Resources._8;
                default:
                    return null;
            }
        }

        public void increaseMineCount() => this.mineCount.Text = "Mayın Sayısı: " + ++this.mineRemaining;
        public void decreaseMineCount() => this.mineCount.Text = "Mayın Sayısı: " + --this.mineRemaining;

        public void onRestartClick()
        {
            switch (this.gameStatus)
            {
                case GameStatus.PREGAME:
                    this.infoMessage.Visible = false;
                    this.isVictory = false;
                    this.startGame();
                    break;
                case GameStatus.MIDGAME:
                    this.endGame();
                    break;
                case GameStatus.ENDGAME:
                    this.continueGame();
                    break;
            }
        }

        public bool gameCheck()
        {
            int openedCells = 0;
            for (int y = 0; y < this.gameboard.GetLength(0); y++)
                for (int x = 0; x < this.gameboard.GetLength(1); x++)
                {
                    Tile tile = this.getTile(x, y);
                    Nullable<int> value = tile.getValue();
                    if (!tile.isMine && (value > 0 || value == -2)) openedCells++;
                }

            if (openedCells == (this.gameboard.Length - this.mineMax))
            {
                this.isVictory = true;
                this.endGame();
                return true;
            }
            return false;
        }

        public void revealMines()
        {
            for (int y = 0; y < this.gameboard.GetLength(0); y++)
                for (int x = 0; x < this.gameboard.GetLength(1); x++)
                {
                    Tile tile = this.getTile(x, y);
                    Button button = tile.getButton();
                    button.MouseUp -= tile.onClick;
                    if (tile.getType() == TileType.MINE) button.Image = Properties.Resources.mine;
                }
        }

        public void endByExplosion(Tile tile)
        {
            Console.WriteLine("PATLADIN!");
            tile.setType(TileType.TRIGGERED);
            tile.getButton().Image = Properties.Resources.mine_red;
            this.endGame();
        }

        private Tile[,] createGameboard(Size size, int mineCount)
        {
            this.mineMax = mineCount;
            this.gameboard = new Tile[size.Width, size.Height];
            int cellCount = size.Width * size.Height;

            List<int[]> mineTiles = new List<int[]>();
            for (int y = 0; y < size.Height; y++)
                for (int x = 0; x < size.Width; x++) {
                    this.gameboard[y,x] = new Tile(this, TileType.EMPTY, new int[2] { x, y });
                    mineTiles.Add(new int[2] { x, y });
                }

            this.mineRemaining = mineCount;
            Random random = new Random();
            while (mineCount > 0)
            {
                int index = random.Next(mineTiles.Count);
                int[] pos = mineTiles[index];
                Tile tile = this.getTile(pos[1], pos[0]);
                if (tile == null) continue;
                tile.setType(TileType.MINE);
                tile.isMine = true;
                mineTiles.RemoveAt(index);
                mineCount--;
            }

            return gameboard;
        }

        private void drawGameboard()
        {
            for (int y = 0; y < this.gameboard.GetLength(0); y++)
                for (int x = 0; x < this.gameboard.GetLength(1); x++) gameboard[y, x].build();
        }

        private void clearBoard()
        {
            for (int y = 0; y < this.gameboard.GetLength(0); y++)
                for (int x = 0; x < gameboard.GetLength(1); x++) this.panel.Controls[x + ":" + y].Dispose();
        }

        private void showScores()
        {
            float secondPerTile = 4.375f;
            float percentage = 100 / mineMax;
            float score = ((mineMax - mineRemaining) * percentage);
            float punishment = (float)((float) secondsElapsed / Math.Max((mineMax - mineRemaining) * 0.5, secondPerTile));
            if (punishment <= secondPerTile) punishment = 0;
            score = score - punishment;

            String endingMessage = "REZALETTİN!";
            if (score == 100) endingMessage = "MÜKEMMELDİN!";
            else if (score > 95) endingMessage = "ÇOK İYİYDİN!";
            else if (score > 87.5) endingMessage = "İYİYDİN!";
            else if (score > 80) endingMessage = "FENA DEĞİLDİN!";
            else if (score > 65) endingMessage = "İDARE EDERDİN!";
            else if (score > 50) endingMessage = "ÇOK KÖTÜ DEĞİLDİN!";
            else if (score > 30) endingMessage = "KÖTÜYDÜN!";
            else if (score > 10) endingMessage = "BERBATTIN!";
            Console.WriteLine("Current Punishment: " + punishment + " | Current Score: " + score);
            
            float secWastedPerMine = this.secondsElapsed / ((this.mineMax - this.mineRemaining) == 0 ? 1 : (this.mineMax - this.mineRemaining));
            this.infoMessage.Text = (
                (this.isVictory ? "Aferim" : "Yazık") + "! Bu oyun " + endingMessage +
                "\nToplam geçen süre: " + this.secondsElapsed +
                "\nMayın başına harcadığın\nsaniye: " + secWastedPerMine.ToString("F2", CultureInfo.GetCultureInfo("en-US"))
            );
            this.infoMessage.Visible = true;
        }
        private void startGame()
        {
            Console.WriteLine("Game is starting.");
            this.gameStatus = GameStatus.MIDGAME;
            this.restartButton.BackColor = Color.Red;
            this.restartButton.Text = "Oyunu Bitir";
            this.createGameboard(new Size(9, 9), 10);
            this.drawGameboard();
            this.mineCount.Text = "Mayın Sayısı: " + this.mineRemaining;
            this.gameTimer.Tick += new EventHandler(this.onSecond);
            this.gameTimer.Start();
            Console.WriteLine("Game has started.");
        }

        private void endGame()
        {
            Console.WriteLine("Game is ending.");
            this.gameStatus = GameStatus.ENDGAME;
            this.restartButton.BackColor = Color.DarkCyan;
            this.restartButton.Text = "Devam Et";
            this.gameTimer.Stop();
            this.gameTimer.Tick -= this.onSecond;
            this.revealMines();
            Console.WriteLine("Game has ended.");
        }

        private void continueGame()
        {
            this.clearBoard();
            this.showScores();

            this.secondsElapsed = 0;
            this.mineCount.Text = "Mayın Sayısı: 0";
            this.timeElapsed.Text = "Geçen Süre: " + this.secondsElapsed;
            this.gameStatus = GameStatus.PREGAME;
            this.restartButton.BackColor = Color.Green;
            this.restartButton.Text = "Oyunu Başlat";
        }

        public bool inBoundries(int x, int y)
        {
            return y >= this.gameboard.GetLength(0) || y < 0 || x >= this.gameboard.GetLength(1) || x < 0 ? false : true;
        }

        private void onSecond(Object obj, EventArgs eventArgs)
        {
            this.timeElapsed.Text = "Geçen Süre: " + ++secondsElapsed;
        }
    }
}
