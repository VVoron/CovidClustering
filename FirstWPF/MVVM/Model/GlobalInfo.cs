using NeuroCovid19.Functions;
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
        public GlobalInfo(int index, int count, KohanenAvarageData table)
        {
            Label = (index + 1).ToString() + " кластер";
            Value = count;
            Table = new List<TableInfo>();

            List<string> colomnsData = KohanenFunc.PropertiesData();
            double[] data = table.GetAllData();
            Table.Add(new TableInfo { propertyName = "Количество", propertyValue = count });
            for (int i = 0; i < data.Length; i++)
            {
                Table.Add(new TableInfo { propertyName = colomnsData[i], propertyValue = data[i] });
            }
        }
    }
}
