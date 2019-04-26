using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfApp1.CustomClasses
{
    /// <summary>
    /// Класс, предназначенный для добавления 3д фигуры по указанным точкам с указанием треугольников для рендера
    /// </summary>
    class Custom3D
    {
        public Model3DGroup model3DGroup = new Model3DGroup();

        public Point3DCollection point3Ds;
        public Point3DCollection triangles3Ds;

        public Color color = new Color();
        public string name;

        public List<object> generalFiguresList;

        public Custom3D(Model3DGroup model3DGroup, Point3DCollection point3Ds, Point3DCollection triangles3Ds, string name, Color color, List<object> generalFiguresList, bool init)
        {
            this.model3DGroup = model3DGroup;
        
            this.point3Ds = point3Ds;
            this.triangles3Ds = triangles3Ds;

            this.name = string.Concat(name, "custom");
            this.color = color;

            this.generalFiguresList = generalFiguresList;

            if (init)
            {
                InitRect();
            }
        }

        private void InitRect()
        {
            Model3DCollection model3Ds = model3DGroup.Children;

            int index = 0;

            foreach (GeometryModel3D model in model3Ds)
            {
                if (model.GetValue(FrameworkElement.NameProperty).ToString() == name)
                {
                    model3Ds.RemoveAt(index);
                    break;
                }
                index++;
            }

            Int32Collection triangles = new Int32Collection();
            
            foreach(Point3D t in triangles3Ds)
            {
                triangles.Add(Convert.ToInt32(t.X));
                triangles.Add(Convert.ToInt32(t.Y));
                triangles.Add(Convert.ToInt32(t.Z));
            }

            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            meshGeometry3D.Positions = point3Ds;
            meshGeometry3D.TriangleIndices = triangles;

            GeometryModel3D geometry = new GeometryModel3D(meshGeometry3D, new DiffuseMaterial(new SolidColorBrush(color)));
            geometry.SetValue(FrameworkElement.NameProperty, name);

            model3DGroup.Children.Add(geometry);
            Custom3D custom3D = new Custom3D(model3DGroup, point3Ds, triangles3Ds, name, color, generalFiguresList, false);
            generalFiguresList.Add(custom3D);

            MainWindow.FiguresListChanges();
        }
    }
}
