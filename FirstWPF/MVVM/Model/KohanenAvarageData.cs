using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class KohanenAvarageData
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
        public double _otoacoustic_l_1 { get; set; }
        public double _otoacoustic_l_2 { get; set; }
        public double _otoacoustic_l_4 { get; set; }
        public double _otoacoustic_l_6 { get; set; }
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
        public double _aSSR_l_05 { get; set; }
        public double _aSSR_l_1 { get; set; }
        public double _aSSR_l_2 { get; set; }
        public double _aSSR_l_4 { get; set; }
        //KSVP
        public double _kSVP_r_20 { get; set; }
        public double _kSVP_r_40 { get; set; }
        public double _kSVP_r_60 { get; set; }
        public double _kSVP_l_20 { get; set; }
        public double _kSVP_l_40 { get; set; }
        public double _kSVP_l_60 { get; set; }

        public KohanenAvarageData(KohanenDataCOVIDEars[] data, int index)
        {
            _name = (index + 1).ToString() + " кластер";

            foreach (var item in data)
            {
                _mother += item._mother;
                _time_pregnancy_ill += item._time_pregnancy_ill;
                _son += item._son;
                _time_ill += item._time_ill;
                _time_gestagration += item._time_gestagration;
                _time_observation += item._time_observation;
                _otoacoustic_r_1 += item._otoacoustic_r_1;
                _otoacoustic_r_2 += item._otoacoustic_r_2;
                _otoacoustic_r_4 += item._otoacoustic_r_4;
                _otoacoustic_r_6 += item._otoacoustic_r_6;
                _otoacoustic_l_1 += item._otoacoustic_l_1;
                _otoacoustic_l_2 += item._otoacoustic_l_2;
                _otoacoustic_l_4 += item._otoacoustic_l_4;
                _otoacoustic_l_6 += item._otoacoustic_l_6;
                _otoacoustic_r_05_15 += item._otoacoustic_r_05_15;
                _otoacoustic_r_15_25 += item._otoacoustic_r_15_25;
                _otoacoustic_r_25_35 += item._otoacoustic_r_25_35;
                _otoacoustic_r_35_45 += item._otoacoustic_r_35_45;
                _otoacoustic_r_45_55 += item._otoacoustic_r_45_55;
                _otoacoustic_l_05_15 += item._otoacoustic_l_05_15;
                _otoacoustic_l_15_25 += item._otoacoustic_l_15_25;
                _otoacoustic_l_25_35 += item._otoacoustic_l_25_35;
                _otoacoustic_l_35_45 += item._otoacoustic_l_35_45;
                _otoacoustic_l_45_55 += item._otoacoustic_l_45_55;
                _aSSR_r_05 += item._aSSR_r_05;
                _aSSR_r_1 += item._aSSR_r_1;
                _aSSR_r_2 += item._aSSR_r_2;
                _aSSR_r_4 += item._aSSR_r_4;
                _aSSR_l_05 += item._aSSR_l_05;
                _aSSR_l_1 += item._aSSR_l_1;
                _aSSR_l_2 += item._aSSR_l_2;
                _aSSR_l_4 += item._aSSR_l_4;
                _kSVP_r_20 += item._kSVP_r_20;
                _kSVP_r_40 += item._kSVP_r_40;
                _kSVP_r_60 += item._kSVP_r_60;
                _kSVP_l_20 += item._kSVP_l_20;
                _kSVP_l_40 += item._kSVP_l_40;
                _kSVP_l_60 += item._kSVP_l_60;
            }
            _mother = Math.Round(_mother / data.Length, 2);
            _time_pregnancy_ill = Math.Round(_time_pregnancy_ill / data.Length, 2);
            _son = Math.Round(_son / data.Length, 2);
            _time_ill = Math.Round(_time_ill / data.Length, 2);
            _time_gestagration = Math.Round(_time_gestagration / data.Length, 2);
            _time_observation = Math.Round(_time_observation / data.Length, 2);
            _otoacoustic_r_1 = Math.Round(_otoacoustic_r_1 / data.Length, 2);
            _otoacoustic_r_2 = Math.Round(_otoacoustic_r_2 / data.Length, 2);
            _otoacoustic_r_4 = Math.Round(_otoacoustic_r_4 / data.Length, 2);
            _otoacoustic_r_6 = Math.Round(_otoacoustic_r_6 / data.Length, 2);
            _otoacoustic_l_1 = Math.Round(_otoacoustic_l_1 / data.Length, 2);
            _otoacoustic_l_2 = Math.Round(_otoacoustic_l_2 / data.Length, 2);
            _otoacoustic_l_4 = Math.Round(_otoacoustic_l_4 / data.Length, 2);
            _otoacoustic_l_6 = Math.Round(_otoacoustic_l_6 / data.Length, 2);
            _otoacoustic_r_05_15 = Math.Round(_otoacoustic_r_05_15 / data.Length, 2);
            _otoacoustic_r_15_25 = Math.Round(_otoacoustic_r_15_25 / data.Length, 2);
            _otoacoustic_r_25_35 = Math.Round(_otoacoustic_r_25_35 / data.Length, 2);
            _otoacoustic_r_35_45 = Math.Round(_otoacoustic_r_35_45 / data.Length, 2);
            _otoacoustic_r_45_55 = Math.Round(_otoacoustic_r_45_55 / data.Length, 2);
            _otoacoustic_l_05_15 = Math.Round(_otoacoustic_l_05_15 / data.Length, 2);
            _otoacoustic_l_15_25 = Math.Round(_otoacoustic_l_15_25 / data.Length, 2);
            _otoacoustic_l_25_35 = Math.Round(_otoacoustic_l_25_35 / data.Length, 2);
            _otoacoustic_l_35_45 = Math.Round(_otoacoustic_l_35_45 / data.Length, 2);
            _otoacoustic_l_45_55 = Math.Round(_otoacoustic_l_45_55 / data.Length, 2);
            _aSSR_r_05 = Math.Round(_aSSR_r_05 / data.Length, 2);
            _aSSR_r_1 = Math.Round(_aSSR_r_1 / data.Length, 2);
            _aSSR_r_2 = Math.Round(_aSSR_r_2 / data.Length, 2);
            _aSSR_r_4 = Math.Round(_aSSR_r_4 / data.Length, 2);
            _aSSR_l_05 = Math.Round(_aSSR_l_05 / data.Length, 2);
            _aSSR_l_1 = Math.Round(_aSSR_l_1 / data.Length, 2);
            _aSSR_l_2 = Math.Round(_aSSR_l_2 / data.Length, 2);
            _aSSR_l_4 = Math.Round(_aSSR_l_4 / data.Length, 2);
            _kSVP_r_20 = Math.Round(_kSVP_r_20 / data.Length, 2);
            _kSVP_r_40 = Math.Round(_kSVP_r_40 / data.Length, 2);
            _kSVP_r_60 = Math.Round(_kSVP_r_60 / data.Length, 2);
            _kSVP_l_20 = Math.Round(_kSVP_l_20 / data.Length, 2);
            _kSVP_l_40 = Math.Round(_kSVP_l_40 / data.Length, 2);
            _kSVP_l_60 = Math.Round(_kSVP_l_60 / data.Length, 2);
        }

        public double[] GetAllData()
        {
            return new double[]  {              _mother,
                                                _time_pregnancy_ill,
                                                _son,
                                                _time_ill,
                                                _time_gestagration,
                                                _time_observation,
                                                _otoacoustic_r_1,
                                                _otoacoustic_r_2,
                                                _otoacoustic_r_4,
                                                _otoacoustic_r_6,
                                                _otoacoustic_l_1,
                                                _otoacoustic_l_2,
                                                _otoacoustic_l_4,
                                                _otoacoustic_l_6,
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
                                                _aSSR_l_05,
                                                _aSSR_l_1,
                                                _aSSR_l_2,
                                                _aSSR_l_4,
                                                _kSVP_r_20,
                                                _kSVP_r_40,
                                                _kSVP_r_60,
                                                _kSVP_l_20,
                                                _kSVP_l_40,
                                                _kSVP_l_60
                };
        }
    }
}
