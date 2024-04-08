using NeuroCovid19.Extensions;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class AvgCovidEars
    {
        //Info
        public string _name { get; set; }
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

        public AvgCovidEars(DataCOVIDEars[] data, int index)
        {
            var clProvider = new ClasterisationProvider();
            _name = App.ContextOfData.SelectedClasterisation != Enumerations.Clasterisation.DBScan ?
                    (index + 1).ToString() + " кластер" :
                    (index == 0 ? "Шум" : index.ToString() + " кластер");
            var numsOfEachProp = new int[clProvider.PropertiesData().Count - 1];

            foreach (var item in data)
            {
                for (int i = 0; i < numsOfEachProp.Length; i++)
                {
                    double value = (double)item.GetPropertyValueById(i + 3);
                    if (!double.IsNaN(value))
                    {
                        this.InsertValueOfDouble(i + 1, this.GetPropertyValueById(i + 1) + value);

                        numsOfEachProp[i]++;
                    }
                }
            }
            for (int i = 0; i < numsOfEachProp.Length; i++)
            {
                if (numsOfEachProp[i] != 0)
                    this.InsertValueOfDouble(i + 1, Math.Round(this.GetPropertyValueById(i + 1) / numsOfEachProp[i], 2));
            }
        }

        public double[] GetData()
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
    }
}
