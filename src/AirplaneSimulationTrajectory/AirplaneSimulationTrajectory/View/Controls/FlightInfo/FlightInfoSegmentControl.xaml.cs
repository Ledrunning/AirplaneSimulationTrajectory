using System.Windows;
using System.Windows.Controls;

namespace AirplaneSimulationTrajectory.View.Controls.FlightInfo
{
    /// <summary>
    /// Interaction logic for FlightInfoSegmentControl.xaml
    /// </summary>
    public partial class FlightInfoSegmentControl : UserControl
    {
        public FlightInfoSegmentControl()
        {
            InitializeComponent();
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(FlightInfoSegmentControl), new FrameworkPropertyMetadata("", OnDescriptionPropertyChanged));

        private static void OnDescriptionPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is FlightInfoSegmentControl control) control.DescriptionControl.Text = (string)e.NewValue;
        }

        public string Info
        {
            get => (string)GetValue(InfoProperty);
            set => SetValue(InfoProperty, value);
        }

        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(FlightInfoSegmentControl), new FrameworkPropertyMetadata("", OnInfoPropertyChanged));

        private static void OnInfoPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is FlightInfoSegmentControl control) control.InfoControl.Text = (string)e.NewValue;
        }
    }
}