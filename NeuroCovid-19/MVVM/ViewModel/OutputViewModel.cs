using NeuroCovid19.Core;
using NeuroCovid19.Enumerations;
using NeuroCovid19.Extensions;
using NeuroCovid19.Interfaces;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using NeuroCovid19.Providers;
using NeuroCovid19.Providers.ClasterisationProviders;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class OutputViewModel: ObservableObject
    {
        private object _load;
        public RelayCommand RelaySelfStudy { get; set; }
        public RelayCommand RelayKohanenWithW { get; set; }
        public RelayCommand ChangeInitDataByClaster { get; set; }
        public RelayCommand GetExcelOutput { get; set; }
        private DataCOVIDEars[]? _data { get; set; }
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
                _data = (DataCOVIDEars[])value;
                OnPropertyChanged();
            }
        }
        private int _selectedClaster { get; set; }
        private int _selectedClasterisaton { get; set; }
        private List<string> _clasterComboBox { get; set; }
        private List<string> _clasteristationComboBox { get; set; }


        public object SelectedClaster
        {
            get
            {
                return _selectedClaster;
            }
            set
            {
                _selectedClaster = Convert.ToInt32(value);
                if (Load == null)
                    Load = new Loading();

                Task.Run(async () =>
                {
                    switch ((Clasterisation)_selectedClasterisaton)
                    {
                        case Clasterisation.Kohanen:
                            App.ContextOfData.KohanenOptions.SelectedClaster = _selectedClaster;
                            Data = App.ContextOfData.KohanenOptions.ClastersInfo[_selectedClaster];
                            break;
                        case Clasterisation.DBScan:
                            App.ContextOfData.DBScanOptions.SelectedClaster = _selectedClaster;
                            Data = App.ContextOfData.DBScanOptions.ClastersInfo[_selectedClaster];
                            break;
                        default:
                            break;
                    }
                    await Task.Delay(1000).ConfigureAwait(false);
                    Load = null;
                });
                OnPropertyChanged();
            }
        }
        public object ClasterComboBox
        {
            get
            {
                return _clasterComboBox;
            }
            set
            {
                _clasterComboBox = (List<string>)value;
                OnPropertyChanged();
            }
        }
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
        public Visibility IsDBScan { get; set; }



        private List<PropertiesModel> _properties = new List<PropertiesModel>();
        private double[,] _allNormalizeData { get; set; }
        private List<DataCOVIDEars> DataWithSkipping { get; set; }
        private int doFirstOfAll()
        {
            int countProps = _properties.Where(x => x.IsUsed).Count();

            DataWithSkipping = new List<DataCOVIDEars>();
            foreach (DataCOVIDEars data in App.ContextOfData.Childrens_Info)
            {
                bool checker = true;
                double[] row = data.DataForClasterisation();
                for (int i = 0; i < row.Length; i++)
                    if (_properties[i].IsUsed && Double.IsNaN(row[i])){
                        checker = false; 
                        break;
                    }
                if (checker)
                    DataWithSkipping.Add(data);
            }

            var dataPoints = ClasterVisualisationExtension.GetGraphPoints(new List<DataCOVIDEars[]>() { DataWithSkipping.ToArray() }, App.ContextOfData.KohanenOptions.Properties);
            _allNormalizeData = new double[dataPoints.Count, 2];
            for (int i = 0; i < dataPoints.Count; i++)
            {
                _allNormalizeData[i, 0] = dataPoints[i].PCAFeatures[0];
                _allNormalizeData[i, 1] = dataPoints[i].PCAFeatures[1];
            }

            return countProps;
        }
        private async Task SelfStudy()
        {
            int countProps = doFirstOfAll();

            IClasterisation clasterisation = null;
            switch ((Clasterisation)_selectedClasterisaton)
            {
                case Clasterisation.Kohanen:
                    if (new KohanenProvider().CheckForError())
                    {
                        MessageBox.Show("Не заданы обязательные парамметры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    clasterisation = new KohanenProvider(DataWithSkipping, _allNormalizeData);
                    break;
                case Clasterisation.DBScan:
                    if (new DBScanProvider().CheckForError())
                    {
                        MessageBox.Show("Не заданы обязательные парамметры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    clasterisation = new DBScanProvider(DataWithSkipping);
                    break;
                default:
                    break;
            }
            FinalStageOfStudy(clasterisation.Clasters);

            SelectedClaster = 0;
            OnPropertyChanged(nameof(ClasterComboBox));
            OnPropertyChanged(nameof(SelectedClaster));
            await Task.Delay(1500);
        }

        private async Task WCoefsStudy()
        {
            int countProps = doFirstOfAll();

            var kohanen = new KohanenProvider();
            if (kohanen.CheckForError())
                MessageBox.Show("Не заданы обязательные парамметры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            var w = GetWCoefs(countProps);
            kohanen.StudyWithW(DataWithSkipping, _allNormalizeData, w);
            FinalStageOfStudy(kohanen.Clasters);

            OnPropertyChanged(nameof(ClasterComboBox));
            SelectedClaster = 0;
            await Task.Delay(1500);
        }

        private void FinalStageOfStudy(List<DataCOVIDEars[]> clasters)
        {
            List<string> clastComboBox = new List<string>();
            for (int i = 0; i < clasters.Count; i++)
            {
                if ((Clasterisation)_selectedClasterisaton == Clasterisation.DBScan && i == 0)
                {
                    clastComboBox.Add("Шум");
                    continue;
                }
                clastComboBox.Add(((Clasterisation)_selectedClasterisaton != Clasterisation.DBScan ? (i + 1).ToString(): i.ToString()) + " кластер");
            }
            _clasterComboBox = clastComboBox;
        }

        private double[,] GetWCoefs(int countProps)
        {
            var data = new List<DataCOVIDEars>();

            for (int clastInd = 0; clastInd < App.ContextOfData.KohanenOptions.W.Count; clastInd++)
            {
                var newClaster = new DataCOVIDEars();
                var item = App.ContextOfData.KohanenOptions.W[clastInd].DataForClasterisation();
                foreach (PropertiesModel prop in App.ContextOfData.KohanenOptions.Properties)
                {
                    if (!prop.IsUsed)
                        continue;

                    newClaster.InsertValueOfDouble(prop.Id + 3, item[prop.Id]);
                }
                data.Add(newClaster);
            }
            var dataPoints = ClasterVisualisationExtension.GetGraphPoints(new List<DataCOVIDEars[]>() { data.ToArray() }, App.ContextOfData.KohanenOptions.Properties);
            double[,] w = new double[App.ContextOfData.KohanenOptions.W.Count, 2];
            for (int i = 0; i < dataPoints.Count; i++)
            {
                w[i, 0] = dataPoints[i].PCAFeatures[0];
                w[i, 1] = dataPoints[i].PCAFeatures[1];
            }
            return w;
        }

        private void SelectClastVM()
        {
            if (_clasterComboBox != null)
            {
                _clasterComboBox.Clear();
                SelectedClaster = 0;
            }
            _data = null;
            OnPropertyChanged(nameof(ClasterComboBox));
            OnPropertyChanged(nameof(SelectedClaster));
            OnPropertyChanged(nameof(Data));

            switch (App.ContextOfData.SelectedClasterisation)
            {
                case Clasterisation.Kohanen:
                    IsDBScan = Visibility.Visible;
                    _properties = App.ContextOfData.KohanenOptions.Properties;
                    if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Any())
                    {
                        List<string> clastComboBox = new List<string>();
                        for (int i = 0; i < App.ContextOfData.KohanenOptions.ClastersInfo.Count; i++)
                            clastComboBox.Add((i + 1).ToString() + " кластер");


                        SelectedClaster = App.ContextOfData.KohanenOptions.SelectedClaster;
                        ClasterComboBox = clastComboBox;
                    }
                    break;
                case Clasterisation.DBScan:
                    IsDBScan = Visibility.Hidden;
                    _properties = App.ContextOfData.DBScanOptions.Properties;
                    if (App.ContextOfData.DBScanOptions.ClastersInfo != null && App.ContextOfData.DBScanOptions.ClastersInfo.Any())
                    {
                        List<string> clastComboBox = new List<string>();
                        for (int i = 0; i < App.ContextOfData.DBScanOptions.ClastersInfo.Count; i++)
                            clastComboBox.Add(i == 0 ? "Шум" : i.ToString() + " кластер");


                        SelectedClaster = App.ContextOfData.DBScanOptions.SelectedClaster;
                        ClasterComboBox = clastComboBox;
                    }
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(IsDBScan));
        }

        private void GetOutputDataToExcel()
        {
            if (Load == null)
                Load = new Loading();

            var columns = new ClasterisationProvider().СolomnsData();
            var fileName = _clasterComboBox[_selectedClaster];
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel Workbook|*.xls*", ValidateNames = true, FileName = fileName })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelPackage excelFile = new ExcelPackage();
                    excelFile.Workbook.Properties.Author = "NeuroNet";
                    excelFile.Workbook.Properties.Title = fileName;
                    excelFile.Workbook.Properties.Created = DateTime.Now;
                    var worksheet = excelFile.Workbook.Worksheets.Add(fileName);
                    for (int i = 0; i < columns.Count; i++)
                        worksheet.Cells[1, i + 1].Value = columns[i];
                    int k = 2;
                    foreach (DataCOVIDEars item in _data)
                    {
                        var info = item.GetAllData();
                        for (int i = 0; i < info.Count(); i++)
                            worksheet.Cells[k, i + 1].Value = info[i].Replace(',', '.');
                        k++;
                    }
                    excelFile.SaveAs(new FileInfo(saveFileDialog.FileName + ".xlsx"));
                    MessageBox.Show("Данные были успешно сохранены в файл\n" + saveFileDialog.FileName);
                }
            }
            Load = null;
        }

        public OutputViewModel() {
            SelectedClasterisation = (int)App.ContextOfData.SelectedClasterisation;
            ClasterisationComboBox = new List<string>() { "Кохонен", "DBScan" };

            SelectClastVM();

            RelaySelfStudy = new RelayCommand(async x => {
                if (App.ContextOfData.Childrens_Info != null 
                    && App.ContextOfData.Childrens_Info.Any())
                {
                    Load = new Loading();
                    await Task.Run(() => SelfStudy());
                    Load = null;
                }
                else
                    MessageBox.Show("Не задана исходная выборка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            });


            RelayKohanenWithW = new RelayCommand(async x =>
            {
                if (App.ContextOfData.KohanenOptions.W != null 
                    && App.ContextOfData.Childrens_Info != null
                    && App.ContextOfData.Childrens_Info.Any())
                {
                    Load = new Loading();
                    await Task.Run(() => WCoefsStudy());
                    Load = null;
                }
                else
                    MessageBox.Show("Не заданы коэффициенты для обучения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            });

            ChangeInitDataByClaster = new RelayCommand(x =>
            {
                switch (App.ContextOfData.SelectedClasterisation)
                {
                    case Clasterisation.Kohanen:
                        if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Any())
                            App.ContextOfData.Childrens_Info = App.ContextOfData.KohanenOptions.ClastersInfo[App.ContextOfData.KohanenOptions.SelectedClaster].ToList();
                        break;
                    case Clasterisation.DBScan:
                        if (App.ContextOfData.DBScanOptions.ClastersInfo != null && App.ContextOfData.DBScanOptions.ClastersInfo.Any())
                            App.ContextOfData.Childrens_Info = App.ContextOfData.DBScanOptions.ClastersInfo[App.ContextOfData.DBScanOptions.SelectedClaster].ToList();
                        break;
                }
            });

            GetExcelOutput = new RelayCommand(x =>
            {
                GetOutputDataToExcel();
            });
        }
    }
}
