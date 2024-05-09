using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using NeuroCovid19.Core;
using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class ClasterVisualisationViewModel : ObservableObject
    {
        public Func<double, string> Formatter { get; set; }
        private int? _select_property { get; set; }
        public int? SelectProperty
        {
            get { return _select_property; }
            set {
                _select_property = value;
                ChangeSecondaryGraph();
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
                ChangeSecondaryGraph();
                OnPropertyChanged();
            }
        }

        private List<string> _clasteristationComboBox { get; set; }
        public object ClasterisationComboBox
        {
            get
            {
                return _clasteristationComboBox;
            }
            set
            {
                _clasteristationComboBox = (List<string>)value;
                OnPropertyChanged();
            }
        }

        private int _selectedClasterisaton { get; set; }
        public object SelectedClasterisation
        {
            get
            {
                return _selectedClasterisaton;
            }
            set
            {
                App.ContextOfData.SelectedClasterisation = (Enumerations.Clasterisation)value;
                _selectedClasterisaton = (int)value;
                SelectClastVM();
                GraphMainChart();
                ChangeSecondaryGraph();
                OnPropertyChanged();
            }
        }


        private List<DataCOVIDEars[]> _clastersData { get; set; }


        public ChartValues<TableInfo> PointsProperty { get; set; }
        public List<string> ColumnsProperty { get; set; }
        public string PropertyName { get; set; }
        public ObservableCollection<string> Properties { get; set; }
        private void ChangeSecondaryGraph()
        {
            if (_select_property == null || _selected_variant == null)
                return;

            PointsProperty = new ChartValues<TableInfo>();
            PropertyName = new ClasterisationProvider().PropertiesData()[(int)_select_property];
            OnPropertyChanged(nameof(PropertyName));

            for (int i = 0; i < AvarageData.Count; i++)
            {


                PointsProperty.Add(new TableInfo
                {
                    propertyName = App.ContextOfData.SelectedClasterisation != Enumerations.Clasterisation.DBScan ? 
                                  (i + 1).ToString() + " кластер":
                                  (i == 0 ? "Шум" : i.ToString() + " кластер"),
                    propertyValue = (_selected_variant == 1) ? Dispersion(i, AvarageData[i].GetData()[(int)_select_property]) : AvarageData[i].GetData()[(int)_select_property]
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

        private double Dispersion(int index, double avarage)
        {
            if (double.IsNaN(avarage))
                return 0;

            double dispersion = 0;
            int num_elements = 0;
            DataCOVIDEars[] data = _clastersData[index];
            for (int i = 0; i < data.Length; i++)
            {
                if (double.IsNaN(data[i].DataForClasterisation()[(int)_select_property]))
                    continue;

                dispersion += (data[i].DataForClasterisation()[(int)_select_property] - avarage) *
                                (data[i].DataForClasterisation()[(int)_select_property] - avarage);
                num_elements++;
            }

            return (num_elements == 0) ? 0 : dispersion / num_elements;
        }


        public ChartValues<GlobalInfo> ChartPoints { get; set; }
        public List<string> Columns { get; set; }
        public List<AvgCovidEars> AvarageData { get; set; }
        private void GraphMainChart()
        {
            AvarageData = new List<AvgCovidEars>();
            if (_clastersData != null)
            {
                ChartPoints = new ChartValues<GlobalInfo>();
                for (int i = 0; i < _clastersData.Count; i++)
                {
                    AvarageData.Add(new AvgCovidEars(_clastersData[i], i));
                    ChartPoints.Add(new GlobalInfo(i, _clastersData[i].Length, AvarageData.ElementAt(i)));
                }
                Columns = new List<string>(ChartPoints.Select(info => info.Label));

                var customerVmMapper = Mappers.Xy<GlobalInfo>()
                                                .X((value, index) => index)
                                                .Y(value => value.Value);
                Charting.For<GlobalInfo>(customerVmMapper);
            }
            OnPropertyChanged(nameof(AvarageData));
            OnPropertyChanged(nameof(ChartPoints));
            OnPropertyChanged(nameof(Columns));
        }

        private void SelectClastVM()
        {
            switch (App.ContextOfData.SelectedClasterisation)
            {
                case Enumerations.Clasterisation.Kohanen:
                    _clastersData = App.ContextOfData.KohanenOptions.ClastersInfo;
                    break;
                case Enumerations.Clasterisation.DBScan:
                    _clastersData = App.ContextOfData.DBScanOptions.ClastersInfo;
                    break;
                default:
                    break;
            }
        }

        public ClasterVisualisationViewModel()
        {
            Formatter = value => Math.Round(value, 2).ToString();

            Properties = new ObservableCollection<string>(new ClasterisationProvider().PropertiesData());
            AvarageData = new List<AvgCovidEars>();
            SelectedClasterisation = (int)App.ContextOfData.SelectedClasterisation;
            ClasterisationComboBox = new List<string>() { "Кохонен", "DBScan" };
            SelectClastVM();
            GraphMainChart();
        }
    }
}
