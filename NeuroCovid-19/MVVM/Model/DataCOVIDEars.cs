using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;

namespace NeuroCovid19.MVVM.Model
{
    public class DataCOVIDEars
    {
        //Info
        public string Name { get; set; }
        public string Anomaly { get; set; }
        public string Genetic_illness { get; set; }
        public double Mother { get; set; }
        public double Time_pregnancy_ill { get; set; }
        public double Son { get; set; }
        public double Time_ill { get; set; }
        public double Time_gestagration { get; set; }
        public double Time_observation { get; set; }
        //Otoacustic
        public double Otoacoustic_r_1 { get; set; }
        public double Otoacoustic_r_2 { get; set; }
        public double Otoacoustic_r_4 { get; set; }
        public double Otoacoustic_r_6 { get; set; }
        public double Otoacoustic_r_summary { get; set; }
        public double Otoacoustic_l_1 { get; set; }
        public double Otoacoustic_l_2 { get; set; }
        public double Otoacoustic_l_4 { get; set; }
        public double Otoacoustic_l_6 { get; set; }
        public double Otoacoustic_l_summary { get; set; }
        public double Otoacoustic_r_05_15 { get; set; }
        public double Otoacoustic_r_15_25 { get; set; }
        public double Otoacoustic_r_25_35 { get; set; }
        public double Otoacoustic_r_35_45 { get; set; }
        public double Otoacoustic_r_45_55 { get; set; }
        public double Otoacoustic_l_05_15 { get; set; }
        public double Otoacoustic_l_15_25 { get; set; }
        public double Otoacoustic_l_25_35 { get; set; }
        public double Otoacoustic_l_35_45 { get; set; }
        public double Otoacoustic_l_45_55 { get; set; }
        //Assr
        public double ASSR_r_05 { get; set; }
        public double ASSR_r_1 { get; set; }
        public double ASSR_r_2 { get; set; }
        public double ASSR_r_4 { get; set; }
        public double ASSR_r_summary { get; set; }
        public double ASSR_l_05 { get; set; }
        public double ASSR_l_1 { get; set; }
        public double ASSR_l_2 { get; set; }
        public double ASSR_l_4 { get; set; }
        public double ASSR_l_summary { get; set; }
        //KSVP
        public double KSVP_r_20 { get; set; }
        public double KSVP_r_40 { get; set; }
        public double KSVP_r_60 { get; set; }
        public double KSVP_r_summary { get; set; }
        public double KSVP_l_20 { get; set; }
        public double KSVP_l_40 { get; set; }
        public double KSVP_l_60 { get; set; }
        public double KSVP_l_summary { get; set; }

