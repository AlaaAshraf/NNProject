using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNProject
{
    class NeuralNetwork
    {
        //List of lists containing all neurons in the network
        List<List<Neuron>> neurons;
        bool bias;
        DataSet TrainingData, TestingData;
        /// <summary>
        /// Builds the Neural network, takes 3 parameters: 1- List containing number of nuerons per layer
        ///                                                2- Number of Input Features
        ///                                                3- Boolean indicating the existence of bias
        /// </summary>
        /// <param name="NetworkStructure"></ListOfNumberOfNeuronsPerLayer>
        /// <param name="numberOfInputFeatures"></NumberOfInputFeatures>
        /// <param name="bias"></BiasBoolean>
        public NeuralNetwork(List<int> NetworkStructure, int numberOfInputFeatures, bool b, string dataPath)
        {
            loadData(dataPath);
            bias = b;
            neurons = new List<List<Neuron>>();
            for (int i = 0; i < NetworkStructure.Count; i++)
            {
                List<Neuron> layer = new List<Neuron>();
                for (int j = 0; j < NetworkStructure[i]; j++)
                {
                    Neuron n;
                    if (i == 0)
                        n = new Neuron(numberOfInputFeatures, bias);
                    else n = new Neuron(NetworkStructure[i - 1], bias);
                    layer.Add(n);
                }
                neurons.Add(layer);
            }
        }

        public void train()
        {
            #region ForwardStep
            for (int i=0; i<TrainingData.closing.Count; i++)// Closing dataset
            {
                List<double> InputOfLayer;
                List<double> OutputOfLayer = new List<double>();
                for (int j=0; j<neurons.Count; j++)// Number of layers
                {
                    if (j == 0)
                    {
                        for (int k = 0; k < neurons[j].Count; ++k)// Number of neurons
                        {
                            OutputOfLayer.Add(TrainingData.closing[i][k]);
                        }
                    }
                    else
                    {
                        InputOfLayer = OutputOfLayer;
                        OutputOfLayer = new List<double>();
                        for (int k = 0; k < neurons[j].Count; ++k)// Number of neurons
                        {
                            OutputOfLayer.Add(neurons[j][k].fireSigmoid(InputOfLayer));
                        }
                    }
                }
            }
            #endregion

            #region BackwardStep

            #endregion
        }

        void loadData(string dataPath)
        {
            string path = dataPath + "Training Dataset\\";
            TrainingData = new DataSet(path);
            path = dataPath + "Testing Dataset\\";
            TestingData = new DataSet(path);

        }
    }
}
