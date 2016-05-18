
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Server.Forms
{
	public partial class DevicesTemperature : Form
	{
		private int xHDDLabelName = 0;
		private int yHDDLabelName = 0;
		private int heightHDDLabelName = 0;
		private int widthHDDLabelName = 0;
		
		private int xHDDLabelValue = 0;
		private int yHDDLabelValue = 0;
		private int heightHDDLabelValue = 0;
		private int widthHDDLabelValue = 0;
		
		public DevicesTemperature()
		{
			InitializeComponent();
			
			InitializeLabaelsHDD();
						
			InitializeFormSize();
			
			// метод заполнения температуры CPU
			FillCPUTemperature();
			
			// метод заполнения температуры HDDs
			FillHDDsTemperature();
			
		}
		
		private void InitializeFormSize()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();
			
			byte countHDD = systemInfo.GetHDDCount();
			for (int i = 0; i < countHDD; i++) {
				this.Size = new Size(this.Size.Width, this.Size.Height + heightHDDLabelValue);
			}
		}
		
		private void InitializeLabaelsHDD()
		{
			xHDDLabelName = label3.Location.X;
			yHDDLabelName = label3.Location.Y;
			heightHDDLabelName = label3.Height;
			widthHDDLabelName = label3.Width;
			
			xHDDLabelValue = label4.Location.X;
			yHDDLabelValue = label4.Location.Y;
			heightHDDLabelValue = label4.Height;
			widthHDDLabelValue = label4.Width;
		}
		
		private void FillCPUTemperature()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();
			
			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о темературе CPU
			// и помещаем ее в переменную CPUTemperature
			string CPUTemperature = systemInfo.GetCPUTemperature();
			
			label2.Text = CPUTemperature;	
		}
		
		private void FillHDDsTemperature()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();
			
			// получаем массив температур HDDs
			List<byte> HDDsTemperature = systemInfo.GetHDDsTemperature();
			
			label4.Text = HDDsTemperature[0].ToString() + " °C";
			
			if(HDDsTemperature.Count > 1)
			{
				for (int i = 1; i < HDDsTemperature.Count; i++) {
					AddHDDTemperatureLabel(i + 1, HDDsTemperature[i]);
				}
			}			
		}
		
		private void AddHDDTemperatureLabel(int i, byte item)
		{
			// создаем метку для названия HDD
			Label labelName = new Label();
			labelName.Size = new Size(widthHDDLabelName, heightHDDLabelName);
			labelName.Location = new Point(xHDDLabelName, yHDDLabelName + 32);
			labelName.Text = "HDD #" + i + " Temperature";
			labelName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			Controls.Add(labelName);
			
			// создаем метку для значения температуры HDD
			Label labelValue = new Label();
			labelValue.Size = new Size(widthHDDLabelValue, heightHDDLabelValue);
			labelValue.Location = new Point(xHDDLabelValue, yHDDLabelValue + 32);
			labelValue.Text = item.ToString() + " °C";
			labelValue.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			Controls.Add(labelValue);

		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			// метод заполнения температуры CPU
			FillCPUTemperature();
			
			// метод заполнения температуры HDDs
			FillHDDsTemperature();
		}
	}
}
