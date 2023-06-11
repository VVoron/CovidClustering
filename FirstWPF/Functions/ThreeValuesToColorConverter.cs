using NeuroCovid19.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NeuroCovid19.Functions
{
    public class ThreeValuesToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
                return Brushes.Transparent;
            var currentColumnValue = (double)values[0];
            var age = (double)values[1];
            var gest = (double)values[2];
            int columnIndex = (int)parameter;


            if (values.Length > 3)
            {
                switch (columnIndex)
                {
                    case 35:
                    case 38:
                        {
                            if (currentColumnValue > 0)
                                return Brushes.Green;
                            else
                                return Brushes.Red;
                        }
                    case 36:
                    case 39:
                        {
                            if (currentColumnValue > 0 && (double)values[3] == 0)
                                return Brushes.Green;
                            else if ((double)values[3] == 0)
                                return Brushes.Red;
                            else
                                return Brushes.Transparent;
                        }
                    case 37:
                    case 40:
                        {
                            if (currentColumnValue > 0 && (double)values[3] == 0 && (double)values[4] == 0)
                                return Brushes.Green;
                            else if ((double)values[3] == 0 && (double)values[4] == 0)
                                return Brushes.Red;
                            else
                                return Brushes.Transparent;
                        }
                }
            }

            for (int i = 0; i < KohanenFunc.colomnsToCheck.Length; i++)
            {
                foreach (int col in KohanenFunc.colomnsToCheck[i])
                {
                    if (col == columnIndex)
                    {
                        double[] range = KohanenFunc.SelectedPropNormal(col, age, gest);
                        if (currentColumnValue < range[0] && i == 0)
                            if (Math.Abs(currentColumnValue - range[0]) <= 1.5)
                                return new SolidColorBrush(Color.FromArgb(255, 255, 250, 150));
                            else
                                return new SolidColorBrush(Color.FromArgb(255, 254, 222, 95));
                        else if (currentColumnValue > range[1] && i == 0)
                        {
                            if (Math.Abs(currentColumnValue - range[1]) <= 1.5)
                                return new SolidColorBrush(Color.FromArgb(255, 255, 138, 138));
                            else
                                return new SolidColorBrush(Color.FromArgb(255, 255, 55, 55));
                        }
                        else
                            if (currentColumnValue >= range[0] && currentColumnValue <= range[1])
                                return new SolidColorBrush(Color.FromArgb(255, 90, 255, 87));
                            else
                                return new SolidColorBrush(Color.FromArgb(255, 255, 55, 55));
                    }
                }
            }
            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}