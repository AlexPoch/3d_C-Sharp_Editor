using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace WpfApp1.camerasFolder
{
    class CameraSelector
    {
        ComboBoxItem comboBoxItem;
        Viewport3D viewport3D = new Viewport3D();

        public CameraSelector(ComboBoxItem comboBoxItem, Viewport3D viewport3D)
        {
            this.comboBoxItem = comboBoxItem;
            this.viewport3D = viewport3D;
            cameraChanger();
        }

        private void cameraChanger()
        {
            string typeOfCamera = comboBoxItem.Name;

            switch (typeOfCamera)
            {
                case "orth":
                    OrthographicCameraCreator orthographicCameraCreator = new OrthographicCameraCreator(viewport3D);
                    orthographicCameraCreator.Show();
                    break;
                case "pers":
                    PerspectiveCameraCreator perspectiveCameraCreator = new PerspectiveCameraCreator(viewport3D);
                    perspectiveCameraCreator.Show();
                    break;
            }
        }
    }
}