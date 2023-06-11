using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Data
{
    public class ContextOfData
    {
        public List<KohanenDataCOVIDEars> Childrens_Info;
        public double? v;
        public double? Rk;
        public int? steps;
        public List<KohanenDataCOVIDEars>? w;
        public List<PropertiesModel> Properties;
        public List<KohanenDataCOVIDEars[]> ClastersInfo;
        public int SelectedСluster;
    }
}
