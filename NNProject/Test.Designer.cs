namespace NNProject
{
	partial class Test
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.outTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openImg = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(228, 120);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// outTextBox
			// 
			this.outTextBox.Location = new System.Drawing.Point(215, 330);
			this.outTextBox.Name = "outTextBox";
			this.outTextBox.ReadOnly = true;
			this.outTextBox.Size = new System.Drawing.Size(100, 20);
			this.outTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(153, 333);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Output";
			// 
			// openImg
			// 
			this.openImg.Location = new System.Drawing.Point(195, 367);
			this.openImg.Name = "openImg";
			this.openImg.Size = new System.Drawing.Size(75, 23);
			this.openImg.TabIndex = 3;
			this.openImg.Text = "Open Image";
			this.openImg.UseVisualStyleBackColor = true;
			this.openImg.Click += new System.EventHandler(this.openImg_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// Test
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 436);
			this.Controls.Add(this.openImg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.outTextBox);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Test";
			this.Text = "Test";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox outTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button openImg;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}