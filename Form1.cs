﻿using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace iwm_blind
{
	public partial class Form1 : Form
	{
		///private const string Ver = "iwm20190908";

		public Form1()
		{
			InitializeComponent();

			// MouseWhell イベント追加
			MouseWheel += new MouseEventHandler(Form_MouseWheel);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SubBgBlind();
		}

		private void Form1_MouseEnter(object sender, EventArgs e)
		{
			ToolTip1.SetToolTip(
				this,
				"[マウスホイール] 暗さを調整\n" +
				"[ダブルクリック] 巻き上げ／下げ\n" +
				"[右クリック] 閉じる"
			);
		}

		private void Form_MouseWheel(object sender, MouseEventArgs e)
		{
			// 上回転 +120
			if (e.Delta > 0)
			{
				SubBgBlindUp();
			}
			// 下回転 -120
			else if (e.Delta < 0)
			{
				SubBgBlindDown();
			}

			ToolTip1.SetToolTip(this, (Opacity * 100) + "%");
		}

		private Point MousePoint;

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				MousePoint = new Point(e.X, e.Y);
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				Left += e.X - MousePoint.X;
				Top += e.Y - MousePoint.Y;
			}
		}

		private int GblHeightMax = 0;

		private void Form1_DoubleClick(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				WindowState = FormWindowState.Normal;
				return;
			}

			if (Height > MinimumSize.Height)
			{
				GblHeightMax = Height;
				Height = MinimumSize.Height;
			}
			else
			{
				Height = GblHeightMax;
			}
		}

		private void Cms1_背景色_Click(object sender, EventArgs e)
		{
			using (ColorDialog CD = new ColorDialog
			{
				Color = BackColor,
				AllowFullOpen = true,
				SolidColorOnly = true,
				CustomColors = new int[] { }
			})
			{
				if (CD.ShowDialog() == DialogResult.OK)
				{
					BackColor = CD.Color;
				}
			}
		}

		private void Cms1_最大化_解除_Click(object sender, EventArgs e)
		{
			WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
		}

		private void Cms1_閉じる_Click(object sender, EventArgs e)
		{
			Close();
		}

		//-------
		// 関数
		//-------
		private void SubBgBlind(
			double level = 0.5
		)
		{
			if (level > 1.01)
			{
				level = 0.1;
			}
			else if (level < 0.1)
			{
				level = 1.0;
			}

			Opacity = level;
		}

		private void SubBgBlindUp()
		{
			SubBgBlind(Opacity + 0.05);
		}

		private void SubBgBlindDown()
		{
			SubBgBlind(Opacity - 0.05);
		}
	}
}