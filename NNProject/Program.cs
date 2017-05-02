using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            string dataPath = "E:\\FCIS\\CS\\Neural Networks\\Project\\Project\\Dataset\\";
            List<int> NetworkStructure = new List<int>(new int[] {5,4, /* Add a number of neurons for each hidden layer */ 4 });
            int numberOfInputFeatures = 19;
            NeuralNetwork nn = new NeuralNetwork(NetworkStructure, numberOfInputFeatures, true, dataPath);
            nn.train();
            double accuracy = nn.test();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
    }
}
