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
        public List<List<double>> closing, down, front, left;
        public DataSet(string path)
        {
            closing = extractFeatures(path, "Closing Eyes\\");
            down = extractFeatures(path, "Looking Down\\");
            front = extractFeatures(path, "Looking Front\\");
            left = extractFeatures(path, "Looking Left\\");

            scale();
        }
        List<List<double>> extractFeatures(string path, string Class)
        {
            List<List<double>> Ret = new List<List<double>>();

            foreach (string file in Directory.EnumerateFiles(path + Class, "*.pts"))
            {
                List<double> features = new List<double>();
                string[] contents = File.ReadAllLines(file);
                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                for (int i = 3; i < 20 + 3; i++)
                {
                    string[] line = contents[i].Split(' ');
                    points.Add(new Tuple<double, double>(Convert.ToDouble(line[0]), Convert.ToDouble(line[1])));
                }
                for (int i = 0; i < 20; i++)
                {
                    if (i == 14)
                        continue;
                    //features.Add(Math.Sqrt(Math.Pow((points[i].Item1 - points[14].Item1), 2) + Math.Pow((points[i].Item2 - points[14].Item2), 2)));
                    features.Add((points[i].Item1 - points[14].Item1) + (points[i].Item2-points[14].Item2));
                }
                Ret.Add(features);
            }
            return Ret;
        }

        void scale()
        {
            double min = double.MaxValue, max = double.MinValue;
            for(int i=0; i<19; i++)
            {
                for(int j=0; j<closing.Count; j++)
                {
                    if (closing[j][i] > max)
                        max = closing[j][i];
                    if (down[j][i] > max)
                        max = down[j][i];
                    if (left[j][i] > max)
                        max = left[j][i];
                    if (front[j][i] > max)
                        max = front[j][i];

                    if (closing[j][i] < min)
                        min = closing[j][i];
                    if (down[j][i] < min)
                        min = down[j][i];
                    if (left[j][i] < min)
                        min = left[j][i];
                    if (front[j][i] < min)
                        min = front[j][i];
                }

                for(int j=0; j<closing.Count; j++)
                {
                    closing[j][i] = (closing[j][i] - min) / (max - min);
                    down[j][i] = (down[j][i] - min) / (max - min);
                    left[j][i] = (left[j][i] - min) / (max - min);
                    front[j][i] = (front[j][i] - min) / (max - min);
                }
            }
        }
    }
}
