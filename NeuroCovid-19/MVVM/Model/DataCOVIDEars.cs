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
        public string _name { get; set; }
        public string _anomaly { get; set; }
        public string _genetic_illness { get; set; }
        public double _mother { get; set; }
        public double _time_pregnancy_ill { get; set; }
        public double _son { get; set; }
        public double _time_ill { get; set; }
        public double _time_gestagration { get; set; }
        public double _time_observation { get; set; }
        //Otoacustic
        public double _otoacoustic_r_1 { get; set; }
        public double _otoacoustic_r_2 { get; set; }
        public double _otoacoustic_r_4 { get; set; }
        public double _otoacoustic_r_6 { get; set; }
        public double _otoacoustic_r_summary { get; set; }
        public double _otoacoustic_l_1 { get; set; }
        public double _otoacoustic_l_2 { get; set; }
        public double _otoacoustic_l_4 { get; set; }
        public double _otoacoustic_l_6 { get; set; }
        public double _otoacoustic_l_summary { get; set; }
        public double _otoacoustic_r_05_15 { get; set; }
        public double _otoacoustic_r_15_25 { get; set; }
        public double _otoacoustic_r_25_35 { get; set; }
        public double _otoacoustic_r_35_45 { get; set; }
        public double _otoacoustic_r_45_55 { get; set; }
        public double _otoacoustic_l_05_15 { get; set; }
        public double _otoacoustic_l_15_25 { get; set; }
        public double _otoacoustic_l_25_35 { get; set; }
        public double _otoacoustic_l_35_45 { get; set; }
        public double _otoacoustic_l_45_55 { get; set; }
        //Assr
        public double _aSSR_r_05 { get; set; }
        public double _aSSR_r_1 { get; set; }
        public double _aSSR_r_2 { get; set; }
        public double _aSSR_r_4 { get; set; }
        public double _aSSR_r_summary { get; set; }
        public double _aSSR_l_05 { get; set; }
        public double _aSSR_l_1 { get; set; }
        public double _aSSR_l_2 { get; set; }
        public double _aSSR_l_4 { get; set; }
        public double _aSSR_l_summary { get; set; }
        //KSVP
        public double _kSVP_r_20 { get; set; }
        public double _kSVP_r_40 { get; set; }
        public double _kSVP_r_60 { get; set; }
        public double _kSVP_r_summary { get; set; }
        public double _kSVP_l_20 { get; set; }
        public double _kSVP_l_40 { get; set; }
        public double _kSVP_l_60 { get; set; }
        public double _kSVP_l_summary { get; set; }

        public DataCOVIDEars() { }
        public DataCOVIDEars(string[] array)
        {
            _name = array[0];
            _anomaly = array[1];
            _genetic_illness = array[2];
            _mother = string.IsNullOrEmpty(array[3]) ? Double.NaN : Convert.ToDouble(array[3]);
            _time_pregnancy_ill = string.IsNullOrEmpty(array[4]) ? Double.NaN : Convert.ToDouble(array[4]);
            _son = string.IsNullOrEmpty(array[5]) ? Double.NaN : Convert.ToDouble(array[5]);
            _time_ill = string.IsNullOrEmpty(array[6]) ? Double.NaN : Convert.ToDouble(array[6]);
            _time_gestagration = string.IsNullOrEmpty(array[7]) ? Double.NaN : Convert.ToDouble(array[7]);
            _time_observation = string.IsNullOrEmpty(array[8]) ? Double.NaN : Convert.ToDouble(array[8]);
            _otoacoustic_r_1 = string.IsNullOrEmpty(array[9]) ? Double.NaN : Convert.ToDouble(array[9]);
            _otoacoustic_r_2 = string.IsNullOrEmpty(array[10]) ? Double.NaN : Convert.ToDouble(array[10]);
            _otoacoustic_r_4 = string.IsNullOrEmpty(array[11]) ? Double.NaN : Convert.ToDouble(array[11]);
            _otoacoustic_r_6 = string.IsNullOrEmpty(array[12]) ? Double.NaN : Convert.ToDouble(array[12]);
            _otoacoustic_l_1 = string.IsNullOrEmpty(array[13]) ? Double.NaN : Convert.ToDouble(array[13]);
            _otoacoustic_l_2 = string.IsNullOrEmpty(array[14]) ? Double.NaN : Convert.ToDouble(array[14]);
            _otoacoustic_l_4 = string.IsNullOrEmpty(array[15]) ? Double.NaN : Convert.ToDouble(array[15]);
            _otoacoustic_l_6 = string.IsNullOrEmpty(array[16]) ? Double.NaN : Convert.ToDouble(array[16]);
            _otoacoustic_r_05_15 = string.IsNullOrEmpty(array[17]) ? Double.NaN : Convert.ToDouble(array[17]);
            _otoacoustic_r_15_25 = string.IsNullOrEmpty(array[18]) ? Double.NaN : Convert.ToDouble(array[18]);
            _otoacoustic_r_25_35 = string.IsNullOrEmpty(array[19]) ? Double.NaN : Convert.ToDouble(array[19]);
            _otoacoustic_r_35_45 = string.IsNullOrEmpty(array[20]) ? Double.NaN : Convert.ToDouble(array[20]);
            _otoacoustic_r_45_55 = string.IsNullOrEmpty(array[21]) ? Double.NaN : Convert.ToDouble(array[21]);
            _otoacoustic_l_05_15 = string.IsNullOrEmpty(array[22]) ? Double.NaN : Convert.ToDouble(array[22]);
            _otoacoustic_l_15_25 = string.IsNullOrEmpty(array[23]) ? Double.NaN : Convert.ToDouble(array[23]);
            _otoacoustic_l_25_35 = string.IsNullOrEmpty(array[24]) ? Double.NaN : Convert.ToDouble(array[24]);
            _otoacoustic_l_35_45 = string.IsNullOrEmpty(array[25]) ? Double.NaN : Convert.ToDouble(array[25]);
            _otoacoustic_l_45_55 = string.IsNullOrEmpty(array[26]) ? Double.NaN : Convert.ToDouble(array[26]);
            _aSSR_r_05 = string.IsNullOrEmpty(array[27]) ? Double.NaN : Convert.ToDouble(array[27]);
            _aSSR_r_1 = string.IsNullOrEmpty(array[28]) ? Double.NaN : Convert.ToDouble(array[28]);
            _aSSR_r_2 = string.IsNullOrEmpty(array[29]) ? Double.NaN : Convert.ToDouble(array[29]);
            _aSSR_r_4 = string.IsNullOrEmpty(array[30]) ? Double.NaN : Convert.ToDouble(array[30]);
            _aSSR_l_05 = string.IsNullOrEmpty(array[31]) ? Double.NaN : Convert.ToDouble(array[31]);
            _aSSR_l_1 = string.IsNullOrEmpty(array[32]) ? Double.NaN : Convert.ToDouble(array[32]);
            _aSSR_l_2 = string.IsNullOrEmpty(array[33]) ? Double.NaN : Convert.ToDouble(array[33]);
            _aSSR_l_4 = string.IsNullOrEmpty(array[34]) ? Double.NaN : Convert.ToDouble(array[34]);
            _kSVP_r_20 = string.IsNullOrEmpty(array[35]) ? Double.NaN : Convert.ToDouble(array[35]);
            _kSVP_r_40 = string.IsNullOrEmpty(array[36]) ? Double.NaN : Convert.ToDouble(array[36]);
            _kSVP_r_60 = string.IsNullOrEmpty(array[37]) ? Double.NaN : Convert.ToDouble(array[37]);
            _kSVP_l_20 = string.IsNullOrEmpty(array[38]) ? Double.NaN : Convert.ToDouble(array[38]);
            _kSVP_l_40 = string.IsNullOrEmpty(array[39]) ? Double.NaN : Convert.ToDouble(array[39]);
            _kSVP_l_60 = string.IsNullOrEmpty(array[40]) ? Double.NaN : Convert.ToDouble(array[40]);

            CalculateAllSummary();
        }

        public DataCOVIDEars(DataCOVIDEars[] data, int index)
        {
            _name = App.ContextOfData.SelectedClasterisation != Enumerations.Clasterisation.DBScan ?
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
            return new double[]  {          _mother,
                                            _time_pregnancy_ill,
                                            _son,
                                            _time_ill,
                                            _time_gestagration,
                                            _time_observation,
                                            _otoacoustic_r_1,
                                            _otoacoustic_r_2,
                                            _otoacoustic_r_4,
                                            _otoacoustic_r_6,
                                            _otoacoustic_r_summary,
                                            _otoacoustic_l_1,
                                            _otoacoustic_l_2,
                                            _otoacoustic_l_4,
                                            _otoacoustic_l_6,
                                            _otoacoustic_l_summary,
                                            _otoacoustic_r_05_15,
                                            _otoacoustic_r_15_25,
                                            _otoacoustic_r_25_35,
                                            _otoacoustic_r_35_45,
                                            _otoacoustic_r_45_55,
                                            _otoacoustic_l_05_15,
                                            _otoacoustic_l_15_25,
                                            _otoacoustic_l_25_35,
                                            _otoacoustic_l_35_45,
                                            _otoacoustic_l_45_55,
                                            _aSSR_r_05,
                                            _aSSR_r_1,
                                            _aSSR_r_2,
                                            _aSSR_r_4,
                                            _aSSR_r_summary,
                                            _aSSR_l_05,
                                            _aSSR_l_1,
                                            _aSSR_l_2,
                                            _aSSR_l_4,
                                            _aSSR_l_summary,
                                            _kSVP_r_20,
                                            _kSVP_r_40,
                                            _kSVP_r_60,
                                            _kSVP_r_summary,
                                            _kSVP_l_20,
                                            _kSVP_l_40,
                                            _kSVP_l_60,
                                            _kSVP_l_summary
            };
        }

        private void CalculateAllSummary()
        {
            var clProvider = new ClasterisationProvider();

            double[] data = DataForClasterisation();

            // Отоакустическая эмиссия
            var columnsToCheck = clProvider.colomnsToCheck[0].Take(4).ToList();
            _otoacoustic_r_summary = CalculateOneSummary(columnsToCheck);
            columnsToCheck = clProvider.colomnsToCheck[0].Skip(4).ToList();
            _otoacoustic_l_summary = CalculateOneSummary(columnsToCheck);

            // ASSR
            columnsToCheck = clProvider.colomnsToCheck[1].Take(4).ToList();
            _aSSR_r_summary = CalculateOneSummary(columnsToCheck);
            columnsToCheck = clProvider.colomnsToCheck[1].Skip(4).ToList();
            _aSSR_l_summary = CalculateOneSummary(columnsToCheck);


            // КСВП
            columnsToCheck = clProvider.colomnsToCheck[3];
            _kSVP_r_summary = CalculateOneSummary(columnsToCheck, true);
            columnsToCheck = clProvider.colomnsToCheck[4];
            _kSVP_l_summary = CalculateOneSummary(columnsToCheck, true);
        }

        private double CalculateOneSummary(List<int> columnsToCheck, bool isForKSVP = false)
        {
            ThreeValuesToColorConverter converter = new ThreeValuesToColorConverter();
            int numCol = 0;
            double incorrect = 0;
            bool first = columnsToCheck[0] <= 41;
            foreach (var col in columnsToCheck)
            {
                var data = isForKSVP ? new object[] { this.GetPropertyValueById(col), _time_gestagration, _time_observation, first ? _kSVP_r_40 : _kSVP_l_40, first ? _kSVP_r_60 : _kSVP_l_60 } :
                                       new object[] { this.GetPropertyValueById(col), _time_gestagration, _time_observation };
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

            if (numCol != 0)
            {
                return Math.Abs((double)incorrect / (double)numCol);
            }

            return Double.NaN;
        }
    }
}
