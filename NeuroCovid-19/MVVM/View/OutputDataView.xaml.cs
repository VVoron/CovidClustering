using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using NeuroCovid19.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using NeuroCovid19.Extensions;
using NeuroCovid19.Providers;

namespace NeuroCovid19.MVVM.View
{
    public partial class OutputDataView : UserControl
    {

        int index = 0;
        List<string> colomnsData = new ClasterisationProvider().СolomnsData();
        public OutputDataView()
        {
            InitializeComponent();
        }

        private void grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = colomnsData[index % colomnsData.Count()];

            var clExt = new ClasterisationProvider();

            if (index < 41)
            {
                foreach (var list in clExt.colomnsToCheck)
                {
                    if (list.Contains(index))
                    {
                        var column = new DataGridTextColumn();
                        column.Header = e.Column.Header;
                        column.Binding = new Binding(e.PropertyName);

                        // Настройте свойство Background для ячеек столбца
                        column.CellStyle = new Style(typeof(DataGridCell));
                        if (list.Count == 3)
                        {
                            
                            column.CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new MultiBinding()
                            {
                                Bindings =
                            {
                                new Binding(e.PropertyName),
                                new Binding("_time_gestagration"),
                                new Binding("_time_observation"),
                                (index <= 41) ? new Binding("_kSVP_r_20") : new Binding("_kSVP_l_20"),
                                (index <= 41) ? new Binding("_kSVP_r_40") : new Binding("_kSVP_l_40"),
                                (index <= 41) ? new Binding("_kSVP_r_60") : new Binding("_kSVP_l_60")
                            },
                                Converter = new ThreeValuesToColorConverter(),
                                ConverterParameter = index
                            }));
                            e.Column = column;
                        }
                        else
                        {
                            column.CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new MultiBinding()
                            {
                                Bindings =
                            {
                                new Binding(e.PropertyName),
                                new Binding("_time_gestagration"),
                                new Binding("_time_observation")
                            },
                                Converter = new ThreeValuesToColorConverter(),
                                ConverterParameter = index
                            }));


                            e.Column = column;
                        }
                        break;
                    }
                }
            }
            index++;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = 0;
        }

        private void OpenInfoTooltip(object sender, System.Windows.Input.MouseEventArgs e)
        {
            InfoTooltip.IsOpen = true;
        }

        private void CloseInfoTooltip(object sender, System.Windows.Input.MouseEventArgs e)
        {
            InfoTooltip.IsOpen = false;
        }
    }
}
