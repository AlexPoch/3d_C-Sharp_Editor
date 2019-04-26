using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Xceed.Wpf.Toolkit;

namespace WpfApp1.CustomClasses
{
    class Circle3D
    {
        public double radius;
        public int resolution;

        public Vector3D normal;
        public Point3D center;

        public Model3DGroup model3DGroup;
        public Color color = new Color();
        public string name;

        public List<object> generalFiguresList;


        public Circle3D(double radius, int resolution, Vector3D normal, Point3D center, Model3DGroup model3DGroup, Color color, string name, List<object> generalFiguresList, bool init)
        {
            this.radius = radius;
            this.resolution = resolution;

            this.normal = normal;
            this.center = center;
            this.model3DGroup = model3DGroup;

            this.color = color;
            this.name = string.Concat(name, "circle");

            this.generalFiguresList = generalFiguresList;

            if (init)
            {
                InitCircle();
            }
        }

        private void InitCircle()
        {
            Model3DCollection model3Ds = model3DGroup.Children;

            int index = 0;

            foreach(GeometryModel3D model in model3Ds)
            {
                if(model.GetValue(FrameworkElement.NameProperty).ToString() == name)
                {
                    model3Ds.RemoveAt(index);
                    break;
                }
                index++;
            }

            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();

            meshGeometry3D.Positions.Add(new Point3D(0,0,0));

            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution; i++)
            {
                meshGeometry3D.Positions.Add(new Point3D(radius * Math.Cos(t * i), 0, -radius * Math.Sin(t * i)));
            }

            for (int i = 0; i < resolution; i++)
            {
                int a = 0;
                int b = i + 1;
                int c = (i < (resolution - 1)) ? i + 2 : 1;

                meshGeometry3D.TriangleIndices.Add(a);
                meshGeometry3D.TriangleIndices.Add(b);
                meshGeometry3D.TriangleIndices.Add(c);
            }

            Transform3DGroup trn = new Transform3DGroup();
            Vector3D up = new Vector3D(0,100,0);
            Vector3D vector3D = normal;
            normal.Normalize();
            Vector3D axis = Vector3D.CrossProduct(up, normal);
            double angle = Vector3D.AngleBetween(up, normal);
            trn.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, angle)));
            trn.Children.Add(new TranslateTransform3D(new Vector3D(center.X, center.Y, center.Z)));
            
            GeometryModel3D geometry = new GeometryModel3D(meshGeometry3D, new DiffuseMaterial(new SolidColorBrush(color)));
            geometry.Transform = trn;
            geometry.SetValue(FrameworkElement.NameProperty, name);
            
            model3DGroup.Children.Add(geometry);
            Circle3D circle3D = new Circle3D(radius, resolution, vector3D, center, model3DGroup, color, name, generalFiguresList, false);
            generalFiguresList.Add(circle3D);

            MainWindow.FiguresListChanges();
        }
    }
}