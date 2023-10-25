using System.Drawing;

namespace Graph6
{
    public class Drawer
    {
        readonly Graphics _graphics;
        private readonly Pen _pen;

        public Drawer(PictureBox canvas)
        {
            _graphics = canvas.CreateGraphics();
            _pen = new Pen(Color.Black, 1);
        }

        public void DrawEdge(Edge edge)
        {
            
        }

        public void DrawFace(Face face)
        {

        }

        public void DrawShape(Shape Shape)
        {

        }
    }
}
