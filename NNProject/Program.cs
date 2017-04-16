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
            string path = "E:\\FCIS\\CS\\Neural Networks\\Project\\Project\\Dataset\\Training Dataset\\";
            DataSet TrainingData = new DataSet(path);
            path = "E:\\FCIS\\CS\\Neural Networks\\Project\\Project\\Dataset\\Testing Dataset\\";
            DataSet TestingData = new DataSet(path);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
        }
    }
}
