using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace WpfApp1
{
    /// <summary>
    /// Свет, исходящий от первого основания и идущий ко второму основанию, под указанным внутренним углом
    /// </summary>
    public partial class Spl : Window
    {
        private Model3DGroup model3DGroup;

        public Spl(Model3DGroup model3DGroup)
        {
            this.model3DGroup = model3DGroup;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorPicker colorPicker = (ColorPicker)FindName("ColorPicker");
                Color color = (Color)colorPicker.SelectedColor;

                TextBox x = (TextBox)FindName("drlX");
                TextBox y = (TextBox)FindName("drlY");
                TextBox z = (TextBox)FindName("drlZ");
                TextBox xDir = (TextBox)FindName("drlXdir");
                TextBox yDir = (TextBox)FindName("drlYdir");
                TextBox zDir = (TextBox)FindName("drlZdir");
                TextBox outerAngle = (TextBox)FindName("outerAngle");
                TextBox innerAngle = (TextBox)FindName("innerAngle");

                SpotLight pointLight = new SpotLight(color, 
                    new Point3D(double.Parse(x.Text), double.Parse(y.Text), double.Parse(z.Text)),
                    new Vector3D(double.Parse(xDir.Text), double.Parse(yDir.Text), double.Parse(zDir.Text)),
                    double.Parse(outerAngle.Text), double.Parse(innerAngle.Text));

                model3DGroup.Children.Clear();
                model3DGroup.Children.Add(pointLight);
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
