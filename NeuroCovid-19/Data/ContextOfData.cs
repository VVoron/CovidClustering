using NeuroCovid19.Enumerations;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.Options;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.Data
{
    public class ContextOfData
    {
        public List<DataCOVIDEars> Childrens_Info;
        public KohanenOptions KohanenOptions;
        public DBScanOptions DBScanOptions;
        public Clasterisation SelectedClasterisation;
        public int[] PropIdsForTake = [0, 1, 2, 3, 4, 5, 6, 9, 26, 31, 32, 35];
        public ContextOfData()
        {
            Childrens_Info = new List<DataCOVIDEars>();

            //Варианты кластеризации
            KohanenOptions = new KohanenOptions();
            DBScanOptions = new DBScanOptions();

            SelectedClasterisation = Clasterisation.Kohanen;
        }
    }
}
