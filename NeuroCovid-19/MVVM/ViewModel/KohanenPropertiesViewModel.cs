using NeuroCovid19.Core;
using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class KohanenPropertiesViewModel : ObservableObject
    {
        public RelayCommand SelectAll { get; set; }
        public RelayCommand UnSelectAll { get; set; }
        public RelayCommand SelectStandart { get; set; }
        public RelayCommand ClearAll { get; set; }
        public RelayCommand AddClast { get; set; }

        public double? _v { get; set; }
        public object V
        {
            get
            {
                return _v;
            }
            set
            {
                App.ContextOfData.KohanenOptions.V = double.Parse(value.ToString().Replace('.', ','));
                _v = App.ContextOfData.KohanenOptions.V;
                OnPropertyChanged();
            }
        }
        private double? _Rk { get; set; }
        public object Rk
        {
            get
            {
                return _Rk;
            }
            set
            {
                App.ContextOfData.KohanenOptions.Rk = double.Parse(value.ToString().Replace('.', ','));
                _Rk = App.ContextOfData.KohanenOptions.Rk;
                OnPropertyChanged();
            }
        }
        private int? _steps { get; set; }
        public object Steps
        {
            get
            {
                return _steps;
            }
            set
            {
                App.ContextOfData.KohanenOptions.Steps = Int32.Parse(value.ToString().Replace('.', ','));
                _steps = App.ContextOfData.KohanenOptions.Steps;
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

        private List<DataCOVIDEars> _w { get; set; }
        public int numClastsToCreate { get; set; }
        public object WCoefs
        {
            get
            {
                return _w;
            }
            set
            {
                _w = (List<DataCOVIDEars>)value;
                OnPropertyChanged();
            }
        }
        public KohanenPropertiesViewModel()
        {
            V = App.ContextOfData.KohanenOptions.V;
            Rk = App.ContextOfData.KohanenOptions.Rk;
            Steps = App.ContextOfData.KohanenOptions.Steps;
            _w = App.ContextOfData.KohanenOptions.W;
            PropertiesList = App.ContextOfData.KohanenOptions.Properties;
            numClastsToCreate = 1;

            //RelayCommands for properties
            SelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = true;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.KohanenOptions.Properties;
            });
            UnSelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = false;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.KohanenOptions.Properties;
            });
            SelectStandart = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = false;
                foreach (int index in App.ContextOfData.PropIdsForTake)
                    _properties[index].IsUsed = true;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.KohanenOptions.Properties;
            });
            ClearAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                {
                    prop.IsUsed = false;
                    prop.Coef = 1;
                }
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.ContextOfData.KohanenOptions.Properties;
            });
            AddClast = new RelayCommand(x =>
            {
                for (int i = 0; i < numClastsToCreate; i++)
                {
                    _w.Add(new DataCOVIDEars(new string[41]));
                }
                WCoefs = new List<DataCOVIDEars>();
                WCoefs = App.ContextOfData.KohanenOptions.W;
                numClastsToCreate = 0;
                OnPropertyChanged(nameof(numClastsToCreate));
            });
        }
    }
}
