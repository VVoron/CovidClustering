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
        public string GeneticIllness { get; set; }
        public double Mother { get; set; }
        public double TimePregnancyIll { get; set; }
        public double Son { get; set; }
        public double TimeIll { get; set; }
        public double TimeGestagration { get; set; }
        public double TimeObservation { get; set; }
        //Otoacustic
        public double OaeRightAvarage { get; set; }
        public double OaeRightNumNulls { get; set; }
        public double OaeRightMax { get; set; }
        public double OaeLeftAvarage { get; set; }
        public double OaeLeftNumNulls { get; set; }
        public double OaeLeftMax { get; set; }

        // Не отображаемые поля
        private double OaeRight_1 { get; set; }
        private double OaeRight_2 { get; set; }
        private double OaeRight_4 { get; set; }
        private double OaeRight_6 { get; set; }
        private double OaeLeft_1 { get; set; }
        private double OaeLeft_2 { get; set; }
        private double OaeLeft_4 { get; set; }
        private double OaeLeft_6 { get; set; }

        // Не используемые
        private double OaeRight5_15 { get; set; }
        private double OaeRight15_25 { get; set; }
        private double OaeRight25_35 { get; set; }
        private double OaeRight35_45 { get; set; }
        private double OaeRight45_55 { get; set; }
        private double OaeLeft05_15 { get; set; }
        private double OaeLeft15_25 { get; set; }
        private double OaeLeft25_35 { get; set; }
        private double OaeLeft35_45 { get; set; }
        private double OaeLeft45_55 { get; set; }
        //Assr
        public double AssrRight_05 { get; set; }
        public double AssrRight_1 { get; set; }
        public double AssrRight_2 { get; set; }
        public double AssrRight_4 { get; set; }
        public double AssrRight_avarage { get; set; }
        public double AssrLeft_05 { get; set; }
        public double AssrLeft_1 { get; set; }
        public double AssrLeft_2 { get; set; }
        public double AssrLeft_4 { get; set; }
        public double AssrLeft_avarage { get; set; }
        //KSVP
        public double KsvpRight_20 { get; set; }
        public double KsvpRight_40 { get; set; }
        public double KsvoRight_60 { get; set; }
        public double KsvpLeft_20 { get; set; }
        public double KsvpLeft_40 { get; set; }
        public double KsvpLeft_60 { get; set; }

        public DataCOVIDEars() { }
        public DataCOVIDEars(string[] array)
        {
            Name = array[0];
            Anomaly = array[1];
            GeneticIllness = array[2];
            Mother = string.IsNullOrEmpty(array[3]) ? Double.NaN : Convert.ToDouble(array[3]);
            TimePregnancyIll = string.IsNullOrEmpty(array[4]) ? Double.NaN : Convert.ToDouble(array[4]);
            Son = string.IsNullOrEmpty(array[5]) ? Double.NaN : Convert.ToDouble(array[5]);
            TimeIll = string.IsNullOrEmpty(array[6]) ? Double.NaN : Convert.ToDouble(array[6]);
            TimeGestagration = string.IsNullOrEmpty(array[7]) ? Double.NaN : Convert.ToDouble(array[7]);
            TimeObservation = string.IsNullOrEmpty(array[8]) ? Double.NaN : Convert.ToDouble(array[8]);

            OaeRight_1 = string.IsNullOrEmpty(array[9]) || array[9] == "0" ? Double.NaN : Convert.ToDouble(array[9]);
            OaeRight_2 = string.IsNullOrEmpty(array[10]) || array[10] == "0" ? Double.NaN : Convert.ToDouble(array[10]);
            OaeRight_4 = string.IsNullOrEmpty(array[11]) || array[11] == "0" ? Double.NaN : Convert.ToDouble(array[11]);
            OaeRight_6 = string.IsNullOrEmpty(array[12]) || array[12] == "0" ? Double.NaN : Convert.ToDouble(array[12]);

            var otoacusticData = new List<double>{
                OaeRight_1,
                OaeRight_2,
                OaeRight_4,
                OaeRight_6
            };
            var notNull = otoacusticData.Where(x => !double.IsNaN(x));
            OaeRightAvarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;
            OaeRightMax = notNull.Any() ? Math.Round(notNull.Max(), 2) : Double.NaN;
            OaeRightNumNulls = otoacusticData.Count(double.IsNaN);

            OaeLeft_1 = string.IsNullOrEmpty(array[13]) || array[13] == "0" ? Double.NaN : Convert.ToDouble(array[13]);
            OaeLeft_1 = string.IsNullOrEmpty(array[14]) || array[14] == "0" ? Double.NaN : Convert.ToDouble(array[14]);
            OaeLeft_1 = string.IsNullOrEmpty(array[15]) || array[15] == "0" ? Double.NaN : Convert.ToDouble(array[15]);
            OaeLeft_1 = string.IsNullOrEmpty(array[16]) || array[16] == "0" ? Double.NaN : Convert.ToDouble(array[16]);
            otoacusticData = new List<double>{
                OaeLeft_1,
                OaeLeft_2,
                OaeLeft_4,
                OaeLeft_6
            };
            notNull = otoacusticData.Where(x => !double.IsNaN(x));
            OaeLeftAvarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;
            OaeLeftMax = notNull.Any() ? Math.Round(notNull.Max(), 2) : Double.NaN;
            OaeLeftNumNulls = otoacusticData.Count(double.IsNaN);

            OaeRight5_15 = string.IsNullOrEmpty(array[17]) ? Double.NaN : Convert.ToDouble(array[17]);
            OaeRight15_25 = string.IsNullOrEmpty(array[18]) ? Double.NaN : Convert.ToDouble(array[18]);
            OaeRight25_35 = string.IsNullOrEmpty(array[19]) ? Double.NaN : Convert.ToDouble(array[19]);
            OaeRight35_45 = string.IsNullOrEmpty(array[20]) ? Double.NaN : Convert.ToDouble(array[20]);
            OaeRight45_55 = string.IsNullOrEmpty(array[21]) ? Double.NaN : Convert.ToDouble(array[21]);
            OaeLeft05_15 = string.IsNullOrEmpty(array[22]) ? Double.NaN : Convert.ToDouble(array[22]);
            OaeLeft15_25 = string.IsNullOrEmpty(array[23]) ? Double.NaN : Convert.ToDouble(array[23]);
            OaeLeft25_35 = string.IsNullOrEmpty(array[24]) ? Double.NaN : Convert.ToDouble(array[24]);
            OaeLeft35_45 = string.IsNullOrEmpty(array[25]) ? Double.NaN : Convert.ToDouble(array[25]);
            OaeLeft45_55 = string.IsNullOrEmpty(array[26]) ? Double.NaN : Convert.ToDouble(array[26]);
            
            AssrRight_05 = string.IsNullOrEmpty(array[27]) ? Double.NaN : Convert.ToDouble(array[27]);
            AssrRight_1 = string.IsNullOrEmpty(array[28]) ? Double.NaN : Convert.ToDouble(array[28]);
            AssrRight_2 = string.IsNullOrEmpty(array[29]) ? Double.NaN : Convert.ToDouble(array[29]);
            AssrRight_4 = string.IsNullOrEmpty(array[30]) ? Double.NaN : Convert.ToDouble(array[30]);
            notNull = new List<double> { AssrRight_05, AssrRight_1, AssrRight_2, AssrRight_4 }.Where(x => !double.IsNaN(x));
            AssrRight_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;

            AssrLeft_05 = string.IsNullOrEmpty(array[31]) ? Double.NaN : Convert.ToDouble(array[31]);
            AssrLeft_1 = string.IsNullOrEmpty(array[32]) ? Double.NaN : Convert.ToDouble(array[32]);
            AssrLeft_2 = string.IsNullOrEmpty(array[33]) ? Double.NaN : Convert.ToDouble(array[33]);
            AssrLeft_4 = string.IsNullOrEmpty(array[34]) ? Double.NaN : Convert.ToDouble(array[34]);
            notNull = new List<double> { AssrLeft_05, AssrLeft_1, AssrLeft_2, AssrLeft_4 }.Where(x => !double.IsNaN(x));
            AssrLeft_avarage = notNull.Any() ? Math.Round(notNull.Average(), 2) : Double.NaN;

            KsvpRight_20 = string.IsNullOrEmpty(array[35]) ? Double.NaN : Convert.ToDouble(array[35]);
            KsvpRight_40 = string.IsNullOrEmpty(array[36]) ? Double.NaN : Convert.ToDouble(array[36]);
            KsvoRight_60 = string.IsNullOrEmpty(array[37]) ? Double.NaN : Convert.ToDouble(array[37]);
            KsvpLeft_20 = string.IsNullOrEmpty(array[38]) ? Double.NaN : Convert.ToDouble(array[38]);
            KsvpLeft_40 = string.IsNullOrEmpty(array[39]) ? Double.NaN : Convert.ToDouble(array[39]);
            KsvpLeft_60 = string.IsNullOrEmpty(array[40]) ? Double.NaN : Convert.ToDouble(array[40]);
        }

        public float GetOaeAvarage()
        {
            return (float)(OaeRightAvarage + OaeLeftAvarage) / 2f;
        }

        public float GetOaeAvarage_1k()
        {
            return (float)(OaeRight_1 + OaeLeft_1) / 2f;
        }

        public float GetOaeAvarage_2k()
        {
            return (float)(OaeRight_2 + OaeLeft_2) / 2f;
        }
        public float GetOaeAvarage_4k()
        {
            return (float)(OaeRight_4 + OaeLeft_4) / 2f;
        }
        public float GetOaeAvarage_6k()
        {
            return (GetOaeAvarage_6k() + GetOaeAvarage_4k()) - (GetOaeAvarage_1k() + GetOaeAvarage_2k());
        }

        public float GetOaeTilt()
        {
            return (float)((OaeRightAvarage + OaeLeftAvarage) / 2.0);
        }

        public bool IsCorrectForClassification()
        {
            var isAnyIncorrect = Double.IsNaN(OaeRightAvarage) || Double.IsNaN(OaeLeftAvarage) ||
                                 Double.IsNaN(OaeRight_1) || Double.IsNaN(OaeRight_2) || Double.IsNaN(OaeRight_4) || Double.IsNaN(OaeRight_6) ||
                                 Double.IsNaN(OaeLeft_1) || Double.IsNaN(OaeLeft_2) || Double.IsNaN(OaeLeft_4) || Double.IsNaN(OaeLeft_6) ||
                                 Double.IsNaN(AssrLeft_avarage) || Double.IsNaN(AssrRight_avarage);
            return !isAnyIncorrect;
        }

        public double[] DataForClasterisation()
        {
            return new double[]  {          Mother,
                                            TimePregnancyIll,
                                            Son,
                                            TimeIll,
                                            TimeGestagration,
                                            TimeObservation,
                                            OaeRightAvarage,
                                            OaeRightNumNulls,
                                            OaeRightMax,
                                            OaeLeftAvarage,
                                            OaeLeftNumNulls,
                                            OaeLeftMax,
/*                                            OaeRight5_15,
                                            OaeRight15_25,
                                            OaeRight25_35,
                                            OaeRight35_45,
                                            OaeRight45_55,
                                            OaeLeft05_15,
                                            OaeLeft15_25,
                                            OaeLeft25_35,
                                            OaeLeft35_45,
                                            OaeLeft45_55,*/
                                            AssrRight_05,
                                            AssrRight_1,
                                            AssrRight_2,
                                            AssrRight_4,
                                            AssrRight_avarage,
                                            AssrLeft_05,
                                            AssrLeft_1,
                                            AssrLeft_2,
                                            AssrLeft_4,
                                            AssrLeft_avarage,
                                            KsvpRight_20,
                                            KsvpRight_40,
                                            KsvoRight_60,
                                            KsvpLeft_20,
                                            KsvpLeft_40,
                                            KsvpLeft_60,
            };
        }

        public string[] GetAllData()
        {
            return new string[]  {          Name,
                                            Anomaly,
                                            GeneticIllness,
                                            Mother.ToString(),
                                            TimePregnancyIll.ToString(),
                                            Son.ToString(),
                                            TimeIll.ToString(),
                                            TimeGestagration.ToString(),
                                            TimeObservation.ToString(),
                                            OaeRightAvarage.ToString(),
                                            OaeRightNumNulls.ToString(),
                                            OaeRightMax.ToString(),
                                            OaeLeftAvarage.ToString(),
                                            OaeLeftNumNulls.ToString(),
                                            OaeLeftMax.ToString(),
/*                                            OaeRight5_15.ToString(),
                                            OaeRight15_25.ToString(),
                                            OaeRight25_35.ToString(),
                                            OaeRight35_45.ToString(),
                                            OaeRight45_55.ToString(),
                                            OaeLeft05_15.ToString(),
                                            OaeLeft15_25.ToString(),
                                            OaeLeft25_35.ToString(),
                                            OaeLeft35_45.ToString(),
                                            OaeLeft45_55.ToString(),*/
                                            AssrRight_05.ToString(),
                                            AssrRight_1.ToString(),
                                            AssrRight_2.ToString(),
                                            AssrRight_4.ToString(),
                                            AssrRight_avarage.ToString(),
                                            AssrLeft_05.ToString(),
                                            AssrLeft_1.ToString(),
                                            AssrLeft_2.ToString(),
                                            AssrLeft_4.ToString(),
                                            AssrLeft_avarage.ToString(),
                                            KsvpRight_20.ToString(),
                                            KsvpRight_40.ToString(),
                                            KsvoRight_60.ToString(),
                                            KsvpLeft_20.ToString(),
                                            KsvpLeft_40.ToString(),
                                            KsvpLeft_60.ToString(),
            };
        }

        public string[] DataForAI
        {
            get
            {
                return
                    [
                        TimeGestagration.ToString(),
                    TimeObservation.ToString(),
                    OaeRight_1.ToString(),
                    OaeRight_2.ToString(),
                    OaeRight_4.ToString(),
                    OaeRight_6.ToString(),

                    OaeLeft_1.ToString(),
                    OaeLeft_2.ToString(),
                    OaeLeft_4.ToString(),
                    OaeLeft_6.ToString(),

                    AssrRight_05.ToString(),
                    AssrRight_1.ToString(),
                    AssrRight_2.ToString(),
                    AssrRight_4.ToString(),

                    AssrLeft_05.ToString(),
                    AssrLeft_1.ToString(),
                    AssrLeft_2.ToString(),
                    AssrLeft_4.ToString(),

                    KsvpRight_20.ToString(),
                    KsvpRight_40.ToString(),
                    KsvoRight_60.ToString(),
                    KsvpLeft_20.ToString(),
                    KsvpLeft_40.ToString(),
                    KsvpLeft_60.ToString()
                    ];
            }
        }

        public int GetAnomalyClass()
        {
            List<bool> propAnomalies = new List<bool>();
            var props = App.ContextOfData.SelectedMethod == Enumerations.Method.Kohanen ? App.ContextOfData.KohanenOptions.Properties : App.ContextOfData.DBScanOptions.Properties;

            if (props.Any(x => x.IsUsed && x.Name.Contains("ОАЭ")))
                propAnomalies.Add((OaeLeftAvarage + OaeRightAvarage) / 2 >= 0.5);
            if (props.Any(x => x.IsUsed && x.Name.Contains("КСВП")))
                propAnomalies.Add((KsvpRight_20 + KsvpRight_20) / 2 >= 0.5);
            if (props.Any(x => x.IsUsed && x.Name.Contains("ASSR")))
                propAnomalies.Add((AssrRight_avarage + AssrLeft_avarage) / 2 >= 0.5);

            return propAnomalies.Count(x => x);
        }

        public int GetGestationClass()
        {
            if (TimeGestagration < 29)
                return 0;
            if (TimeGestagration >= 29 && TimeGestagration <= 32)
                return 1;
            if (TimeGestagration >= 33 && TimeGestagration <= 36)
                return 2;

            return 3;
        }

        public int GetTimeObservationClass()
        {
            if (TimeObservation <= 3)
                return 0;
            if (TimeObservation > 3 && TimeObservation <= 6)
                return 1;
            return 2;
        }

        public bool IsEqualsClass(DataCOVIDEars item)
        {
            return GetAnomalyClass() == item.GetAnomalyClass() &&
                   Mother == item.Mother &&
                   Son == item.Son &&
                   (int)TimePregnancyIll / 3 == (int)item.TimePregnancyIll / 3 &&
                   (int)TimeIll / 3 == (int)item.TimeIll / 3 &&
                   GetTimeObservationClass() == item.GetTimeObservationClass() &&
                   GetGestationClass() == item.GetGestationClass();
        }
    }
}
