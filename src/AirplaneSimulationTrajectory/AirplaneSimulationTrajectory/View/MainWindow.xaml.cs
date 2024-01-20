using System.Windows;
using System.Windows.Media;
using AirplaneSimulationTrajectory.ViewModel;

namespace AirplaneSimulationTrajectory.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Initialize(MainViewModel viewModel)
        {
            DataContext = viewModel;
        }
    }
}