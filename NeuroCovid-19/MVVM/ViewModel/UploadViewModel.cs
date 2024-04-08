using NeuroCovid19.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using NeuroCovid19.Data;
using NeuroCovid19.MVVM.View;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace NeuroCovid19.MVVM.ViewModel
{
    public class UploadViewModel : ObservableObject
    {
        public RelayCommand OpenSaveDialog { get; set; }
        public RelayCommand OpenLoadDialog { get; set; }
        public string DirToSave { get; set; }
        public string DirToLoad { get; set; }


        private object _load;
        public object Load
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged();
            }
        }

        public void SaveData()
        {
            try
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    DirToSave = dialog.FileName;
                    OnPropertyChanged(nameof(DirToSave));
                    string jsonData = JsonConvert.SerializeObject(App.ContextOfData);
                    string filePath = System.IO.Path.Combine(DirToSave, $"data_{DateTime.Now.ToString("d")}.json");
                    System.IO.File.WriteAllText(filePath, jsonData);
                    MessageBox.Show("Данные успешно сохранены.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Выберите JSON файл";
                dialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                if (dialog.ShowDialog() == true)
                {
                    string filename = dialog.FileName;
                    DirToLoad = filename;
                    OnPropertyChanged(nameof(DirToLoad));
                    var jsonData = System.IO.File.ReadAllText(filename);
                    App.ContextOfData = JsonConvert.DeserializeObject<ContextOfData>(jsonData);
                    MessageBox.Show("Данные загружены успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public UploadViewModel()
        {
            Loading load = new Loading();

            OpenSaveDialog = new RelayCommand(async x =>
            {
                Load = load;
                SaveData();
                Load = null;
            });

            OpenLoadDialog = new RelayCommand(async x =>
            {
                Load = load;
                await Task.Run(() => LoadData());
                Load = null;
            });
        }
    }
}
