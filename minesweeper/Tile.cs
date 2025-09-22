using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace minesweeper
{
    public enum TileType
    {
        MINE,
        FLAGGED,
        EMPTY,
        DANGEROUS,
        EXPLORED,
        TRIGGERED
    }
    public class Tile
    {
        public bool isMine = false;
        private Minesweeper game;
        private GamePanel panel;
        private TileType type;
        private int[] pos;
        private Button button;
        private int[] gap = new int[2] { 64, 64 };
        private int[] size = new int[2] { 64, 64 };
        private MouseButtons lastClickType;

        public Tile(Minesweeper game, TileType type, int[] pos)
        {
            this.game = game;
            this.panel = game.panel;
            this.type = type;
            this.pos = pos;
        }

        public void setType(TileType type) => this.type = type;
        public TileType getType() => this.type;
        public int[] getPos() => this.pos;
        public Button getButton() => this.button;
        public Nullable<short> getValue()
        {
            switch (this.type)
            {
                case TileType.EXPLORED:
                    return -2;
                case TileType.FLAGGED:
                    return -1;
                case TileType.MINE:
                    return 0;
                case TileType.DANGEROUS:
                    return (short)this.getNearMines().Length;
                default:
                    return null;
            }
        }

        private List<int[]> getNear()
        {
            List<int[]> near = new List<int[]>();

            for (int pointer = 0; pointer < 8; pointer++)
            {
                int x = this.getPos()[0];
                int y = this.getPos()[1];

                if (pointer < 3) {
                    x += -1 + pointer;
                    y--;
                }
                else if (pointer == 3) x--;
                else if (pointer == 4) x++;
                else {
                    x += -1 + (pointer % 5);
                    y++;
                }

                Tile tile = this.game.getTile(x, y);
                if (tile == null) continue;
                near.Add(tile.getPos());
            }
            return near;
        }

        private Tile[] getNearMines()
        {
            List<int[]> near = getNear();
            List<Tile> mines = new List<Tile>();
            foreach (int[] pos in near)
            {
                Tile tile = this.game.getTile(pos[0], pos[1]);
                if (tile.isMine) mines.Add(tile);
            }
            return mines.ToArray();
        }

        private bool isSafe() => this.getNearMines().Length == 0 ? true : false;

        public void toggleMarker()
        {
            if (this.getType() == TileType.EXPLORED) return;
            if (this.getType() == TileType.FLAGGED)
            {
                this.setType(this.isMine ? TileType.MINE : TileType.EMPTY);
                this.button.Image = Properties.Resources.closed;
                this.game.increaseMineCount();
                return;
            }

            if (this.game.getRemainingMine() <= 0) return;
            this.setType(TileType.FLAGGED);
            button.Image = Properties.Resources.flag;
            this.game.decreaseMineCount();
        }

        public void reveal()
        {
            if (this.getType() == TileType.EXPLORED) return;
            Nullable<short> value = this.getValue();
            if (value > 0)
            {
                this.button.Image = this.game.getDangerousImage(value);
                return;
            }

            if (value == null)
            {
                this.setType(TileType.DANGEROUS);
                this.button.Image = this.game.getDangerousImage(this.getValue());
            }
            if (this.isSafe())
            {
                this.button.Image = Properties.Resources.opened;
                this.setType(TileType.EXPLORED);

                List<int[]> nears = this.getNear();
                foreach (int[] pos in nears)
                {
                    Tile tile = this.game.getTile(pos[0], pos[1]);
                    if (tile.getValue() == null) tile.reveal();
                }
            }
        }

        private void onMouseDown(object sender, MouseEventArgs e) => this.lastClickType = e.Button == MouseButtons.Left ? MouseButtons.Left : MouseButtons.Right;
        public void onClick(object sender, MouseEventArgs e)
        {
            Nullable<short> value = this.getValue();
            if (this.lastClickType == MouseButtons.Left)
            {
                if (value < 0) return;
                if (value == null && !this.isSafe()) this.setType(TileType.DANGEROUS);
                if (value == 0)
                {
                    this.game.endByExplosion(this);
                    return;
                }
                this.reveal();
                this.game.gameCheck();
                return;
            }
            this.toggleMarker();
            this.game.gameCheck();
        }

        public Tile build()
        {
            this.button = new Button();
            int x = this.pos[0];
            int y = this.pos[1];
            button.Location = new Point(x * this.gap[0] + Minesweeper.paddingX, y * this.gap[1] + Minesweeper.paddingTop);
            button.Size = new Size(this.size[0], this.size[1]);
            button.Margin = new Padding();
            button.BackColor = Color.White;
            button.Image = Properties.Resources.closed;
            button.Name = x + ":" + y;
            button.MouseDown += new MouseEventHandler(onMouseDown);
            button.MouseUp += new MouseEventHandler(onClick);
            this.panel.Controls.Add(button);
            return this;
        }
    }
}
