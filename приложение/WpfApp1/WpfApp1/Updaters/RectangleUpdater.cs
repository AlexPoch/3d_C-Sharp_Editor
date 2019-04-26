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
    class RectangleUpdater
    {
        public RectangleUpdater(Rectangle3D rectangle3D, Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            Rectangle rectangle = new Rectangle(model3DGroup, 4, generalFiguresList);
            rectangle.Show();

            rectangle.ColorPicker.SelectedColor = rectangle3D.color;

            rectangle.X1.Text = rectangle3D.pointLU.X.ToString();
            rectangle.Y1.Text = rectangle3D.pointLU.Y.ToString();
            rectangle.Z1.Text = rectangle3D.pointLU.Z.ToString();
            
            rectangle.X2.Text = rectangle3D.pointLD.X.ToString();
            rectangle.Y2.Text = rectangle3D.pointLD.Y.ToString();
            rectangle.Z2.Text = rectangle3D.pointLD.Z.ToString();

            rectangle.X3.Text = rectangle3D.pointRD.X.ToString();
            rectangle.Y3.Text = rectangle3D.pointRD.Y.ToString();
            rectangle.Z3.Text = rectangle3D.pointRD.Z.ToString();
            
            rectangle.elementName.Text = rectangle3D.name.Substring(0, rectangle3D.name.Length - 6);
        }
    }
}
