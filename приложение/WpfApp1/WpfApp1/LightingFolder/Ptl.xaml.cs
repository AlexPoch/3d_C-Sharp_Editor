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
    /// Создает точку света, у которой сила света угасает к концу радиуса освещения
    /// </summary>
    public partial class Ptl : Window
    {
        private Model3DGroup model3DGroup;

        public Ptl(Model3DGroup model3DGroup)
        {
            this.model3DGroup = model3DGroup;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            try {
                TextBox textBox = (TextBox)this.FindName("drlR");

                ColorPicker colorPicker = (ColorPicker)FindName("ColorPicker");
                Color color = (Color)colorPicker.SelectedColor;

                TextBox x = (TextBox)FindName("drlX");
                TextBox y = (TextBox)FindName("drlY");
                TextBox z = (TextBox)FindName("drlZ");
                TextBox range = (TextBox)this.FindName("drlR");

                PointLight pointLight = new PointLight(color, new Point3D(double.Parse(x.Text), double.Parse(y.Text), double.Parse(z.Text)));

                if (double.Parse(range.Text) < 0)
                {
                    range.Text = (double.Parse(range.Text) * (-1)).ToString();
                }

                pointLight.Range = double.Parse(range.Text);

                model3DGroup.Children.Clear();
                model3DGroup.Children.Add(pointLight);
                this.Close();
            }
            catch(Exception ex)
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
