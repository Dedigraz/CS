﻿
namespace LegacyWinForms
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.label1 = new System.Windows.Forms.Label();
			this.btnGoToChristmas = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(63, 53);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(577, 202);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// btnGoToChristmas
			// 
			this.btnGoToChristmas.Location = new System.Drawing.Point(553, 95);
			this.btnGoToChristmas.Name = "btnGoToChristmas";
			this.btnGoToChristmas.Size = new System.Drawing.Size(126, 23);
			this.btnGoToChristmas.TabIndex = 2;
			this.btnGoToChristmas.Text = "Go To Christmas";
			this.btnGoToChristmas.UseVisualStyleBackColor = true;
			this.btnGoToChristmas.Click += new System.EventHandler(this.btnGoToChristmas_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(580, 273);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnGoToChristmas);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.monthCalendar1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnGoToChristmas;
		private System.Windows.Forms.TextBox textBox1;
	}
}
