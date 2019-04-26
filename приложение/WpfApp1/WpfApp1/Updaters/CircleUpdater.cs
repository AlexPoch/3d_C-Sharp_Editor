using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using WpfApp1.CustomClasses;

namespace WpfApp1.Updaters
{
    class CircleUpdater
    {
        public CircleUpdater(Circle3D circle3D, Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            Circle circle = new Circle(model3DGroup, generalFiguresList);
            circle.Show();

            circle.ColorPicker.SelectedColor = circle3D.color;
            circle.Radius.Text = circle3D.radius.ToString();

            circle.CX.Text = circle3D.center.X.ToString();
            circle.CY.Text = circle3D.center.Y.ToString();
            circle.CZ.Text = circle3D.center.Z.ToString();

            circle.NX.Text = circle3D.normal.X.ToString();
            circle.NY.Text = circle3D.normal.Y.ToString();
            circle.NZ.Text = circle3D.normal.Z.ToString();

            circle.elementName.Text = circle3D.name.Substring(0, circle3D.name.Length - 12);
            circle.Resolution.Text = circle3D.resolution.ToString();
        }
    }
}
