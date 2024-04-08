using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.Model
{
    public class GlobalInfo
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public List<TableInfo> Table { get; set; }
        public GlobalInfo(int index, int count, AvgCovidEars table)
        {
            Label = (App.ContextOfData.SelectedClasterisation == Enumerations.Clasterisation.DBScan ?
                   index.ToString() : (index + 1).ToString()) + " кластер";

            if (App.ContextOfData.SelectedClasterisation == Enumerations.Clasterisation.DBScan && index == 0)
                Label = "Шум";
            Value = count;
            Table = new List<TableInfo>();

            List<string> colomnsData = new ClasterisationProvider().PropertiesData();
            double[] data = table.GetData();
            Table.Add(new TableInfo { propertyName = "Количество", propertyValue = count });
            var props = App.ContextOfData.SelectedClasterisation == Enumerations.Clasterisation.Kohanen ?
                        App.ContextOfData.KohanenOptions.Properties :
                        App.ContextOfData.DBScanOptions.Properties;
            for (int i = 0; i < data.Length; i++)
            {
                if (!props[i].IsUsed)
                    continue;
                Table.Add(new TableInfo { propertyName = colomnsData[i], propertyValue = data[i] });
            }
        }
    }
}
