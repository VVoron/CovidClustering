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

        // Классификация не требует особых настроек, храним пока что здесь
        public Method SelectedMethod;
        public int SelectedClass;

        public List<ClasterInfo> ClassificationClasses { get; set; }
        public int[] PropIdsForTake = [0, 1, 2, 3, 4, 5, 6, 9, 16, 21, 22, 25];

        public string SelectedClasterisationString => SelectedMethod == Method.Kohanen ? "Кохонен" :
                                                      SelectedMethod == Method.DBScan  ? "DBSCAN" :
                                                      "Классификация";

        public ContextOfData()
        {
            Childrens_Info = new List<DataCOVIDEars>();

            //Варианты кластеризации
            KohanenOptions = new KohanenOptions();
            DBScanOptions = new DBScanOptions();

            ClassificationClasses = new List<ClasterInfo>();

            SelectedMethod = Method.Kohanen;
        }
    }
}
