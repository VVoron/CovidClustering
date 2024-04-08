using NeuroCovid19.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class DataCOVIDEarsFloatType
    {
        public float _clastIndex { get; set; }
        public float _mother { get; set; }
        public float _time_pregnancy_ill { get; set; }
        public float _son { get; set; }
        public float _time_ill { get; set; }
        public float _time_gestagration { get; set; }
        public float _time_observation { get; set; }
        //Otoacustic
        public float _otoacoustic_r_1 { get; set; }
        public float _otoacoustic_r_2 { get; set; }
        public float _otoacoustic_r_4 { get; set; }
        public float _otoacoustic_r_6 { get; set; }
        public float _otoacoustic_r_summary { get; set; }
        public float _otoacoustic_l_1 { get; set; }
        public float _otoacoustic_l_2 { get; set; }
        public float _otoacoustic_l_4 { get; set; }
        public float _otoacoustic_l_6 { get; set; }
        public float _otoacoustic_l_summary { get; set; }
        public float _otoacoustic_r_05_15 { get; set; }
        public float _otoacoustic_r_15_25 { get; set; }
        public float _otoacoustic_r_25_35 { get; set; }
        public float _otoacoustic_r_35_45 { get; set; }
        public float _otoacoustic_r_45_55 { get; set; }
        public float _otoacoustic_l_05_15 { get; set; }
        public float _otoacoustic_l_15_25 { get; set; }
        public float _otoacoustic_l_25_35 { get; set; }
        public float _otoacoustic_l_35_45 { get; set; }
        public float _otoacoustic_l_45_55 { get; set; }
        //Assr
        public float _aSSR_r_05 { get; set; }
        public float _aSSR_r_1 { get; set; }
        public float _aSSR_r_2 { get; set; }
        public float _aSSR_r_4 { get; set; }
        public float _aSSR_r_summary { get; set; }
        public float _aSSR_l_05 { get; set; }
        public float _aSSR_l_1 { get; set; }
        public float _aSSR_l_2 { get; set; }
        public float _aSSR_l_4 { get; set; }
        public float _aSSR_l_summary { get; set; }
        //KSVP
        public float _kSVP_r_20 { get; set; }
        public float _kSVP_r_40 { get; set; }
        public float _kSVP_r_60 { get; set; }
        public float _kSVP_r_summary { get; set; }
        public float _kSVP_l_20 { get; set; }
        public float _kSVP_l_40 { get; set; }
        public float _kSVP_l_60 { get; set; }
        public float _kSVP_l_summary { get; set; }

        public DataCOVIDEarsFloatType(DataCOVIDEars data, float clasterIndex, List<PropertiesModel> props)
        {
            this._clastIndex = clasterIndex;
            this._mother = (float)(data._mother * props[0].Coef);
            this._time_pregnancy_ill = (float)(data._time_pregnancy_ill * props[1].Coef);
            this._son = (float)(data._son * props[2].Coef);
            this._time_ill = (float)(data._time_ill * props[3].Coef);
            this._time_gestagration = (float)(data._time_gestagration * props[4].Coef);
            this._time_observation = (float)(data._time_observation * props[5].Coef);
            //Otoacustic
            this._otoacoustic_r_1 = (float)(data._otoacoustic_r_1 * props[6].Coef);
            this._otoacoustic_r_2 = (float) (data._otoacoustic_r_2 * props[7].Coef);
            this._otoacoustic_r_4 = (float)(data._otoacoustic_r_4 * props[8].Coef);
            this._otoacoustic_r_6 = (float)(data._otoacoustic_r_6 * props[9].Coef);
            this._otoacoustic_r_summary = (float)(data._otoacoustic_r_summary * props[10].Coef);

            this._otoacoustic_l_1 = (float) (data._otoacoustic_l_1 * props[11].Coef);
            this._otoacoustic_l_2 = (float)(data._otoacoustic_l_2 * props[12].Coef);
            this._otoacoustic_l_4 = (float)(data._otoacoustic_l_4 * props[13].Coef);
            this._otoacoustic_l_6 = (float)(data._otoacoustic_l_6 * props[14].Coef);
            this._otoacoustic_l_summary = (float)(data._otoacoustic_l_summary * props[15].Coef);

            this._otoacoustic_r_05_15 = (float)(data._otoacoustic_r_05_15 * props[16].Coef);
            this._otoacoustic_r_15_25 = (float)(data._otoacoustic_r_15_25 * props[17].Coef);
            this._otoacoustic_r_25_35 = (float)(data._otoacoustic_r_25_35 * props[18].Coef);
            this._otoacoustic_r_35_45 = (float)(data._otoacoustic_r_35_45 * props[19].Coef);
            this._otoacoustic_r_45_55 = (float)(data._otoacoustic_r_45_55 * props[20].Coef);
            this._otoacoustic_l_05_15 = (float)(data._otoacoustic_l_05_15 * props[21].Coef);
            this._otoacoustic_l_15_25 = (float)(data._otoacoustic_l_15_25 * props[22].Coef);
            this._otoacoustic_l_25_35 = (float)(data._otoacoustic_l_25_35 * props[23].Coef);
            this._otoacoustic_l_35_45 = (float)(data._otoacoustic_l_35_45 * props[24].Coef);
            this._otoacoustic_l_45_55 = (float)(data._otoacoustic_l_45_55 * props[25].Coef);
            //Assr
            this._aSSR_r_05 = (float)(data._aSSR_r_05 * props[26].Coef);
            this._aSSR_r_1 = (float)(data._aSSR_r_1 * props[27].Coef);
            this._aSSR_r_2 = (float)(data._aSSR_r_2 * props[28].Coef);
            this._aSSR_r_4 = (float)(data._aSSR_r_4 * props[29].Coef);
            this._aSSR_r_summary = (float)(data._aSSR_r_summary * props[30].Coef);

            this._aSSR_l_05 = (float)(data._aSSR_l_05 * props[31].Coef);
            this._aSSR_l_1 = (float)(data._aSSR_l_1 * props[32].Coef);
            this._aSSR_l_2 = (float)(data._aSSR_l_2 * props[33].Coef);
            this._aSSR_l_4 = (float)(data._aSSR_l_4 * props[34].Coef);
            this._aSSR_l_summary = (float)(data._aSSR_l_summary * props[35].Coef);
            //KSVP
            this._kSVP_r_20 = (float)(data._kSVP_r_20 * props[36].Coef);
            this._kSVP_r_40 = (float)(data._kSVP_r_40 * props[37].Coef);
            this._kSVP_r_60 = (float)(data._kSVP_r_60 * props[38].Coef);
            this._kSVP_r_summary = (float)(data._kSVP_r_summary * props[39].Coef);

            this._kSVP_l_20 = (float)(data._kSVP_l_20 * props[40].Coef);
            this._kSVP_l_40 = (float)(data._kSVP_l_40 * props[41].Coef);
            this._kSVP_l_60 = (float)(data._kSVP_l_60 * props[42].Coef);
            this._kSVP_l_summary = (float)(data._kSVP_l_summary * props[43].Coef);
        }
    }
}
