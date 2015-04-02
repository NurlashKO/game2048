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
    class Board
    {
        //<Variables>
        public const int Size = 5;
        public int[ , ] a = new int[Size , Size];
        Random rand = new Random();
        public Dictionary<int, System.Drawing.Color> color = new Dictionary<int, Color>();
        
        //<Constructors>
        public Board()
        {
            add();
            add();
            color[2] = System.Drawing.Color.Gray;
            color[4] = System.Drawing.Color.GreenYellow;
            color[8] = System.Drawing.Color.Green;
            color[16] = System.Drawing.Color.Blue;
            color[32] = System.Drawing.Color.Orange;
            color[64] = System.Drawing.Color.DarkOrange;
            color[128] = System.Drawing.Color.Red;
            color[256] = System.Drawing.Color.LemonChiffon;
            color[512] = System.Drawing.Color.LavenderBlush;
            color[1024] = System.Drawing.Color.RoyalBlue;
            color[2048] = System.Drawing.Color.Gold;
            color[4096] = System.Drawing.Color.Fuchsia;
            color[8192] = System.Drawing.Color.MediumTurquoise;
        }

        //<Methods>
        bool add()
        {
            List<int> freeCells = new List<int>();
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (a[i, j] == 0)
                        freeCells.Add(i * Size + j);

            if (freeCells.Count == 0)
                return false;
            int pos = freeCells[rand.Next() % freeCells.Count];
            a[pos / Size, pos % Size] = 2;
            return true;
        }

        public bool GameOver()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (a[i, j] == 0)
                        return false;
                    if (i > 0 && a[i, j] == a[i - 1, j])
                        return false;
                    if (i < Size - 1 && a[i, j] == a[i + 1, j])
                        return false;
                    if (j > 0 && a[i, j] == a[i, j - 1])
                        return false;
                    if (j < Size - 1 && a[i, j] == a[i, j + 1])
                        return false;
                }
            return true;
        }

        void Shift()
        {
            int[] d = new int[Size];
            int cnt, c;
            for (int i = 0; i < Size; i++)
            {
                cnt = c = 0;
                for (int j = 0; j < Size; j++)
                {
                    if (a[i, j] != 0)
                        d[cnt++] = a[i, j];
                    a[i, j] = 0;
                }
                for (int j = 1; j < cnt; j++)
                    if (d[j] == d[j - 1])
                    {
                        d[j - 1] *= 2;
                        d[j] = 0;
                    }
                for (int j = 0; j < cnt; j++)
                    if (d[j] != 0)
                        a[i, c++] = d[j];
            }
        }

        void rotate()
        {
            int[ , ] b = new int[Size, Size];
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    b[i, j] = a[j, Size - i - 1];
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    a[i, j] = b[i, j];
        }

        public void Move(int d)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == d)
                {
                    Console.Write("OK");
                    Shift();
                }
                rotate();
            }
            add();
        }
    }
}
