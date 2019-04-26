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
    /// Направленные параллельные лучи света от источника к центру координат
    /// </summary>
    public partial class Drl : Window
    {
        private Model3DGroup model3DGroup;

        public Drl(Model3DGroup model3DGroup)
        {
            this.model3DGroup = model3DGroup;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            DirectionalLight directionalLight = new DirectionalLight();

            ColorPicker colorPicker = (ColorPicker)FindName("ColorPicker");
            directionalLight.Color = (Color)colorPicker.SelectedColor;

            TextBox x = (TextBox)FindName("drlX");
            TextBox y = (TextBox)FindName("drlY");
            TextBox z = (TextBox)FindName("drlZ");
            directionalLight.Direction = new Vector3D(double.Parse(x.Text),double.Parse(y.Text),double.Parse(z.Text));
            
            model3DGroup.Children.Clear();
            model3DGroup.Children.Add(directionalLight);
            this.Close();
        }

        private void ClearValues(object sender, RoutedEventArgs e)
        {
            TextBox x = (TextBox)FindName("drlX");
            TextBox y = (TextBox)FindName("drlY");
            TextBox z = (TextBox)FindName("drlZ");
            x.Text = "0";
            y.Text = "0";
            z.Text = "-20";
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
