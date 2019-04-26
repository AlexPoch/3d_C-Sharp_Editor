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
    class CustomUpdater
    {
        public CustomUpdater(Custom3D custom3D, Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            Custom3DFigure custom3DFigure = new Custom3DFigure(model3DGroup, generalFiguresList);
            custom3DFigure.Show();

            custom3DFigure.point3Ds = custom3D.point3Ds;
            custom3DFigure.PointsList();

            custom3DFigure.triangles3Ds = custom3D.triangles3Ds;
            custom3DFigure.TrianglesList();

            custom3DFigure.ColorPicker.SelectedColor = custom3D.color;
            custom3DFigure.elementName.Text = custom3D.name.Substring(0, custom3D.name.Length - 12);
        }
    }
}
