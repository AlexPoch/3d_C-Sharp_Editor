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
    /// Класс, предназначенный для создания модели прямоугольника
    /// </summary>
    class Rectangle3D
    {
        public Point3D pointLU = new Point3D();
        public Point3D pointRD = new Point3D();
        public Point3D pointLD = new Point3D();
        public Color color = new Color();
        public Model3DGroup model3DGroup = new Model3DGroup();
        public string name;
        public int figureType;

        public List<object> generalFiguresList;

        public Rectangle3D(Point3D pointLU, Point3D pointRD, Point3D pointLD, Color color, Model3DGroup model3DGroup, string name, int figureType, List<object> generalFiguresList, bool init)
        {
            this.pointLU.X = pointLU.X;
            this.pointLU.Y = pointLU.Y;
            this.pointLU.Z = pointLU.Z;

            this.pointLD.X = pointLD.X;
            this.pointLD.Y = pointLD.Y;
            this.pointLD.Z = pointLD.Z;
            
            this.pointRD.X = pointRD.X;
            this.pointRD.Y = pointRD.Y;
            this.pointRD.Z = pointRD.Z;
            
            this.name = name;
            this.figureType = figureType;

            this.color = color;
            this.model3DGroup = model3DGroup;

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
                string valid = model.GetValue(FrameworkElement.NameProperty).ToString();
                if (valid.Substring(0,valid.Length-6) == name)
                {
                    model3Ds.RemoveAt(index);
                    break;
                }
                index++;
            }
            
            Point3DCollection point3Ds = new Point3DCollection();
            point3Ds.Add(pointLU);
            point3Ds.Add(pointLD);
            point3Ds.Add(pointRD);

            if (figureType == 4)
            {
                Point3D pointRU = Get4Point3D();

                point3Ds.Add(pointRU);
            }

            Int32Collection triangles = new Int32Collection();

            if(figureType == 4)
            {
                triangles.Add(0);
                triangles.Add(1);
                triangles.Add(2);
                triangles.Add(0);
                triangles.Add(2);
                triangles.Add(3);
                triangles.Add(3);
                triangles.Add(2);
                triangles.Add(1);
                triangles.Add(3);
                triangles.Add(1);
                triangles.Add(0);
                name = string.Concat(name,"rectan");
            }
            else
            {
                triangles.Add(0);
                triangles.Add(1);
                triangles.Add(2);
                triangles.Add(2);
                triangles.Add(1);
                triangles.Add(0);
                name = string.Concat(name, "triang");
            }


            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            meshGeometry3D.Positions = point3Ds;
            meshGeometry3D.TriangleIndices = triangles;

            GeometryModel3D geometry = new GeometryModel3D(meshGeometry3D,new DiffuseMaterial(new SolidColorBrush(color)));
            geometry.SetValue(FrameworkElement.NameProperty,name);
            
            model3DGroup.Children.Add(geometry);

            Rectangle3D Rectangle3D = new Rectangle3D(pointLU, pointRD, pointLD, color, model3DGroup, name, figureType, generalFiguresList, false);
            generalFiguresList.Add(Rectangle3D);

            MainWindow.FiguresListChanges();
        }

        private Point3D Get4Point3D()
        {
            Point3D point = new Point3D();
            point.X = pointRD.X + (pointLU.X - pointLD.X);
            point.Y = pointRD.Y + (pointLU.Y - pointLD.Y);
            point.Z = pointRD.Z + (pointLU.Z - pointLD.Z);
            return point;
        }
    }
}
