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
    class FigureUpdater
    {
        Model3DGroup model3DGroup;
        List<object> generalFiguresList;
        int iterator;
        string figureName;
        
        public FigureUpdater(ComboBox comboBox, Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            this.model3DGroup = model3DGroup;
            this.iterator = comboBox.SelectedIndex;
            this.generalFiguresList = generalFiguresList;
            UpdateInit();
        }

        private void UpdateInit()
        {
            Circle3D circle3d = generalFiguresList[iterator] as Circle3D;
            if(circle3d != null)
            {
                figureName = "circle";
            }

            Rectangle3D rectangle3d = generalFiguresList[iterator] as Rectangle3D;
            if(rectangle3d != null)
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

            switch (figureName)
            {
                case "circle":
                    Circle3D circle3D = (Circle3D)generalFiguresList[iterator];
                    CircleUpdater circleUpdater = new CircleUpdater(circle3D, model3DGroup, generalFiguresList);
                    break;

                case "rectan":
                    Rectangle3D rectangle3D = (Rectangle3D)generalFiguresList[iterator];
                    RectangleUpdater rectangleUpdater = new RectangleUpdater(rectangle3D, model3DGroup, generalFiguresList);
                    break;

                case "triang":
                    Rectangle3D triangle3D = (Rectangle3D)generalFiguresList[iterator];
                    TriangleUpdater triangleUpdater = new TriangleUpdater(triangle3D, model3DGroup, generalFiguresList);
                    break;

                case "custom":
                    Custom3D custom3D = (Custom3D)generalFiguresList[iterator];
                    CustomUpdater customUpdater = new CustomUpdater(custom3D, model3DGroup, generalFiguresList);
                    break;
            }
        }
    }
}
