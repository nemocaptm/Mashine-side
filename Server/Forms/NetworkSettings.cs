using System;
using System.Drawing;
using System.Windows.Forms;

using Server.Classes;
using Server.AdditionalClasses;

namespace Server.Forms
{

	public partial class NetworkSettings : Form
	{
		public NetworkSettings()
		{
			InitializeComponent();
			
			// проверяем настройки, имеет ли сервер доступ в интернет
			if(ProgramState.ServerHaveInternet)
			{
				radioButton1.Checked = true;
				radioButton2.Checked = false;
				
				textBox1.Text = string.Empty;
				textBox2.Text = string.Empty;
				textBox3.Text = string.Empty;
				textBox4.Text = string.Empty;
			}
			else
			{
				radioButton1.Checked = false;
				radioButton2.Checked = true;
				
				// конвертируем IP в string массив
				string[] ipParts = IP.StringToIp(ProgramState.IP);
				
				// заполняем отдельные части IP в textBox-ы
				if(ipParts.Length == 4) 
				{
					textBox1.Text = ipParts[0];
					textBox2.Text = ipParts[1];
					textBox3.Text = ipParts[2];
					textBox4.Text = ipParts[3];
				}
				
				
			}
		}
		
		// если выбрано, что сервер имеет доступ к интернету
		void RadioButton1CheckedChanged(object sender, EventArgs e)
		{
			// активируем textBox-ы
			RadioButtonsReduction(true);
			
			//
		}
		
		// если выбрано, что сервер НЕ имеет доступа к интернету
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			// блокируем ввод в textBox-ы
			RadioButtonsReduction(false);
			
		}
		
		// метод, для изменения состояния textBox-ов
		private void RadioButtonsReduction(bool state)
		{
			textBox1.ReadOnly = state;
			textBox2.ReadOnly = state;
			textBox3.ReadOnly = state;
			textBox4.ReadOnly = state;
		}
	}
}
