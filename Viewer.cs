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
        0,0,0, -1f/-200,
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

        public void SetProjection(Projection projection)
        {
            _projection = projection;
        }

        public void View(Shape shape)
        {
            _graphics.Clear(Color.White);

            switch (_projection)
            {
                case Projection.Perspective:
                    Perspective(Scale(shape));
                    break;
                case Projection.Isometric:
                    Isometric(shape);
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

        private void Isometric(Shape shape)
        {
            foreach (var (a, b) in shape.edges)
            {
                _graphics.DrawLine(_p, shape.points[a].X, shape.points[a].Y, shape.points[b].X, shape.points[b].Y);
            }
        }
        private void Perspective(Shape shape)
        {
            foreach (var (a, b) in shape.edges)
            {
                MyMatrix t = new MyMatrix(1, 4, new float[] { shape.points[a].X, shape.points[a].Y, shape.points[a].Z, 1 }) * projectionMatrix;
                MyMatrix r = new MyMatrix(1, 4, new float[] { shape.points[b].X, shape.points[b].Y, shape.points[b].Z, 1 }) * projectionMatrix;
                _graphics.DrawLine(_p, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
            }
        }
    }
}
