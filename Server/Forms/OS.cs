
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Server.AdditionalClasses;

namespace Server.Forms
{
	public partial class OS : Form
	{
		public OS()
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
			// собирает инфрормацию об  ОС
			// и помещаем ее в переменную OSInfo
			List<Param> OSInfo = systemInfo.GetOperatingSystem();
			
			// добавялем каждый элемент в dataGridView
			foreach (Param row in OSInfo) {
				AddElementToDataGridView(row);
			}
		}
		
		private void DataGridViewInitialize()
		{
			var column1 = new DataGridViewColumn();
			column1.HeaderText = "Параметр"; 
			column1.Name = "param";
			column1.CellTemplate = new DataGridViewTextBoxCell();
			
			var column2 = new DataGridViewColumn();
			column2.HeaderText = "Значение";
			column2.Name = "value";
			column2.CellTemplate = new DataGridViewTextBoxCell();
			
			dataGridView1.Columns.Add(column1);
			dataGridView1.Columns.Add(column2);
		}
		
		private void AddElementToDataGridView(Param element)
		{
			var row = new DataGridViewRow();
			
			// пустая сточка означет разделитель между 
			// отдельными интерфейсами
			if(element.ParamName == string.Empty)
			{
				dataGridView1.Rows.Add(row);
				return;	
			}
			
			// добавляем первую ячейку
			var cell1 = new DataGridViewTextBoxCell() 
			{
			    Value = element.ParamName + ":"
			};
			cell1.Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
			row.Cells.Add(cell1);
			
			// добавляем вторую ячейку
			var cell2 = new DataGridViewTextBoxCell() 
			{
			    Value = element.ParamValue
			};
			row.Cells.Add(cell2);

			// добавляем строчку в dataGridView
			dataGridView1.Rows.Add(row);
		}
	}
}
