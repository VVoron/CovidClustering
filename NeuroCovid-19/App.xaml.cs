using NeuroCovid19.Data;
using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.Options;
using NeuroCovid19.Providers;
using System.Collections.Generic;
using System.Windows;

namespace NeuroCovid19
{
    public partial class App : Application
    {
        public static ContextOfData ContextOfData { get; set; }
        public static List<PropertiesModel> ClassificationDefaultProperties { get; set; }

        public App()
        {
            ContextOfData = new ContextOfData();
            ContextOfData.KohanenOptions.Properties = new List<PropertiesModel>();
            ContextOfData.DBScanOptions.Properties = new List<PropertiesModel>();
            ClassificationDefaultProperties = new List<PropertiesModel>();
            List<string> col = new ClasterisationProvider().PropertiesData();
            for (int i = 0; i < col.Count; i++)
            {
                ContextOfData.KohanenOptions.Properties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
                ContextOfData.DBScanOptions.Properties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
                ClassificationDefaultProperties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
            }

            ClassificationDefaultProperties[4].IsUsed = true;
            ClassificationDefaultProperties[5].IsUsed = true;
            ClassificationDefaultProperties[6].IsUsed = true;
            ClassificationDefaultProperties[9].IsUsed = true;
            ClassificationDefaultProperties[16].IsUsed = true;
            ClassificationDefaultProperties[21].IsUsed = true;
        }
    }
}
