using System;
using System.Collections.Generic;
using System.IO;

namespace NNProject
{
	class DataSet
	{
		//Features of the different classes
		public List<List<double>> Closing, Down, Front, Left;

		public DataSet(string DataSetPath)
		{
			Closing = ExtractFeatures(DataSetPath, "Closing Eyes\\");
			Down = ExtractFeatures(DataSetPath, "Looking Down\\");
			Front = ExtractFeatures(DataSetPath, "Looking Front\\");
			Left = ExtractFeatures(DataSetPath, "Looking Left\\");

			Normalize();
		}

		List<List<double>> ExtractFeatures(string DataSetPath, string ClassName)
		{
			List<List<double>> Ret = new List<List<double>>();

			foreach (string Sample in Directory.EnumerateFiles(DataSetPath + ClassName, "*.pts"))
			{
				List<double> Features = new List<double>();
				string[] Contents = System.IO.File.ReadAllLines(Sample);
				List<Tuple<double, double>> Points = new List<Tuple<double, double>>();

				for (int i = 3; i < 20 + 3; i++)
				{
					string[] Line = Contents[i].Split(' ');
					Points.Add(new Tuple<double, double>(Convert.ToDouble(Line[0]), Convert.ToDouble(Line[1])));
				}

				for (int i = 0; i < 20; i++)
				{
					if (i == 14)continue;

					Features.Add(Math.Sqrt(Math.Pow((Points[i].Item1 - Points[14].Item1), 2) + Math.Pow((Points[i].Item2 - Points[14].Item2), 2)));
					//features.Add((points[i].Item1 - points[14].Item1) + (points[i].Item2-points[14].Item2));
				}

				Ret.Add(Features);
			}

			return Ret;
		}

		void Normalize()
		{
			double Minimum = double.MaxValue, Maximum = double.MinValue;
			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < Closing.Count; j++)
				{
					if (Closing[j][i] > Maximum)
						Maximum = Closing[j][i];
					if (Down[j][i] > Maximum)
						Maximum = Down[j][i];
					if (Left[j][i] > Maximum)
						Maximum = Left[j][i];
					if (Front[j][i] > Maximum)
						Maximum = Front[j][i];

					if (Closing[j][i] < Minimum)
						Minimum = Closing[j][i];
					if (Down[j][i] < Minimum)
						Minimum = Down[j][i];
					if (Left[j][i] < Minimum)
						Minimum = Left[j][i];
					if (Front[j][i] < Minimum)
						Minimum = Front[j][i];
				}

				for (int j = 0; j < Closing.Count; j++)
				{
					Closing[j][i] = (Closing[j][i] - Minimum) / (Maximum - Minimum);
					Down[j][i] = (Down[j][i] - Minimum) / (Maximum - Minimum);
					Left[j][i] = (Left[j][i] - Minimum) / (Maximum - Minimum);
					Front[j][i] = (Front[j][i] - Minimum) / (Maximum - Minimum);
				}
			}
		}
	}
}
