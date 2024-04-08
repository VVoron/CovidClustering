using NeuroCovid19.Extensions;
using NeuroCovid19.Interfaces;
using NeuroCovid19.MVVM.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Providers.ClasterisationProviders
{
    public class KohanenProvider : IClasterisation
    {
        public List<DataCOVIDEars[]> Clasters { get; set; }
        public KohanenProvider() { }
        public KohanenProvider(List<DataCOVIDEars> items, double[,] data)
        {
            var wCoefs = WStartWithout(data, (double)App.ContextOfData.KohanenOptions.Rk);
            WCorrect(items, data, (double)App.ContextOfData.KohanenOptions.V, (int)App.ContextOfData.KohanenOptions.Steps, wCoefs);

            App.ContextOfData.KohanenOptions.ClastersInfo = Clasters;
        }

        public void StudyWithW(List<DataCOVIDEars> items, double[,] x, double[,] w)
        {
            WCorrect(items, x, (double)App.ContextOfData.KohanenOptions.V, (int)App.ContextOfData.KohanenOptions.Steps, w);
            App.ContextOfData.KohanenOptions.ClastersInfo = Clasters;
        }

        public double[,] WStartWithout(double[,] x, double Rk)
        {
            int N = x.GetLength(0);
            int M = x.GetLength(1);
            List<double[]> w = new List<double[]>
            {
                BaseExtensions.GetArrayLine(x, 0)
            };

            var wasUsedIndicies = new List<int>();
            for (int i = 0; i < N; i++)
            {
                double[] R = new double[w.Count];
                for (int j = 0; j < w.Count; j++)
                {
                    R[j] = 0;
                    for (int k = 0; k < M; k++)
                    {
                        double difference = x[i, k] - w[j][k];
                        R[j] += Math.Pow(difference, 2);
                    }
                    R[j] = Math.Sqrt(R[j]);
                }

                int bestR = 0;
                double best = int.MaxValue;
                for (int j = 0; j < w.Count; j++)
                {
                    if (R[j] < Rk && R[j] < best)
                    {
                        best = R[j];
                        bestR = j;
                    }
                }
                if (best != int.MaxValue)
                {
                    for (int j = 0; j < M; j++)
                    {
                        w[bestR][j] += (double)App.ContextOfData.KohanenOptions.V * (x[i, j] - w[bestR][j]);
                    }
                    if (!wasUsedIndicies.Contains(bestR))
                        wasUsedIndicies.Add(bestR);
                }
                else
                {
                    wasUsedIndicies.Add(w.Count);
                    w.Add(BaseExtensions.GetArrayLine(x, i));
                }
            }
            double[,] final_w = GetFinalW(w, wasUsedIndicies);
            return final_w;
        }

        public double[,] WCorrect(List<DataCOVIDEars> items, double[,] x, double v, int steps, double[,] w)
        {
            double step = v / steps;
            int N = x.GetLength(0);
            int M = x.GetLength(1);
            List<double[]> newWList = new List<double[]>();
            for (int i = 0; i < w.GetLength(0); i++)
                newWList.Add(BaseExtensions.GetArrayLine(w, i));

            var Rk = App.ContextOfData.KohanenOptions.Rk;
            //Изменение весовых коэффициентов
            var wasUsedIndicies = new List<int>();
            for (int z = 0; z < steps; z++)
            {
                double new_v = v - step * z;
                int[] listforstep = Enumerable.Range(1, N).ToArray();
                listforstep = new ClasterisationProvider().ListShafle(listforstep);
                for (int i = 0; i < N; i++)
                {
                    double[] R = new double[newWList.Count()];
                    int number = listforstep[i] - 1;
                    for (int j = 0; j < newWList.Count; j++)
                    {
                        for (int k = 0; k < M; k++)
                        {
                            double difference = x[number, k] - newWList[j][k];
                            R[j] += Math.Pow(difference, 2);
                        }
                        R[j] = Math.Sqrt(R[j]);
                    }
                    int bestR = 0;
                    double best = int.MaxValue;
                    for (int j = 0; j < newWList.Count; j++)
                    {
                        if (R[j] < Rk && R[j] < best)
                        {
                            best = R[j];
                            bestR = j;
                        }
                    }
                    if (best != int.MaxValue)
                    {
                        for (int j = 0; j < M; j++)
                        {
                            newWList[bestR][j] += new_v * (x[number, j] - newWList[bestR][j]);
                            if (!wasUsedIndicies.Contains(bestR))
                                wasUsedIndicies.Add(bestR);
                        }
                    }
                    else
                    {
                        wasUsedIndicies.Add(newWList.Count);
                        newWList.Add(BaseExtensions.GetArrayLine(x, number));
                    }
                }
            }
            w = GetFinalW(newWList, wasUsedIndicies);
            double[,] wWithoutZero;
            ChildInClasters(items, x, w, out wWithoutZero);
            return wWithoutZero;
        }

        private double[,] GetFinalW(List<double[]> w, List<int> wasUsedIndicies)
        {
            double[,] final_w = new double[wasUsedIndicies.Count, w[0].Length];
            int currentIndex = 0;
            for (int i = 0; i < final_w.GetLength(0); i++)
            {
                if (!wasUsedIndicies.Contains(i))
                    continue;

                for (int j = 0; j < final_w.GetLength(1); j++)
                {
                    final_w[currentIndex, j] = w[i][j];
                }
                currentIndex++;
            }
            return final_w;
        }

        public void ChildInClasters(List<DataCOVIDEars> items, double[,] x, double[,] w, out double[,] wWithoutZero)
        {
            int N = x.GetLength(0);
            int M = x.GetLength(1);
            int count = w.GetLength(0);

            List<List<DataCOVIDEars>> clasters = new List<List<DataCOVIDEars>>();
            for (int i = 0; i < count; i++)
                clasters.Add(new List<DataCOVIDEars>());

            for (int i = 0; i < N; i++)
            {
                double[] R = new double[count];
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        double difference = x[i, k] - w[j, k];
                        R[j] += Math.Pow(difference, 2);
                    }
                    R[j] = Math.Sqrt(R[j]);
                }
                int bestR = 0;
                double best = int.MaxValue;
                var RContains = new Dictionary<int, double>();
                for (int j = 0; j < count; j++)
                {
                    if (R[j] < App.ContextOfData.KohanenOptions.Rk)
                        RContains.Add(j, R[j]);
                    if (R[j] < App.ContextOfData.KohanenOptions.Rk && R[j] < best)
                    {
                        best = R[j];
                        bestR = j;
                    }
                }

                if (best != int.MaxValue)
                    clasters[bestR].Add(items[i]);
            }
            var wCorrectIndex = clasters.Where(x => x.Any()).Select(x => clasters.IndexOf(x)).ToList();
            wWithoutZero = new double[wCorrectIndex.Count, M];
            int number = 0;
            foreach (var index in wCorrectIndex)
            {
                for (int j = 0; j < M; j++)
                    wWithoutZero[number, j] = w[index, j];

                number++;
            }

            Clasters = clasters.Where(x => x.Any()).Select(x => x.ToArray()).ToList();
        }

        public bool CheckForError()
        {
            return App.ContextOfData.KohanenOptions.Rk == null ||
                   App.ContextOfData.KohanenOptions.V == null ||
                   App.ContextOfData.KohanenOptions.Steps == null ||
                   !App.ContextOfData.KohanenOptions.Properties.Any(x => x.IsUsed);
        }
    }
}
