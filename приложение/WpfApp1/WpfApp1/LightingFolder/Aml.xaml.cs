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
    /// Источник глобального рассеяного света
    /// </summary>
    public partial class Aml : Window
    {
        private Model3DGroup model3DGroup;

        public Aml(Model3DGroup model3DGroupSender)
        {
            this.model3DGroup = model3DGroupSender;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            AmbientLight ambientLight = new AmbientLight();

            ColorPicker colorPicker = (ColorPicker)FindName("ColorPicker");
            ambientLight.Color = (Color)colorPicker.SelectedColor;
            
            model3DGroup.Children.Clear();
            model3DGroup.Children.Add(ambientLight);
            this.Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}