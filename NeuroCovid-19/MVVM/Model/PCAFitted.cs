using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class PCAFitted
    {
        public float _clastIndex { get; set; }
        public float[] PCAFeatures { get; set; }
    }
}
