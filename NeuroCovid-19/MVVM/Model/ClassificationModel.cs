using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class HearingSample
    {
        public HearingSample() { }

        public HearingSample(DataCOVIDEars sample)
        {
            ObservationMonths = (float)sample.TimeObservation;
            GestWeeks = (float)sample.TimeGestagration;
            OAE_1k = sample.GetOaeAvarage_1k();
            OAE_2k = sample.GetOaeAvarage_1k();
            OAE_4k = sample.GetOaeAvarage_1k();
            OAE_6k = sample.GetOaeAvarage_1k();

            OAE_Mean = sample.GetOaeAvarage();
            OAE_HF_Tilt = sample.GetOaeTilt();
            ASSR_Mean_dB = (float)(sample.AssrLeft_avarage + sample.AssrRight_avarage) / 2f;
        }

        public float ObservationMonths { get; set; }
        public float GestWeeks { get; set; }

        public float OAE_1k { get; set; }
        public float OAE_2k { get; set; }
        public float OAE_4k { get; set; }
        public float OAE_6k { get; set; }

        public float OAE_Mean { get; set; }
        public float OAE_HF_Tilt { get; set; }

        public float ASSR_Mean_dB { get; set; }

        // Метка класса (1..7) — как текст, так удобнее для ML.NET
        public string Class { get; set; } = "";
    }

    public class HearingPrediction
    {
        public string PredictedLabel { get; set; } = "";
        public float[] Score { get; set; } = Array.Empty<float>();
        public string[] LabelNames { get; set; } = Array.Empty<string>();
    }
}
