using System;
using System.Windows.Forms;

namespace NNProject
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string DataSetPath = "A:\\Work\\FCIS\\Years\\4\\Terms\\2\\Neural Networks\\Project\\Dataset\\";
			MultilayerPerceptron BP = new MultilayerPerceptron(1000, 0.01, DataSetPath);
			BP.Train();
			double Accuracy = BP.Test();

			RadialBasisFunction RBF = new RadialBasisFunction(5, 1000, 0.01, 0.000000001, DataSetPath);
			RBF.Train();
			RBF.Test();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
