using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Server.Forms;

namespace Server
{

	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			
		}
		
		
		void СоединениеToolStripMenuItemClick(object sender, EventArgs e)
		{
			NetworkSettings networkSettingsForm = new NetworkSettings();
			networkSettingsForm.ShowDialog();
		}
		
		void ПользовательскиеДанныеToolStripMenuItemClick(object sender, EventArgs e)
		{
			Profile profileForm = new Profile();
			profileForm.ShowDialog();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			HDD HDDForm = new HDD();
			HDDForm.ShowDialog();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Network lanInfo = new Network();
			lanInfo.ShowDialog();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			Applications appInfo = new Applications();
			appInfo.ShowDialog();
		}
	}
}
