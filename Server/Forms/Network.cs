
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Server.AdditionalClasses;

namespace Server.Forms
{
	public partial class Network : Form
	{
		public Network()
		{
			InitializeComponent();
			
			// инициализируем DataGridView1
			DataGridViewInitialize1();
			
			// инициализируем DataGridView2
			DataGridViewInitialize2();
			
			// заполняем данными DataGridView1
			FillDataGridView1();
			
			// заполняем данными DataGridView2
			FillDataGridView2();
		}
		
		private void FillDataGridView1()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();

			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о сетевых интерфейсах
			// и помещаем ее в переменную networkInfo
			List<Param> networkInfo = systemInfo.GetNetworkInfo(checkBox1.Checked);

			// добавялем каждый элемент в dataGridView1
			foreach (Param row in networkInfo) {
				AddElementToDataGridView1(row);
			}
		}
		
		private void FillDataGridView2()
		{
			//получаем объект типа SystemInfo
			SystemInfo systemInfo = new SystemInfo();

			// вызываем метод объекта systemInfo, который
			// собирает инфрормацию о индексах сетевых интерфейсах
			// и помещаем ее в переменную interfacesIndexs
	
			List<Route> interfacesIndexs = systemInfo.GetStaticRoutes();
			
			// добавялем каждый элемент в dataGridView2
			foreach (Route route in interfacesIndexs) {
				AddElementToDataGridView2(route.Destination, route.Mask, route.Gateway, route.Interface, route.Metric);
			}

		}
		
		private void DataGridViewInitialize1()
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
		
		private void DataGridViewInitialize2()
		{
			var column1 = new DataGridViewColumn();
			column1.HeaderText = "Назначение"; 
			column1.Name = "Destination";
			column1.CellTemplate = new DataGridViewTextBoxCell();
			
			var column2 = new DataGridViewColumn();
			column2.HeaderText = "Маска";
			column2.Name = "Mask";
			column2.CellTemplate = new DataGridViewTextBoxCell();
			
			var column3 = new DataGridViewColumn();
			column3.HeaderText = "Шлюз";
			column3.Name = "Gateway";
			column3.CellTemplate = new DataGridViewTextBoxCell();
			
			var column4 = new DataGridViewColumn();
			column4.HeaderText = "Интерфейс";
			column4.Name = "Interface";
			column4.CellTemplate = new DataGridViewTextBoxCell();
			
			var column5 = new DataGridViewColumn();
			column5.HeaderText = "Метрика";
			column5.Name = "Metric";
			column5.CellTemplate = new DataGridViewTextBoxCell();
			
			dataGridView2.Columns.Add(column1);
			dataGridView2.Columns.Add(column2);
			dataGridView2.Columns.Add(column3);
			dataGridView2.Columns.Add(column4);
			dataGridView2.Columns.Add(column5);
		}
		
		private void AddElementToDataGridView1(Param element)
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
		
		private void AddElementToDataGridView2(string _destination, string _mask, string _gateway, string _interface, string _metric)
		{
			var row = new DataGridViewRow();			
			
			// добавляем первую ячейку
			var cell1 = new DataGridViewTextBoxCell() 
			{
			    Value = _destination
			};
			row.Cells.Add(cell1);
			
			// добавляем вторую ячейку
			var cell2 = new DataGridViewTextBoxCell() 
			{
			    Value = _mask
			};
			row.Cells.Add(cell2);

			// добавляем третью ячейку
			var cell3 = new DataGridViewTextBoxCell() 
			{
			    Value = _gateway
			};
			row.Cells.Add(cell3);
			
			// добавляем четвертую ячейку
			var cell4 = new DataGridViewTextBoxCell() 
			{
			    Value = _interface
			};
			row.Cells.Add(cell4);
			
			// добавляем пятую ячейку
			var cell5 = new DataGridViewTextBoxCell() 
			{
			    Value = _metric
			};
			row.Cells.Add(cell5);
			
			// добавляем строчку в dataGridView2
			dataGridView2.Rows.Add(row);
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			//очищаем DataGridView1
			dataGridView1.Rows.Clear();
			
			// заполняем данными DataGridView1
			FillDataGridView1();
			
		}
	}
}