        public DataCOVIDEars() { }
        public DataCOVIDEars(string[] array)
        {
            Name = array[0];
            Anomaly = array[1];
            Genetic_illness = array[2];
            Mother = string.IsNullOrEmpty(array[3]) ? Double.NaN : Convert.ToDouble(array[3]);
            Time_pregnancy_ill = string.IsNullOrEmpty(array[4]) ? Double.NaN : Convert.ToDouble(array[4]);
            Son = string.IsNullOrEmpty(array[5]) ? Double.NaN : Convert.ToDouble(array[5]);
            Time_ill = string.IsNullOrEmpty(array[6]) ? Double.NaN : Convert.ToDouble(array[6]);
            Time_gestagration = string.IsNullOrEmpty(array[7]) ? Double.NaN : Convert.ToDouble(array[7]);
            Time_observation = string.IsNullOrEmpty(array[8]) ? Double.NaN : Convert.ToDouble(array[8]);
            Otoacoustic_r_1 = string.IsNullOrEmpty(array[9]) ? Double.NaN : Convert.ToDouble(array[9]);
            Otoacoustic_r_2 = string.IsNullOrEmpty(array[10]) ? Double.NaN : Convert.ToDouble(array[10]);
            Otoacoustic_r_4 = string.IsNullOrEmpty(array[11]) ? Double.NaN : Convert.ToDouble(array[11]);
            Otoacoustic_r_6 = string.IsNullOrEmpty(array[12]) ? Double.NaN : Convert.ToDouble(array[12]);
            Otoacoustic_l_1 = string.IsNullOrEmpty(array[13]) ? Double.NaN : Convert.ToDouble(array[13]);
            Otoacoustic_l_2 = string.IsNullOrEmpty(array[14]) ? Double.NaN : Convert.ToDouble(array[14]);
            Otoacoustic_l_4 = string.IsNullOrEmpty(array[15]) ? Double.NaN : Convert.ToDouble(array[15]);
            Otoacoustic_l_6 = string.IsNullOrEmpty(array[16]) ? Double.NaN : Convert.ToDouble(array[16]);
            Otoacoustic_r_05_15 = string.IsNullOrEmpty(array[17]) ? Double.NaN : Convert.ToDouble(array[17]);
            Otoacoustic_r_15_25 = string.IsNullOrEmpty(array[18]) ? Double.NaN : Convert.ToDouble(array[18]);
            Otoacoustic_r_25_35 = string.IsNullOrEmpty(array[19]) ? Double.NaN : Convert.ToDouble(array[19]);
            Otoacoustic_r_35_45 = string.IsNullOrEmpty(array[20]) ? Double.NaN : Convert.ToDouble(array[20]);
            Otoacoustic_r_45_55 = string.IsNullOrEmpty(array[21]) ? Double.NaN : Convert.ToDouble(array[21]);
            Otoacoustic_l_05_15 = string.IsNullOrEmpty(array[22]) ? Double.NaN : Convert.ToDouble(array[22]);
            Otoacoustic_l_15_25 = string.IsNullOrEmpty(array[23]) ? Double.NaN : Convert.ToDouble(array[23]);
            Otoacoustic_l_25_35 = string.IsNullOrEmpty(array[24]) ? Double.NaN : Convert.ToDouble(array[24]);
            Otoacoustic_l_35_45 = string.IsNullOrEmpty(array[25]) ? Double.NaN : Convert.ToDouble(array[25]);
            Otoacoustic_l_45_55 = string.IsNullOrEmpty(array[26]) ? Double.NaN : Convert.ToDouble(array[26]);
            ASSR_r_05 = string.IsNullOrEmpty(array[27]) ? Double.NaN : Convert.ToDouble(array[27]);
            ASSR_r_1 = string.IsNullOrEmpty(array[28]) ? Double.NaN : Convert.ToDouble(array[28]);
            ASSR_r_2 = string.IsNullOrEmpty(array[29]) ? Double.NaN : Convert.ToDouble(array[29]);
            ASSR_r_4 = string.IsNullOrEmpty(array[30]) ? Double.NaN : Convert.ToDouble(array[30]);
            ASSR_l_05 = string.IsNullOrEmpty(array[31]) ? Double.NaN : Convert.ToDouble(array[31]);
            ASSR_l_1 = string.IsNullOrEmpty(array[32]) ? Double.NaN : Convert.ToDouble(array[32]);
            ASSR_l_2 = string.IsNullOrEmpty(array[33]) ? Double.NaN : Convert.ToDouble(array[33]);
            ASSR_l_4 = string.IsNullOrEmpty(array[34]) ? Double.NaN : Convert.ToDouble(array[34]);
            KSVP_r_20 = string.IsNullOrEmpty(array[35]) ? Double.NaN : Convert.ToDouble(array[35]);
            KSVP_r_40 = string.IsNullOrEmpty(array[36]) ? Double.NaN : Convert.ToDouble(array[36]);
            KSVP_r_60 = string.IsNullOrEmpty(array[37]) ? Double.NaN : Convert.ToDouble(array[37]);
            KSVP_l_20 = string.IsNullOrEmpty(array[38]) ? Double.NaN : Convert.ToDouble(array[38]);
            KSVP_l_40 = string.IsNullOrEmpty(array[39]) ? Double.NaN : Convert.ToDouble(array[39]);
            KSVP_l_60 = string.IsNullOrEmpty(array[40]) ? Double.NaN : Convert.ToDouble(array[40]);

            CalculateAllSummary();
        }

