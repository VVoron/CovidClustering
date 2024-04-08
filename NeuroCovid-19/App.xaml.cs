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
        public App()
        {
            ContextOfData = new ContextOfData();
            ContextOfData.KohanenOptions.Properties = new List<PropertiesModel>();
            ContextOfData.DBScanOptions.Properties = new List<PropertiesModel>();
            List<string> col = new ClasterisationProvider().PropertiesData();
            for (int i = 0; i < col.Count; i++)
            {
                ContextOfData.KohanenOptions.Properties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
                ContextOfData.DBScanOptions.Properties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
            }
        }
    }
}
