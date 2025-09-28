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
            this._time_pregnancy_ill = (float)(data.TimePregnancyIll * props[1].Coef);
            this._son = (float)(data.Son * props[2].Coef);
            this._time_ill = (float)(data.TimeIll * props[3].Coef);
            this._time_gestagration = (float)(data.TimeGestagration * props[4].Coef);
            this._time_observation = (float)(data.TimeObservation * props[5].Coef);
            //Otoacustic
            this._otoacoustic_r_max = (float)(data.OaeRightMax * props[6].Coef);
            this._otoacoustic_r_avarage = (float) (data.OaeRightAvarage * props[7].Coef);
            this._otoacoustic_r_num_nulls = (float)(data.OaeRightNumNulls * props[8].Coef);

            this._otoacoustic_l_max = (float)(data.OaeLeftMax * props[9].Coef);
            this._otoacoustic_l_avarage = (float)(data.OaeLeftAvarage * props[10].Coef);
            this._otoacoustic_l_num_nulls = (float)(data.OaeLeftNumNulls * props[11].Coef);

            //Assr
            this._aSSR_r_05 = (float)(data.AssrRight_05 * props[12].Coef);
            this._aSSR_r_1 = (float)(data.AssrRight_1 * props[13].Coef);
            this._aSSR_r_2 = (float)(data.AssrRight_2 * props[14].Coef);
            this._aSSR_r_4 = (float)(data.AssrRight_4 * props[15].Coef);
            this._aSSR_r_avarage = (float)(data.AssrRight_avarage * props[16].Coef);

            this._aSSR_l_05 = (float)(data.AssrLeft_05 * props[17].Coef);
            this._aSSR_l_1 = (float)(data.AssrLeft_1 * props[18].Coef);
            this._aSSR_l_2 = (float)(data.AssrLeft_2 * props[19].Coef);
            this._aSSR_l_4 = (float)(data.AssrLeft_4 * props[20].Coef);
            this._aSSR_l_avarage = (float)(data.AssrLeft_avarage * props[21].Coef);
            //KSVP
            this._kSVP_r_20 = (float)(data.KsvpRight_20 * props[22].Coef);
            this._kSVP_r_40 = (float)(data.KsvpRight_40 * props[23].Coef);
            this._kSVP_r_60 = (float)(data.KsvoRight_60 * props[24].Coef);

            this._kSVP_l_20 = (float)(data.KsvpLeft_20 * props[25].Coef);
            this._kSVP_l_40 = (float)(data.KsvpLeft_40 * props[26].Coef);
            this._kSVP_l_60 = (float)(data.KsvpLeft_60 * props[27].Coef);
        }
    }
}
