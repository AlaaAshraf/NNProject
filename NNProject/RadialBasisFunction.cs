using System;
using System.Collections.Generic;

namespace NNProject
{
	public class RadialBasisFunction
	{
		public NeuralNetwork NN;
		List<List<double>> Centroids;
		public List<List<int>> ConfusionMatrix;
		List<double> Sum;
		List<double> Size;
		List<double> Variance;
		double MeanSquareErrorThreshold;
		double LearningRate;
		public double Accuracy;
		const int NumberOfInputFeatures = 19;
		const int NumberOfNeuronsInOutputLayer = 4;
		int NumberOfEpochs;
		int HiddenLayerSize;

		public RadialBasisFunction(int HiddenLayerSize, int NumberOfEpochs, double LearningRate, double MeanSquareErrorThreshold, string DataSetPath)
		{
			List<int> NetworkStructure = new List<int>(new int[] { NumberOfNeuronsInOutputLayer });
			NN = new NeuralNetwork(NetworkStructure, HiddenLayerSize, true, DataSetPath);

			this.NumberOfEpochs = NumberOfEpochs;
			this.LearningRate = LearningRate;
			this.MeanSquareErrorThreshold = MeanSquareErrorThreshold;
			this.HiddenLayerSize = HiddenLayerSize;
		}

		void InitializeKMeans()
		{
			int TempSize = HiddenLayerSize;

			Centroids = new List<List<double>>();
			Sum = new List<double>();
			Size = new List<double>();
			Variance = new List<double>();

			for (int i = 0; i < HiddenLayerSize; ++i)
			{
				Sum.Add(0);
				Size.Add(0);
			}

			//	Fetch centroids from closing data.
			for (int i = 0; i < Math.Min(TempSize, NN.TrainingData.Closing.Count); ++i)
				Centroids.Add(NN.TrainingData.Closing[i]);
			TempSize -= NN.TrainingData.Closing.Count;
			if (TempSize < 0) TempSize = 0;

			//	Fetch centroids from down data.
			for (int i = 0; i < Math.Min(TempSize, NN.TrainingData.Down.Count); ++i)
				Centroids.Add(NN.TrainingData.Down[i]);
			TempSize -= NN.TrainingData.Down.Count;
			if (TempSize < 0) TempSize = 0;

			//	Fetch centroids from front data.
			for (int i = 0; i < Math.Min(TempSize, NN.TrainingData.Front.Count); ++i)
				Centroids.Add(NN.TrainingData.Front[i]);
			TempSize -= NN.TrainingData.Front.Count;
			if (TempSize < 0) TempSize = 0;

			//	Fetch centroids from left data.
			for (int i = 0; i < Math.Min(TempSize, NN.TrainingData.Left.Count); ++i)
				Centroids.Add(NN.TrainingData.Left[i]);
		}

		double EuclideanDistance(List<double> Centroid, List<double> Sample)
		{
			double Ret = 0;

			for (int i = 0; i < Centroid.Count; ++i)
				Ret += (Centroid[i] - Sample[i]) * (Centroid[i] - Sample[i]);

			return Math.Sqrt(Ret);
		}

		void CalculateVariance()
		{
			for (int i = 0; i < HiddenLayerSize; ++i)
				Variance.Add(Sum[i] / Size[i]);
		}

		void KMeans()
		{
			InitializeKMeans();

			for (int i = 0; i < NN.TrainingData.Closing.Count; ++i)
			{
				List<double> Distance;
				double Minimum;
				int Index = 0;

				#region Closing
				Distance = new List<double>();
				for (int j = 0; j < HiddenLayerSize; ++j)
					Distance.Add(EuclideanDistance(Centroids[j], NN.TrainingData.Closing[i]));
				Minimum = double.MaxValue;
				for (int j = 0; j < HiddenLayerSize; ++j)
					if (Minimum > Distance[j])
					{
						Minimum = Distance[j];
						Index = j;
					}
				for (int j = 0; j < NumberOfInputFeatures; ++j)
					Centroids[Index][j] += NN.TrainingData.Closing[i][j];
				Sum[Index] += Distance[Index];
				++Size[Index];
				#endregion

				#region Down
				Distance = new List<double>();
				for (int j = 0; j < HiddenLayerSize; ++j)
					Distance.Add(EuclideanDistance(Centroids[j], NN.TrainingData.Down[i]));
				Minimum = double.MaxValue;
				for (int j = 0; j < HiddenLayerSize; ++j)
					if (Minimum > Distance[j])
					{
						Minimum = Distance[j];
						Index = j;
					}
				for (int j = 0; j < NumberOfInputFeatures; ++j)
					Centroids[Index][j] += NN.TrainingData.Down[i][j];
				Sum[Index] += Distance[Index];
				++Size[Index];
				#endregion

				#region Front
				Distance = new List<double>();
				for (int j = 0; j < HiddenLayerSize; ++j)
					Distance.Add(EuclideanDistance(Centroids[j], NN.TrainingData.Front[i]));
				Minimum = double.MaxValue;
				for (int j = 0; j < HiddenLayerSize; ++j)
					if (Minimum > Distance[j])
					{
						Minimum = Distance[j];
						Index = j;
					}
				for (int j = 0; j < NumberOfInputFeatures; ++j)
					Centroids[Index][j] += NN.TrainingData.Front[i][j];
				Sum[Index] += Distance[Index];
				++Size[Index];
				#endregion

				#region Left
				Distance = new List<double>();
				for (int j = 0; j < HiddenLayerSize; ++j)
					Distance.Add(EuclideanDistance(Centroids[j], NN.TrainingData.Left[i]));
				Minimum = double.MaxValue;
				for (int j = 0; j < HiddenLayerSize; ++j)
					if (Minimum > Distance[j])
					{
						Minimum = Distance[j];
						Index = j;
					}
				for (int j = 0; j < NumberOfInputFeatures; ++j)
					Centroids[Index][j] += NN.TrainingData.Left[i][j];
				Sum[Index] += Distance[Index];
				++Size[Index];
				#endregion
			}

			CalculateVariance();
		}

