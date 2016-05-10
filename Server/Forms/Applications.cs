
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Server.Forms
{

	public partial class Applications : Form
	{
		public Applications()
		{

			InitializeComponent();

			// инициализируем DataGridView
			DataGridViewInitialize();
			
			// заполняем данными DataGridView
			FillDataGridView();
		}
		
		private void FillDataGridView()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();

			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о установленных программах
			// и помещаем ее в переменную ApplicationsInfo
			List<string> ApplicationsInfo = systemInfo.GetApplicationsInfo();
			
			// добавялем каждый элемент в dataGridView
			foreach (string row in ApplicationsInfo) {
				AddElementToDataGridView(row);
			}
		}
		
		private void DataGridViewInitialize()
		{
			var column1 = new DataGridViewColumn();
			column1.HeaderText = "Название"; 
			column1.Name = "Name";
			column1.CellTemplate = new DataGridViewTextBoxCell();
			
			dataGridView1.Columns.Add(column1);
		}
		
		private void AddElementToDataGridView(string ProgramName)
		{
			var row = new DataGridViewRow();
			
			// добавляем ячейку
			var cell1 = new DataGridViewTextBoxCell() 
			{
			    Value = ProgramName
			};
			row.Cells.Add(cell1);

			// добавляем строчку в dataGridView
			dataGridView1.Rows.Add(row);
		}
	}
}
