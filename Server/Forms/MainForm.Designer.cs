/*
 * Created by SharpDevelop.
 * User: Uno
 * Date: 28-Apr-16
 * Time: 6:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Server
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.соединениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.пользовательскиеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.настройкаToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(221, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// настройкаToolStripMenuItem
			// 
			this.настройкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.соединениеToolStripMenuItem,
									this.пользовательскиеДанныеToolStripMenuItem});
			this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
			this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
			this.настройкаToolStripMenuItem.Text = "Настройка";
			// 
			// соединениеToolStripMenuItem
			// 
			this.соединениеToolStripMenuItem.Name = "соединениеToolStripMenuItem";
			this.соединениеToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.соединениеToolStripMenuItem.Text = "Соединение по сети";
			this.соединениеToolStripMenuItem.Click += new System.EventHandler(this.СоединениеToolStripMenuItemClick);
			// 
			// пользовательскиеДанныеToolStripMenuItem
			// 
			this.пользовательскиеДанныеToolStripMenuItem.Name = "пользовательскиеДанныеToolStripMenuItem";
			this.пользовательскиеДанныеToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.пользовательскиеДанныеToolStripMenuItem.Text = "Учетные данные";
			this.пользовательскиеДанныеToolStripMenuItem.Click += new System.EventHandler(this.ПользовательскиеДанныеToolStripMenuItemClick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(46, 41);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Жесткие диски";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(46, 70);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(128, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Сеть";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(46, 99);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(128, 23);
			this.button3.TabIndex = 3;
			this.button3.Text = "Программы";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(46, 128);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(128, 23);
			this.button4.TabIndex = 4;
			this.button4.Text = "ОЗУ";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(46, 157);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(128, 23);
			this.button5.TabIndex = 5;
			this.button5.Text = "ЦП";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(46, 186);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(128, 23);
			this.button6.TabIndex = 6;
			this.button6.Text = "Температура";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(46, 216);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(128, 23);
			this.button7.TabIndex = 7;
			this.button7.Text = "Занятость устройств";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(221, 258);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Сервер";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStripMenuItem пользовательскиеДанныеToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem соединениеToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
	}
}
