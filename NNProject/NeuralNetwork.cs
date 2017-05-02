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
        Random random;
        bool bias;
        DataSet TrainingData, TestingData;
        List<int> NetworkStructure;
        double eta;
        int epochs;
        List<List<List<double>>> error;
        List<List<List<double>>> outputPerLayer;
        List<List<List<double>>> derivativePerLayer;
        /// <summary>
        /// Builds the Neural network, takes 3 parameters: 1- List containing number of nuerons per layer
        ///                                                2- Number of Input Features
        ///                                                3- Boolean indicating the existence of bias
        /// </summary>
        /// <param name="NetworkStructure"></ListOfNumberOfNeuronsPerLayer>
        /// <param name="numberOfInputFeatures"></NumberOfInputFeatures>
        /// <param name="bias"></BiasBoolean>
        public NeuralNetwork(List<int> NetworkStruct, int numberOfInputFeatures, bool b, string dataPath)
        {
            random = new Random();
            epochs = 1000;
            eta = 0.01;
            NetworkStructure = NetworkStruct;
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
                        n = new Neuron(numberOfInputFeatures, bias, random);
                    else n = new Neuron(NetworkStructure[i - 1], bias, random);
                    layer.Add(n);
                }
                neurons.Add(layer);
            }
        }

        public void train()
        {
            int count = TrainingData.closing.Count;
            List<double> closingOutput = new List<double>(new double[] { 1, 0, 0, 0 }), downOutput = new List<double>(new double[] { 0, 1, 0, 0 }), frontOutput = new List<double>(new double[] { 0, 0, 1, 0 }), leftOutput = new List<double>(new double[] { 0, 0, 0, 1 });
            for (int j = 0; j < epochs; j++)
            {
                outputPerLayer = new List<List<List<double>>>();
                derivativePerLayer = new List<List<List<double>>>();
                error = new List<List<List<double>>>();
                int k = 0;
                for (int i = 0; i < count; i++)
                {
                    
                    trainPoint(TrainingData.closing[i], closingOutput, k);
                    BackProp(TrainingData.closing[i], closingOutput, k);
                    k++;
                    trainPoint(TrainingData.down[i], downOutput, k);
                    BackProp(TrainingData.down[i], downOutput, k);
                    k++;
                    trainPoint(TrainingData.front[i], frontOutput,k);
                    BackProp(TrainingData.front[i], frontOutput, k);
                    k++;
                    trainPoint(TrainingData.left[i], leftOutput, k);
                    BackProp(TrainingData.left[i], leftOutput, k);
                    k++;
                }
                /*k = 0;
                for (int i = 0; i < count; i++)
                {
                    BackProp(TrainingData.closing[i], closingOutput, k);
                    k++;
                    BackProp(TrainingData.down[i], downOutput, k);
                    k++;
                    BackProp(TrainingData.front[i], frontOutput, k);
                    k++;
                    BackProp(TrainingData.left[i], leftOutput, k);
                    k++;
                }*/
            }
        }


        void trainPoint(List<double> dataPoint, List<double> desiredOutput, int index)
        {
            List<double> layerInput;
            List<double> layerOutput = new List<double>();
            List<double> layerDerivative = new List<double>();
            #region FeedForward
            layerInput = dataPoint;
            outputPerLayer.Add(new List<List<double>>());
            derivativePerLayer.Add(new List<List<double>>());
            for (int j = 0; j < neurons.Count; j++)// Number of layers
            {

                for (int k = 0; k < neurons[j].Count; ++k)// Number of neurons
                {
                    layerOutput.Add(neurons[j][k].fireSigmoid(layerInput).Item1);
                    layerDerivative.Add(neurons[j][k].fireSigmoid(layerInput).Item2);
                }
                layerInput = layerOutput;
                //if(j!=0)
                outputPerLayer[index].Add(layerOutput);
                derivativePerLayer[index].Add(layerDerivative);
                layerOutput = new List<double>();
                layerDerivative = new List<double>();


            }
            //outputPerLayer.Add(layerOutput);
            #endregion

            
        }

        void BackProp(List<double> dataPoint, List<double> desiredOutput, int index)
        {
            #region BackProp
            error.Add(new List<List<double>>());
            int temp = 0;
            if (bias)
                temp = 1;
            for (int i = 0; i < NetworkStructure.Count; i++)
            {
                error[index].Add(new List<double>(new double[NetworkStructure[i] + temp]));
            }

            for (int i = neurons.Count - 1; i >= 0; i--)
            {

                for (int j = 0; j < neurons[i].Count; j++)
                {
                    if (i == neurons.Count - 1)
                    {
                        error[index][i][j] = desiredOutput[j] - outputPerLayer[index][i][j];
                    }
                    else
                    {
                        for (int k = 0; k < neurons[i + 1].Count; k++)
                        {
                            error[index][i][j] += neurons[i + 1][k].weights[j]*error[index][i+1][k];
                        }
                        //error[index][i][j] *= error[index][i + 1][j];
                    }

                    //error[index][i][j] *= outputPerLayer[index][i][j] * (1 - outputPerLayer[index][i][j]);
                    error[index][i][j] *= derivativePerLayer[index][i][j];
                }

            }
            outputPerLayer[index].Insert(0, new List<double>());
            for (int i = 0; i < dataPoint.Count; i++)
                outputPerLayer[index][0].Add(dataPoint[i]);
            //outputPerLayer.Insert(0, dataPoint);
            if (bias)
            {
                for (int i = 0; i < outputPerLayer[index].Count; i++)
                    outputPerLayer[index][i].Add(1);
            }
            for (int i = 0; i < neurons.Count; i++)
            {
                for (int j = 0; j < neurons[i].Count; j++)
                {
                    double delta = 0;
                    for (int k = 0; k < neurons[i][j].weights.Count; k++)
                    {
                        //delta += neurons[i][j].weights[k] * outputPerLayer[index][i][k];
                        delta = eta * error[index][i][j] * outputPerLayer[index][i][k];
                        neurons[i][j].weights[k] += delta;
                    }
                  /*  delta *= eta/(60);
                    for (int k = 0; k < neurons[i][j].weights.Count; k++)
                    {
                        neurons[i][j].weights[k] += delta;
                    }
                    */
                }
            }

            #endregion
        }

        public double test()
        {
            double total = 0, totalTrue = 0;
            List<double> closingOutput = new List<double>(new double[] { 1, 0,0,0 }), downOutput = new List<double>(new double[] { 0,1,0,0 }), frontOutput = new List<double>(new double[] { 0,0,1,0 }), leftOutput = new List<double>(new double[] { 0,0,0,1 });
            for (int i=0; i<TestingData.closing.Count; i++)
            {
                total+=4;

                totalTrue += testPoint(TestingData.closing[i], closingOutput);

                totalTrue += testPoint(TestingData.down[i], downOutput);
                totalTrue += testPoint(TestingData.front[i], frontOutput);
                totalTrue += testPoint(TestingData.left[i], leftOutput);
            }
            return totalTrue / total;
        }

        public int testPoint(List<double> dataPoint, List<double> desiredOutput)
        {
            List<double> layerInput;
            List<double> layerOutput = new List<double>();
            List<List<double>> outputPerLayer = new List<List<double>>();
            #region FeedForward
            layerInput = dataPoint;
            for (int j = 0; j < neurons.Count; j++)// Number of layers
            {

                for (int k = 0; k < neurons[j].Count; ++k)// Number of neurons
                {
                    layerOutput.Add(neurons[j][k].fireSigmoid(layerInput).Item1);
                }
                layerInput = layerOutput;
                //if(j!=0)
                outputPerLayer.Add(layerOutput);
                layerOutput = new List<double>();


            }
            //outputPerLayer.Add(layerOutput);
            #endregion
            double m = double.MinValue;int  mi= 0;
            for (int i = 0; i < desiredOutput.Count; i++)
            {
                if (outputPerLayer[outputPerLayer.Count - 1][i] > m)
                {
                    m = outputPerLayer[outputPerLayer.Count - 1][i];
                    mi = i;
                }
            }
            for (int i = 0; i < desiredOutput.Count; i++)
            {
                if (i==mi)
                {
                   outputPerLayer[outputPerLayer.Count - 1][i] = 1;
                }
                else outputPerLayer[outputPerLayer.Count - 1][i] = 0;
            }
            for (int i = 0; i < desiredOutput.Count; i++)
                if (desiredOutput[i]!=outputPerLayer[outputPerLayer.Count-1][i])
                return 0;
            return 1;
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
