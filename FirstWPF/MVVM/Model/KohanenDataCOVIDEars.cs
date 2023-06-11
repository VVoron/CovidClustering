using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class KohanenDataCOVIDEars
    {
        //Info
        public string _name { get; set; }
        public string _anomaly { get; set; }
        public string _genetic_illness { get; set; }
        public int _mother { get; set; }
        public double _time_pregnancy_ill { get; set; }
        public int _son { get; set; }
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

        public KohanenDataCOVIDEars(string[] array)
        {
            _name = array[0];
            _anomaly = array[1];
            _genetic_illness = array[2];
            _mother = Convert.ToInt32(array[3]);
            _time_pregnancy_ill = Convert.ToInt32(array[4]);
            _son = Convert.ToInt32(array[5]);
            _time_ill = Convert.ToDouble(array[6]);
            _time_gestagration = Convert.ToDouble(array[7]);
            _time_observation = CheckTimeObserv(Convert.ToDouble(array[8]));
            _otoacoustic_r_1 = (array[9] == "") ? Double.NaN : Convert.ToDouble(array[9]);
            _otoacoustic_r_2 = (array[10] == "") ? Double.NaN : Convert.ToDouble(array[10]);
            _otoacoustic_r_4 = (array[11] == "") ? Double.NaN : Convert.ToDouble(array[11]);
            _otoacoustic_r_6 = (array[12] == "") ? Double.NaN : Convert.ToDouble(array[12]);
            _otoacoustic_l_1 = (array[13] == "") ? Double.NaN : Convert.ToDouble(array[13]);
            _otoacoustic_l_2 = (array[14] == "") ? Double.NaN : Convert.ToDouble(array[14]);
            _otoacoustic_l_4 = (array[15] == "") ? Double.NaN : Convert.ToDouble(array[15]);
            _otoacoustic_l_6 = (array[16] == "") ? Double.NaN : Convert.ToDouble(array[16]);
            _otoacoustic_r_05_15 = (array[17] == "") ? Double.NaN : Convert.ToDouble(array[17]);
            _otoacoustic_r_15_25 = (array[18] == "") ? Double.NaN : Convert.ToDouble(array[18]);
            _otoacoustic_r_25_35 = (array[19] == "") ? Double.NaN : Convert.ToDouble(array[19]);
            _otoacoustic_r_35_45 = (array[20] == "") ? Double.NaN : Convert.ToDouble(array[20]);
            _otoacoustic_r_45_55 = (array[21] == "") ? Double.NaN : Convert.ToDouble(array[21]);
            _otoacoustic_l_05_15 = (array[22] == "") ? Double.NaN : Convert.ToDouble(array[22]);
            _otoacoustic_l_15_25 = (array[23] == "") ? Double.NaN : Convert.ToDouble(array[23]);
            _otoacoustic_l_25_35 = (array[24] == "") ? Double.NaN : Convert.ToDouble(array[24]);
            _otoacoustic_l_35_45 = (array[25] == "") ? Double.NaN : Convert.ToDouble(array[25]);
            _otoacoustic_l_45_55 = (array[26] == "") ? Double.NaN : Convert.ToDouble(array[26]);
            _aSSR_r_05 = (array[27] == "") ? Double.NaN : Convert.ToDouble(array[27]);
            _aSSR_r_1 = (array[28] == "") ? Double.NaN : Convert.ToDouble(array[28]);
            _aSSR_r_2 = (array[29] == "") ? Double.NaN : Convert.ToDouble(array[29]);
            _aSSR_r_4 = (array[30] == "") ? Double.NaN : Convert.ToDouble(array[30]);
            _aSSR_l_05 = (array[31] == "") ? Double.NaN : Convert.ToDouble(array[31]);
            _aSSR_l_1 = (array[32] == "") ? Double.NaN : Convert.ToDouble(array[32]);
            _aSSR_l_2 = (array[33] == "") ? Double.NaN : Convert.ToDouble(array[33]);
            _aSSR_l_4 = (array[34] == "") ? Double.NaN : Convert.ToDouble(array[34]);
            _kSVP_r_20 = (array[35] == "") ? Double.NaN : Convert.ToDouble(array[35]);
            _kSVP_r_40 = (array[36] == "") ? Double.NaN : Convert.ToDouble(array[36]);
            _kSVP_r_60 = (array[37] == "") ? Double.NaN : Convert.ToDouble(array[37]);
            _kSVP_l_20 = (array[38] == "") ? Double.NaN : Convert.ToDouble(array[38]);
            _kSVP_l_40 = (array[39] == "") ? Double.NaN : Convert.ToDouble(array[39]);
            _kSVP_l_60 = (array[40] == "") ? Double.NaN : Convert.ToDouble(array[40]);
        }

            private double CheckTimeObserv(double time)
            {
                if (time <= 3.0)
                    return 3.0;
                if (time <= 6.0)
                    return 6.0;
                if (time <= 9.0)
                    return 9.0;
                if (time <= 12.0)
                    return 12.0;
                return 16.0;
            }

            public string[] OutPutInfo()
            {
                return new string[] {_name,
                                                _anomaly,
                                                _genetic_illness,
                                                _mother.ToString(),
                                                _time_pregnancy_ill.ToString(),
                                                _son.ToString(),
                                                _time_ill.ToString(),
                                                _time_gestagration.ToString(),
                                                _time_observation.ToString(),
                                                _otoacoustic_r_1.ToString(),
                                                _otoacoustic_r_2.ToString(),
                                                _otoacoustic_r_4.ToString(),
                                                _otoacoustic_r_6.ToString(),
                                                _otoacoustic_l_1.ToString(),
                                                _otoacoustic_l_2.ToString(),
                                                _otoacoustic_l_4.ToString(),
                                                _otoacoustic_l_6.ToString(),
                                                _otoacoustic_r_05_15.ToString(),
                                                _otoacoustic_r_15_25.ToString(),
                                                _otoacoustic_r_25_35.ToString(),
                                                _otoacoustic_r_35_45.ToString(),
                                                _otoacoustic_r_45_55.ToString(),
                                                _otoacoustic_l_05_15.ToString(),
                                                _otoacoustic_l_15_25.ToString(),
                                                _otoacoustic_l_25_35.ToString(),
                                                _otoacoustic_l_35_45.ToString(),
                                                _otoacoustic_l_45_55.ToString(),
                                                _aSSR_r_05.ToString(),
                                                _aSSR_r_1.ToString(),
                                                _aSSR_r_2.ToString(),
                                                _aSSR_r_4.ToString(),
                                                _aSSR_l_05.ToString(),
                                                _aSSR_l_1.ToString(),
                                                _aSSR_l_2.ToString(),
                                                _aSSR_l_4.ToString(),
                                                _kSVP_r_20.ToString(),
                                                _kSVP_r_40.ToString(),
                                                _kSVP_r_60.ToString(),
                                                _kSVP_l_20.ToString(),
                                                _kSVP_l_40.ToString(),
                                                _kSVP_l_60.ToString()
                };
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
