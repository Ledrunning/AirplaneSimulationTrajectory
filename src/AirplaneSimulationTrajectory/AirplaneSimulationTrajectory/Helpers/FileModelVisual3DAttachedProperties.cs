using System.Windows;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.Helpers
{
    public class FileModelVisual3DAttachedProperties
    {
        public static readonly DependencyProperty FileModelVisual3DProperty =
            DependencyProperty.RegisterAttached("FileModelVisual3D", typeof(FileModelVisual3D),
                typeof(FileModelVisual3DAttachedProperties));

        public static FileModelVisual3D GetFileModelVisual3D(DependencyObject obj)
        {
            return (FileModelVisual3D)obj.GetValue(FileModelVisual3DProperty);
        }

        public static void SetFileModelVisual3D(DependencyObject obj, FileModelVisual3D value)
        {
            obj.SetValue(FileModelVisual3DProperty, value);
        }
    }
}