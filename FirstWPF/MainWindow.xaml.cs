using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NeuroCovid19
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1000)
            {
                Home.FontSize = 15;
                MainLabel.FontSize = 15;
                ButtonsColumn.Width = new GridLength(220);
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Buttons); i++)
                {
                    var child = VisualTreeHelper.GetChild(Buttons, i);

                    if (child is RadioButton button)
                    {
                        button.FontSize = 12;
                    }
                }
            }
            else
            {
                ButtonsColumn.Width = new GridLength(270);
                Home.FontSize = 20;
                MainLabel.FontSize = 20;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Buttons); i++)
                {
                    var child = VisualTreeHelper.GetChild(Buttons, i);

                    if (child is RadioButton button)
                    {
                        button.FontSize = 14;
                    }
                }
            }
        }
    }
}
