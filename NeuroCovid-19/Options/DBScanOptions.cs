using NeuroCovid19.MVVM.Model;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Options
{
    public class DBScanOptions
    {
        public double MinPts { get; set; }
        public double Eps { get; set; }
        public List<PropertiesModel> Properties { get; set; }
        public List<DataCOVIDEars[]> ClastersInfo { get; set; }
        public int SelectedClaster { get; set; }
        public DBScanOptions()
        {
            MinPts = 4;
            Eps = 2;
            SelectedClaster = 1;
            Properties = new List<PropertiesModel>();
            ClastersInfo = new List<DataCOVIDEars[]>();
        }
    }
}
