using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using NeuroCovid19.Core;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    class ClasterVisualisationViewModel : ObservableObject
    {
        private int? _select_property { get; set; }
        public int? SelectProperty
        {
            get { return _select_property; }
            set {
                _select_property = value;
                ChangeGraph();
                OnPropertyChanged();
            }
        }

        private int? _selected_variant { get; set; }
        public int? SelectedVariant
        {
            get { return _selected_variant; }
            set
            {
                _selected_variant = value;
                ChangeGraph();
                OnPropertyChanged();
            }
        }

        public ChartValues<TableInfo> PointsProperty { get; set; }
        public List<string> ColumnsProperty { get; set; }
        public string PropertyName { get; set; }
        public ObservableCollection<string> Properties { get; set; }

        private void ChangeGraph()
        {
            if (_select_property == null || _selected_variant == null)
                return;

            PointsProperty = new ChartValues<TableInfo>();
            PropertyName = KohanenFunc.PropertiesData()[(int)_select_property];
            OnPropertyChanged(nameof(PropertyName));

            for (int i = 0; i < AvarageData.Count; i++)
            {
                PointsProperty.Add(new TableInfo { 
                    propertyName = (i + 1).ToString() + " кластер",
                    propertyValue = AvarageData[i].GetAllData()[(int)_select_property] 
                });
                ColumnsProperty = new List<string>(PointsProperty.Select(info => info.propertyName));
            }
            var customerVmMapper = Mappers.Xy<TableInfo>()
                                                .X((value, index) => index)
                                                .Y(value => value.propertyValue);
            Charting.For<TableInfo>(customerVmMapper);
            OnPropertyChanged(nameof(PointsProperty));
            OnPropertyChanged(nameof(ColumnsProperty));
        }


        public ChartValues<GlobalInfo> ChartPoints { get; set; }
        public List<string> Columns { get; set; }
        public List<KohanenAvarageData> AvarageData { get; set; }



        public ClasterVisualisationViewModel()
        {
            Properties = new ObservableCollection<string>(KohanenFunc.PropertiesData());
            AvarageData = new List<KohanenAvarageData>();
            var data = App.contextOfData.ClastersInfo;
            if (data != null)
            {
                ChartPoints = new ChartValues<GlobalInfo>();
                for (int i = 0; i < data.Count; i++)
                {
                    AvarageData.Add(new KohanenAvarageData(App.contextOfData.ClastersInfo.ElementAt(i), i));
                    ChartPoints.Add(new GlobalInfo(i, App.contextOfData.ClastersInfo.ElementAt(i).Length, AvarageData.ElementAt(i)));
                }
                Columns = new List<string>(ChartPoints.Select(info => info.Label));

                var customerVmMapper = Mappers.Xy<GlobalInfo>()
                                                .X((value, index) => index)
                                                .Y(value => value.Value);
                Charting.For<GlobalInfo>(customerVmMapper);
            }
        }
    }
}
