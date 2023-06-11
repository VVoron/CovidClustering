using NeuroCovid19.Core;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.MVVM.View;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace NeuroCovid19.MVVM.ViewModel
{
    internal class InputDataViewModel: ObservableObject
    {
        public RelayCommand ChangedInputData { get; set; }
        public RelayCommand ClearData { get; set; }
        public List<KohanenDataCOVIDEars> _items { get; set; }
        private object _load;
        private bool _isLoading;
        public object Load
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public object Items
        {
            get { return _items; }
            set
            {
                _items = (List<KohanenDataCOVIDEars>)value;
                OnPropertyChanged();
            }
        }
        public async void ReadDataFromFile()
        {
            try
            {
                IsLoading = true;
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
                        IEnumerable<string> row = worksheet.Cells[rowIndex, 1, rowIndex, new_M].Select(c => c.Value == null ? "0".ToString() : c.Value.ToString());
                        string[] list = row.ToArray();
                        for (int j = 0; j < list.Length; j++)
                        {
                            Data[i, j] = list[j];
                        }
                        i++;
                    }


                    App.contextOfData.Childrens_Info = new List<KohanenDataCOVIDEars>();

                    for (int j = 0; j < Data.GetLength(0); j++)
                    {
                        string[] row = new string[Data.GetLength(1)];
                        for (int k = 0; k < Data.GetLength(1); k++)
                            row[k] = Data[j, k];

                        App.contextOfData.Childrens_Info.Add(new KohanenDataCOVIDEars(row));
                    }
                }
            }
            finally
            {
                Items = App.contextOfData.Childrens_Info;
                await Task.Delay(1000);
                Load = null;
            }
        }

        public InputDataViewModel()
        {
            Loading load = new Loading();
            _items = App.contextOfData.Childrens_Info;

            ChangedInputData = new RelayCommand(x =>
            {
                Load = load;
                Task.Run(()=>ReadDataFromFile());
            });

            ClearData = new RelayCommand(x =>
            {
                App.contextOfData.Childrens_Info = new List<KohanenDataCOVIDEars>();
                Items = App.contextOfData.Childrens_Info;
            });
        }
    }
}
