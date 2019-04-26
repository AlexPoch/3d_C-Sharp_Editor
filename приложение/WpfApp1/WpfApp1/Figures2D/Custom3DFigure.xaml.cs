using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using WpfApp1.CustomClasses;
using Xceed.Wpf.Toolkit;

namespace WpfApp1
{
    /// <summary>
    /// Класс, для создания модели кастомной 3д фигуры
    /// </summary>
    public partial class Custom3DFigure : Window
    {
        public Point3DCollection point3Ds = new Point3DCollection();
        public Point3DCollection triangles3Ds = new Point3DCollection();

        Model3DGroup model3DGroup;
        TextBox x;
        TextBox y;
        TextBox z;
        TextBox nameFigure;

        ListBox points;
        ListBox triangles;
        
        ColorPicker colorPicker;

        List<object> generalFiguresList;

        public Custom3DFigure(Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            this.model3DGroup = model3DGroup;
            this.generalFiguresList = generalFiguresList;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.x = (TextBox)FindName("X");
            this.y = (TextBox)FindName("Y");
            this.z = (TextBox)FindName("Z");

            this.points = (ListBox)FindName("pointsList");
            this.triangles = (ListBox)FindName("trianglesList");

            this.nameFigure = (TextBox)FindName("elementName");
            this.colorPicker = (ColorPicker)FindName("ColorPicker");

            if (ValuesValidator())
            {
                Custom3D custom3D = new Custom3D(model3DGroup, point3Ds, triangles3Ds, nameFigure.Text, (Color)colorPicker.SelectedColor, generalFiguresList, true);
                this.Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private bool ValuesValidator()
        {
            string textX1 = x.Text;
            string textY1 = y.Text;
            string textZ1 = z.Text;

            ValidateDoubleValue(x);
            ValidateDoubleValue(y);
            ValidateDoubleValue(z);

            string name = nameFigure.Text;

            if (textX1 != x.Text || textY1 != y.Text || textZ1 != z.Text)
            {
                System.Windows.MessageBox.Show("Данные координат были изменены из-за неверного формата");
                return false;
            }
            else if ((string.IsNullOrEmpty(textX1) || string.IsNullOrEmpty(textY1) || string.IsNullOrEmpty(textZ1)) && trianglesList.Items.IsEmpty)
            {
                System.Windows.MessageBox.Show("Не все поля таблицы были заполнены");
                return false;
            }
            else if (ColorPicker.SelectedColor == null)
            {
                System.Windows.MessageBox.Show("Цвет не был выбран");
                return false;
            }
            else if (!Regex.IsMatch(name, @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$"))
            {
                System.Windows.MessageBox.Show("Имя не было введено либо было введено не корректно");
                nameFigure.Text = string.Empty;
                return false;
            }
            else if(triangles3Ds.Count == 0)
            {
                System.Windows.MessageBox.Show("Ни одного треугольника не было дабавлено");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ValidateDoubleValue(TextBox sender)
        {
            string text = sender.Text;
            Regex regex = new Regex(@"-?\d+(?:\.\d+)?");
            Match match = regex.Match(text);
            sender.Text = match.Value;
        }

        private void pointsList_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (e.Key == Key.Delete)
            {
                int selectedIndex = listBox.SelectedIndex;
                point3Ds.RemoveAt(selectedIndex);
                PointsList();
                trianglesList.Items.Clear();
                triangles3Ds.Clear();
            }
        }

        private void trianglesList_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (e.Key == Key.Delete)
            {
                int selectedIndex = listBox.SelectedIndex;
                triangles3Ds.RemoveAt(selectedIndex);
                TrianglesList();
            }
        }

        private void AddPoint(object sender, RoutedEventArgs e)
        {
            TextBox textx = (TextBox)FindName("X");
            TextBox texty = (TextBox)FindName("Y");
            TextBox textz = (TextBox)FindName("Z");

            string textX1 = textx.Text;
            string textY1 = texty.Text;
            string textZ1 = textz.Text;
            
            ValidateDoubleValue(textx);
            ValidateDoubleValue(texty);
            ValidateDoubleValue(textz);

            if (textX1 != textx.Text || textY1 != texty.Text || textZ1 != textz.Text || string.IsNullOrEmpty(textx.Text) || string.IsNullOrEmpty(texty.Text) || string.IsNullOrEmpty(textz.Text))
            {
                System.Windows.MessageBox.Show("Данные координат имели неверный формат");
            }
            else
            {
                Point3D point3D = new Point3D(double.Parse(textx.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(texty.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(textz.Text.ToString(), CultureInfo.InvariantCulture));
                point3Ds.Add(point3D);
                PointsList();

                textx.Text = string.Empty;
                texty.Text = string.Empty;
                textz.Text = string.Empty;
            }
        }

        public void PointsList()
        {
            ListBox listBox = (ListBox)FindName("pointsList");
            listBox.Items.Clear();
            int iteratorPoints = 0;

            ComboBox pointn1 = (ComboBox)FindName("point1");
            ComboBox pointn2 = (ComboBox)FindName("point2");
            ComboBox pointn3 = (ComboBox)FindName("point3");
            pointn1.Items.Clear();
            pointn2.Items.Clear();
            pointn3.Items.Clear();

            foreach (Point3D t in point3Ds)
            {
                listBox.Items.Add(iteratorPoints + " - (" + t.X + "," + t.Y + "," + t.Z + ")");
                point1.Items.Add(iteratorPoints + " - (" + t.X + "," + t.Y + "," + t.Z + ")");
                point2.Items.Add(iteratorPoints + " - (" + t.X + "," + t.Y + "," + t.Z + ")");
                point3.Items.Add(iteratorPoints + " - (" + t.X + "," + t.Y + "," + t.Z + ")");
                iteratorPoints++;
            }
        }

        private void AddTriangle(object sender, RoutedEventArgs e)
        {
            ComboBox pointn1 = (ComboBox)FindName("point1");
            ComboBox pointn2 = (ComboBox)FindName("point2");
            ComboBox pointn3 = (ComboBox)FindName("point3");

            if(string.IsNullOrEmpty(point1.Text) || string.IsNullOrEmpty(point2.Text) || string.IsNullOrEmpty(point3.Text))
            {
                System.Windows.MessageBox.Show("Не все точки для построения треугольника были выбраны");
            }
            else
            {
                Point3D point3D = new Point3D(double.Parse(pointn1.SelectedItem.ToString().Substring(0, 1), CultureInfo.InvariantCulture), double.Parse(pointn2.SelectedItem.ToString().Substring(0, 1), CultureInfo.InvariantCulture), double.Parse(pointn3.SelectedItem.ToString().Substring(0, 1), CultureInfo.InvariantCulture));
                triangles3Ds.Add(point3D);
                TrianglesList();
            }
        }

        public void TrianglesList()
        {
            ListBox listBox = (ListBox)FindName("trianglesList");
            listBox.Items.Clear();
            int iteratorPoints = 0;

            foreach (Point3D t in triangles3Ds)
            {
                listBox.Items.Add("(" + t.X + "," + t.Y + "," + t.Z + ")");
                iteratorPoints++;
            }
        }
    }
}