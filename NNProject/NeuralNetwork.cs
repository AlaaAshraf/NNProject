using System;
using System.Collections.Generic;

namespace NNProject
{
	public class NeuralNetwork
	{
		public DataSet TrainingData, TestingData;
		public List<List<Neuron>> Neurons;  //List of lists containing all neurons in the network
		public List<int> NetworkStructure;
		public bool Bias;
		Random Random;
		/// <summary>
		/// Builds the Neural network, takes 3 parameters: 1- List containing number of nuerons per layer
		///                                                2- Number of Input Features
		///                                                3- Boolean indicating the existence of bias
		/// </summary>
		/// <param name="NetworkStructure"></ListOfNumberOfNeuronsPerLayer>
		/// <param name="NumberOfInputFeatures"></NumberOfInputFeatures>
		/// <param name="bias"></BiasBoolean>
		public NeuralNetwork(List<int> NetworkStructure, int NumberOfInputFeatures, bool Bias, string DataSetPath)
		{
			LoadData(DataSetPath);
			Neurons = new List<List<Neuron>>();
			this.NetworkStructure = NetworkStructure;
			this.Bias = Bias;
			Random = new Random();

			for (int i = 0; i < this.NetworkStructure.Count; i++)
			{
				List<Neuron> Layer = new List<Neuron>();
				for (int j = 0; j < this.NetworkStructure[i]; j++)
				{
					Neuron Neuron;

					if (i == 0) Neuron = new Neuron(NumberOfInputFeatures, this.Bias, Random);
					else Neuron = new Neuron(this.NetworkStructure[i - 1], this.Bias, Random);

					Layer.Add(Neuron);
				}

				Neurons.Add(Layer);
			}
		}

		void LoadData(string DataSetPath)
		{
			string Path = DataSetPath + "Training Dataset\\";
			TrainingData = new DataSet(Path);
			Path = DataSetPath + "Testing Dataset\\";
			TestingData = new DataSet(Path);
		}
	}
}