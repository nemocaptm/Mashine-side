
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Server.AdditionalClasses;

namespace Server.Forms
{
	public partial class Busy : Form
	{
		public Busy()
		{
			InitializeComponent();
			
			DataGridViewInitialize();
			
			// производим начальное заполнение значениями			
			RefreshValues();
		}
		
		private void RefreshValues()
		{
			// вызываем метод заполнения занятости CPU
			FillBusyCPU();
			
			// вызываем метод заполнения занятости оперативной памяти
			FillBusyRAM();
			
			// вызываем метод заполнения занятости жестких дисков
			FillBusyHDDs();			
		}
		
		private void DataGridViewInitialize()
		{
			var column1 = new DataGridViewColumn();
			column1.HeaderText = "Метка"; 
			column1.Name = "Name";
			column1.CellTemplate = new DataGridViewTextBoxCell();
			
			var column2 = new DataGridViewColumn();
			column2.HeaderText = "Общий размер";
			column2.Name = "Size";
			column2.CellTemplate = new DataGridViewTextBoxCell();
			
			var column3 = new DataGridViewColumn();
			column3.HeaderText = "Занято";
			column3.Name = "Busy";
			column3.CellTemplate = new DataGridViewTextBoxCell();
			
			dataGridView1.Columns.Add(column1);
			dataGridView1.Columns.Add(column2);
			dataGridView1.Columns.Add(column3);
		}
		
		private void FillBusyCPU()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();
			
			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о занятости CPU
			// и помещаем ее в переменную CPUTemperature
			string busyCPU = systemInfo.GetBusyCPU();
			
			label2.Text = busyCPU;	
		}
		
		private void FillBusyRAM()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();
			
			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о занятости ОЗУ
			// и помещаем ее в переменную busyRAM
			string busyRAM = systemInfo.GetBusyRAM();
			
			label4.Text = busyRAM;	
		}
		
		private void FillBusyHDDs()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();

			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о локальных дисках
			// и помещаем ее в переменную hddInfo
			List<HDDParam> hddInfo = systemInfo.GetBusyHDDs();
			
			// добавялем каждый элемент в dataGridView
			foreach (HDDParam row in hddInfo) {
				AddElementToDataGridView(row);
			}
		}
		
		private void AddElementToDataGridView(HDDParam element)
		{
			var row = new DataGridViewRow();
			
			// пустая сточка означет разделитель между 
			// отдельными локальными дисками
			if(element.Label == string.Empty)
			{
				dataGridView1.Rows.Add(row);
				return;	
			}
			
			// добавляем первую ячейку
			var cell1 = new DataGridViewTextBoxCell() 
			              {
			              	Value = element.Label
			              };
			cell1.Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
			row.Cells.Add(cell1);
			
			// добавляем вторую ячейку
			var cell2 = new DataGridViewTextBoxCell() 
			              {
			              	Value = element.Size
			              };
			row.Cells.Add(cell2);

			// добавляем третью ячейку
			var cell3 = new DataGridViewTextBoxCell() 
			              {
			              	Value = element.Busy
			              };
			row.Cells.Add(cell3);
			
			// добавляем строчку в dataGridView
			dataGridView1.Rows.Add(row);
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
			
			RefreshValues();
		}
	}
}
