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
        public int[] PropIdsForSkip =  new int[] { 6, 10, 11, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 30, 31, 35, 37, 38, 39, 41, 42, 43 };
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
