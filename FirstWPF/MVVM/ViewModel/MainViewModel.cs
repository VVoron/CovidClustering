using NeuroCovid19.Core;
using NeuroCovid19.Data;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    internal class MainViewModel: ObservableObject
    {
        public RelayCommand InputDataViewCommand { get; set; }
        public RelayCommand ClasterisationInfoViewCommand { get; set; }
        public RelayCommand OutputDataViewCommand { get; set; }
        public RelayCommand ClasterVisualisationViewCommand { get; set; }
        public InputDataViewModel InputDataVM { get; set; }
        public ClasterisationInfoViewModel ClasterisationInfoVM { get; set; }
        public OutputViewModel OutputDataVM { get; set; }
        public ClasterVisualisationViewModel ClasterVisualisationVM { get; set; }
        private string _pageTitle;
        public object PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = (string)value;
                OnPropertyChanged();
            }
        }
        private object _load;
        public object Loader
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged();
            }
        }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            InputDataVM = new InputDataViewModel();
            ClasterisationInfoVM = new ClasterisationInfoViewModel();
            OutputDataVM = new OutputViewModel();
            ClasterVisualisationVM = new ClasterVisualisationViewModel();
            CurrentView = InputDataVM;
            PageTitle = "Исходные данные";
            InputDataViewCommand = new RelayCommand(x =>
            {
                PageTitle = "Исходные данные";
                if (App.contextOfData.ClastersInfo != null && App.contextOfData.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    CurrentView = InputDataVM;
                    Task.Run(async () => { await Task.Delay(1000); Loader = null; });
                }
                CurrentView = InputDataVM;
            });
            ClasterisationInfoViewCommand = new RelayCommand(x =>
            {
                PageTitle = "Параметры кластеризации";
                CurrentView = ClasterisationInfoVM;
            });
            OutputDataViewCommand = new RelayCommand(x =>
            {
                PageTitle = "Информация по кластерам";
                if (App.contextOfData.ClastersInfo != null && App.contextOfData.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { await Task.Delay(1000); Loader = null; });
                }
                CurrentView = OutputDataVM;
            });
            ClasterVisualisationViewCommand = new RelayCommand(x =>
            {
                PageTitle = "Визуализация кластеров";
                if (App.contextOfData.ClastersInfo != null && App.contextOfData.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { await Task.Delay(1500); Loader = null; });
                }
                CurrentView = ClasterVisualisationVM;
            });
        }
    }
}
