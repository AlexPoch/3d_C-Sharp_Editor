using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using WpfApp1.Updaters;

namespace WpfApp1.CustomClasses
{
    class FigureDeleter
    {
        Model3DGroup model3DGroup;
        List<object> generalFiguresList;
        ComboBox comboBox;
        int iterator;
        string figureName;

        public FigureDeleter(ComboBox comboBox, Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            this.model3DGroup = model3DGroup;
            this.comboBox = comboBox;
            this.iterator = comboBox.SelectedIndex;
            this.generalFiguresList = generalFiguresList;
            UpdateInit();
        }

        private void UpdateInit()
        { 
            Circle3D circle3d = generalFiguresList[iterator] as Circle3D;
            if (circle3d != null)
            {
                figureName = "circle";
            }

            Rectangle3D rectangle3d = generalFiguresList[iterator] as Rectangle3D;
            if (rectangle3d != null)
            {
                if (rectangle3d.figureType == 3)
                {
                    figureName = "triang";
                }
                else
                {
                    figureName = "rectan";
                }
            }

            Custom3D custom3d = generalFiguresList[iterator] as Custom3D;
            if (custom3d != null)
            {
                figureName = "custom";
            }

            string modelName = string.Concat(comboBox.SelectedItem.ToString(), figureName);

            int it = 0;
            Model3DCollection model3Ds = model3DGroup.Children;
            foreach(Model3D t in model3Ds)
            {
                if(t.GetValue(FrameworkElement.NameProperty).ToString() == modelName)
                {
                    model3Ds.RemoveAt(it);
                    break;
                }
                it++;
            }

            generalFiguresList.RemoveAt(iterator);

            MainWindow.FiguresListChanges();
        }
    }
}
