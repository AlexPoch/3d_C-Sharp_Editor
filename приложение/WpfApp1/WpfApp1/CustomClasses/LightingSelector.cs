using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfApp1.CustomClasses
{
    /// <summary>
    /// Класс, предоставляющий возможность выбрать источник освещения
    /// </summary>
    class LightingSelector : MainWindow
    {
        public LightingSelector(ComboBoxItem comboBoxItem, Model3DGroup model3DGroup)
        {
            Model3DGroup model3DGrouppa = (Model3DGroup)this.FindName("LightGroup");
            
            string typeOfLight = comboBoxItem.Name;
            switch (typeOfLight)
            {
                case "drl":
                    Drl drl = new Drl(model3DGroup);
                    drl.Show();
                    break;

                case "aml":
                    Aml aml = new Aml(model3DGroup);
                    aml.Show();
                    break;

                case "ptl":
                    Ptl ptl = new Ptl(model3DGroup);
                    ptl.Show();
                    break;

                case "spl":
                    Spl spl = new Spl(model3DGroup);
                    spl.Show();
                    break;
            }
        }
    }
}
