using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private ProcessStartInfo theProcess;
		public Test(MultilayerPerceptron MLP, RadialBasisFunction RBF, bool MethodSelected)
		{
			InitializeComponent();
			this.MLP = MLP;
			this.RBF = RBF;
			this.MethodSelected = MethodSelected;
			Output = new Dictionary<int, string>();
			Output[0] = "Closing";
			Output[1] = "Down";
			Output[2] = "Front";
			Output[3] = "Left";
			theProcess = new ProcessStartInfo("mspaint.exe");
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


				//0 closing close, 1 down minimize, 2 front open, 3 left maximize

				if (x == 1)
				{
					theProcess.WindowStyle = ProcessWindowStyle.Minimized;
				}
				else if (x == 2)
				{
					theProcess.WindowStyle = ProcessWindowStyle.Normal;
				}
				else
				{
					theProcess.WindowStyle = ProcessWindowStyle.Maximized;
				}
				// Retrieve the app's exit code
				Process p = Process.Start(theProcess);
				if (x == 0)
				{
					System.Threading.Thread.Sleep(1000);
					p.Kill();
				}
				else
					p.WaitForExit();

			}
			catch (Exception Ex)
			{
				MessageBox.Show(Ex.ToString());
			}
		}
	}
}
