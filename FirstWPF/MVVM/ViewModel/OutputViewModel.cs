using NeuroCovid19.Core;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NeuroCovid19.MVVM.ViewModel
{
    internal class OutputViewModel: ObservableObject
    {
        KohanenFunc KohanenFunc = new KohanenFunc();
        private object _load;
        public RelayCommand KohanenSelfStudy { get; set; }
        public RelayCommand KohanenWithW { get; set; }
        private KohanenDataCOVIDEars[] _data { get; set; }
        public object Load
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged();
            }
        }
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = (KohanenDataCOVIDEars[])value;
                OnPropertyChanged();
            }
        }
        private int _selectedCluster { get; set; }
        public object SelectedCluster
        {
            get
            {
                return _selectedCluster;
            }
            set
            {
                _selectedCluster = Convert.ToInt32(value);
                App.contextOfData.SelectedСluster = _selectedCluster;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Load = new Loading();
                });
                Task.Run(async () =>
                {
                    Data = App.contextOfData.ClastersInfo[_selectedCluster];
                    await Task.Delay(1000);
                    Load = null;
                });
                OnPropertyChanged();
            }
        }
        private List<string> _clusterComboBox { get; set; }
        public object ClusterComboBox
        {
            get
            {
                return _clusterComboBox;
            }
            set
            {
                _clusterComboBox = (List<string>)value;
                OnPropertyChanged();
            }
        }

        private double[,] _allNormalizeData { get; set; }
        private double[] _coefs { get; set; }
        private double[] _wCoefs { get; set; }
        private void doFirstOfAll()
        {
            int countProps = 0;
            foreach (PropertiesModel prop in App.contextOfData.Properties)
                if (prop.IsUsed)
                    countProps++;

            List<KohanenDataCOVIDEars> DataWithSkipping = new List<KohanenDataCOVIDEars>();
            foreach (KohanenDataCOVIDEars data in App.contextOfData.Childrens_Info)
            {
                bool checker = true;
                double[] row = data.DataForClasterisation();
                for (int i = 0; i < row.Length; i++)
                    if (App.contextOfData.Properties[i].IsUsed && Double.IsNaN(row[i])){
                        checker = false; 
                        break;
                    }
                if (checker)
                    DataWithSkipping.Add(data);
            }

            _allNormalizeData = new double[DataWithSkipping.Count, countProps];
            _coefs = new double[countProps];

            for (int i = 0; i < DataWithSkipping.Count; i++)
            {
                double[] row = DataWithSkipping[i].DataForClasterisation();
                int colomnIndex = 0;
                for (int j = 0; j < row.Length; j++)
                    if (App.contextOfData.Properties[j].IsUsed)
                    {
                        _allNormalizeData[i, colomnIndex] = row[j];
                        _coefs[colomnIndex] = App.contextOfData.Properties[j].Coef;
                        colomnIndex++;
                    }
            }

            _allNormalizeData = KohanenFunc.NeuroStart(_allNormalizeData);
        }
        private async Task SelfStudy()
        {
            try
            {
                doFirstOfAll();
                List<string> clustComboBox = new List<string>();
                double[,] w = KohanenFunc.WStartWithout(_allNormalizeData, (double)App.contextOfData.Rk, (double)App.contextOfData.v, _coefs);
                w = KohanenFunc.WCorrect(_allNormalizeData, (double)App.contextOfData.v, (int)App.contextOfData.steps, w, _coefs);
                string[] childsInClusts = KohanenFunc.ChildInClusters(_allNormalizeData, w, (double)App.contextOfData.Rk);
                App.contextOfData.ClastersInfo = new List<KohanenDataCOVIDEars[]>();
                for (int i = 0; i < childsInClusts.Length; i++)
                {
                    clustComboBox.Add((i + 1).ToString() + " кластер");
                    string[] row = childsInClusts[i].Split(' ');
                    App.contextOfData.ClastersInfo.Add(new KohanenDataCOVIDEars[row.Length]);
                    for (int j = 0; j < row.Length; j++)
                    {
                        App.contextOfData.ClastersInfo[i][j] = App.contextOfData.Childrens_Info[Convert.ToInt32(row[j]) - 1];
                    }
                }
                _clusterComboBox = clustComboBox;
            }
            finally
            {
                OnPropertyChanged(nameof(ClusterComboBox));
                SelectedCluster = 0;
                await Task.Delay(1500);
                Load = null;
            }
        }
        public OutputViewModel() {
            if (App.contextOfData.ClastersInfo != null)
            {
                List<string> clustComboBox = new List<string>();
                for (int i = 0; i < App.contextOfData.ClastersInfo.Count; i++)
                    clustComboBox.Add((i + 1).ToString() + " кластер");


                _selectedCluster = App.contextOfData.SelectedСluster;
                Data = App.contextOfData.ClastersInfo[_selectedCluster];
                ClusterComboBox = clustComboBox;
            }

            KohanenSelfStudy = new RelayCommand(x => {
                if (App.contextOfData.Childrens_Info != null)
                {
                    Load = new Loading();
                    Task t = SelfStudy();
                }
                else
                    MessageBox.Show("Не задана исходная выборка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }
    }
}
