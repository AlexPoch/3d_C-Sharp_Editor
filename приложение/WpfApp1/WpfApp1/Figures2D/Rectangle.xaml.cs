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
    /// Класс, предназначенный для добавления на экран прямоугольников 
    /// </summary>
    public partial class Rectangle : Window
    {
        Model3DGroup model3DGroup;
        TextBox x1;
        TextBox y1;
        TextBox z1;
        TextBox x2;
        TextBox y2;
        TextBox z2;
        TextBox x3;
        TextBox y3;
        TextBox z3;
        TextBox nameFigure;

        int figureType = 0;
        ColorPicker colorPicker;
        List<object> generalFiguresList;

        public Rectangle(Model3DGroup model3DGroup, int figureType, List<object> generalFiguresList)
        {
            this.model3DGroup = model3DGroup;
            this.figureType = figureType;
            this.generalFiguresList = generalFiguresList;
            
            InitializeComponent();

            if (figureType == 3)
            {
                this.Title = "Меню создания треугольника";
            }
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.x1 = (TextBox)FindName("X1");
            this.y1 = (TextBox)FindName("Y1");
            this.z1 = (TextBox)FindName("Z1");
            this.x2 = (TextBox)FindName("X2");
            this.y2 = (TextBox)FindName("Y2");
            this.z2 = (TextBox)FindName("Z2");
            this.x3 = (TextBox)FindName("X3");
            this.y3 = (TextBox)FindName("Y3");
            this.z3 = (TextBox)FindName("Z3");
            this.nameFigure = (TextBox)FindName("elementName");

            this.colorPicker = (ColorPicker)FindName("ColorPicker");
            
            if(ValuesValidator())
            {
                Point3D pointLU = new Point3D(double.Parse(x1.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(y1.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(z1.Text.ToString(), CultureInfo.InvariantCulture));
                Point3D pointLD = new Point3D(double.Parse(x3.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(y3.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(z3.Text.ToString(), CultureInfo.InvariantCulture));
                Point3D pointRD = new Point3D(double.Parse(x2.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(y2.Text.ToString(), CultureInfo.InvariantCulture), double.Parse(z2.Text.ToString(), CultureInfo.InvariantCulture));

                Rectangle3D rectangle3D = new Rectangle3D(pointLU, pointLD, pointRD, (Color)colorPicker.SelectedColor, model3DGroup, nameFigure.Text, figureType, generalFiguresList, true);
                
                this.Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValuesValidator()
        {
            string textX1 = x1.Text;
            string textY1 = y1.Text;
            string textZ1 = z1.Text;

            string textX2 = x2.Text;
            string textY2 = y2.Text;
            string textZ2 = z2.Text;

            string textX3 = x3.Text;
            string textY3 = y3.Text;
            string textZ3 = z3.Text;

            ValidateDoubleValue(x1);
            ValidateDoubleValue(y1);
            ValidateDoubleValue(z1);

            ValidateDoubleValue(x2);
            ValidateDoubleValue(y2);
            ValidateDoubleValue(z2);

            ValidateDoubleValue(x3);
            ValidateDoubleValue(y3);
            ValidateDoubleValue(z3);

            string name = nameFigure.Text;

            if (textX1 != x1.Text || textY1 != y1.Text || textZ1 != z1.Text
                || textY2 != y2.Text || textY2 != y2.Text || textZ2 != z2.Text 
                || textX3 != x3.Text || textY3 != y3.Text || textZ3 != z3.Text)
            {
                System.Windows.MessageBox.Show("Данные координат были изменены из-за неверного формата");
                return false;
            }
            else if (string.IsNullOrEmpty(textX1) || string.IsNullOrEmpty(textY1) || string.IsNullOrEmpty(textZ1)
                || string.IsNullOrEmpty(textX2) || string.IsNullOrEmpty(textY2) || string.IsNullOrEmpty(textZ2)
                || string.IsNullOrEmpty(textX3) || string.IsNullOrEmpty(textY3) || string.IsNullOrEmpty(textZ3))
            {
                System.Windows.MessageBox.Show("Не все поля таблицы были заполнены");
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
