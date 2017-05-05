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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mlpButton_Click(object sender, EventArgs e)
        {
            methodSelected = true;
            int Epochs = Convert.ToInt32(epochs.Text);
            double Eta = Convert.ToDouble(eta.Text);
            BLP = new MultilayerPerceptron(Epochs, Eta);
            BLP.Train();
            double Accuracy = BLP.Test();
            accuracy.Text = Accuracy.ToString();

            //confusionMatrix.Rows[0].Cells.Add();
            confusionMatrix.Rows.Add(4);
            for (int i = 0; i < 4; i++)
                confusionMatrix.Rows[0].Cells[i].Value = BLP.ClosingCount[i];
            for (int i = 0; i < 4; i++)
                confusionMatrix.Rows[1].Cells[i].Value = BLP.DownCount[i];
            for (int i = 0; i < 4; i++)
                confusionMatrix.Rows[2].Cells[i].Value = BLP.FrontCount[i];
            for (int i = 0; i < 4; i++)
                confusionMatrix.Rows[3].Cells[i].Value = BLP.LeftCount[i];


            /*confusionMatrix.Rows.Add(BP.downCount);
            confusionMatrix.Rows.Add(BP.frontCount);
            confusionMatrix.Rows.Add(BP.leftCount);*/
        }

        private void testSample_Click(object sender, EventArgs e)
        {
            Test testForm = new Test(BLP,RBF, methodSelected);
            testForm.Show();
        }
    }
}
