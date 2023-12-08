
using System.Xml.Serialization;

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
        public MyMatrix ToCameraCoordinates { get; private set; } = new(4, 4, new float[] { 1, 0, 0, 0,
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
            CameraVector = new MyPoint(0, 0, 1);
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

                //if (Math.Abs(t[0, 3]) < float.Epsilon)
                //{
                //    t[0, 3] = 1;
                //}
                //if (Math.Abs(r[0, 3]) < float.Epsilon)
                //{
                //    r[0, 3] = 1;
                //}
                Graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                for (int i = 1; i < face.Count; i++)
                {
                    var b = face[i];
                    t = new MyMatrix(1, 4, new float[] { shape.Points[a].X, shape.Points[a].Y, shape.Points[a].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                    r = new MyMatrix(1, 4, new float[] { shape.Points[b].X, shape.Points[b].Y, shape.Points[b].Z, 1 }) * shape.MatrixToWorld * this.ToCameraCoordinates * projectionMatrix;
                    //if (Math.Abs(t[0, 3]) < float.Epsilon)
                    //{
                    //    t[0, 3] = 1;
                    //}
                    //if (Math.Abs(r[0, 3]) < float.Epsilon)
                    //{
                    //    r[0, 3] = 1;
                    //}
                    Graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                    a = b;
                }
            }
        }

        public void ResetPosition()
        {
            Position = new MyPoint(0, 0, 0);
            ToCameraCoordinates = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 1, 0,
                                                                             0, 0, 0, 1 });
        }

        public void MoveUp()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,10f,0,1});
            Move(T);
        }
        public void MoveDown()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,-10f,0,1});
            Move(T);

        }
        public void MoveLeft()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            -10,0f,0,1});
            Move(T);
        }
        public void MoveRight()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            10f,0,0,1});
            Move(T);
        }

        public void MoveForward()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,-10,1});
            Move(T);
        }

        public void MoveBackward()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,10,1});
            Move(T);
        }

        private void Move(MyMatrix T)
        {
            ToCameraCoordinates = ToCameraCoordinates * T;
            var tmp = new MyMatrix(1, 4, new float[] { Position.X, Position.Y, Position.Z, 1 });
            tmp *= T;
            Position = new MyPoint(tmp[0, 0], tmp[0, 1], tmp[0, 2]);
        }

        private float degree = 10 * (float)(Math.PI / 180);

        public void RotateUp()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(degree), (float)Math.Sin(degree), 0,
                                                                     0, -(float)Math.Sin(degree), (float)Math.Cos(degree), 0,
                                                                     0, 0, 0, 1});
            Rotate(T);
        }
        public void RotateDown()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(-degree), (float)Math.Sin(-degree), 0,
                                                                     0, -(float)Math.Sin(-degree), (float)Math.Cos(-degree), 0,
                                                                     0, 0, 0, 1});
            Rotate(T);
        }
        public void RotateLeft()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(degree), 0, -(float)Math.Sin(degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(degree), 0, (float)Math.Cos(degree), 0,
                        0, 0, 0, 1});
            Rotate(T);
        }
        public void RotateRight()
        {
            MyMatrix T = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(-degree), 0, -(float)Math.Sin(-degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(-degree), 0, (float)Math.Cos(-degree), 0,
                        0, 0, 0, 1});
            Rotate(T);
        }

        private void Rotate(MyMatrix T)
        {
            MyMatrix toCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                 0, 1, 0, 0,
                                                                 0, 0, 1, 0,
                                                                 -Position.X, -Position.Y, -Position.Z, 1});

            MyMatrix fromCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                   0, 1, 0, 0,
                                                                   0, 0, 1, 0,
                                                                   Position.X, Position.Y, Position.Z, 1});



            ToCameraCoordinates = ToCameraCoordinates /** toCenter*/ * T /** fromCenter*/;
            var tmp = new MyMatrix(1, 4, new float[] { CameraVector.X, CameraVector.Y, CameraVector.Z, 1 });
            tmp = tmp * /*toCenter **/ T /** fromCenter*/;
            CameraVector = new MyPoint(tmp[0, 0], tmp[0, 1], tmp[0, 2]).Normalize();


        }

        public void Rotate2(Shape s)
        {
            MyPoint vector = s.GetCenter() - s.GetCenter()+ new MyPoint(0,10,0);
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            float l = vector.X / length;
            float m = vector.Y / length;
            float n = vector.Z / length;

            //Ужас, но работает
            MyMatrix scaleMatrix = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(degree)*(1 - l*l), l*(1 - (float)Math.Cos(degree))*m + n*(float)Math.Sin(degree), l * (1 - (float)Math.Cos(degree)) * n - m * (float)Math.Sin(degree), 0,
            l*(1 - (float)Math.Cos(degree))*m - n*(float)Math.Sin(degree), m*m + (float)Math.Cos(degree) * (1 - m*m), m*(1 - (float)Math.Cos(degree)) * n + l* (float)Math.Sin(degree), 0,
            l * (1 - (float)Math.Cos(degree))*n + m * (float)Math.Sin(degree), m * (1 - (float)Math.Cos(degree)) * n - l*(float)Math.Sin(degree), n * n + (float)Math.Cos(degree) * (1 - n * n), 0,
            0, 0, 0, 1});
            Rotate(scaleMatrix);
        }
    }
}