        public DataCOVIDEars(DataCOVIDEars[] data, int index)
        {
            Name = App.ContextOfData.SelectedClasterisation != Enumerations.Clasterisation.DBScan ?
                    (index + 1).ToString() + " кластер" :
                    (index == 0 ? "Шум" : index.ToString() + " кластер");
            var numsOfEachProp = new int[DataForClasterisation().Length];

            foreach (var item in data)
            {
                for (int i = 0; i < numsOfEachProp.Length; i++)
                {
                    double value = (double)item.GetPropertyValueById(i + 3);
                    if (!double.IsNaN(value))
                    {
                        this.InsertValueOfDouble(i + 3, this.GetPropertyValueById(i + 3) + value);

                        numsOfEachProp[i]++;
                    }
                }
            }
            for (int i = 0; i < numsOfEachProp.Length; i++)
            {
                if (numsOfEachProp[i] != 0)
                    this.InsertValueOfDouble(i + 3, Math.Round(this.GetPropertyValueById(i + 3) / numsOfEachProp[i], 2));
            }
        }

        public double[] DataForClasterisation()
        {
            return new double[]  {          Mother,
                                            Time_pregnancy_ill,
                                            Son,
                                            Time_ill,
                                            Time_gestagration,
                                            Time_observation,
                                            Otoacoustic_r_1,
                                            Otoacoustic_r_2,
                                            Otoacoustic_r_4,
                                            Otoacoustic_r_6,
                                            Otoacoustic_r_summary,
                                            Otoacoustic_l_1,
                                            Otoacoustic_l_2,
                                            Otoacoustic_l_4,
                                            Otoacoustic_l_6,
                                            Otoacoustic_l_summary,
                                            Otoacoustic_r_05_15,
                                            Otoacoustic_r_15_25,
                                            Otoacoustic_r_25_35,
                                            Otoacoustic_r_35_45,
                                            Otoacoustic_r_45_55,
                                            Otoacoustic_l_05_15,
                                            Otoacoustic_l_15_25,
                                            Otoacoustic_l_25_35,
                                            Otoacoustic_l_35_45,
                                            Otoacoustic_l_45_55,
                                            ASSR_r_05,
                                            ASSR_r_1,
                                            ASSR_r_2,
                                            ASSR_r_4,
                                            ASSR_r_summary,
                                            ASSR_l_05,
                                            ASSR_l_1,
                                            ASSR_l_2,
                                            ASSR_l_4,
                                            ASSR_l_summary,
                                            KSVP_r_20,
                                            KSVP_r_40,
                                            KSVP_r_60,
                                            KSVP_r_summary,
                                            KSVP_l_20,
                                            KSVP_l_40,
                                            KSVP_l_60,
                                            KSVP_l_summary
            };
        }

