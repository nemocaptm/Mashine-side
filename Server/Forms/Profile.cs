
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
			if(ProgramState.UnicalID != string.Empty)
			{
				textBox1.Text = ProgramState.UnicalID;
			}
		}
		
		// сохранение введенного уникального идентификатора
		void Button1Click(object sender, EventArgs e)
		{
			ProgramState.UnicalID = textBox1.Text;
			Close();
		}
	}
}
