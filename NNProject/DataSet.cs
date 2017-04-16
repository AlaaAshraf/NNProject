using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NNProject
{
    class DataSet
    {
        //Features of the different classes
        public List<double> closing, down, front, left;
        public DataSet(string path)
        {
            closing = extractFeatures(path, "Closing Eyes\\");
            down = extractFeatures(path, "Looking Down\\");
            front = extractFeatures(path, "Looking Front\\");
            left = extractFeatures(path, "Looking Left\\");
        }
        List<double> extractFeatures(string path, string Class)
        {
            List<double> features = new List<double>();
            foreach (string file in Directory.EnumerateFiles(path + Class, "*.pts"))
            {
                string[] contents = File.ReadAllLines(file);
                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                for (int i = 3; i < 20 + 3; i++)
                {
                    string []line = contents[i].Split(' ');
                    points.Add(new Tuple<double, double>(Convert.ToDouble(line[0]), Convert.ToDouble(line[1])));
                }
                for (int i = 0; i < 20; i++)
                {
                    if (i == 14)
                        continue;
                    features.Add(Math.Sqrt(Math.Pow((points[i].Item1 - points[14].Item1), 2) + Math.Pow((points[i].Item2 - points[14].Item2), 2)));
                }
            }
            return features;
        }
    }
}
