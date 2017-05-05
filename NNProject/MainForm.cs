using System;
using System.Windows.Forms;

namespace NNProject
{
	public partial class MainForm : Form
	{
		MultilayerPerceptron BLP;
		RadialBasisFunction RBF;
		bool methodSelected;
		public MainForm()
		{
			InitializeComponent();
			confusionMatrix.Rows.Add(4);
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void mlpButton_Click(object sender, EventArgs e)
		{
			string DataSetPath = "A:\\Work\\FCIS\\Years\\4\\Terms\\2\\Neural Networks\\Project\\Dataset\\";
			int Epochs = 0;
			double Eta = 0;

			try
			{
				Epochs = Convert.ToInt32(epochs.Text);
				Eta = Convert.ToDouble(eta.Text);
			}
			catch (Exception Ex)
			{
				MessageBox.Show(Ex.ToString());
			}
			BLP = new MultilayerPerceptron(Epochs, Eta, DataSetPath);
			BLP.Train();
			double Accuracy = BLP.Test();
			accuracy.Text = Accuracy.ToString();

			for (int i = 0; i < 4; i++)
				confusionMatrix.Rows[0].Cells[i].Value = BLP.ClosingCount[i];
			for (int i = 0; i < 4; i++)
				confusionMatrix.Rows[1].Cells[i].Value = BLP.DownCount[i];
			for (int i = 0; i < 4; i++)
				confusionMatrix.Rows[2].Cells[i].Value = BLP.FrontCount[i];
			for (int i = 0; i < 4; i++)
				confusionMatrix.Rows[3].Cells[i].Value = BLP.LeftCount[i];

			methodSelected = true;
		}

		private void RBFbtn_Click(object sender, EventArgs e)
		{
			string DataSetPath = "A:\\Work\\FCIS\\Years\\4\\Terms\\2\\Neural Networks\\Project\\Dataset\\";
			int HiddenLayerSize = 0;
			int Epochs = 0;
			double Eta = 0;
			double MeanSquareErrorThreshold = 0;

			try
			{
				HiddenLayerSize = Convert.ToInt32(HiddenLayerSizetxt.Text);
				Epochs = Convert.ToInt32(epochs.Text);
				Eta = Convert.ToDouble(eta.Text);
				MeanSquareErrorThreshold = Convert.ToDouble(MeanSquareErrorThresholdtxt.Text);
			}
			catch (Exception Ex)
			{
				MessageBox.Show(Ex.ToString());
			}

			RBF = new RadialBasisFunction(HiddenLayerSize, Epochs, Eta, MeanSquareErrorThreshold, DataSetPath);
			RBF.Train();
			RBF.Test();

			for (int i = 0; i < 4; ++i)
				for (int j = 0; j < 4; ++j)
					confusionMatrix.Rows[i].Cells[j].Value = RBF.ConfusionMatrix[i][j];

			accuracy.Text = RBF.Accuracy.ToString();

			methodSelected = false;
		}

		private void testSample_Click(object sender, EventArgs e)
		{
			Test testForm = new Test(BLP, RBF, methodSelected);
			testForm.Show();
		}

	}
}