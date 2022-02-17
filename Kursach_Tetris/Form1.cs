using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.IO;

namespace Kursach_Tetris
{
	public partial class Form1 : Form
	{
		private Figure figure;
		private Figure figure_next;
		private Panel[,] mas;
		private Panel[,] mas_next;
		private Gamefield field;
		private int current_timer;
		private int highscore;
		

		public Form1()
		{
			InitializeComponent();
			this.KeyPreview = true;
			panel2.Location= new Point (panel1.Height,0);
			panel2.Size = new Size (panel2.Width, panel2.Width);
			panel3.Location = new Point(panel1.Height, panel2.Width);
			panel3.Size = new Size(panel2.Width, panel1.Height);
			panel4.Location = new Point(panel1.Height + panel2.Width, 0);
			panel4.Size = new Size(2000,2000);
			StreamReader sr = new StreamReader("highscore.txt");
			highscore = Convert.ToInt32(sr.ReadLine());
			label7.Text = Convert.ToString(highscore);
			sr.Close();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Gamefield g = new Gamefield(10, 20);
			g.Create_Gamefield(panel1);
			Gamefield g_next = new Gamefield(4, 4);
			g_next.Create_Next_Figure_Box(panel2);

			Figure f = new Figure(new int[] { 3,-1 }, g.mas);

			figure = f;
			field = g;
			mas = g.mas;
			mas_next = g_next.mas;

			timer1.Interval = 1000;
			current_timer = 1000;
			timer1.Start();
			figure.Figure_Draw();

			Figure next = new Figure(new int[] { 0, 0 }, g_next.mas);
			figure_next = next;
			figure_next.Figure_Draw();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				timer1.Interval = 50;
			}

			if (e.KeyCode == Keys.Left)
			{
				figure.Figure_Left();

			}
			if (e.KeyCode == Keys.Right)
			{
				figure.Figure_Right();

			}
			if (e.KeyCode == Keys.Up)
			{
				figure.Figure_Rotate();
			}
			if (e.KeyCode == Keys.Space)
			{
				timer1.Enabled = !timer1.Enabled;
			}
		}

		private void timer1_Tick_1(object sender, EventArgs e)
		{
			if (figure.Figure_Stop_Falling() == false)
			{
				figure.Figure_Update();
				
			}
			else
			{
				if (field.Is_Game_Over())
				{
					timer1.Stop();
					field.Claer_Gamefield();
					this.Hide();
					Form2 form2 = new Form2();
					if (Convert.ToInt32(label2.Text) > Convert.ToInt32(label7.Text))
					{
						highscore = Convert.ToInt32(label2.Text);
						StreamWriter sw = new StreamWriter("highscore.txt", false);
						sw.WriteLine(label2.Text);
						sw.Close();
					}
					form2.label3.Text = label2.Text;
					label2.Text = "0";
					label4.Text = "1";
					label7.Text = Convert.ToString(highscore);

					form2.ShowDialog();
					if (DialogResult.Abort == form2.DialogResult || form2.IsDisposed)
					{
						Application.Exit();
					}
					if (DialogResult.Retry == form2.DialogResult)
					{
						this.Show();
						timer1.Start();
						timer1.Interval = 1000;
					}

				}				
				field.Gamefield_Has_Line(label2,label4, timer1, current_timer);
				figure_next.Figure_Del();

				figure = figure_next;
				figure.m_Position = new int[] { 3, -1 };
				figure.mas = mas;
				figure_next = new Figure(new int[] { 0, 0 }, mas_next);

				figure.Figure_Draw();
				figure_next.Figure_Draw();
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				timer1.Interval = current_timer;
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void panel4_Paint(object sender, PaintEventArgs e)
		{

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}
	}
}
