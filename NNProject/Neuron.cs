using System;
using System.Collections.Generic;

namespace NNProject
{
	public class Neuron
	{
		public List<double> Weights;
		bool Bias;
		Random Random;
		/// <summary>
		/// The constructor recieves number of inputs and boolean indicating existence of the bias & initializes the weights with random numbers
		/// </summary>
		/// <param name="NumberOfInputs"></NumberOfInputFeatures>
		/// <param name="Bias"></Bias>
		public Neuron(int NumberOfInputs, bool Bias, Random Random)
		{
			Weights = new List<double>();
			this.Bias = Bias;
			this.Random = Random;

			for (int i = 0; i < NumberOfInputs; i++)
				Weights.Add(GetRandomNumber(-1, 1));

			if (this.Bias)
				Weights.Add(GetRandomNumber(-1, 1));
		}

		/// <summary>
		/// The fireSigmoid function applies the sigmoid function on the input features 
		/// </summary>
		/// <param name="features"></InputFeatures>
		/// <returns></WeightsXInputFeatures>
		public Tuple<double, double> FireSigmoid(List<double> Features)
		{
			List<double> TempFeatures = new List<double>();
			for (int i = 0; i < Features.Count; i++)
				TempFeatures.Add(Features[i]);
			if (Bias)
				TempFeatures.Add(1);

			double Value = 0;

			for (int i = 0; i < TempFeatures.Count; i++)
				Value += TempFeatures[i] * Weights[i];

			double Result = 1 / (1 + Math.Exp(-Value));
			double Derivative = Result * (1 - Result);
			//result = value / (1 + Math.Abs(value));
			//derivative = value / ((1 + Math.Abs(value)) * (1 + Math.Abs(value)));
			Result = (1 - Math.Exp(-Value)) / (1 + Math.Exp(-Value));
			Derivative = (4 * Math.Exp(2 * Value)) / Math.Pow((1 + Math.Exp(2 * Value)), 2);

			return new Tuple<double, double>(Result, Derivative);
			//return 1.0/(1.0+Math.Exp(-value));
			//return Math.Atan(value);
		}

		double GetRandomNumber(double Minimum, double Maximum)
		{
			//Random random = new Random();
			double x = Random.NextDouble() * (Maximum - Minimum) + Minimum;
			//random = null;

			return x;
		}
	}
}