using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Providers
{
    public class ClasterisationProvider
    {
        public double[,] NeuroStart(double[,] x_0, double[] coefs)
        {
            int N = x_0.GetLength(0);
            int M = x_0.GetLength(1);
            //Нормализация данных
            double[] min = new double[M];
            double[] max = new double[M];
            double[,] b = new double[M, N];
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                {
                    b[i, j] = x_0[j, i] * coefs[i];
                }

            for (int i = 0; i < M; i++)
            {
                min[i] = b[i, 0];
                max[i] = b[i, 0];
                for (int j = 1; j < N; j++)
                {
                    min[i] = Math.Min(min[i], b[i, j]);
                    max[i] = Math.Max(max[i], b[i, j]);
                }
            }

            double[,] x = new double[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    x[i, j] = (x_0[i, j] - min[j]) / (max[j] - min[j]);
                }
            }

            return x;
        }
        public List<string> СolomnsData()
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
                                                        "ОАЭ правого уха среднее",
                                                        "ОАЭ правого уха пустых значений",
                                                        "ОАЭ правого уха максимальное",
                                                        "ОАЭ левого уха среднее",
                                                        "ОАЭ левого уха пустых значений",
                                                        "ОАЭ левого уха максимальное",
                                                        "ОАЭ правого уха 0,5-1,5кГц",
                                                        "ОАЭ правого уха 1,5-2,5кГц",
                                                        "ОАЭ правого уха 2,5-3,5кГц",
                                                        "ОАЭ правого уха 3,5-4,5кГц",
                                                        "ОАЭ правого уха 4,5-5,5кГц",
                                                        "ОАЭ левого уха 0,5-1,5кГц",
                                                        "ОАЭ левого уха 1,5-2,5кГц",
                                                        "ОАЭ левого уха 2,5-3,5кГц",
                                                        "ОАЭ левого уха 3,5-4,5кГц",
                                                        "ОАЭ левого уха 4,5-5,5кГц",
                                                        "ASSR правого уха 500Гц",
                                                        "ASSR правого уха 1000Гц",
                                                        "ASSR правого уха 2000Гц",
                                                        "ASSR правого уха 4000Гц",
                                                        "ASSR правого уха среднее",
                                                        "ASSR левого уха 500Гц",
                                                        "ASSR левого уха 1000Гц",
                                                        "ASSR левого уха 2000Гц",
                                                        "ASSR левого уха 4000Гц",
                                                        "ASSR левого уха среднее",
                                                        "КСВП латентность правого уха 20",
                                                        "КСВП латентность правого уха 40",
                                                        "КСВП латентность правого уха 60",
                                                        "КСВП латентность левого уха 20",
                                                        "КСВП латентность левого уха 40",
                                                        "КСВП латентность левого уха 60"
                                                        };
        }

        public List<string> PropertiesData()
        {
            return new List<string>() {                 // Для колонок +3
                                                        "Мать",                                         //0
                                                        "Срок беременности на момент болезни",          //1
                                                        "Ребенок",                                      //2
                                                        "Возраст на момент болезни",                    //3
                                                        "срок гестации на момент обследования(нед)",    //4
                                                        "возраст на момент обследования",               //5
                                                        "ОАЭ правого уха среднее",                      //6
                                                        "ОАЭ правого уха пустых значений",              //7
                                                        "ОАЭ правого уха максимальное",                 //8
                                                        "ОАЭ левого уха среднее",                       //9
                                                        "ОАЭ левого уха пустых значений",               //10
                                                        "ОАЭ левого уха максимальное",                  //11
                                                        "ОАЭ правого уха 0,5-1,5кГц",    //12
                                                        "ОАЭ правого уха 1,5-2,5кГц",    //13
                                                        "ОАЭ правого уха 2,5-3,5кГц",    //14
                                                        "ОАЭ правого уха 3,5-4,5кГц",    //15
                                                        "ОАЭ правого уха 4,5-5,5кГц",    //16
                                                        "ОАЭ левого уха 0,5-1,5кГц",   //17
                                                        "ОАЭ левого уха 1,5-2,5кГц",   //18
                                                        "ОАЭ левого уха 2,5-3,5кГц",   //19
                                                        "ОАЭ левого уха 3,5-4,5кГц",   //20
                                                        "ОАЭ левого уха 4,5-5,5кГц",   //21
                                                        "ASSR правого уха 500Гц",                            //22
                                                        "ASSR правого уха 1000Гц",                           //23
                                                        "ASSR правого уха 2000Гц",                           //24
                                                        "ASSR правого уха 4000Гц",                           //25
                                                        "ASSR правого уха среднее", //26
                                                        "ASSR левого уха 500Гц",                           //27
                                                        "ASSR левого уха 1000Гц",                          //28
                                                        "ASSR левого уха 2000Гц",                          //29
                                                        "ASSR левого уха 4000Гц",                          //30
                                                        "ASSR левого уха среднее",//31
                                                        "КСВП правого уха 20",                   //32
                                                        "КСВП правого уха 40",                   //33
                                                        "КСВП правого уха 60",                   //34
                                                        "КСВП левого уха 20",                  //35
                                                        "КСВП левого уха 40",                  //36
                                                        "КСВП левого уха 60",                  //37
                                                        };
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

        public List<int>[] colomnsToCheck =
        [
                    new List<int> { 9, 12 },
                    new List<int> { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 },
                    new List<int> { 35, 36, 37 },
                    new List<int> { 38, 39, 40 }
        ];

        public double[] SelectedPropNormal(int indexProp, double time_observ, double gestagration)
        {
            if (colomnsToCheck[0].Contains(indexProp))
            {
                if (time_observ <= 3)
                {
                    if (gestagration <= 28)
                        return [0.65, 14.85];
                    else if (gestagration <= 32)
                        return [1.15, 13.35];
                    else if (gestagration <= 36)
                        return [2.94, 14.54];
                    else
                        return [2.6, 14.4];
                }
                else if (time_observ <= 6)
                {
                    if (gestagration <= 28)
                        return [1.75, 12.55];
                    else if (gestagration <= 32)
                        return [2.45, 13.65];
                    else if (gestagration <= 36)
                        return [4.34, 15.14];
                    else
                        return [1.71, 14.91];
                }
                else
                {
                    if (gestagration <= 28)
                        return [2.03, 14.63];
                    else if (gestagration <= 32)
                        return [2.86, 13.66];
                    else if (gestagration <= 36)
                        return [4.83, 15.23];
                    else
                        return [2.12, 14.92];
                }
            }
            if (colomnsToCheck[1].Contains(indexProp))
                return new double[] { 0.0001, 25.001 };
            return null;
        }

        public void CalculateRandIndex(List<DataCOVIDEars[]> clasters, out string info, bool isDBSCAN = false)
        {
            int TP = 0, FP = 0, FN = 0, TN = 0;

            var allExamples = new List<DataCOVIDEars>();
            int clastIndex = 0;
            foreach (var cluster in clasters)
            {
                clastIndex++;
                if (isDBSCAN && clastIndex < 2)
                    continue;
                allExamples.AddRange(cluster);
            }

            // Подсчитываем все пары
            for (int i = 0; i < allExamples.Count; i++)
            {
                for (int j = i + 1; j < allExamples.Count; j++)
                {
                    bool sameCluster = false;
                    bool sameClass = allExamples[i].IsEqualsClass(allExamples[j]);

                    // Проверяем принадлежность к одному кластеру
                    foreach (var cluster in clasters)
                    {
                        if (cluster.Contains(allExamples[i]) && cluster.Contains(allExamples[j]))
                        {
                            sameCluster = true;
                            break;
                        }
                    }

                    if (sameCluster && sameClass)
                    {
                        TP++;
                    }
                    else if (sameCluster && !sameClass)
                    {
                        FP++;
                    }
                    else if (!sameCluster && sameClass)
                    {
                        FN++;
                    }
                    else if (!sameCluster && !sameClass)
                    {
                        TN++;
                    }
                }
            }

            // Выводим результаты
            info = $"TP: {TP}, FP: {FP}, FN: {FN}, TN: {TN}";

            // Рассчитываем индекс Rand
            double randIndex = (double)(TP + TN) / (TP + TN + FP + FN);

            info += $"\nИндекс Rand: {randIndex}";
        }
    }
}
