using NeuroCovid19.Core;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using Microsoft.Win32;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class InputDataViewModel: ObservableObject
    {
        public RelayCommand ChangedInputData { get; set; }
        public RelayCommand ClearData { get; set; }
        public List<DataCOVIDEars> _items { get; set; }
        private object _load;
        private bool _isntLoading;
        public bool IsntLoading
        {
            get { return _isntLoading; }
            set
            {
                _isntLoading = value;
                OnPropertyChanged();
            }
        }
        public object Load
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged();
            }
        }
        public object Items
        {
            get { return _items; }
            set
            {
                _items = (List<DataCOVIDEars>)value;
                OnPropertyChanged();
            }
        }
        public async void ReadDataFromFile()
        {
            try
            {
                IsntLoading = false;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    string filename = openFileDialog.FileName;
                    ExcelPackage excelFile = new ExcelPackage(new FileInfo(filename));
                    ExcelWorksheet worksheet = excelFile.Workbook.Worksheets[0];

                    int new_N = worksheet.Dimension.End.Row;
                    int new_M = worksheet.Dimension.End.Column;
                    string[,] Data = new string[new_N, new_M];

                    int i = 0;
                    for (int rowIndex = 4; rowIndex < new_N + 1; rowIndex++)
                    {
                        IEnumerable<string> row = worksheet.Cells[rowIndex, 1, rowIndex, new_M].Select(c => c.Value == null ? "" : c.Value.ToString());
                        string[] list = row.ToArray();
                        for (int j = 0; j < list.Length; j++)
                        {
                            Data[i, j] = list[j];
                        }
                        i++;
                    }


                    App.ContextOfData.Childrens_Info = new List<DataCOVIDEars>();

                    for (int j = 0; j < Data.GetLength(0); j++)
                    {
                        string[] row = new string[Data.GetLength(1)];
                        for (int k = 0; k < Data.GetLength(1); k++)
                            row[k] = Data[j, k];

                        App.ContextOfData.Childrens_Info.Add(new DataCOVIDEars(row));
                    }
                }
            }
            finally
            {
                Items = App.ContextOfData.Childrens_Info;
                await Task.Delay(1500);
                Parallel.Invoke(()=> IsntLoading = true,
                                ()=> Load = null
                );
            }
        }

        public InputDataViewModel()
        {
            Loading load = new Loading();
            IsntLoading = true;
            _items = App.ContextOfData.Childrens_Info;

            ChangedInputData = new RelayCommand(async x =>
            {
                Load = load;

                await Task.Run(()=>ReadDataFromFile());
                Load = null;
            });

            ClearData = new RelayCommand(x =>
            {
                App.ContextOfData.Childrens_Info = new List<DataCOVIDEars>();
                Items = App.ContextOfData.Childrens_Info;
            });
        }
    }
}
