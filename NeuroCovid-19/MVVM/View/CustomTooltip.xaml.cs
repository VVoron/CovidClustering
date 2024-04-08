using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeuroCovid19.MVVM.View
{
    public partial class CustomTooltip : IChartTooltip
    {
        private TooltipData _data;

        public CustomTooltip()
        {
            InitializeComponent();
            this.Visibility = Visibility.Visible;
            DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public TooltipData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Ellipse_MouseDown(object sender, MouseEventArgs e)
        {
            Data = null;
        }
    }
}
