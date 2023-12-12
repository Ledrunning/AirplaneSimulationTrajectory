using System.Windows;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.Helpers
{
    public class HelixViewport3DAttachedProperties
    {
        public static readonly DependencyProperty HelixViewportProperty =
            DependencyProperty.RegisterAttached("HelixViewport", typeof(HelixViewport3D),
                typeof(HelixViewport3DAttachedProperties));

        public static HelixViewport3D GetHelixViewport(DependencyObject obj)
        {
            return (HelixViewport3D)obj.GetValue(HelixViewportProperty);
        }

        public static void SetHelixViewport(DependencyObject obj, HelixViewport3D value)
        {
            obj.SetValue(HelixViewportProperty, value);
        }
    }
}