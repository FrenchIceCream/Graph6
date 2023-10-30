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
        private Graphics _graphics;

        private Projection _projection;

        private Pen _p;
        private int _height, _width;

        public Viewer(Graphics g, int width, int height , Projection projection)
        {
            _graphics = g;
            _projection = projection;
            _p = new Pen(Color.Red, 1);
            _height = height/2;
            _width = width/2; 
        }

        public void View(Shape shape)
        {
            switch (_projection)
            {
                case Projection.Perspective:
                    Perspective(shape);
                    break;
                case Projection.Isometric:
                    break;
                case Projection.Axonometry:
                    break;
            }
        }

        private void Perspective(Shape shape)
        {
            foreach (var v in shape._faces)
            {
                foreach (var e in v.Value)
                {
                    _graphics.DrawLine(new Pen(Color.Red, 1), shape._points[v.Key].X/ shape._points[v.Key].Z, shape._points[v.Key].Y/ shape._points[v.Key].Z, shape._points[e].X/ shape._points[e].Z, shape._points[e].Y / shape._points[e].Z);
                }
            }
        }


    }
}
