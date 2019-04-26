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
    /// Класс, предназначенный для добавления на экран кругов 
    /// </summary>
    public partial class Circle : Window
    {
        Model3DGroup model3DGroup;
        TextBox cx;
        TextBox cy;
        TextBox cz;
        TextBox nx;
        TextBox ny;
        TextBox nz;
        TextBox radius;
        TextBox resolution;
        TextBox nameFigure;
        ColorPicker colorPicker;

        List<object> generalFiguresList;

        public Circle(Model3DGroup model3DGroup, List<object> generalFiguresList)
        {
            this.model3DGroup = model3DGroup;
            this.generalFiguresList = generalFiguresList;
            InitializeComponent();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.cx = (TextBox)FindName("CX");
            this.cy = (TextBox)FindName("CY");
            this.cz = (TextBox)FindName("CZ");
            this.nx = (TextBox)FindName("NX");
            this.ny = (TextBox)FindName("NY");
            this.nz = (TextBox)FindName("NZ");
            this.radius = (TextBox)FindName("Radius");
            this.resolution = (TextBox)FindName("Resolution");
            this.nameFigure = (TextBox)FindName("elementName");

            this.colorPicker = (ColorPicker)FindName("ColorPicker");

            if (ValuesValidator())
            {
                Vector3D normal = new Vector3D(double.Parse(nx.Text.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(ny.Text.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(nz.Text.ToString(), CultureInfo.InvariantCulture));

                Point3D center = new Point3D(double.Parse(cx.Text.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(cy.Text.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(cz.Text.ToString(), CultureInfo.InvariantCulture));

                Circle3D circle = new Circle3D(double.Parse(radius.Text.ToString(), CultureInfo.InvariantCulture),
                    int.Parse(resolution.Text.ToString(), CultureInfo.InvariantCulture), normal,
                    center, model3DGroup, (Color)colorPicker.SelectedColor, nameFigure.Text,
                    generalFiguresList, true);
                
                this.Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValuesValidator()
        {
            string textcx = cx.Text;
            string textcy = cy.Text;
            string textcz = cz.Text;

            string textnx = nx.Text;
            string textny = ny.Text;
            string textnz = nz.Text;

            string textRadius = radius.Text;
            string textResolution = resolution.Text;

            ValidateDoubleValue(cx);
            ValidateDoubleValue(cy);
            ValidateDoubleValue(cz);

            ValidateDoubleValue(nx);
            ValidateDoubleValue(ny);
            ValidateDoubleValue(nz);

            ValidateDoubleValue(radius);
            ValidateDoubleValue(resolution);

            string name = nameFigure.Text;

            if (textcx != cx.Text || textcy != cy.Text || textcz != cz.Text
                || textnx != nx.Text || textny != ny.Text || textnz != nz.Text
                || textRadius != radius.Text || textResolution != resolution.Text)
            {
                System.Windows.MessageBox.Show("Данные координат были изменены из-за неверного формата");
                return false;
            }
            else if (string.IsNullOrEmpty(textcx) || string.IsNullOrEmpty(textnx) || string.IsNullOrEmpty(textResolution)
                || string.IsNullOrEmpty(textcy) || string.IsNullOrEmpty(textny) || string.IsNullOrEmpty(textRadius)
                || string.IsNullOrEmpty(textcz) || string.IsNullOrEmpty(textnz))
            {
                System.Windows.MessageBox.Show("Не все поля были заполнены");
                return false;
            }
            else if (colorPicker.SelectedColor == null)
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
    }
}
