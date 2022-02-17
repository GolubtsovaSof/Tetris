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
	class Figure
	{
		public int[] m_Position;
		public List<List<int[]>> Body;
		public Panel[,] mas;
		public int Current_Rotate;
		private Random rnd = new Random(DateTime.Now.Millisecond);

		public Figure (int[] position, Panel[,] amas)
		{
			mas = amas;
			m_Position = position;
			Current_Rotate = 0;
			int variant = rnd.Next(0,7);

			switch (variant)
			{
				case 0:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 0, 1 }, new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1} },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 3 } } };
					break;
				case 1:
					Body = new List<List<int[]>>() { new List<int[]> { new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 1, 2 }, new int[] { 2, 2 } } };
					break;
				case 2:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 1, 2 }, new int[] { 2, 2 }, new int[] { 2, 1 }, new int[] { 3, 1 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 3, 1 }, new int[] { 3, 2 } } };
					break;
				case 3:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 3, 2 } },
													new List<int[]> { new int[] { 3, 0 }, new int[] { 3, 1 }, new int[] { 2, 1 }, new int[] { 2, 2 } } };
					break;
				case 4:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 1, 2 }, new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 3, 2 } },
													new List<int[]> { new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 }, new int[] { 3, 0 } },
													new List<int[]> { new int[] { 1, 0 }, new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 } } };
					break;
				case 5:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 }, new int[] { 3, 2 } },
													new List<int[]> { new int[] { 3, 0 }, new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 } },
													new List<int[]> { new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 1, 2 } } };
					break;
				case 6:
					Body = new List<List<int[]>>() {new List<int[]> { new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 }, new int[] { 2, 2 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 3, 1 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 3, 1 } },
													new List<int[]> { new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 1, 1 } } };
					break;
			}
		}

		public List<int[]> Get_Body()
		{
			return Body[Current_Rotate];
		}

		public void Figure_Rotate()
		{
			Figure_Del();
			int Pre_Rotate = Current_Rotate;
			Current_Rotate++;
			
			if (Current_Rotate == Body.Count) Current_Rotate = 0;
			if (Figure_Has_Collision()) Current_Rotate--;
			if (Current_Rotate == -1) Current_Rotate = Pre_Rotate;
			Figure_Draw();
		}

		public void Figure_Draw()
		{
			foreach (int[] x in Get_Body())
			{
				mas[m_Position[0] + x[0], m_Position[1] + x[1]].BackColor = Color.Yellow;
			}
		}

		public void Figure_Del()
		{
			foreach (int[] x in Get_Body())
			{
				mas[m_Position[0] + x[0], m_Position[1] + x[1]].BackColor = Color.Black;
			}
		}
		public void Figure_Update()
		{
			Figure_Del();
			m_Position[1]++;
			Figure_Draw();
		}
		public void Figure_Left()
		{
			Figure_Del();
			m_Position[0]--;
			if (Figure_Has_Collision() == true)
			{
				m_Position[0]++;
			}
			Figure_Draw();

		}
		public void Figure_Right()
		{
			Figure_Del();
			m_Position[0]++;
			if (Figure_Has_Collision() == true)
			{
				m_Position[0]--;
			}
			Figure_Draw();
		}

		public bool Figure_Has_Collision()
		{
			foreach (int[] x in Get_Body())
			{
				if (m_Position[0] + x[0] == -1 || m_Position[0] + x[0] == mas.GetLength(0) || m_Position[1] + x[1] == - 1 || m_Position[1] + x[1] == mas.GetLength(1) ||
					(Is_In_List(new int[] { x[0]+1, x[1] }) == false && mas[m_Position[0] + x[0], m_Position[1] + x[1]].BackColor == Color.Yellow)||
					(Is_In_List(new int[] { x[0]-1, x[1] }) == false && mas[m_Position[0] + x[0], m_Position[1] + x[1]].BackColor == Color.Yellow))
				{
					return true;
				}
			}
			return false;
		}

		public bool Figure_Stop_Falling()
		{
			foreach (int[] x in Get_Body())
			{
				if (m_Position[1] + x[1] == mas.GetLength(1) - 1 || (Is_In_List(new int[] { x[0], 1 + x[1] }) == false && mas[m_Position[0] + x[0], m_Position[1] + 1 + x[1]].BackColor == Color.Yellow))
				{
					return true;
				}
			}
			return false;
		}

		public bool Is_In_List(int[] pnt)
		{
			List<int[]> lst = Get_Body();
			foreach (int[] x in lst)
			{
				if (x.SequenceEqual(pnt)) return true;
			}
			return false;
		}
	}
}
