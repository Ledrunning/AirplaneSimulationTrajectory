using System.Globalization;
using System;
using System.Windows.Data;

namespace AirplaneSimulationTrajectory.Converters
{
    /// <summary>
    /// NOTICE! Use next line helpers:HelixViewport3DAttachedProperties.HelixViewport="{Binding CameraView3D, UpdateSourceTrigger=PropertyChanged}"
    /// in main XAML for debug
    ///     <Window.Resources>
    /// <local:DebugConverter x:Key="DebugConverter" />
    ///    </Window.Resources>
    /// </summary>
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Set a breakpoint here
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}