using NeuroCovid19.Core;
using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class DBScanPropertiesViewModel : ObservableObject
    {
        public RelayCommand SelectAll { get; set; }
        public RelayCommand UnSelectAll { get; set; }
        public RelayCommand SelectStandart { get; set; }
        public RelayCommand ClearAll { get; set; }

        public double? _eps { get; set; }
        public object Eps
        {
            get
            {
                return _eps;
            }
            set
            {
                App.ContextOfData.DBScanOptions.Eps = double.Parse(value.ToString().Replace('.', ','));
                _eps = App.ContextOfData.DBScanOptions.Eps;
                OnPropertyChanged();
            }
        }
        private double? _minPts { get; set; }
        public object MinPts
        {
            get
            {
                return _minPts;
            }
            set
            {
                App.ContextOfData.DBScanOptions.MinPts = double.Parse(value.ToString().Replace('.', ','));
                _minPts = App.ContextOfData.DBScanOptions.MinPts;
                OnPropertyChanged();
            }
        }

        private List<PropertiesModel> _properties { get; set; }
        public object PropertiesList
        {
            get
            {
                return _properties;
            }
            set
            {
                _properties = (List<PropertiesModel>)value;
                OnPropertyChanged();
            }
        }

        public DBScanPropertiesViewModel()
        {
            Eps = App.ContextOfData.DBScanOptions.Eps;
            MinPts = App.ContextOfData.DBScanOptions.MinPts;
            PropertiesList = App.ContextOfData.DBScanOptions.Properties;

            //RelayCommands for properties
            SelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = true;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.DBScanOptions.Properties;
            });
            UnSelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = false;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.DBScanOptions.Properties;
            });
            SelectStandart = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = true;
                foreach (int index in App.ContextOfData.PropIdsForSkip)
                    _properties[index].IsUsed = false;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.DBScanOptions.Properties;
            });
            ClearAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                {
                    prop.IsUsed = false;
                    prop.Coef = 1;
                }
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.DBScanOptions.Properties;
            });
        }
    }
}
