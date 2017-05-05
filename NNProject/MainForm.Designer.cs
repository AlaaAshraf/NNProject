namespace NNProject
{
	partial class MainForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.epochs = new System.Windows.Forms.TextBox();
			this.eta = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.accuracy = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.mlpButton = new System.Windows.Forms.Button();
			this.confusionMatrix = new System.Windows.Forms.DataGridView();
			this.Closing = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.testSample = new System.Windows.Forms.Button();
			this.RBFbtn = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.HiddenLayerSizetxt = new System.Windows.Forms.TextBox();
			this.MeanSquareErrorThresholdtxt = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.confusionMatrix)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Epochs";
			// 
			// epochs
			// 
			this.epochs.Location = new System.Drawing.Point(13, 25);
			this.epochs.Name = "epochs";
			this.epochs.Size = new System.Drawing.Size(76, 20);
			this.epochs.TabIndex = 1;
			// 
			// eta
			// 
			this.eta.Location = new System.Drawing.Point(107, 25);
			this.eta.Name = "eta";
			this.eta.Size = new System.Drawing.Size(76, 20);
			this.eta.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(109, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Learning Rate";
			// 
			// accuracy
			// 
			this.accuracy.Location = new System.Drawing.Point(107, 75);
			this.accuracy.Name = "accuracy";
			this.accuracy.ReadOnly = true;
			this.accuracy.Size = new System.Drawing.Size(76, 20);
			this.accuracy.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Accuracy";
			// 
			// mlpButton
			// 
			this.mlpButton.Location = new System.Drawing.Point(16, 128);
			this.mlpButton.Name = "mlpButton";
			this.mlpButton.Size = new System.Drawing.Size(76, 23);
			this.mlpButton.TabIndex = 6;
			this.mlpButton.Text = "MLP";
			this.mlpButton.UseVisualStyleBackColor = true;
			this.mlpButton.Click += new System.EventHandler(this.mlpButton_Click);
			// 
			// confusionMatrix
			// 
			this.confusionMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.confusionMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Closing,
            this.Column2,
            this.Column3,
            this.Column4});
			this.confusionMatrix.Location = new System.Drawing.Point(257, 60);
			this.confusionMatrix.Name = "confusionMatrix";
			this.confusionMatrix.Size = new System.Drawing.Size(243, 150);
			this.confusionMatrix.TabIndex = 7;
			// 
			// Closing
			// 
			this.Closing.HeaderText = "Closing";
			this.Closing.Name = "Closing";
			this.Closing.Width = 50;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Down";
			this.Column2.Name = "Column2";
			this.Column2.Width = 50;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Front";
			this.Column3.Name = "Column3";
			this.Column3.Width = 50;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Left";
			this.Column4.Name = "Column4";
			this.Column4.Width = 50;
			// 
			// testSample
			// 
			this.testSample.Location = new System.Drawing.Point(62, 187);
			this.testSample.Name = "testSample";
			this.testSample.Size = new System.Drawing.Size(75, 23);
			this.testSample.TabIndex = 8;
			this.testSample.Text = "Test Sample";
			this.testSample.UseVisualStyleBackColor = true;
			this.testSample.Click += new System.EventHandler(this.testSample_Click);
			// 
			// RBFbtn
			// 
			this.RBFbtn.Location = new System.Drawing.Point(107, 128);
			this.RBFbtn.Name = "RBFbtn";
			this.RBFbtn.Size = new System.Drawing.Size(76, 23);
			this.RBFbtn.TabIndex = 9;
			this.RBFbtn.Text = "RBF";
			this.RBFbtn.UseVisualStyleBackColor = true;
			this.RBFbtn.Click += new System.EventHandler(this.RBFbtn_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(200, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(179, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "RBF number of hidden layer neurons";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(385, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(162, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "RBF mean square error threshold";
			// 
			// HiddenLayerSizetxt
			// 
			this.HiddenLayerSizetxt.Location = new System.Drawing.Point(203, 25);
			this.HiddenLayerSizetxt.Name = "HiddenLayerSizetxt";
			this.HiddenLayerSizetxt.Size = new System.Drawing.Size(176, 20);
			this.HiddenLayerSizetxt.TabIndex = 12;
			// 
			// MeanSquareErrorThresholdtxt
			// 
			this.MeanSquareErrorThresholdtxt.Location = new System.Drawing.Point(388, 25);
			this.MeanSquareErrorThresholdtxt.Name = "MeanSquareErrorThresholdtxt";
			this.MeanSquareErrorThresholdtxt.Size = new System.Drawing.Size(159, 20);
			this.MeanSquareErrorThresholdtxt.TabIndex = 13;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 238);
			this.Controls.Add(this.MeanSquareErrorThresholdtxt);
			this.Controls.Add(this.HiddenLayerSizetxt);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.RBFbtn);
			this.Controls.Add(this.testSample);
			this.Controls.Add(this.confusionMatrix);
			this.Controls.Add(this.mlpButton);
			this.Controls.Add(this.accuracy);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.eta);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.epochs);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "Main";
			((System.ComponentModel.ISupportInitialize)(this.confusionMatrix)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox epochs;
		private System.Windows.Forms.TextBox eta;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox accuracy;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button mlpButton;
		private System.Windows.Forms.DataGridView confusionMatrix;
		private System.Windows.Forms.DataGridViewTextBoxColumn Closing;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.Button testSample;
		private System.Windows.Forms.Button RBFbtn;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox HiddenLayerSizetxt;
		private System.Windows.Forms.TextBox MeanSquareErrorThresholdtxt;
	}
}