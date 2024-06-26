﻿using NeuroCovid19.Extensions;
using NeuroCovid19.Functions;
using NeuroCovid19.MVVM.ViewModel;
using NeuroCovid19.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeuroCovid19.MVVM.View
{
    public partial class InputDataView : UserControl
    {
        int index = 0;
        List<string> colomnsData = new ClasterisationProvider().СolomnsData();
        public InputDataView()
        {
            InitializeComponent();
        }
        private void grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = colomnsData[index % colomnsData.Count()];
            index++;
        }
    }
}
