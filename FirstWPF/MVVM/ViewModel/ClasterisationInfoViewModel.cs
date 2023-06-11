using NeuroCovid19.Core;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace NeuroCovid19.MVVM.ViewModel
{
    class ClasterisationInfoViewModel:ObservableObject
    {
        public RelayCommand SelectAll { get; set; }
        public RelayCommand UnSelectAll { get; set; }
        public RelayCommand SelectStandart { get; set; }
        public RelayCommand ClearAll { get; set; }
        public RelayCommand AddClust { get; set; }

        private int[] _indexOfStandartToSkip = new int[] { 6, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 28, 33, 34, 36, 37 };

        public double? _v { get; set; }
        public object V
        {
            get
            {
                return _v;
            }
            set
            {
                App.contextOfData.v = double.Parse(value.ToString().Replace('.',','));
                _v = App.contextOfData.v;
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
                App.contextOfData.Rk = double.Parse(value.ToString().Replace('.', ','));
                _Rk = App.contextOfData.Rk;
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
                App.contextOfData.steps = Int32.Parse(value.ToString().Replace('.', ','));
                _steps = App.contextOfData.steps;
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
        private List<KohanenDataCOVIDEars> _w { get; set; }
        public int numClustsToCreate { get; set; }
        public object WCoefs
        {
            get
            {
                return _w;
            }
            set
            {
                _w = (List<KohanenDataCOVIDEars>)value;
                OnPropertyChanged();
            }
        }
        public ClasterisationInfoViewModel()
        {
            V = App.contextOfData.v;
            Rk = App.contextOfData.Rk;
            Steps = App.contextOfData.steps;
            PropertiesList = App.contextOfData.Properties;
            _w = App.contextOfData.w;
            numClustsToCreate = 1;

            //RelayCommands for properties
            SelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = true;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.contextOfData.Properties;
            });
            UnSelectAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = false;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.contextOfData.Properties;
            });
            SelectStandart = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                    prop.IsUsed = true;
                foreach (int index in _indexOfStandartToSkip)
                    _properties[index].IsUsed = false;
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.contextOfData.Properties;
            });
            ClearAll = new RelayCommand(x =>
            {
                foreach (PropertiesModel prop in _properties)
                {
                    prop.IsUsed = false;
                    prop.Coef = 1;
                }
                PropertiesList = new List<PropertiesModel>();
                PropertiesList = App.contextOfData.Properties;
            });
            AddClust = new RelayCommand(x =>
            {
                for (int i = 0; i < numClustsToCreate; i++)
                {
                    _w.Add(new KohanenDataCOVIDEars(new string[41]));
                }
                WCoefs = new List<KohanenDataCOVIDEars>();
                WCoefs = App.contextOfData.w;
                numClustsToCreate = 0;
                OnPropertyChanged(nameof(numClustsToCreate));
            });
        }
    }
}
