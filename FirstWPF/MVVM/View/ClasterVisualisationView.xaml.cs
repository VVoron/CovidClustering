﻿using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Input;

namespace NeuroCovid19.MVVM.View
{
    public partial class ClasterVisualisation : UserControl
    {
        int index = 0;
        List<string> colomnsData = KohanenFunc.СolomnsData();
        public ClasterVisualisation()
        {
            InitializeComponent();
        }

        private void AvarageGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (index == 0)
                e.Column.Header = " ";
            else
                e.Column.Header = colomnsData[(index + 2) % colomnsData.Count()];
            index++;
        }

        private void UserControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1000)
            {
                AvarageGrid.FontSize = 10;
                ListPorps.FontSize = 10;
                ListVars.FontSize = 10;
            }
            else
            {
                AvarageGrid.FontSize = 14;
                ListPorps.FontSize = 16;
                ListVars.FontSize = 16;
            }
        }
    }
}
