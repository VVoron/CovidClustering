using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Functions
{
    internal class KohanenFunc
    {
        public static List<string> СolomnsData()
        {
            return new List<string>() {
                                                        "ФИО",
                                                        "Аномалии развития при рождении",
                                                        "Генетические заболевания",
                                                        "Мать",
                                                        "Срок беременности на момент болезни",
                                                        "Ребенок",
                                                        "Возраст на момент болезни",
                                                        "срок гестации на момент обследования(нед)",
                                                        "возраст на момент обследования",
                                                        "отоакустическая эмиссия Пр.уха 1кГц",
                                                        "отоакустическая эмиссия Пр.уха 2кГц",
                                                        "отоакустическая эмиссия Пр.уха 4кГц",
                                                        "отоакустическая эмиссия Пр.уха 6кГц",
                                                        "отоакустическая эмиссия Лев.уха 1кГц",
                                                        "отоакустическая эмиссия Лев.уха 2кГц",
                                                        "отоакустическая эмиссия Лев.уха 4кГц",
                                                        "отоакустическая эмиссия Лев.уха 6кГц",
                                                        "отоакустическая эмиссия Пр.уха 0,5-1,5кГц",
                                                        "отоакустическая эмиссия Пр.уха 1,5-2,5кГц",
                                                        "отоакустическая эмиссия Пр.уха 2,5-3,5кГц",
                                                        "отоакустическая эмиссия Пр.уха 3,5-4,5кГц",
                                                        "отоакустическая эмиссия Пр.уха 4,5-5,5кГц",
                                                        "отоакустическая эмиссия Лев.уха 0,5-1,5кГц",
                                                        "отоакустическая эмиссия Лев.уха 1,5-2,5кГц",
                                                        "отоакустическая эмиссия Лев.уха 2,5-3,5кГц",
                                                        "отоакустическая эмиссия Лев.уха 3,5-4,5кГц",
                                                        "отоакустическая эмиссия Лев.уха 4,5-5,5кГц",
                                                        "ASSR Пр.уха 500Гц",
                                                        "ASSR Пр.уха 1000Гц",
                                                        "ASSR Пр.уха 2000Гц",
                                                        "ASSR Пр.уха 4000Гц",
                                                        "ASSR Лев.уха 500Гц",
                                                        "ASSR Лев.уха 1000Гц",
                                                        "ASSR Лев.уха 2000Гц",
                                                        "ASSR Лев.уха 4000Гц",
                                                        "КСВП латентность Пр.уха 20",
                                                        "КСВП латентность Пр.уха 40",
                                                        "КСВП латентность Пр.уха 60",
                                                        "КСВП латентность Лев.уха 20",
                                                        "КСВП латентность Лев.уха 40",
                                                        "КСВП латентность Лев.уха 60"
                                                        };
        }

        public static List<string> PropertiesData()
        {
            return new List<string>() {                 // Для колонок +3
                                                        "Мать",                                         //0
                                                        "Срок беременности на момент болезни",          //1
                                                        "Ребенок",                                      //2
                                                        "Возраст на момент болезни",                    //3
                                                        "срок гестации на момент обследования(нед)",    //4
                                                        "возраст на момент обследования",               //5
                                                        "отоакустическая эмиссия Пр.уха 1кГц",          //6
                                                        "отоакустическая эмиссия Пр.уха 2кГц",          //7
                                                        "отоакустическая эмиссия Пр.уха 4кГц",          //8
                                                        "отоакустическая эмиссия Пр.уха 6кГц",          //9
                                                        "отоакустическая эмиссия Лев.уха 1кГц",         //10
                                                        "отоакустическая эмиссия Лев.уха 2кГц",         //11
                                                        "отоакустическая эмиссия Лев.уха 4кГц",         //12
                                                        "отоакустическая эмиссия Лев.уха 6кГц",         //13
                                                        "отоакустическая эмиссия Пр.уха 0,5-1,5кГц",    //14
                                                        "отоакустическая эмиссия Пр.уха 1,5-2,5кГц",    //15
                                                        "отоакустическая эмиссия Пр.уха 2,5-3,5кГц",    //16
                                                        "отоакустическая эмиссия Пр.уха 3,5-4,5кГц",    //17
                                                        "отоакустическая эмиссия Пр.уха 4,5-5,5кГц",    //18
                                                        "отоакустическая эмиссия Лев.уха 0,5-1,5кГц",   //19
                                                        "отоакустическая эмиссия Лев.уха 1,5-2,5кГц",   //20
                                                        "отоакустическая эмиссия Лев.уха 2,5-3,5кГц",   //21
                                                        "отоакустическая эмиссия Лев.уха 3,5-4,5кГц",   //22
                                                        "отоакустическая эмиссия Лев.уха 4,5-5,5кГц",   //23
                                                        "ASSR Пр.уха 500Гц",                            //24
                                                        "ASSR Пр.уха 1000Гц",                           //25
                                                        "ASSR Пр.уха 2000Гц",                           //26
                                                        "ASSR Пр.уха 4000Гц",                           //27
                                                        "ASSR Лев.уха 500Гц",                           //28
                                                        "ASSR Лев.уха 1000Гц",                          //29
                                                        "ASSR Лев.уха 2000Гц",                          //30
                                                        "ASSR Лев.уха 4000Гц",                          //31
                                                        "КСВП латентность Пр.уха 20",                   //32
                                                        "КСВП латентность Пр.уха 40",                   //33
                                                        "КСВП латентность Пр.уха 60",                   //34
                                                        "КСВП латентность Лев.уха 20",                  //35
                                                        "КСВП латентность Лев.уха 40",                  //36
                                                        "КСВП латентность Лев.уха 60"                   //37
                                                        };                  
        }

        public double[,] NeuroStart(double[,] x_0)
        {
            int N = x_0.GetLength(1);
            int M = x_0.GetLength(0);
            //Нормализация данных
            double[] min = new double[N];
            double[] max = new double[N];
            double[,] b = new double[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    b[i, j] = x_0[j, i];
                }
            }
            for (int i = 0; i < N; i++)
            {
                min[i] = b[i, 0];
                max[i] = b[i, 0];
                for (int j = 1; j < M; j++)
                {
                    min[i] = Math.Min(min[i], b[i, j]);
                    max[i] = Math.Max(max[i], b[i, j]);
                }
            }

            double[,] x = new double[M, N];
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    x[i, j] = (x_0[i, j] - min[j]) / (max[j] - min[j]);
                }
            }

            return x;
        }

        public int[] ListShafle(int[] a)
        {
            Random random = new Random();
            for (int i = a.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = a[j];
                a[j] = a[i];
                a[i] = temp;
            }
            return a;
        }
        public double[] GetStrOfArray(double[,] x, int index)
        {
            double[] str_of_array = new double[x.GetLength(1)];
            for (int i = 0; i < x.GetLength(1); i++)
                str_of_array[i] = x[index, i];
            return str_of_array;
        }
        public bool CheckForChange(double[] x)
        {
            for (int j = 0; j < x.Length; j++)
                if (Double.IsNaN(x[j]))
                {
                    return false;
                }
            return true;
        }

        public double[,] WStartWithout(double[,] x, double Rk, double v, double[] coefs)
        {
            int count = 0;
            int N = x.GetLength(1);
            int M = x.GetLength(0);
            double[,] w = new double[M, N];
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    w[0, j] = x[i, j];

            for (int i = 0; i < M; i++)
            {
                double[] R = new double[count + 1];
                for (int j = 0; j < (count + 1); j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        double difference = x[i, k] - w[j, k];
                        R[j] += coefs[k] * Math.Pow(difference, 2);
                    }
                    R[j] = Math.Sqrt(R[j]);
                }

                int bestR = 0;
                double best = R[0];
                for (int j = 0; j < R.Length; j++)
                {
                    best = Math.Min(best, R[j]);
                    if (best == R[j]) { bestR = j; }
                }
                if (best <= Rk)
                {
                    for (int j = 0; j < N; j++)
                    {
                        w[bestR, j] += v * (x[i, j] - w[bestR, j]);
                    }
                }
                else
                {
                    count++;
                    for (int j = 0; j < N; j++)
                    {
                        w[count, j] = x[i, j];
                    }
                }
            }
            double[,] final_w = new double[count + 1, N];
            for (int i = 0; i < final_w.GetLength(0); i++)
            {
                for (int j = 0; j < final_w.GetLength(1); j++)
                {
                    final_w[i, j] = w[i, j];
                }
            }
            return final_w;
        }

        public double[,] WCorrect(double[,] x, double v, int steps, double[,] w, double[] coefs)
        {
            double step = v / steps;
            int N = x.GetLength(1);
            int M = x.GetLength(0);
            int count = w.GetLength(0);
            //Изменение весовых коэффициентов
            for (int z = 0; z < steps; z++)
            {
                double new_v = v - step * z;
                int[] listforstep = Enumerable.Range(1, M).ToArray();
                listforstep = ListShafle(listforstep);
                for (int i = 0; i < M; i++)
                {

                    double[] R = new double[count];
                    int number = listforstep[i] - 1;
                    for (int j = 0; j < (count); j++)
                    {
                        for (int k = 0; k < N; k++)
                        {
                            double difference = x[number, k] - w[j, k];
                            R[j] += coefs[k] * Math.Pow(difference, 2);
                        }
                        R[j] = Math.Sqrt(R[j]);
                    }
                    int bestR = 0;
                    double best = R[0];
                    for (int j = 0; j < R.Length; j++)
                    {
                        best = Math.Min(best, R[j]);
                        if (best == R[j]) { bestR = j; }
                    }
                    for (int j = 0; j < N; j++)
                    {
                        w[bestR, j] += new_v * (x[number, j] - w[bestR, j]);
                    }
                }
            }
            return w;
        }

        public string[] ChildInClusters(double[,] x, double[,] w, double Rk)
        {
            int M = x.GetLength(0);
            int N = x.GetLength(1);
            int count = w.GetLength(0);
            string[] ChildForClusters = new string[count];
            List<string> variables_w = new List<string>();
            for (int i = 0; i < M; i++)
            {
                double[] R = new double[count];
                for (int j = 0; j < (count); j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        R[j] += Math.Pow(x[i, k] - w[j, k], 2);
                    }
                    R[j] = Math.Sqrt(R[j]);
                }
                int bestR = 0;
                double best = R[0];
                for (int j = 0; j < R.Length; j++)
                {
                    best = Math.Min(best, R[j]);
                    if (best == R[j]) { bestR = j; }
                }
                if (ChildForClusters[bestR] != null)
                    ChildForClusters[bestR] += " ";
                ChildForClusters[bestR] += (i + 1).ToString();
                if (!variables_w.Contains(bestR.ToString()))
                    variables_w.Add(bestR.ToString());
            }
            int index = 0;
            string[] FinalChildForClusters = new string[variables_w.Count];
            for (int i = 0; i < count; i++)
                if (ChildForClusters[i] != null)
                {
                    FinalChildForClusters[index++] = ChildForClusters[i];
                    Console.WriteLine(FinalChildForClusters[index - 1]);
                }

            return FinalChildForClusters;
        }

        public static List<int>[] colomnsToCheck = new List<int>[]
        {
            new List<int> { 9, 10, 11, 12, 13, 14, 15, 16 },
            new List<int> { 27, 28, 29, 30, 31, 32, 33, 34 },
            new List<int> { 35, 36, 37 },
            new List<int> { 38, 39, 40 }
        };

        public static double[] SelectedPropNormal(int indexProp, double time_observ, double gestagration)
        {
            if (indexProp == 9 || indexProp == 13)
            {
                if (time_observ <= 3)
                {
                    if (gestagration <= 28)
                        return new double[] { -4.0, -2.0 };
                    else if (gestagration <= 32)
                        return new double[] { 0.0, 3.0 };
                    else if (gestagration <= 36)
                        return new double[] { 1.0, 4.0 };
                    else
                        return new double[] { 2.0, 5.0 };
                }
                else if (time_observ <= 6)
                {
                    if (gestagration <= 28)
                        return new double[] { 0.6, 4.0 };
                    else if (gestagration <= 32)
                        return new double[] { 2.0, 5.0 };
                    else if (gestagration <= 36)
                        return new double[] { 3.0, 5.0 };
                    else
                        return new double[] { 0.0, 5.0 };
                }
                else
                {
                    if (gestagration <= 28)
                        return new double[] { 3.0, 6.0 };
                    else if (gestagration <= 32)
                        return new double[] { 3.0, 6.0 };
                    else if (gestagration <= 36)
                        return new double[] { 4.0, 6.0 };
                    else
                        return new double[] { 3.0, 7.0 };
                }
            }
            if (indexProp == 10 || indexProp == 14)
            {
                if (time_observ <= 3)
                {
                    if (gestagration <= 28)
                        return new double[] { 11.0, 15.0 };
                    else if (gestagration <= 32)
                        return new double[] { 11.0, 14.0 };
                    else if (gestagration <= 36)
                        return new double[] { 12.0, 14.0 };
                    else
                        return new double[] { 12.0, 16.0 };
                }
                else if (time_observ <= 6)
                {
                    if (gestagration <= 28)
                        return new double[] { 13.0, 16.0 };
                    else if (gestagration <= 32)
                        return new double[] { 13.0, 15.0 };
                    else if (gestagration <= 36)
                        return new double[] { 14.0, 16.0 };
                    else
                        return new double[] { 10.0, 15.0 };
                }
                else
                {
                    if (gestagration <= 28)
                        return new double[] { 14.0, 17.0 };
                    else if (gestagration <= 32)
                        return new double[] { 12.0, 15.0 };
                    else if (gestagration <= 36)
                        return new double[] { 14.0, 16.0 };
                    else
                        return new double[] { 12.0, 15.0 };
                }
            }
            if (indexProp == 11 || indexProp == 15)
            {
                if (time_observ <= 3)
                {
                    if (gestagration <= 28)
                        return new double[] { 6.0, 11.0 };
                    else if (gestagration <= 32)
                        return new double[] { 9.0, 12.0 };
                    else if (gestagration <= 36)
                        return new double[] { 8.0, 10.0 };
                    else
                        return new double[] { 8.0, 13.0 };
                }
                else if (time_observ <= 6)
                {
                    if (gestagration <= 28)
                        return new double[] { 9.0, 12.0 };
                    else if (gestagration <= 32)
                        return new double[] { 11.0, 13.0 };
                    else if (gestagration <= 36)
                        return new double[] { 10.0, 12.0 };
                    else
                        return new double[] { 7.0, 10.0 };
                }
                else
                {
                    if (gestagration <= 28)
                        return new double[] { 11.0, 14.0 };
                    else if (gestagration <= 32)
                        return new double[] { 11.0, 13.0 };
                    else if (gestagration <= 36)
                        return new double[] { 10.0, 12.0 };
                    else
                        return new double[] { 7.0, 10.0 };
                }
            }
            if (indexProp == 12 || indexProp == 16)
            {
                if (time_observ <= 3)
                {
                    if (gestagration <= 28)
                        return new double[] { 4.0, 10.0 };
                    else if (gestagration <= 32)
                        return new double[] { 6.0, 10.0 };
                    else if (gestagration <= 36)
                        return new double[] { 7.0, 9.0 };
                    else
                        return new double[] { 8.0, 12.0 };
                }
                else if (time_observ <= 6)
                {
                    if (gestagration <= 28)
                        return new double[] { 7.0, 12.0 };
                    else if (gestagration <= 32)
                        return new double[] { 6.0, 9.0 };
                    else if (gestagration <= 36)
                        return new double[] { 7.0, 9.0 };
                    else
                        return new double[] { 4.0, 8.0 };
                }
                else
                {
                    if (gestagration <= 28)
                        return new double[] { 8.0, 12.0 };
                    else if (gestagration <= 32)
                        return new double[] { 6.0, 9.0 };
                    else if (gestagration <= 36)
                        return new double[] { 8.0, 11.0 };
                    else
                        return new double[] { 6.0, 10.0 };
                }
            }
            if (indexProp >= 27 && indexProp <= 34)
                return new double[] { 0.0001, 25.001 };
            return null;
        }
    }
}
