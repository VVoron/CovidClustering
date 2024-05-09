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
                                                        "ОАЭ правого уха 1кГц",
                                                        "ОАЭ правого уха 2кГц",
                                                        "ОАЭ правого уха 4кГц",
                                                        "ОАЭ правого уха 6кГц",
                                                        "наличие аномалий исследования ОАЭ по правому уху",
                                                        "ОАЭ левого уха 1кГц",
                                                        "ОАЭ левого уха 2кГц",
                                                        "ОАЭ левого уха 4кГц",
                                                        "ОАЭ левого уха 6кГц",
                                                        "наличие аномалий исследования ОАЭ по левому уху",
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
                                                        "наличие аномалий исследования ASSR по правому уху",
                                                        "ASSR левого уха 500Гц",
                                                        "ASSR левого уха 1000Гц",
                                                        "ASSR левого уха 2000Гц",
                                                        "ASSR левого уха 4000Гц",
                                                        "наличие аномалий исследования ASSR по левому уху",
                                                        "КСВП латентность правого уха 20",
                                                        "КСВП латентность правого уха 40",
                                                        "КСВП латентность правого уха 60",
                                                        "наличие аномалий исследования КСВП по правому уху",
                                                        "КСВП латентность левого уха 20",
                                                        "КСВП латентность левого уха 40",
                                                        "КСВП латентность левого уха 60",
                                                        "наличие аномалий исследования КСВП по левому уху"
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
                                                        "ОАЭ правого уха 1кГц",          //6
                                                        "ОАЭ правого уха 2кГц",          //7
                                                        "ОАЭ правого уха 4кГц",          //8
                                                        "ОАЭ правого уха 6кГц",          //9
                                                        "наличие аномалий исследования ОАЭ по правому уху", //10
                                                        "ОАЭ левого уха 1кГц",         //11
                                                        "ОАЭ левого уха 2кГц",         //12
                                                        "ОАЭ левого уха 4кГц",         //13
                                                        "ОАЭ левого уха 6кГц",         //14
                                                        "наличие аномалий исследования ОАЭ по левому уху", //15
                                                        "ОАЭ правого уха 0,5-1,5кГц",    //16
                                                        "ОАЭ правого уха 1,5-2,5кГц",    //17
                                                        "ОАЭ правого уха 2,5-3,5кГц",    //18
                                                        "ОАЭ правого уха 3,5-4,5кГц",    //19
                                                        "ОАЭ правого уха 4,5-5,5кГц",    //20
                                                        "ОАЭ левого уха 0,5-1,5кГц",   //21
                                                        "ОАЭ левого уха 1,5-2,5кГц",   //22
                                                        "ОАЭ левого уха 2,5-3,5кГц",   //23
                                                        "ОАЭ левого уха 3,5-4,5кГц",   //24
                                                        "ОАЭ левого уха 4,5-5,5кГц",   //25
                                                        "ASSR правого уха 500Гц",                            //26
                                                        "ASSR правого уха 1000Гц",                           //27
                                                        "ASSR правого уха 2000Гц",                           //28
                                                        "ASSR правого уха 4000Гц",                           //29
                                                        "наличие аномалий исследования ASSR по правому уху", //30
                                                        "ASSR левого уха 500Гц",                           //31
                                                        "ASSR левого уха 1000Гц",                          //32
                                                        "ASSR левого уха 2000Гц",                          //33
                                                        "ASSR левого уха 4000Гц",                          //34
                                                        "наличие аномалий исследования ASSR по левому уху",//35
                                                        "КСВП правого уха 20",                   //36
                                                        "КСВП правого уха 40",                   //37
                                                        "КСВП правого уха 60",                   //38
                                                        "наличие аномалий исследования КСВП по правому уху",//39
                                                        "КСВП левого уха 20",                  //40
                                                        "КСВП левого уха 40",                  //41
                                                        "КСВП левого уха 60",                  //42
                                                        "наличие аномалий исследования КСВП по левому уху",//43
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

        public List<int>[] colomnsToCheck = new List<int>[]
        {
                    new List<int> { 9, 10, 11, 12, 14, 15, 16, 17 },
                    new List<int> { 29, 30, 31, 32, 34, 35, 36, 37 },
                    new List<int>() { 13, 18, 33, 38, 42, 46 },
                    new List<int> { 39, 40, 41 },
                    new List<int> { 43, 44, 45 }
        };

        public double[] SelectedPropNormal(int indexProp, double time_observ, double gestagration)
        {
            if (indexProp == 9 || indexProp == 14)
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
            if (indexProp == 10 || indexProp == 15)
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
            if (indexProp == 11 || indexProp == 16)
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
            if (indexProp == 12 || indexProp == 17)
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
            if (indexProp >= 29 && indexProp <= 37 && !colomnsToCheck[2].Contains(indexProp))
                return new double[] { 0.0001, 25.001 };
            if (colomnsToCheck[2].Contains(indexProp))
                return new double[] { 0, 0.5 };
            return null;
        }
    }
}
