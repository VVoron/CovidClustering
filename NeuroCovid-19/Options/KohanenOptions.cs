using NeuroCovid19.Data;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Options
{
    public class KohanenOptions
    {
        public double? V { get; set; }
        public double? Rk { get; set; }
        public int? Steps { get; set; }
        public List<DataCOVIDEars>? W { get; set; }
        public List<PropertiesModel> Properties { get; set; }
        public List<DataCOVIDEars[]> ClastersInfo { get; set; }
        public int SelectedClaster { get; set; }

        public KohanenOptions()
        {
            V = 0.3;
            Rk = 1.8;
            Steps = 100;
            W = new List<DataCOVIDEars>();
            Properties = new List<PropertiesModel>();
            ClastersInfo = new List<DataCOVIDEars[]>();
        }
    }
}
