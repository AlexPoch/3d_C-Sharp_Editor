using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WpfApp1.camerasFolder
{
    /// <summary>
    /// Класс, предназначеный для смены камеры на перспективную проекцию
    /// </summary>
    public partial class PerspectiveCameraCreator : Window
    {
        TextBox cx;
        TextBox cy;
        TextBox cz;
        TextBox nx;
        TextBox ny;
        TextBox nz;

        Viewport3D viewport3D = new Viewport3D();

        public PerspectiveCameraCreator(Viewport3D viewport3D)
        {
            this.viewport3D = viewport3D;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.cx = (TextBox)FindName("CX");
            this.cy = (TextBox)FindName("CY");
            this.cz = (TextBox)FindName("CZ");
            this.nx = (TextBox)FindName("NX");
            this.ny = (TextBox)FindName("NY");
            this.nz = (TextBox)FindName("NZ");

            PerspectiveCamera perspectiveCamera = new PerspectiveCamera();
            perspectiveCamera.Position = new Point3D(double.Parse(cx.Text.ToString(), CultureInfo.InvariantCulture),
                double.Parse(cy.Text.ToString(), CultureInfo.InvariantCulture),
                double.Parse(cz.Text.ToString(), CultureInfo.InvariantCulture));
            perspectiveCamera.LookDirection = new Vector3D(double.Parse(nx.Text.ToString(), CultureInfo.InvariantCulture),
                double.Parse(ny.Text.ToString(), CultureInfo.InvariantCulture),
                double.Parse(nz.Text.ToString(), CultureInfo.InvariantCulture));
            perspectiveCamera.SetCurrentValue(FrameworkElement.NameProperty, "perspectiveCameraEditor");

            viewport3D.Camera = perspectiveCamera;

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
