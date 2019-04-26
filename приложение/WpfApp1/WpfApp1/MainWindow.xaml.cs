using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp1.camerasFolder;
using WpfApp1.CustomClasses;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        List<object> generalFiguresList = new List<object>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            login login = new login(menuItem);
            login.Show();
        }

        private void LightSelected(object sender, RoutedEventArgs e)
        {
            Model3DGroup model3DGroup = (Model3DGroup)this.FindName("LightGroup");
            ComboBoxItem comboBoxItem = (ComboBoxItem)sender;
            LightingSelector lightingSelector = new LightingSelector(comboBoxItem, model3DGroup);
        }

        private void ObjectSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PerspectiveCamera camera = (PerspectiveCamera)FindName("perspectiveCameraEditor");
            SliderValueChanger sliderValueChanger = new SliderValueChanger(sender, camera);
        }

        private void RectCreate(object sender, RoutedEventArgs e)
        {
            Model3DGroup model3DGroup = (Model3DGroup)this.FindName("Figures3D");
            Rectangle rectangle = new Rectangle(model3DGroup, 4, generalFiguresList);
            rectangle.Show();
        }

        private void CircleCreate(object sender, RoutedEventArgs e)
        {
            Model3DGroup model3DGroup = (Model3DGroup)this.FindName("Figures3D");
            Circle circle = new Circle(model3DGroup, generalFiguresList);
            circle.Show();
        }
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TriangleCreate(object sender, RoutedEventArgs e)
        {
            Model3DGroup model3DGroup = (Model3DGroup)this.FindName("Figures3D");
            Rectangle rectangle = new Rectangle(model3DGroup, 3, generalFiguresList);
            rectangle.Show();
        }

        private void CreateCustom3D(object sender, RoutedEventArgs e)
        {
            Model3DGroup model3DGroup = (Model3DGroup)this.FindName("Figures3D");
            Custom3DFigure custom3DFigure = new Custom3DFigure(model3DGroup, generalFiguresList);
            custom3DFigure.Show();
        }

        public static void FiguresListChanges()
        {
            MainWindow mainWindow = Window.GetWindow(Application.Current.MainWindow) as MainWindow;
            ComboBox comboBox = (ComboBox)mainWindow.FiguresList3D;
            comboBox.Items.Clear();
            Model3DGroup model3DGroup = mainWindow.Figures3D;
            Model3DCollection model3Ds = model3DGroup.Children;
            
            foreach(GeometryModel3D t in model3Ds)
            {
                string unHandled = t.GetValue(FrameworkElement.NameProperty).ToString();
                string handled = unHandled.Substring(0, unHandled.Length-6);
                comboBox.Items.Add(handled);
            }
        }

        private void UpdateFigure(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)FindName("FiguresList3D");
            Model3DGroup model3DGroup = (Model3DGroup)FindName("Figures3D");
            FigureUpdater figureUpdater = new FigureUpdater(comboBox, model3DGroup, generalFiguresList);
        }

        private void DeleteFigure(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)FindName("FiguresList3D");
            Model3DGroup model3DGroup = (Model3DGroup)FindName("Figures3D");
            FigureDeleter figureDeleter = new FigureDeleter(comboBox, model3DGroup, generalFiguresList);
        }

        private void FiguresList3D_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Button updater = (Button)FindName("Updater");
            Button deleter = (Button)FindName("Deleter");

            if (comboBox.SelectedItem == null)
            {
                updater.IsEnabled = false;
                deleter.IsEnabled = false;
            }
            else
            {
                updater.IsEnabled = true;
                deleter.IsEnabled = true;
            }
        }

        private void CameraSelected(object sender, RoutedEventArgs e)
        {
            Viewport3D viewport3D = (Viewport3D)this.FindName("mainViewport");
            ComboBoxItem comboBoxItem = (ComboBoxItem)sender;
            CameraSelector cameraSelector = new CameraSelector(comboBoxItem, viewport3D);
            InitializeComponent();
        }
    }
}