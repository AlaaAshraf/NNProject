﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace NNProject
{
    public partial class Test : Form
    {
        MultilayerPerceptron MLP;
        RadialBasisFunction RBF;
        Dictionary<int, string> Output;
        bool MethodSelected;
        public Test(MultilayerPerceptron m, RadialBasisFunction f, bool MethodSelected)
        {
            InitializeComponent();
            MLP = m;
            RBF = f;
            this.MethodSelected = MethodSelected;
            Output = new Dictionary<int, string>();
            Output[0] = "Closing";
            Output[1] = "Down";
            Output[2] = "Front";
            Output[3] = "Left";
        }

        private void openImg_Click(object sender, EventArgs e)
        {
			try
			{
				string fn = "";
				Bitmap B = null;

				openFileDialog1.ShowDialog();
				fn = openFileDialog1.FileName;
				B = PGMUtil.ToBitmap(fn);
				pictureBox1.Size = B.Size;
				pictureBox1.Image = B;
				//this.Size = B.Size;
				int s = fn.LastIndexOf('\\') + 1;
				int x;
				fn = fn.Replace(@"\\", @"\'");
				if (MethodSelected)
				{
					if (MLP.NN.TrainingData.Pics.ContainsKey(fn)) x = MLP.TestPoint(MLP.NN.TrainingData.Pics[fn]);
					else x = MLP.TestPoint(MLP.NN.TestingData.Pics[fn]);

					outTextBox.Text = Output[x];
				}
				else
				{
					if (RBF.NN.TrainingData.Pics.ContainsKey(fn)) x = RBF.TestSample(RBF.NN.TrainingData.Pics[fn]);
					else x = RBF.TestSample(RBF.NN.TestingData.Pics[fn]);

					outTextBox.Text = Output[x];
				}
			}
			catch (Exception Ex)
			{
				MessageBox.Show(Ex.ToString());
			}
        }
    }
}
