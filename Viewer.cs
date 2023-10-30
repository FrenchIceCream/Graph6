using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph6
{
    public enum Projection
    {
        Perspective,
        Isometric,
        Axonometry,
    }

    public class Viewer

    {
        private readonly MyMatrix projectionMatrix = new(4, 4, new float[] { 1,0,0,0,
        0,1,0,0,
        0,0,0, -1/150,
        0,0,0,1});

        private Graphics _graphics;

        private Projection _projection;

        private Pen _p;

        private readonly float scaleFactor = 15;

        public Viewer(Graphics g/*, int width, int height*/ , Projection projection)
        {
            _graphics = g;
            _projection = projection;
            _p = new Pen(Color.Red, 1);
        }

        public void View(Shape shape)
        {
            switch (_projection)
            {
                case Projection.Perspective:
                    Perspective(Scale(shape));
                    break;
                case Projection.Isometric:
                    break;
                case Projection.Axonometry:
                    break;
            }
        }

        public Shape Scale(Shape shape)
        {
            //List<MyPoint> points = new List<MyPoint>();
            //foreach(var p in shape._points)
            //{
            //    points.Add(new MyPoint(p.X*scaleFactor, p.Y * scaleFactor, p.Z * scaleFactor));
            //}
            //Dictionary<int, List<int>> d = new Dictionary<int, List<int>>(shape._faces);
            //return new Shape(points, d);
            return shape;

        }

        private void Perspective(Shape shape)
        {



            foreach (var v in shape._faces)
            {
                MyMatrix t = new MyMatrix(1, 4, new float[] { shape._points[v.Key].X, shape._points[v.Key].Y, shape._points[v.Key].Z, 1 }) * projectionMatrix;


                foreach (var e in v.Value)
                {
                    MyMatrix r = new MyMatrix(1, 4, new float[] { shape._points[e].X, shape._points[e].Y, shape._points[e].Z, 1 }) * projectionMatrix;
                    _graphics.DrawLine(_p, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                }
            }
        }


    }
}
