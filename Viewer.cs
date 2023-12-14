
using System;
using System.Drawing;
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
        private Projection _projection;

        private Pen _pen;

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
        public MyMatrix ToWorldCoordinates { get; private set; } = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 1, 0,
                                                                             0, 0, 0, 1 });
        private Graphics _graphics;

        public Graphics Graphics
        {
            get => _graphics;
            set => _graphics = value; //По-хорошему вынести бы в метод UpdateGraphics;
        }

        public Viewer(Graphics graphics, Projection projection)
        {
            _graphics = graphics;
            _projection = projection;
            _pen = new Pen(Color.Red, 1);

            Position = new MyPoint(0, 0, -200);
            CameraVector = new MyPoint(0, 0, 1);
        }

        public void SetProjection(Projection projection)
        {
            _projection = projection;
        }

        public void View(IList<Shape> shapes)
        {
            _graphics.Clear(Color.White);
            switch (_projection)
            {
                case Projection.Perspective:
                    foreach (var shape in shapes)
                    {
                        Perspective(shape);
                    }
                    break;
                case Projection.Isometric:
                    foreach (var shape in shapes)
                    {
                        Isometric(shape);
                    }
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
                    var myPointA = shape.Points[a];
                    var myPointB = shape.Points[b];
                    var mat1 = new MyMatrix(1, 4, new float[] { myPointA.X, myPointA.Y, myPointA.Z, 1 });
                    var mat2 = new MyMatrix(1, 4, new float[] { myPointB.X, myPointB.Y, myPointB.Z, 1 });
                    mat1 = mat1 * shape.MatrixToWorld * this.ToCameraCoordinates;
                    mat2 = mat2 * shape.MatrixToWorld * this.ToCameraCoordinates;
                    _graphics.DrawLine(_pen, mat1[0,0], mat1[0, 1], mat2[0, 0], mat2[0, 1]);
                    a = b;
                }
                var myPointA2 = shape.Points[a];
                var myPointB2 = shape.Points[0];
                var mat11 = new MyMatrix(1, 4, new float[] { myPointA2.X, myPointA2.Y, myPointA2.Z, 1 });
                var mat21 = new MyMatrix(1, 4, new float[] { myPointB2.X, myPointB2.Y, myPointB2.Z, 1 });
                mat11 = mat11 * shape.MatrixToWorld * this.ToCameraCoordinates;
                mat21 = mat21 * shape.MatrixToWorld * this.ToCameraCoordinates;
                _graphics.DrawLine(_pen, mat11[0, 0], mat11[0, 1], mat21[0, 0], mat21[0, 1]);
            }
        }


        public PointF ToIsometric(Shape shape, MyPoint point)
        {
            var matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
            matrix = matrix * shape.MatrixToWorld * ToCameraCoordinates;
            return new(matrix[0,0], matrix[0, 1]);
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
                _graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
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
                    _graphics.DrawLine(_pen, t[0, 0] / t[0, 3], t[0, 1] / t[0, 3], r[0, 0] / r[0, 3], r[0, 1] / r[0, 3]);
                    a = b;
                }
            }
        }

        public void ResetPosition()
        {
            Position = new MyPoint(0, 0, -200);
            CameraVector = new MyPoint(0, 0, 1);
            ToCameraCoordinates = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 1, 0,
                                                                             0, 0, 0, 1 });
            ToWorldCoordinates = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 1, 0,
                                                                             0, 0, 0, 1 });
        }

        public void MoveUp()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,10f,0,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,-10f,0,1}));
        }
        public void MoveDown()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,-10f,0,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0,10f,0,1}));

        }
        public void MoveLeft()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            -10,0f,0,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            10,0f,0,1}));
        }
        public void MoveRight()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            10f,0,0,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            -10f,0,0,1}));
        }

        public void MoveForward()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,-10,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,10,1}));
        }

        public void MoveBackward()
        {
            Move(new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,10,1}),
            new MyMatrix(4, 4, new float[] { 1,0, 0, 0,
            0, 1, 0, 0 ,
            0, 0, 1, 0,
            0f,0,-10,1})
            );
        }

        private void Move(MyMatrix T, MyMatrix T2)
        {
            ToWorldCoordinates = ToWorldCoordinates * T2;
            ToCameraCoordinates = ToCameraCoordinates * T;
            var tmp = new MyMatrix(1, 4, new float[] { Position.X, Position.Y, Position.Z, 1 });
            tmp *= T;
            Position = new MyPoint(tmp[0, 0], tmp[0, 1], tmp[0, 2]);
        }

        private float degree = 10 * (float)(Math.PI / 180);

        public void RotateUp()
        {
            Rotate(new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(degree), (float)Math.Sin(degree), 0,
                                                                     0, -(float)Math.Sin(degree), (float)Math.Cos(degree), 0,
                                                                     0, 0, 0, 1}),
                                                                     new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(-degree), (float)Math.Sin(-degree), 0,
                                                                     0, -(float)Math.Sin(-degree), (float)Math.Cos(-degree), 0,
                                                                     0, 0, 0, 1}));
        }
        public void RotateDown()
        {
            Rotate(new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(-degree), (float)Math.Sin(-degree), 0,
                                                                     0, -(float)Math.Sin(-degree), (float)Math.Cos(-degree), 0,
                                                                     0, 0, 0, 1}),
                                                                     new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(degree), (float)Math.Sin(degree), 0,
                                                                     0, -(float)Math.Sin(degree), (float)Math.Cos(degree), 0,
                                                                     0, 0, 0, 1}));
        }
        public void RotateLeft()
        {
            Rotate(new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(degree), 0, -(float)Math.Sin(degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(degree), 0, (float)Math.Cos(degree), 0,
                        0, 0, 0, 1}),
                        new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(-degree), 0, -(float)Math.Sin(-degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(-degree), 0, (float)Math.Cos(-degree), 0,
                        0, 0, 0, 1}));
        }
        public void RotateRight()
        {
            Rotate(new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(-degree), 0, -(float)Math.Sin(-degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(-degree), 0, (float)Math.Cos(-degree), 0,
                        0, 0, 0, 1}),
                        new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(degree), 0, -(float)Math.Sin(degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(degree), 0, (float)Math.Cos(degree), 0,
                        0, 0, 0, 1}));
        }

        private void Rotate(MyMatrix T, MyMatrix T2)
        {
            //MyMatrix toCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
            //                                                     0, 1, 0, 0,
            //                                                     0, 0, 1, 0,
            //                                                     -Position.X, -Position.Y, -Position.Z, 1});

            //MyMatrix fromCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
            //                                                       0, 1, 0, 0,
            //                                                       0, 0, 1, 0,
            //                                                       Position.X, Position.Y, Position.Z, 1});



            ToCameraCoordinates = ToCameraCoordinates /** toCenter*/ * T /** fromCenter*/;

            var tmp1 = new MyMatrix(1, 4, new float[] { 0, 0, -200, 1 });
            tmp1 = tmp1 * T;
            Position = new MyPoint(tmp1[0, 0], tmp1[0, 1], tmp1[0, 2]).Normalize();

            ToWorldCoordinates = ToWorldCoordinates * T2;
            var tmp = new MyMatrix(1, 4, new float[] { 0, 0, 1, 1 });
            tmp = tmp * /*toCenter **/ ToWorldCoordinates /** fromCenter*/;
            CameraVector = new MyPoint(tmp[0, 0], tmp[0, 1], tmp[0, 2]).Normalize();


        }

    }
}
