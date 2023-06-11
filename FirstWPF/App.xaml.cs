using NeuroCovid19.Data;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NeuroCovid19
{
    public partial class App : Application
    {
        public static ContextOfData contextOfData { get; set; }
        public App()
        {
            contextOfData = new ContextOfData();
            contextOfData.Properties = new List<PropertiesModel>();
            List<string> col = KohanenFunc.PropertiesData();
            for (int i = 0; i < col.Count; i++)
                contextOfData.Properties.Add(new PropertiesModel { Id = i, Name = col[i], IsUsed = false, Coef = 1 });
            
            contextOfData.v = 0.3;
            contextOfData.Rk = 1.8;
            contextOfData.steps = 100;
            contextOfData.w = new List<KohanenDataCOVIDEars>();
        }
    }
}
