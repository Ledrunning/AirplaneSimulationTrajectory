using System.Windows;
using AirplaneSimulationTrajectory.ViewModel;

namespace AirplaneSimulationTrajectory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            // Set DataContext directly in the constructor
            DataContext = mainViewModel;

            // Other initialization logic if needed
        }
    }
}
