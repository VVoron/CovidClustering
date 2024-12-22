using NeuroCovid19.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public double Otoacoustic_r_avarage { get; set; }
        public double Otoacoustic_r_num_nulls { get; set; }
        public double Otoacoustic_r_max { get; set; }
        public double Otoacoustic_l_avarage { get; set; }
        public double Otoacoustic_l_num_nulls { get; set; }
        public double Otoacoustic_l_max { get; set; }
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
        public double ASSR_r_avarage { get; set; }
        public double ASSR_l_05 { get; set; }
        public double ASSR_l_1 { get; set; }
        public double ASSR_l_2 { get; set; }
        public double ASSR_l_4 { get; set; }
        public double ASSR_l_avarage { get; set; }
        //KSVP
        public double KSVP_r_20 { get; set; }
        public double KSVP_r_40 { get; set; }
        public double KSVP_r_60 { get; set; }
        public double KSVP_l_20 { get; set; }
        public double KSVP_l_40 { get; set; }
        public double KSVP_l_60 { get; set; }

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

            var otoacusticData = new List<double>{
                string.IsNullOrEmpty(array[9]) || array[9] == "0" ? Double.NaN : Convert.ToDouble(array[9]),
                string.IsNullOrEmpty(array[10])|| array[10] == "0" ? Double.NaN : Convert.ToDouble(array[10]),
                string.IsNullOrEmpty(array[11])|| array[11] == "0" ? Double.NaN : Convert.ToDouble(array[11]),
                string.IsNullOrEmpty(array[12])|| array[12] == "0" ? Double.NaN : Convert.ToDouble(array[12])
            };
            var notNull = otoacusticData.Where(x => !double.IsNaN(x));
            Otoacoustic_r_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;
            Otoacoustic_r_max = notNull.Any() ? Math.Round(notNull.Max(), 2) : Double.NaN;
            Otoacoustic_r_num_nulls = otoacusticData.Count(double.IsNaN);

            otoacusticData = new List<double>{
                string.IsNullOrEmpty(array[13]) || array[13] == "0" ? Double.NaN : Convert.ToDouble(array[13]),
                string.IsNullOrEmpty(array[14]) || array[14] == "0" ? Double.NaN : Convert.ToDouble(array[14]),
                string.IsNullOrEmpty(array[15]) || array[15] == "0" ? Double.NaN : Convert.ToDouble(array[15]),
                string.IsNullOrEmpty(array[16]) || array[16] == "0" ? Double.NaN : Convert.ToDouble(array[16])
            };
            notNull = otoacusticData.Where(x => !double.IsNaN(x));
            Otoacoustic_l_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;
            Otoacoustic_l_max = notNull.Any() ? Math.Round(notNull.Max(), 2) : Double.NaN;
            Otoacoustic_l_num_nulls = otoacusticData.Count(double.IsNaN);

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
            notNull = new List<double> { ASSR_r_05, ASSR_r_1, ASSR_r_2, ASSR_r_4 }.Where(x => !double.IsNaN(x));
            ASSR_r_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;

            ASSR_l_05 = string.IsNullOrEmpty(array[31]) ? Double.NaN : Convert.ToDouble(array[31]);
            ASSR_l_1 = string.IsNullOrEmpty(array[32]) ? Double.NaN : Convert.ToDouble(array[32]);
            ASSR_l_2 = string.IsNullOrEmpty(array[33]) ? Double.NaN : Convert.ToDouble(array[33]);
            ASSR_l_4 = string.IsNullOrEmpty(array[34]) ? Double.NaN : Convert.ToDouble(array[34]);
            notNull = new List<double> { ASSR_l_05, ASSR_l_1, ASSR_l_2, ASSR_l_4 }.Where(x => !double.IsNaN(x));
            ASSR_l_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;

            KSVP_r_20 = string.IsNullOrEmpty(array[35]) ? Double.NaN : Convert.ToDouble(array[35]);
            KSVP_r_40 = string.IsNullOrEmpty(array[36]) ? Double.NaN : Convert.ToDouble(array[36]);
            KSVP_r_60 = string.IsNullOrEmpty(array[37]) ? Double.NaN : Convert.ToDouble(array[37]);
            KSVP_l_20 = string.IsNullOrEmpty(array[38]) ? Double.NaN : Convert.ToDouble(array[38]);
            KSVP_l_40 = string.IsNullOrEmpty(array[39]) ? Double.NaN : Convert.ToDouble(array[39]);
            KSVP_l_60 = string.IsNullOrEmpty(array[40]) ? Double.NaN : Convert.ToDouble(array[40]);
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
                                            Otoacoustic_r_avarage,
                                            Otoacoustic_r_num_nulls,
                                            Otoacoustic_r_max,
                                            Otoacoustic_l_avarage,
                                            Otoacoustic_l_num_nulls,
                                            Otoacoustic_l_max,
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
                                            ASSR_r_avarage,
                                            ASSR_l_05,
                                            ASSR_l_1,
                                            ASSR_l_2,
                                            ASSR_l_4,
                                            ASSR_l_avarage,
                                            KSVP_r_20,
                                            KSVP_r_40,
                                            KSVP_r_60,
                                            KSVP_l_20,
                                            KSVP_l_40,
                                            KSVP_l_60,
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
                                            Otoacoustic_r_avarage.ToString(),
                                            Otoacoustic_r_num_nulls.ToString(),
                                            Otoacoustic_r_max.ToString(),
                                            Otoacoustic_l_avarage.ToString(),
                                            Otoacoustic_l_num_nulls.ToString(),
                                            Otoacoustic_l_max.ToString(),
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
                                            ASSR_r_avarage.ToString(),
                                            ASSR_l_05.ToString(),
                                            ASSR_l_1.ToString(),
                                            ASSR_l_2.ToString(),
                                            ASSR_l_4.ToString(),
                                            ASSR_l_avarage.ToString(),
                                            KSVP_r_20.ToString(),
                                            KSVP_r_40.ToString(),
                                            KSVP_r_60.ToString(),
                                            KSVP_l_20.ToString(),
                                            KSVP_l_40.ToString(),
                                            KSVP_l_60.ToString(),
            };
        }

        public int GetAnomalyClass()
        {
            List<bool> propAnomalies = new List<bool>();
            var props = App.ContextOfData.SelectedClasterisation == Enumerations.Clasterisation.Kohanen ? App.ContextOfData.KohanenOptions.Properties : App.ContextOfData.DBScanOptions.Properties;

            if (props.Any(x => x.IsUsed && x.Name.Contains("ОАЭ")))
                propAnomalies.Add((Otoacoustic_l_avarage + Otoacoustic_r_avarage) / 2 >= 0.5);
            if (props.Any(x => x.IsUsed && x.Name.Contains("КСВП")))
                propAnomalies.Add((KSVP_r_20 + KSVP_r_20) / 2 >= 0.5);
            if (props.Any(x => x.IsUsed && x.Name.Contains("ASSR")))
                propAnomalies.Add((ASSR_r_avarage + ASSR_l_avarage) / 2 >= 0.5);

            return propAnomalies.Count(x => x);
        }

        public int GetGestationClass()
        {
            if (Time_gestagration < 29)
                return 0;
            if (Time_gestagration >= 29 && Time_gestagration <= 32)
                return 1;
            if (Time_gestagration >= 33 && Time_gestagration <= 36)
                return 2;

            return 3;
        }

        public int GetTimeObservationClass()
        {
            if (Time_observation <= 3)
                return 0;
            if (Time_observation > 3 && Time_observation <= 6)
                return 1;
            return 2;
        }

        public bool IsEqualsClass(DataCOVIDEars item)
        {
            return GetAnomalyClass() == item.GetAnomalyClass() &&
                   Mother == item.Mother &&
                   Son == item.Son &&
                   (int)Time_pregnancy_ill / 3 == (int)item.Time_pregnancy_ill / 3 &&
                   (int)Time_ill / 3 == (int)item.Time_ill / 3 &&
                   GetTimeObservationClass() == item.GetTimeObservationClass() &&
                   GetGestationClass() == item.GetGestationClass();
        }
    }
}
