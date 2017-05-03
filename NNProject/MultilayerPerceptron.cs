using System;
using System.Collections.Generic;

namespace NNProject
{
	class MultilayerPerceptron
	{
		NeuralNetwork NN;
		List<List<List<double>>> DerivativePerLayer;
		List<List<List<double>>> Error;
		List<List<List<double>>> OutputPerLayer;
		double LearningRate;
		int NumberOfEpochs;

		public MultilayerPerceptron(int NumberOfEpochs, double LearningRate, string DataSetPath)
		{
			List<int> NetworkStructure = new List<int>(new int[] { 5, 4, /* Add a number of neurons for each hidden layer */ 4 });
			int NumberOfInputFeatures = 19;
			NN = new NeuralNetwork(NetworkStructure, NumberOfInputFeatures, true, DataSetPath);
			this.NumberOfEpochs = NumberOfEpochs;
			this.LearningRate = LearningRate;
		}

		public void Train()
		{
			int Count = NN.TrainingData.Closing.Count;
			List<double> ClosingOutput = new List<double>(new double[] { 1, 0, 0, 0 });
			List<double> DownOutput = new List<double>(new double[] { 0, 1, 0, 0 });
			List<double> FrontOutput = new List<double>(new double[] { 0, 0, 1, 0 });
			List<double> LeftOutput = new List<double>(new double[] { 0, 0, 0, 1 });

			for (int j = 0; j < NumberOfEpochs; j++)
			{
				OutputPerLayer = new List<List<List<double>>>();
				DerivativePerLayer = new List<List<List<double>>>();
				Error = new List<List<List<double>>>();
				int k = 0;

				for (int i = 0; i < Count; i++)
				{
					TrainPoint(NN.TrainingData.Closing[i], ClosingOutput, k);
					BackProp(NN.TrainingData.Closing[i], ClosingOutput, k);
					k++;
					TrainPoint(NN.TrainingData.Down[i], DownOutput, k);
					BackProp(NN.TrainingData.Down[i], DownOutput, k);
					k++;
					TrainPoint(NN.TrainingData.Front[i], FrontOutput, k);
					BackProp(NN.TrainingData.Front[i], FrontOutput, k);
					k++;
					TrainPoint(NN.TrainingData.Left[i], LeftOutput, k);
					BackProp(NN.TrainingData.Left[i], LeftOutput, k);
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

		void TrainPoint(List<double> DataPoint, List<double> DesiredOutput, int Index)
		{
			List<double> LayerInput;
			List<double> LayerOutput = new List<double>();
			List<double> LayerDerivative = new List<double>();
			#region FeedForward
			LayerInput = DataPoint;
			OutputPerLayer.Add(new List<List<double>>());
			DerivativePerLayer.Add(new List<List<double>>());
			for (int j = 0; j < NN.Neurons.Count; j++)// Number of layers
			{
				for (int k = 0; k < NN.Neurons[j].Count; ++k)// Number of neurons
				{
					LayerOutput.Add(NN.Neurons[j][k].FireSigmoid(LayerInput).Item1);
					LayerDerivative.Add(NN.Neurons[j][k].FireSigmoid(LayerInput).Item2);
				}

				LayerInput = LayerOutput;
				//if(j!=0)
				OutputPerLayer[Index].Add(LayerOutput);
				DerivativePerLayer[Index].Add(LayerDerivative);

				LayerOutput = new List<double>();
				LayerDerivative = new List<double>();
			}
			//outputPerLayer.Add(layerOutput);
			#endregion
		}

		void BackProp(List<double> DataPoint, List<double> DesiredOutput, int Index)
		{
			#region BackProp
			int temp = 0;

			Error.Add(new List<List<double>>());
			if (NN.Bias)temp = 1;

			for (int i = 0; i < NN.NetworkStructure.Count; i++)
				Error[Index].Add(new List<double>(new double[NN.NetworkStructure[i] + temp]));

			for (int i = NN.Neurons.Count - 1; i >= 0; i--)
				for (int j = 0; j < NN.Neurons[i].Count; j++)
				{
					if (i == NN.Neurons.Count - 1)
						Error[Index][i][j] = DesiredOutput[j] - OutputPerLayer[Index][i][j];
					else
						for (int k = 0; k < NN.Neurons[i + 1].Count; k++)
							Error[Index][i][j] += NN.Neurons[i + 1][k].Weights[j] * Error[Index][i + 1][k];
					//error[index][i][j] *= error[index][i + 1][j];
					//error[index][i][j] *= outputPerLayer[index][i][j] * (1 - outputPerLayer[index][i][j]);

					Error[Index][i][j] *= DerivativePerLayer[Index][i][j];
				}

			OutputPerLayer[Index].Insert(0, new List<double>());

			for (int i = 0; i < DataPoint.Count; i++)
				OutputPerLayer[Index][0].Add(DataPoint[i]);
			//outputPerLayer.Insert(0, dataPoint);
			if (NN.Bias)
				for (int i = 0; i < OutputPerLayer[Index].Count; i++)
					OutputPerLayer[Index][i].Add(1);

			for (int i = 0; i < NN.Neurons.Count; i++)
				for (int j = 0; j < NN.Neurons[i].Count; j++)
				{
					double Delta = 0;

					for (int k = 0; k < NN.Neurons[i][j].Weights.Count; k++)
					{
						//delta += neurons[i][j].weights[k] * outputPerLayer[index][i][k];
						Delta = LearningRate * Error[Index][i][j] * OutputPerLayer[Index][i][k];
						NN.Neurons[i][j].Weights[k] += Delta;
					}
					/*  delta *= eta/(60);
					  for (int k = 0; k < neurons[i][j].weights.Count; k++)
					  {
						  neurons[i][j].weights[k] += delta;
					  }
					  */
				}
			#endregion
		}

		public double Test()
		{
			double total = 0, totalTrue = 0;
			List<double> closingOutput = new List<double>(new double[] { 1, 0, 0, 0 });
			List<double> downOutput = new List<double>(new double[] { 0, 1, 0, 0 });
			List<double> frontOutput = new List<double>(new double[] { 0, 0, 1, 0 });
			List<double> leftOutput = new List<double>(new double[] { 0, 0, 0, 1 });

			for (int i = 0; i < NN.TestingData.Closing.Count; i++)
			{
				total += 4;
				totalTrue += TestPoint(NN.TestingData.Closing[i], closingOutput);
				totalTrue += TestPoint(NN.TestingData.Down[i], downOutput);
				totalTrue += TestPoint(NN.TestingData.Front[i], frontOutput);
				totalTrue += TestPoint(NN.TestingData.Left[i], leftOutput);
			}

			return totalTrue / total;
		}

		public int TestPoint(List<double> DataPoint, List<double> DesiredOutput)
		{
			List<double> LayerInput;
			List<double> LayerOutput = new List<double>();
			List<List<double>> outputPerLayer = new List<List<double>>();
			
			#region FeedForward
			LayerInput = DataPoint;

			for (int j = 0; j < NN.Neurons.Count; j++)// Number of layers
			{
				for (int k = 0; k < NN.Neurons[j].Count; ++k)// Number of neurons
					LayerOutput.Add(NN.Neurons[j][k].FireSigmoid(LayerInput).Item1);

				LayerInput = LayerOutput;
				//if(j!=0)
				outputPerLayer.Add(LayerOutput);
				LayerOutput = new List<double>();
			}
			//outputPerLayer.Add(layerOutput);
			#endregion

			double m = double.MinValue; int mi = 0;

			for (int i = 0; i < DesiredOutput.Count; i++)
			{
				if (outputPerLayer[outputPerLayer.Count - 1][i] > m)
				{
					m = outputPerLayer[outputPerLayer.Count - 1][i];
					mi = i;
				}
			}

			for (int i = 0; i < DesiredOutput.Count; i++)
				if (i == mi)outputPerLayer[outputPerLayer.Count - 1][i] = 1;
				else outputPerLayer[outputPerLayer.Count - 1][i] = 0;

			for (int i = 0; i < DesiredOutput.Count; i++)
				if (DesiredOutput[i] != outputPerLayer[outputPerLayer.Count - 1][i])
					return 0;

			return 1;
		}
	}
}
