using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using NeuroCovid19.Core;
using NeuroCovid19.Extensions;
using NeuroCovid19.MVVM.View;
using NeuroCovid19.Providers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class DynamicGraphsViewModel: ObservableObject
    {
        private SeriesCollection _series;

        public SeriesCollection Series
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        private string _search { get; set; }
        public object Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = (string)value;
                ItemsList = new ObservableCollection<string>(_fullItemsList);
                if (!string.IsNullOrEmpty(_search))
                    ItemsList = new ObservableCollection<string>(_fullItemsList.Where(x => x.ToUpper().Contains(_search.ToUpper())).ToList());
                OnPropertyChanged(nameof(ItemsList));
            }
        }
        private ObservableCollection<string> _itemsList { get; set; }
        public ObservableCollection<string> ItemsList
        {
            get
            {
                return _itemsList;
            }
            set
            {
                _itemsList = value;
                OnPropertyChanged();
            }
        }
        private string _selectedItem { get; set; }
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                GetEnabledProperties();
                if (_selectedProp != null)
                    PlotNewGraph();
            }
        }
        public ObservableCollection<string> PropsList { get; set; }
        private string _selectedProp { get; set; }
        public string SelectedProp
        {
            get
            {
                return _selectedProp;
            }
            set
            {
                _selectedProp = value;
                PlotNewGraph();
            }
        }

        private void GetEnabledProperties()
        {
            if (_selectedItem == null)
                return;
            var items = App.ContextOfData.Childrens_Info.Where(x => !string.IsNullOrEmpty(x._name) && x._name.Contains(_selectedItem))
                                                        .DistinctBy(x => x._time_observation)
                                                        .OrderBy(x => x._time_observation).ToList();
            var count = new int[_fullPropsList.Count];
            for (int i = 0; i < _fullPropsList.Count; i++)
                foreach (var item in items)
                {
                    var prop = item.GetPropertyValueById(i + 9);
                    if (!double.IsNaN(prop))
                        count[i]++;
                }
            var props = new List<string>();
            for (int i = 0; i < items.Count; i++)
                if (count[i] > 1)
                    props.Add(_fullPropsList[i]);

            PropsList = new ObservableCollection<string>(props);
            OnPropertyChanged(nameof(PropsList));
        }

        private void PlotNewGraph()
        {
            _series = new SeriesCollection();
            var items = App.ContextOfData.Childrens_Info.Where(x => x._name == _selectedItem)
                                                        .DistinctBy(x => x._time_observation)
                                                        .OrderBy(x => x._time_observation).ToList();
            var selectedProp = _fullPropsList.IndexOf(_selectedProp);
            var values = items.Select(x => x.GetPropertyValueById(selectedProp + 9)).Where(x => !Double.IsNaN(x)).ToArray();
            var points = new LineSeries
            {
                Title = _selectedItem,
                Values = new ChartValues<double>(values),
                PointGeometry = DefaultGeometries.Circle
            };

            _series.Add(points);
            OnPropertyChanged(nameof(Series));
        }

        private List<string> _fullItemsList { get; set; }
        private List<string> _fullPropsList { get; set; }
        public DynamicGraphsViewModel()
        {
            _fullPropsList = new ClasterisationProvider().PropertiesData().Skip(6).ToList();
            _fullItemsList = App.ContextOfData.Childrens_Info.Where(x => !string.IsNullOrEmpty(x._name))
                                                             .GroupBy(x => x._name)
                                                             .Where(group => group.Select(x => x._time_observation).Distinct().Count() > 2)
                                                             .Select(group => group.Key).ToList();
            ItemsList = new ObservableCollection<string>(_fullItemsList);
        }
    }
}
