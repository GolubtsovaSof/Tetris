using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach_Tetris
{
	class Gamefield
	{
		public int Width;
		public int Height;
		public Panel[,] mas;


		public Gamefield(int W, int H) 
		{
			Width = W;
			Height = H;
			mas = new Panel[W, H];
		}

		public void Create_Gamefield(Panel p)
		{
			for (int i = 0; i<Width; i++)
			{
				for (int j = 0; j<Height; j++)
				{
					mas[i, j] = new Panel();
					mas[i, j].Size = new Size(p.Height/Height, p.Height / Height);
					mas[i, j].Location = new Point((p.Height / Height) * i, (p.Height / Height) * j);
					mas[i, j].BackColor = Color.Black;
					mas[i, j].BorderStyle = BorderStyle.FixedSingle;
					p.Controls.Add(mas[i, j]);
				}
			}
		}

		public void Create_Next_Figure_Box(Panel p)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					mas[i, j] = new Panel();
					mas[i, j].Size = new Size(p.Width / Width, p.Width / Width);
					mas[i, j].Location = new Point((p.Width/ Width) * i, (p.Width / Width) * j);
					mas[i, j].BackColor = Color.Black;
					mas[i, j].BorderStyle = BorderStyle.None;
					p.Controls.Add(mas[i, j]);
				}
			}
		}

		public void Gamefield_Has_Line(Label score, Label level, Timer timer1, int curr)
		{
			for (int i = 0; i < Height; i++)
			{
				int count = 0;
				for (int j = 0; j < Width; j++)
				{
					if (mas[j,i].BackColor == Color.Yellow) count++;
				}
				if (count == Width)
				{
					int new_score = (Convert.ToInt32(score.Text));
					new_score++;
					score.Text = Convert.ToString(new_score);
					for (int z = i; z >= 0; z--)
					{
						for (int j = 0; j < Width; j++)
						{
							if (z == 0) mas[j, z].BackColor = Color.Black;
							else mas[j, z].BackColor = mas[j, z - 1].BackColor;
						}
					}
					if (Convert.ToInt32(score.Text) != 0 && Convert.ToInt32(score.Text) % 8 == 0)
					{
						level.Text = Convert.ToString(Convert.ToInt32(level.Text) + 1);
						if (timer1.Interval > 100) timer1.Interval -= 100;
						if (timer1.Interval <= 100 && timer1.Interval > 50) timer1.Interval -= 10;
						else timer1.Interval = 50;
						curr = timer1.Interval;
					}
				}
				
			}
		}
		public bool Is_Game_Over()
		{
			for (int i = 0; i<Width; i++)
			{
				if (mas[i, 0].BackColor == Color.Yellow) return true;
			}
			return false;
		}

		public void Claer_Gamefield()
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					mas[i, j].BackColor = Color.Black;
				}
			}
		}


	}
}
