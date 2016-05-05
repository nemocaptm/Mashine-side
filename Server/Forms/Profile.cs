
using System;
using System.Drawing;
using System.Windows.Forms;

using Server.Classes;

namespace Server.Forms
{
	public partial class Profile : Form
	{
		public Profile()
		{
			InitializeComponent();
			
			// если был сохранен уникальный ID, то заполняем textBox
			if(ProgramState.mobileID != string.Empty)
			{
				textBox1.Text = ProgramState.mobileID;
			}
		}
		
		// сохранение введенного уникального идентификатора
		void Button1Click(object sender, EventArgs e)
		{
			ProgramState.mobileID = textBox1.Text;
			Close();
		}
	}
}
