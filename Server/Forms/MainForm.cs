using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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
		
		void Button4Click(object sender, EventArgs e)
		{
			RAM ramInfo = new RAM();
			ramInfo.ShowDialog();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			CPU CPUInfo = new CPU();
			CPUInfo.ShowDialog();
		}
		
				
		void Button6Click(object sender, EventArgs e)
		{
			//Temperature temperatureInfo = new Temperature();
			//temperatureInfo.ShowDialog();
			Thread temperatureThread = new Thread(() => new DevicesTemperature().ShowDialog());
			temperatureThread.Start();
		}
	}
}