		List<double> NetValue(List<double> Features)
		{
			List<double> Ret = new List<double>();

			for (int i = 0; i < HiddenLayerSize; ++i)
			{
				double Sum = 0;

				for (int j = 0; j < NumberOfInputFeatures; ++j)
					Sum += (Features[j] - Centroids[i][j]) * (Features[j] - Centroids[i][j]);

				if (Variance[i] < 0.000000001) Ret.Add(0);
				else Ret.Add(Math.Exp(Sum / Variance[i] / Variance[i] / -2));
			}

			return Ret;
		}

		double TrainSample(List<double> Features, int Label)
		{
			List<double> G = NetValue(Features);
			List<double> Output = new List<double>();
			double Error;
			double Maximum = double.MinValue;
			int Index = 0;

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
			{
				double Sum = 0;

				for (int j = 0; j < G.Count; ++j)
					Sum += G[j] * NN.Neurons[0][i].Weights[j];

				Output.Add(Sum);
			}

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
				if (Output[i] > Maximum)
				{
					Maximum = Output[i];
					Index = i;
				}

			Error = Label - Index;

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
				for (int j = 0; j < G.Count; ++j)
					NN.Neurons[0][i].Weights[j] += LearningRate * Error * G[j];

			return Error;
		}

		public void Train()
		{
			KMeans();

			for (int i = 0; i < NumberOfEpochs; ++i)
			{
				List<double> Error = new List<double>();
				double MeanSquareError = 0;

				for (int j = 0; j < NN.TrainingData.Closing.Count; ++j)
				{
					Error.Add(TrainSample(NN.TrainingData.Closing[j], 0));
					Error.Add(TrainSample(NN.TrainingData.Down[j], 1));
					Error.Add(TrainSample(NN.TrainingData.Front[j], 2));
					Error.Add(TrainSample(NN.TrainingData.Left[j], 3));
				}

				for (int j = 0; j < Error.Count; ++j)
					MeanSquareError += Error[j] * Error[j];
				MeanSquareError /= Error.Count;
				MeanSquareError /= 2;
				if (MeanSquareError < MeanSquareErrorThreshold) break;
			}
		}

		public int TestSample(List<double> Features)
		{
			List<double> G = NetValue(Features);
			List<double> Output = new List<double>();
			double Maximum = double.MinValue;
			int Index = 0;

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
			{
				double Sum = 0;
				for (int j = 0; j < G.Count; ++j)
					Sum += G[j] * NN.Neurons[0][i].Weights[j];
				Output.Add(1 / (1 + Math.Exp(-Sum)));
			}

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
				if (Output[i] > Maximum)
				{
					Maximum = Output[i];
					Index = i;
				}

			return Index;
		}

		public void Test()
		{
			ConfusionMatrix = new List<List<int>>();

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
			{
				List<int> L = new List<int>();

				for (int j = 0; j < NumberOfNeuronsInOutputLayer; ++j)
					L.Add(0);

				ConfusionMatrix.Add(L);
			}

			for (int i = 0; i < NN.TestingData.Closing.Count; ++i)
			{
				++ConfusionMatrix[0][TestSample(NN.TestingData.Closing[i])];
				++ConfusionMatrix[1][TestSample(NN.TestingData.Down[i])];
				++ConfusionMatrix[2][TestSample(NN.TestingData.Front[i])];
				++ConfusionMatrix[3][TestSample(NN.TestingData.Left[i])];
			}

			CalculateAccuracy();
		}

		void CalculateAccuracy()
		{
			Accuracy = 0;

			for (int i = 0; i < NumberOfNeuronsInOutputLayer; ++i)
				Accuracy += ConfusionMatrix[i][i];

			Accuracy *= 100;
			Accuracy /= NN.TestingData.Closing.Count;
			Accuracy /= 4;
		}
	}
}