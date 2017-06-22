namespace CSVFileImportUtility
{
	partial class Import
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
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.cbApplication = new System.Windows.Forms.ComboBox();
			this.lblApplication = new System.Windows.Forms.Label();
			this.lblFile = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(405, 103);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(152, 104);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(257, 20);
			this.textBox1.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(88, 179);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(101, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Import CSV file";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(90, 223);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(216, 179);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(114, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "Import JSON";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(356, 180);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(170, 22);
			this.button4.TabIndex = 5;
			this.button4.Text = "Convert CSV to JSON";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// cbApplication
			// 
			this.cbApplication.FormattingEnabled = true;
			this.cbApplication.Location = new System.Drawing.Point(152, 53);
			this.cbApplication.Name = "cbApplication";
			this.cbApplication.Size = new System.Drawing.Size(257, 21);
			this.cbApplication.TabIndex = 6;
			this.cbApplication.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ConcatComboValues);
			// 
			// lblApplication
			// 
			this.lblApplication.AutoSize = true;
			this.lblApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblApplication.Location = new System.Drawing.Point(27, 53);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(98, 17);
			this.lblApplication.TabIndex = 7;
			this.lblApplication.Text = "Application :";
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFile.Location = new System.Drawing.Point(76, 107);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(49, 17);
			this.lblFile.TabIndex = 8;
			this.lblFile.Text = "File : ";
			// 
			// Import
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(685, 262);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.lblApplication);
			this.Controls.Add(this.cbApplication);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "Import";
			this.Text = "Import";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ComboBox cbApplication;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.Label lblFile;


	}
}