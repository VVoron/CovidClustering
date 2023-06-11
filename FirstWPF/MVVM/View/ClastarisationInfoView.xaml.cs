using NeuroCovid19.MVVM.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
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

namespace NeuroCovid19.MVVM.View
{
    public partial class ClastarisationInfoView : UserControl
    {
        string[] propsNames = new string[] {"_mother",
                                            "_time_pregnancy_ill",
                                            "_son",
                                            "_time_ill",
                                            "_time_gestagration",
                                            "_time_observation",
                                            "_otoacoustic_r_1", "_otoacoustic_r_2", "_otoacoustic_r_4", "_otoacoustic_r_6",
                                            "_otoacoustic_l_1", "_otoacoustic_l_2", "_otoacoustic_l_4", "_otoacoustic_l_6",
                                            "_otoacoustic_r_05_15", "_otoacoustic_r_15_25", "_otoacoustic_r_25_35", "_otoacoustic_r_35_45", "_otoacoustic_r_45_55",
                                            "_otoacoustic_l_05_15", "_otoacoustic_l_15_25", "_otoacoustic_l_25_35", "_otoacoustic_l_35_45", "_otoacoustic_l_45_55",
                                            "_aSSR_r_05", "_aSSR_r_1", "_aSSR_r_2", "_aSSR_r_4",
                                            "_aSSR_l_05", "_aSSR_l_1", "_aSSR_l_2", "_aSSR_l_4",
                                            "_kSVP_r_20", "_kSVP_r_40", "_kSVP_r_60",
                                            "_kSVP_l_20", "_kSVP_l_40", "_kSVP_l_60"};

        private int[] indexOfStandartToSkip = new int[] { 6, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 28, 33, 34, 36, 37 };
        public ClastarisationInfoView()
        {
            InitializeComponent();
            foreach (PropertiesModel p in App.contextOfData.Properties)
            {
                if (p.IsUsed)
                {
                    if (App.contextOfData.w != null)
                        LoadData();
                    break;
                }
            }
        }

        public void LoadData()
        {
            if (App.contextOfData.w.Count > 0)
            {
                List<int> indices = new List<int>();
                foreach (PropertiesModel prop in App.contextOfData.Properties)
                    if (prop.IsUsed)
                        indices.Add(prop.Id);

                foreach (PropertiesModel prop in propGrid.ItemsSource)
                {
                    if (indices.Contains(prop.Id))
                    {
                        DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: propsNames[prop.Id]) };
                        DGTable.Columns.Add(col);
                    }
                }
            }
            else {
                foreach (PropertiesModel prop in propGrid.ItemsSource)
                {
                    if (!indexOfStandartToSkip.Contains(prop.Id))
                    {
                        DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: propsNames[prop.Id]) };
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
                    DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: propsNames[prop.Id]) };
                    DGTable.Columns.Add(col);
                }
            }
        }

        private void Select_All_Click(object sender, RoutedEventArgs e)
        {
            DGTable.Columns.Clear();
            foreach (PropertiesModel prop in propGrid.ItemsSource)
            {
                DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: propsNames[prop.Id]) };
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
                if (!indexOfStandartToSkip.Contains(prop.Id))
                {
                    DataGridTextColumn col = new DataGridTextColumn { Header = prop.Name, Binding = new Binding(path: propsNames[prop.Id]) };
                    DGTable.Columns.Add(col);
                }
            }
        }
    }
}
