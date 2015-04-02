using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        Board board;
        const int Scalar = 100;
        List<Button> Cells = new List<Button>();
        public main()
        {
            InitializeComponent();
        }

        class NoSelectButton : Button
        {

            public NoSelectButton()
            {

                SetStyle(ControlStyles.Selectable, false);
                TabStop = false;
                Enabled = false;
                this.Font = new Font("Arial", 16f);
            }
        }

        void Draw()
        {
            for (int i = 0; i < Board.Size; i++)
                for (int j = 0; j < Board.Size; j++)
                {
                    int pos = i * Board.Size + j;
                    if (board.a[i, j] != 0)
                    {
                        Cells[pos].Enabled = true;
                        Cells[pos].Text = board.a[i, j].ToString();
                        Cells[pos].BackColor = board.color[board.a[i, j]];
                    }
                    else
                    {
                        Cells[pos].Text = "";
                        Cells[pos].BackColor = System.Drawing.Color.White;
                        Cells[pos].Enabled = false;
                    }
                }
        }

        private void main_Load(object sender, EventArgs e)
        {
            this.Text = "2048";
            board = new Board();
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    int pos = i * Board.Size + j;
                    Cells.Add(new NoSelectButton());
                    Cells[pos].Parent = this;
                    Cells[pos].Top = i * Scalar;
                    Cells[pos].Left = j * Scalar;
                    Cells[pos].Width = Cells[pos].Height = Scalar;
                }
            }
            this.Width = Board.Size * Scalar + 17;
            this.Height = Board.Size * Scalar + 38;
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            Draw();
        }

        private void main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left :
                    board.Move(0);
                break;
                
                case Keys.Right :
                    board.Move(2);
                break;
                
                case Keys.Up :
                    board.Move(1);
                break;

                case Keys.Down :
                    board.Move(3);
                break;
            }
            Draw();
            if (board.GameOver())
            {
                MessageBox.Show("GAME OVER!!!");
                this.Close();
            }
        }
    }
}
