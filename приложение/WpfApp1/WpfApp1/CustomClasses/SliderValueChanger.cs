using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace WpfApp1.CustomClasses
{
    /// <summary>
    /// Класс, предназначенный для сдвига фигур вдоль осей
    /// </summary>
    public partial class SliderValueChanger : Window
    {
        public SliderValueChanger(object sender, PerspectiveCamera perspectiveCameraEditor)
        {
            Slider slider = (Slider)sender;

            string sliderName = slider.Name;
           
            Regex regexX = new Regex(@"X$");
            Regex regexY = new Regex(@"Y$");
            Regex regexZ = new Regex(@"Z$");

            Match matchX = regexX.Match(sliderName);
            Match matchY = regexY.Match(sliderName);
            Match matchZ = regexZ.Match(sliderName);
            
            Point3D point3D = perspectiveCameraEditor.Position;

            if (matchX.Value != "")
            {
                point3D.X = slider.Value;
            }
            else if (matchY.Value != "")
            {
                point3D.Y = slider.Value;
            }
            else
            {
                point3D.Z = slider.Value;
            }

            perspectiveCameraEditor.Position = point3D;
        }
    }
}
