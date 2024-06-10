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
using System.Windows.Threading;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class MainViewModel: ObservableObject
    {
        public RelayCommand InputDataViewCommand { get; set; }
        public RelayCommand ClasterisationInfoViewCommand { get; set; }
        public RelayCommand OutputDataViewCommand { get; set; }
        public RelayCommand ClasterVisualisationViewCommand { get; set; }
        public RelayCommand ClastersGraphInterpViewCommand { get; set; }
        public RelayCommand DynamicGraphsViewCommand { get; set; }
        public RelayCommand UploadViewCommand { get; set; }

        public InputDataViewModel InputDataVM { get; set; }
        public ClasterisationInfoViewModel ClasterisationInfoVM { get; set; }
        public OutputViewModel OutputDataVM { get; set; }
        public ClasterVisualisationViewModel ClasterVisualisationVM { get; set; }
        public ClastersGraphInterpViewModel ClastersGraphInterpVM { get; set; }
        public DynamicGraphsViewModel DynamicGraphsVM { get; set; }
        public UploadViewModel UploadVM { get; set; }
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
            ClastersGraphInterpVM = new ClastersGraphInterpViewModel();
            DynamicGraphsVM = new DynamicGraphsViewModel();
            UploadVM = new UploadViewModel();

            CurrentView = InputDataVM;
            PageTitle = "Исходные данные";


            InputDataViewCommand = new RelayCommand(x =>
            {
                PageTitle = "Исходные данные";
                if (App.ContextOfData.Childrens_Info != null && App.ContextOfData.Childrens_Info.Count() != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { CurrentView = InputDataVM; await Task.Delay(1500); Loader = null; });
                }
                else
                    CurrentView = InputDataVM;
            });
            ClasterisationInfoViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Параметры кластеризации";
                CurrentView = ClasterisationInfoVM;
            });
            OutputDataViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Информация по кластерам";
                if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { CurrentView = OutputDataVM; await Task.Delay(1000); Loader = null; });
                }
                else
                    CurrentView = OutputDataVM;
            });
            ClasterVisualisationViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Визуализация кластеров";
                if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { CurrentView = ClasterVisualisationVM; await Task.Delay(1500); Loader = null; });
                }
                else
                    CurrentView = ClasterVisualisationVM;
            });
            ClastersGraphInterpViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Графическое представление кластеров";
                if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Count != 0)
                {
                    Loader = new Loading();
                    Task.Run(async () => { CurrentView = ClastersGraphInterpVM; await Task.Delay(1500); Loader = null; });
                }
                else
                    CurrentView = ClastersGraphInterpVM;
            });
            DynamicGraphsViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Динамика изменения";
                CurrentView = DynamicGraphsVM;
            });
            UploadViewCommand = new RelayCommand(x =>
            {
                Loader = null;
                PageTitle = "Сохраниение/Загрузка настроек";
                CurrentView = UploadVM;
            });
        }
    }
}
