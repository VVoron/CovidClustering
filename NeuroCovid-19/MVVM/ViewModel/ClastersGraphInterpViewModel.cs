using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using NeuroCovid19.Core;
using NeuroCovid19.Extensions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using ScatterSeries = LiveCharts.Wpf.ScatterSeries;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class ClastersGraphInterpViewModel: ObservableObject
    {
        private SeriesCollection _series;
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Series
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
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
                App.ContextOfData.SelectedMethod = (Enumerations.Method)value;
                _selectedClasterisaton = (int)value;
                GetPoints();
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


        private void GetPoints()
        {
            _series = new SeriesCollection();
            List<ClasterInfo> clasters = null;
            List<PropertiesModel> properties = null;
            Enumerations.Method method = Enumerations.Method.Kohanen;
            switch (App.ContextOfData.SelectedMethod)
            {
                case Enumerations.Method.Kohanen:
                    clasters = App.ContextOfData.KohanenOptions.ClastersInfo;
                    properties = App.ContextOfData.KohanenOptions.Properties;
                    method = Enumerations.Method.Kohanen;
                    break;
                case Enumerations.Method.DBScan:
                    clasters = App.ContextOfData.DBScanOptions.ClastersInfo;
                    properties = App.ContextOfData.DBScanOptions.Properties;
                    method = Enumerations.Method.DBScan;
                    break;
                case Enumerations.Method.Classification:
                    clasters = App.ContextOfData.ClassificationClasses;
                    properties = App.ClassificationDefaultProperties;
                    method = Enumerations.Method.Classification;
                    break;
                default:
                    break;
            }
            if (clasters == null || !clasters.Any()
              || properties == null || !properties.Any())
                return;

            var points = ClasterVisualisationExtension.GetGraphPoints(clasters.Select(x => x.Items).ToList(), properties);
            if (points == null)
                return;
            for (int clastIndex = 0; clastIndex < clasters.Count; clastIndex++)
            {
                var ScatterSeries = new ScatterSeries
                {
                    Title = clasters[clastIndex].Name,
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Circle
                };
                var clasterPoints = points.Where(x => x._clastIndex == (float)clastIndex).ToList();
                foreach(var point in clasterPoints)
                {
                    if (double.IsNaN(point.PCAFeatures[0]))
                        point.PCAFeatures[0] = 0;
                    if (double.IsNaN(point.PCAFeatures[1]))
                        point.PCAFeatures[1] = 0;
                    ScatterSeries.Values.Add(new ObservablePoint(Math.Round(point.PCAFeatures[0], 2), Math.Round(point.PCAFeatures[1], 2)));
                }
                _series.Add(ScatterSeries);
            }

            OnPropertyChanged(nameof(Series));
        }

        public ClastersGraphInterpViewModel()
        {
            Formatter = value => Math.Round(value, 2).ToString();
            SelectedClasterisation = (int)App.ContextOfData.SelectedMethod;

            ClasterisationComboBox = new List<string>() { "Кохонен", "DBScan", "Классификация" };
        }
    }
}
