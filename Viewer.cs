
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
        private readonly MyMatrix projectionMatrix = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 0, -1f/-200,
                                                                             0, 0, 0, 1 });

        public MyPoint Position { get; private set; }
        public MyPoint CameraVector { get; private set; }
        public MyMatrix ToCameraCoordinates {  get; private set; } = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 1, 0,
                                                                             0, 0, 0, 1 });

        public Graphics Graphics;

        private Projection _projection;

        private Pen _pen;

        public Viewer(Graphics graphics, Projection projection)
        {
            Graphics = graphics;
            _projection = projection;
            _pen = new Pen(Color.Red, 1);

            Position = new MyPoint(0, 0, 0);
            Position = new MyPoint(0, 0, 1);
        }

        public void SetProjection(Projection projection)
        {
            _projection = projection;
        }

        public void View(Shape shape)
        {
            Graphics.Clear(Color.White);

            switch (_projection)
            {
                case Projection.Perspective:
                   // foreach (var shape in shapes)
                   // {
                        Perspective(shape);
                    //}
                    break;
                case Projection.Isometric:
                    //foreach (var shape in shapes)
                    //{
                        Isometric(shape);
                    //}
                    break;
                case Projection.Axonometry:
                    break;
            }
        }

        private void Isometric(Shape shape)
        {
            foreach (var face in shape.Faces)
            {
                var a = face[0];
                for (int i = 1; i < face.Count; i++)
                {
                    var b = face[i];
                    Graphics.DrawLine(_pen, shape.Points[a].X, shape.Points[a].Y, shape.Points[b].X, shape.Points[b].Y);
                    a = b;
                }
                Graphics.DrawLine(_pen, shape.Points[a].X, shape.Points[a].Y, shape.Points[face[0]].X, shape.Points[face[0]].Y);
            }
        }

        private void Perspective(Shape shape)
        {
            foreach (var face in shape.Faces)
            {
                var a = face[0];
                MyMatrix t = new MyMatrix(1, 4, new float[] { shape.Points[a].X, shape.Points[a].Y, shape.Points[a].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                MyMatrix r = new MyMatrix(1, 4, new float[] { shape.Points[face.Last()].X, shape.Points[face.Last()].Y, shape.Points[face.Last()].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                Graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                for (int i = 1; i < face.Count; i++)
                {
                    var b = face[i];
                    t = new MyMatrix(1, 4, new float[] { shape.Points[a].X, shape.Points[a].Y, shape.Points[a].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                    r = new MyMatrix(1, 4, new float[] { shape.Points[b].X, shape.Points[b].Y, shape.Points[b].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                    Graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                    a = b;
                }
            }
        }
    }
}
