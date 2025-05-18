using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.Providers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
        public readonly SolidColorBrush MoreBigger = new SolidColorBrush(Color.FromArgb(255, 255, 55, 55));
        public readonly SolidColorBrush MoreSmaller = new SolidColorBrush(Color.FromArgb(255, 254, 222, 95));

        public static readonly System.Drawing.Color MoreBiggerDrawingColor = System.Drawing.Color.FromArgb(255, 255, 55, 55);
        public static readonly System.Drawing.Color MoreSmallerDrawingColor = System.Drawing.Color.FromArgb(255, 254, 222, 95);

        public readonly SolidColorBrush SuchBigger = new SolidColorBrush(Color.FromArgb(255, 255, 138, 138));
        public readonly SolidColorBrush SuchSmaller = new SolidColorBrush(Color.FromArgb(255, 255, 250, 150));

        public static readonly System.Drawing.Color SuchBiggerDrawingColor = System.Drawing.Color.FromArgb(255, 255, 138, 138);
        public static readonly System.Drawing.Color SuchSmallerDrawingColor = System.Drawing.Color.FromArgb(255, 255, 250, 150);

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
                            else if (currentColumnValue == 0)
                                return Brushes.Red;
                            return Brushes.Transparent;
                        }
                    case 36:
                    case 39:
                        {
                            if (currentColumnValue > 0 && ((double)values[3] == 0 || Double.IsNaN((double)values[3])))
                                return Brushes.Green;
                            else if (((double)values[3] == 0 || Double.IsNaN((double)values[3])) && currentColumnValue == 0)
                                return Brushes.Red;
                            else
                                return Brushes.Transparent;
                        }
                    case 37:
                    case 40:
                        {
                            if (currentColumnValue > 0 && ((double)values[3] == 0 || Double.IsNaN((double)values[3])) && ((double)values[4] == 0 || Double.IsNaN((double)values[4])))
                                return Brushes.Green;
                            else if (((double)values[3] == 0 || Double.IsNaN((double)values[3])) && ((double)values[4] == 0 || Double.IsNaN((double)values[4])) && currentColumnValue == 0)
                                return Brushes.Red;
                            else
                                return Brushes.Transparent;
                        }
                }
            }

            var clExt = new ClasterisationProvider();

            for (int i = 0; i < clExt.colomnsToCheck.Length - 2; i++)
            {
                foreach (int col in clExt.colomnsToCheck[i])
                {
                    if (col == columnIndex)
                    {
                        if (double.IsNaN(currentColumnValue))
                            return Brushes.Transparent;

                        double[] range = clExt.SelectedPropNormal(col, age, gest);
                        if (currentColumnValue < range[0] && i == 0)
                            if (Math.Abs(currentColumnValue - range[0]) <= 1.5)
                                return SuchSmaller;
                            else
                                return MoreSmaller;
                        else if (currentColumnValue > range[1] && i == 0)
                        {
                            if (Math.Abs(currentColumnValue - range[1]) <= 1.5)
                                return SuchBigger;
                            else
                                return MoreBigger;
                        }
                        else
                            if (currentColumnValue >= range[0] && currentColumnValue <= range[1])
                                return new SolidColorBrush(Color.FromArgb(255, 90, 255, 87));
                            else
                                return MoreBigger;
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