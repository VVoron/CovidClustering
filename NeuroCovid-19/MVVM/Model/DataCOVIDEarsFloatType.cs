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
        public float _otoacoustic_r_avarage { get; set; }
        public float _otoacoustic_r_num_nulls { get; set; }
        public float _otoacoustic_r_max { get; set; }
        public float _otoacoustic_l_avarage { get; set; }
        public float _otoacoustic_l_num_nulls { get; set; }
        public float _otoacoustic_l_max { get; set; }
        
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
        public float _aSSR_r_avarage { get; set; }
        public float _aSSR_l_05 { get; set; }
        public float _aSSR_l_1 { get; set; }
        public float _aSSR_l_2 { get; set; }
        public float _aSSR_l_4 { get; set; }
        public float _aSSR_l_avarage { get; set; }
        //KSVP
        public float _kSVP_r_20 { get; set; }
        public float _kSVP_r_40 { get; set; }
        public float _kSVP_r_60 { get; set; }
        public float _kSVP_l_20 { get; set; }
        public float _kSVP_l_40 { get; set; }
        public float _kSVP_l_60 { get; set; }

        public DataCOVIDEarsFloatType(DataCOVIDEars data, float clasterIndex, List<PropertiesModel> props)
        {
            this._clastIndex = clasterIndex;
            this._mother = (float)(data.Mother * props[0].Coef);
            this._time_pregnancy_ill = (float)(data.Time_pregnancy_ill * props[1].Coef);
            this._son = (float)(data.Son * props[2].Coef);
            this._time_ill = (float)(data.Time_ill * props[3].Coef);
            this._time_gestagration = (float)(data.Time_gestagration * props[4].Coef);
            this._time_observation = (float)(data.Time_observation * props[5].Coef);
            //Otoacustic
            this._otoacoustic_r_max = (float)(data.Otoacoustic_r_max * props[6].Coef);
            this._otoacoustic_r_avarage = (float) (data.Otoacoustic_r_avarage * props[7].Coef);
            this._otoacoustic_r_num_nulls = (float)(data.Otoacoustic_r_num_nulls * props[8].Coef);

            this._otoacoustic_l_max = (float)(data.Otoacoustic_l_max * props[9].Coef);
            this._otoacoustic_l_avarage = (float)(data.Otoacoustic_l_avarage * props[10].Coef);
            this._otoacoustic_l_num_nulls = (float)(data.Otoacoustic_l_num_nulls * props[11].Coef);

            this._otoacoustic_r_05_15 = (float)(data.Otoacoustic_r_05_15 * props[12].Coef);
            this._otoacoustic_r_15_25 = (float)(data.Otoacoustic_r_15_25 * props[13].Coef);
            this._otoacoustic_r_25_35 = (float)(data.Otoacoustic_r_25_35 * props[14].Coef);
            this._otoacoustic_r_35_45 = (float)(data.Otoacoustic_r_35_45 * props[15].Coef);
            this._otoacoustic_r_45_55 = (float)(data.Otoacoustic_r_45_55 * props[16].Coef);
            this._otoacoustic_l_05_15 = (float)(data.Otoacoustic_l_05_15 * props[17].Coef);
            this._otoacoustic_l_15_25 = (float)(data.Otoacoustic_l_15_25 * props[18].Coef);
            this._otoacoustic_l_25_35 = (float)(data.Otoacoustic_l_25_35 * props[19].Coef);
            this._otoacoustic_l_35_45 = (float)(data.Otoacoustic_l_35_45 * props[20].Coef);
            this._otoacoustic_l_45_55 = (float)(data.Otoacoustic_l_45_55 * props[21].Coef);
            //Assr
            this._aSSR_r_05 = (float)(data.ASSR_r_05 * props[22].Coef);
            this._aSSR_r_1 = (float)(data.ASSR_r_1 * props[23].Coef);
            this._aSSR_r_2 = (float)(data.ASSR_r_2 * props[24].Coef);
            this._aSSR_r_4 = (float)(data.ASSR_r_4 * props[25].Coef);
            this._aSSR_r_avarage = (float)(data.ASSR_r_avarage * props[26].Coef);

            this._aSSR_l_05 = (float)(data.ASSR_l_05 * props[27].Coef);
            this._aSSR_l_1 = (float)(data.ASSR_l_1 * props[28].Coef);
            this._aSSR_l_2 = (float)(data.ASSR_l_2 * props[29].Coef);
            this._aSSR_l_4 = (float)(data.ASSR_l_4 * props[30].Coef);
            this._aSSR_l_avarage = (float)(data.ASSR_l_avarage * props[31].Coef);
            //KSVP
            this._kSVP_r_20 = (float)(data.KSVP_r_20 * props[32].Coef);
            this._kSVP_r_40 = (float)(data.KSVP_r_40 * props[33].Coef);
            this._kSVP_r_60 = (float)(data.KSVP_r_60 * props[34].Coef);

            this._kSVP_l_20 = (float)(data.KSVP_l_20 * props[35].Coef);
            this._kSVP_l_40 = (float)(data.KSVP_l_40 * props[36].Coef);
            this._kSVP_l_60 = (float)(data.KSVP_l_60 * props[37].Coef);
        }
    }
}
