using NeuroCovid19.Core;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class ClasterisationInfoViewModel:ObservableObject
    {
        private object _currentClasterisationView;
        public object CurrentClasterisationView
        {
            get { return _currentClasterisationView; }
            set
            {
                _currentClasterisationView = value;
                OnPropertyChanged();
            }
        }

        public KohanenPropertiesViewModel KohanenPropertiesVM { get; set; }
        public DBScanPropertiesViewModel DBScanPropertiesVM { get; set; }

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
                OnPropertyChanged();
            }
        }

        private void SelectClastVM()
        {
            switch (App.ContextOfData.SelectedClasterisation)
            {
                case Enumerations.Clasterisation.Kohanen:
                    CurrentClasterisationView = KohanenPropertiesVM;
                    break;
                case Enumerations.Clasterisation.DBScan:
                    CurrentClasterisationView = DBScanPropertiesVM;
                    break;
                default:
                    break;
            }
        }

        public ClasterisationInfoViewModel()
        {
            KohanenPropertiesVM = new KohanenPropertiesViewModel();
            DBScanPropertiesVM = new DBScanPropertiesViewModel();

            SelectedClasterisation = (int)App.ContextOfData.SelectedClasterisation;

            ClasterisationComboBox = new List<string>() { "Кохонен", "DBScan" };
            SelectClastVM();
        }
    }
}