        public string[] GetAllData()
        {
            return new string[]  {          Name,
                                            Anomaly,
                                            Genetic_illness,
                                            Mother.ToString(),
                                            Time_pregnancy_ill.ToString(),
                                            Son.ToString(),
                                            Time_ill.ToString(),
                                            Time_gestagration.ToString(),
                                            Time_observation.ToString(),
                                            Otoacoustic_r_1.ToString(),
                                            Otoacoustic_r_2.ToString(),
                                            Otoacoustic_r_4.ToString(),
                                            Otoacoustic_r_6.ToString(),
                                            Otoacoustic_r_summary.ToString(),
                                            Otoacoustic_l_1.ToString(),
                                            Otoacoustic_l_2.ToString(),
                                            Otoacoustic_l_4.ToString(),
                                            Otoacoustic_l_6.ToString(),
                                            Otoacoustic_l_summary.ToString(),
                                            Otoacoustic_r_05_15.ToString(),
                                            Otoacoustic_r_15_25.ToString(),
                                            Otoacoustic_r_25_35.ToString(),
                                            Otoacoustic_r_35_45.ToString(),
                                            Otoacoustic_r_45_55.ToString(),
                                            Otoacoustic_l_05_15.ToString(),
                                            Otoacoustic_l_15_25.ToString(),
                                            Otoacoustic_l_25_35.ToString(),
                                            Otoacoustic_l_35_45.ToString(),
                                            Otoacoustic_l_45_55.ToString(),
                                            ASSR_r_05.ToString(),
                                            ASSR_r_1.ToString(),
                                            ASSR_r_2.ToString(),
                                            ASSR_r_4.ToString(),
                                            ASSR_r_summary.ToString(),
                                            ASSR_l_05.ToString(),
                                            ASSR_l_1.ToString(),
                                            ASSR_l_2.ToString(),
                                            ASSR_l_4.ToString(),
                                            ASSR_l_summary.ToString(),
                                            KSVP_r_20.ToString(),
                                            KSVP_r_40.ToString(),
                                            KSVP_r_60.ToString(),
                                            KSVP_r_summary.ToString(),
                                            KSVP_l_20.ToString(),
                                            KSVP_l_40.ToString(),
                                            KSVP_l_60.ToString(),
                                            KSVP_l_summary.ToString()
            };
        }

        private void CalculateAllSummary()
        {
            var clProvider = new ClasterisationProvider();

            double[] data = DataForClasterisation();

            // Отоакустическая эмиссия
            var columnsToCheck = clProvider.colomnsToCheck[0].Take(4).ToList();
            Otoacoustic_r_summary = CalculateOneSummary(columnsToCheck);
            columnsToCheck = clProvider.colomnsToCheck[0].Skip(4).ToList();
            Otoacoustic_l_summary = CalculateOneSummary(columnsToCheck);

            // ASSR
            columnsToCheck = clProvider.colomnsToCheck[1].Take(4).ToList();
            ASSR_r_summary = CalculateOneSummary(columnsToCheck);
            columnsToCheck = clProvider.colomnsToCheck[1].Skip(4).ToList();
            ASSR_l_summary = CalculateOneSummary(columnsToCheck);


            // КСВП
            columnsToCheck = clProvider.colomnsToCheck[3];
            KSVP_r_summary = CalculateOneSummary(columnsToCheck, true);
            columnsToCheck = clProvider.colomnsToCheck[4];
            KSVP_l_summary = CalculateOneSummary(columnsToCheck, true);
        }

        private double CalculateOneSummary(List<int> columnsToCheck, bool isForKSVP = false)
        {
            ThreeValuesToColorConverter converter = new ThreeValuesToColorConverter();
            int numCol = 0;
            double incorrect = 0;
            foreach (var col in columnsToCheck)
            {
                var colValue = this.GetPropertyValueById(col);
                if (!isForKSVP)
                {
                    var data = new object[] { colValue, Time_gestagration, Time_observation };
                    SolidColorBrush res = (SolidColorBrush)converter.Convert(data, typeof(Brushes), col, CultureInfo.CurrentCulture);
                    if (res != Brushes.Transparent)
                    {
                        numCol++;
                        var a = res == new SolidColorBrush(Color.FromArgb(255, 255, 55, 55));
                        if (res == Brushes.Red || res == converter.MoreSmaller || res == converter.MoreBigger)
                            incorrect += 1.0;
                        if (!isForKSVP && (res == converter.SuchSmaller || res == converter.SuchBigger))
                            incorrect += 0.5;
                    }
                }
                else
                {
                    if (double.IsNaN(colValue))
                        continue;

                    if (colValue == 0 && numCol == 0)
                        incorrect++;
                    else if (colValue == 0 && numCol > 0 && incorrect > 0)
                        incorrect++;

                    numCol++;
                }
            }

            if (numCol != 0)
            {
                return Math.Round(Math.Abs((double)incorrect / (double)numCol), 2);
            }

            return Double.NaN;
        }


    }
}
