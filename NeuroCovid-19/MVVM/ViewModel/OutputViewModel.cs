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
        public RelayCommand RelayClassificationStudy { get; set; }
        public RelayCommand ChangeInitDataByClaster { get; set; }
        public RelayCommand GetExcelOutput { get; set; }
        public RelayCommand GetReportOutput { get; set; }
        public RelayCommand RelayAIData { get; set; }
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
                    switch ((Method)_selectedClasterisaton)
                    {
                        case Method.Kohanen:
                            App.ContextOfData.KohanenOptions.SelectedClaster = _selectedClaster;
                            Data = App.ContextOfData.KohanenOptions.ClastersInfo[_selectedClaster].Items;
                            break;
                        case Method.DBScan:
                            App.ContextOfData.DBScanOptions.SelectedClaster = _selectedClaster;
                            Data = App.ContextOfData.DBScanOptions.ClastersInfo[_selectedClaster].Items;
                            break;
                        case Method.Classification:
                            App.ContextOfData.SelectedClass = _selectedClaster;
                            Data = App.ContextOfData.ClassificationClasses[_selectedClaster].Items;
                            break;
                        default:
                            break;
                    }
                    await Task.Delay(1000);
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
                App.ContextOfData.SelectedMethod = (Enumerations.Method)value;
                _selectedClasterisaton = (int)value;
                SelectClastVM();
                OnPropertyChanged();
            }
        }

        public Visibility CoefStudyVisible { get; set; }

        public Visibility SelfStudyVisible { get; set; }
        public Visibility ClassificationVisible { get; set; }



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
            doFirstOfAll();

            if (_allNormalizeData == null || _allNormalizeData.Length == 0)
                return;

            IClasterisation clasterisation = null;
            switch ((Method)_selectedClasterisaton)
            {
                case Method.Kohanen:
                    if (new KohanenProvider().CheckForError())
                    {
                        MessageBox.Show("Не заданы обязательные парамметры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    clasterisation = new KohanenProvider(DataWithSkipping, _allNormalizeData);
                    break;
                case Method.DBScan:
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
            await FinalStageOfStudy(clasterisation.Clasters);

            SelectedClaster = 0;
            OnPropertyChanged(nameof(ClasterComboBox));
            OnPropertyChanged(nameof(SelectedClaster));
            var metricMessage = (new ClasterisationProvider()).CalculateRandIndex(clasterisation.Clasters, out _, out _);
            await Task.Delay(1500);
            MessageBox.Show(metricMessage, "Метрики", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task DoClassification()
        {
            var correctData = App.ContextOfData.Childrens_Info.Where(x => x.IsCorrectForClassification()).ToList();
            var classificationProvider = new ClassificationProvider();
            var metrics = classificationProvider.StartClassification();
            classificationProvider.DoClassificationForData(correctData);

            await FinalStageOfStudy(App.ContextOfData.ClassificationClasses);
            OnPropertyChanged(nameof(ClasterComboBox));
            OnPropertyChanged(nameof(SelectedClaster));
            OnPropertyChanged(nameof(Data));

            await Task.Delay(1500);
            MessageBox.Show(metrics, "Метрики", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task WCoefsStudy()
        {
            int countProps = doFirstOfAll();

            var kohanen = new KohanenProvider();
            if (kohanen.CheckForError())
                MessageBox.Show("Не заданы обязательные парамметры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            var w = GetWCoefs(countProps);
            kohanen.StudyWithW(DataWithSkipping, _allNormalizeData, w);
            await FinalStageOfStudy(kohanen.Clasters);

            SelectedClaster = 0;
            await Task.Delay(1500);
        }

        private async Task FinalStageOfStudy(List<ClasterInfo> clasters)
        {
            var analyzerProvider = new AIAnalyzerProvider();
            await analyzerProvider.AnalyzeClustersAsync(clasters);

            List<string> clastComboBox = new List<string>();
            for (int i = 0; i < clasters.Count; i++)
            {
                if ((Method)_selectedClasterisaton == Method.DBScan && i == 0)
                {
                    clastComboBox.Add("Шум");
                    continue;
                }
                if ((Method)_selectedClasterisaton != Method.Classification)
                {
                    clastComboBox.Add(((Method)_selectedClasterisaton != Method.DBScan ? (i + 1).ToString() : i.ToString()) + " кластер");
                    continue;
                }

                clastComboBox.Add($"{i + 1} класс");
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

            switch (App.ContextOfData.SelectedMethod)
            {
                case Method.Kohanen:
                    SelfStudyVisible = Visibility.Visible;
                    CoefStudyVisible = Visibility.Visible;
                    ClassificationVisible = Visibility.Hidden;
                    _properties = App.ContextOfData.KohanenOptions.Properties;
                    if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Any())
                    {
                        SelectedClaster = App.ContextOfData.KohanenOptions.SelectedClaster;
                        ClasterComboBox = App.ContextOfData.KohanenOptions.ClastersInfo.Select(x => x.Name).OrderBy(x => x).ToList();
                    }
                    break;
                case Method.DBScan:
                    SelfStudyVisible = Visibility.Visible;
                    CoefStudyVisible = Visibility.Hidden;
                    ClassificationVisible = Visibility.Hidden;
                    _properties = App.ContextOfData.DBScanOptions.Properties;
                    if (App.ContextOfData.DBScanOptions.ClastersInfo != null && App.ContextOfData.DBScanOptions.ClastersInfo.Any())
                    {
                        SelectedClaster = App.ContextOfData.DBScanOptions.SelectedClaster;
                        ClasterComboBox = App.ContextOfData.DBScanOptions.ClastersInfo.Select(x => x.Name).OrderBy(x => x).ToList();
                    }
                    break;
                case Method.Classification:
                    SelfStudyVisible = Visibility.Hidden;
                    CoefStudyVisible = Visibility.Hidden;
                    ClassificationVisible = Visibility.Visible;
                    if (App.ContextOfData.ClassificationClasses != null && App.ContextOfData.ClassificationClasses.Any())
                    {
                        SelectedClaster = App.ContextOfData.SelectedClass;
                        ClasterComboBox = App.ContextOfData.ClassificationClasses.Select(x => x.Name).OrderBy(x => x).ToList();
                    }
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(SelfStudyVisible));
            OnPropertyChanged(nameof(CoefStudyVisible));
            OnPropertyChanged(nameof(ClassificationVisible));
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

        private void GetClasterReport()
        {
            if (Load == null)
                Load = new Loading();

            ReportExtensions.GetClasterReport();
            Load = null;
        }

        public OutputViewModel() {
            SelectedClasterisation = (int)App.ContextOfData.SelectedMethod;
            ClasterisationComboBox = new List<string>() { "Кохонен", "DBScan", "Классификация" };

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

            RelayClassificationStudy = new RelayCommand(async x => {
                if (App.ContextOfData.Childrens_Info != null
                    && App.ContextOfData.Childrens_Info.Any())
                {
                    Load = new Loading();
                    await Task.Run(() => DoClassification());
                    Load = null;
                }
                else
                    MessageBox.Show("Не задана исходная выборка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            });

            ChangeInitDataByClaster = new RelayCommand(x =>
            {
                switch (App.ContextOfData.SelectedMethod)
                {
                    case Method.Kohanen:
                        if (App.ContextOfData.KohanenOptions.ClastersInfo != null && App.ContextOfData.KohanenOptions.ClastersInfo.Any())
                            App.ContextOfData.Childrens_Info = App.ContextOfData.KohanenOptions.ClastersInfo[App.ContextOfData.KohanenOptions.SelectedClaster].Items.ToList();
                        break;
                    case Method.DBScan:
                        if (App.ContextOfData.DBScanOptions.ClastersInfo != null && App.ContextOfData.DBScanOptions.ClastersInfo.Any())
                            App.ContextOfData.Childrens_Info = App.ContextOfData.DBScanOptions.ClastersInfo[App.ContextOfData.DBScanOptions.SelectedClaster].Items.ToList();
                        break;
                    case Method.Classification:
                        if (App.ContextOfData.DBScanOptions.ClastersInfo != null && App.ContextOfData.ClassificationClasses.Any())
                            App.ContextOfData.Childrens_Info = App.ContextOfData.ClassificationClasses[App.ContextOfData.SelectedClass].Items.ToList();
                        break;
                }
            });

            GetExcelOutput = new RelayCommand(x =>
            {
                GetOutputDataToExcel();
            });

            GetReportOutput = new RelayCommand(x =>
            {
                GetClasterReport();
            });

            RelayAIData = new RelayCommand(x =>
            {
                string aiData = string.Empty;

                try
                {

                    switch ((Method)_selectedClasterisaton)
                    {
                        case Method.Kohanen:
                            App.ContextOfData.KohanenOptions.SelectedClaster = _selectedClaster;
                            aiData = App.ContextOfData.KohanenOptions.ClastersInfo[_selectedClaster].DeepseekAnalysis;
                            break;
                        case Method.DBScan:
                            App.ContextOfData.DBScanOptions.SelectedClaster = _selectedClaster;
                            aiData = App.ContextOfData.DBScanOptions.ClastersInfo[_selectedClaster].DeepseekAnalysis;
                            break;
                        case Method.Classification:
                            App.ContextOfData.SelectedClass = _selectedClaster;
                            aiData = App.ContextOfData.ClassificationClasses[_selectedClaster].DeepseekAnalysis;
                            break;
                        default:
                            break;
                    }

                    if (string.IsNullOrEmpty(aiData))
                    {
                        throw new Exception("No data");
                    }

                    MessageBox.Show(aiData, "Характеристика от ИИ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Информации от ИИ нет, попробуйте провести кластеризацию/классификацию заново", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
