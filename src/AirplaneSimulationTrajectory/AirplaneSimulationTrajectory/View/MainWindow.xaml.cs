using System.Windows;
using AirplaneSimulationTrajectory.ViewModel;

namespace AirplaneSimulationTrajectory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
