using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace NeuroCovid19.MVVM.View
{
    public partial class KohanenProperties : UserControl
    {
        public KohanenProperties()
        {
            InitializeComponent();
            foreach (PropertiesModel p in App.ContextOfData.KohanenOptions.Properties)
            {
                if (p.IsUsed)
                {
                    if (App.ContextOfData.KohanenOptions.W != null)
                        LoadData();
                    break;
                }
            }
        }

        public void LoadData()
        {
            if (App.ContextOfData.KohanenOptions.W.Count > 0)
            {
                List<int> indices = new List<int>();
                foreach (PropertiesModel prop in App.ContextOfData.KohanenOptions.Properties)
                    if (prop.IsUsed)
                        indices.Add(prop.Id);

                foreach (PropertiesModel prop in propGrid.ItemsSource)
                {
                    if (indices.Contains(prop.Id))
                    {
                        DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: typeof(DataCOVIDEars).GetProperties()[prop.Id + 3].Name) };
                        DGTable.Columns.Add(col);
                    }
                }
            }
            else
            {
                foreach (PropertiesModel prop in propGrid.ItemsSource)
                {
                    if (!App.ContextOfData.PropIdsForSkip.Contains(prop.Id))
                    {
                        DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: typeof(DataCOVIDEars).GetProperties()[prop.Id + 3].Name) };
                        DGTable.Columns.Add(col);
                    }
                }
            }
        }
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DGTable.Columns.Clear();
            foreach (PropertiesModel prop in propGrid.ItemsSource)
            {
                if ((prop != e.Row.Item as PropertiesModel && prop.IsUsed) || (prop == e.Row.Item as PropertiesModel && !prop.IsUsed))
                {
                    DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: typeof(DataCOVIDEars).GetProperties()[prop.Id + 3].Name) };
                    DGTable.Columns.Add(col);
                }
            }
        }

        private void Select_All_Click(object sender, RoutedEventArgs e)
        {
            DGTable.Columns.Clear();
            foreach (PropertiesModel prop in propGrid.ItemsSource)
            {
                DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: typeof(DataCOVIDEars).GetProperties()[prop.Id + 3].Name) };
                DGTable.Columns.Add(col);
            }
        }

        private void UnSelect_All_Click(object sender, RoutedEventArgs e)
        {
            DGTable.Columns.Clear();
        }

        private void Select_Standatrt(object sender, RoutedEventArgs e)
        {
            DGTable.Columns.Clear();
            foreach (PropertiesModel prop in propGrid.ItemsSource)
            {
                if (!App.ContextOfData.PropIdsForSkip.Contains(prop.Id))
                {
                    DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: typeof(DataCOVIDEars).GetProperties()[prop.Id + 3].Name) };
                    DGTable.Columns.Add(col);
                }
            }
        }
    }
}
