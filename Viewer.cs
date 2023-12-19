
using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;

namespace Graph6
{
    public enum Projection
    {
        Perspective,
        Isometric,
        Axonometry,
    }

    public enum RenderMode
    { 
        Sceleton,
        Buffer,
        Trimming,
        Texturing,
    }

    public class Viewer
    {
        private Projection _projection;

        private Pen _pen;
        private PictureBox _canvas;
        private readonly MyMatrix projectionMatrix = new(4, 4, new float[] { 1, 0, 0, 0,
                                                                             0, 1, 0, 0,
                                                                             0, 0, 0, -1f/-200,
                                                                             0, 0, 0, 1 });
        private double[,] _buffer;

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
        private int _width;
        private int _height;

        public Graphics Graphics
        {
            get => _graphics;
            set => _graphics = value; //По-хорошему вынести бы в метод UpdateGraphics;
        }

        public RenderMode RenderMode { get; set; } = RenderMode.Sceleton;

        public Viewer(PictureBox canvas, Projection projection)
        {
            _projection = projection;
            _pen = new Pen(Color.Red, 1);
            _canvas = canvas;
            _graphics = canvas.CreateGraphics();
            _width = canvas.Width;
            _height = canvas.Height;
            _graphics.TranslateTransform(_width / 2 , _height / 2);
            _graphics.ScaleTransform(1, -1);
            Position = new MyPoint(0, 0, -200);
            CameraVector = new MyPoint(0, 0, 1);
            _buffer = new double[_width, _height];
        }

        public void SetProjection(Projection projection)
        {
            _projection = projection;
        }

        public void View(IList<Shape> shapes)
        {
            _graphics.Clear(Color.White);

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _buffer[x, y] = double.MinValue;
                }
            }
            switch (RenderMode) //TODO зарефакторить
            {
                case RenderMode.Sceleton:
                    switch (_projection)
                    {
                        case Projection.Perspective:
                                Perspective(shapes);
                            break;
                        case Projection.Isometric:
                                Isometric(shapes);
                            break;
                    }
                    break;
                case RenderMode.Buffer:
                    ZBuffer(shapes);
                    break;
                case RenderMode.Trimming:
                    RemoveEdges(shapes);
                    break;
                case RenderMode.Texturing:
                    Texturing(shapes);
                    break;
            }
        }

        private void Texturing(IList<Shape> shapes)
        {
            Bitmap bitmap = new Bitmap(_width, _height);
           

            foreach (var shape in shapes)
            {
                if (shape.Texture == null)
                    continue;
                var matrix = shape.MatrixToWorld * ToCameraCoordinates;
                for (int j = 0; j < shape.Faces.Count; ++j) //В данном случае face - треугольник
                {
                    var face = shape.Faces[j];
                    var result = IsVisible(face, matrix, shape);
                    if (!result.Item4 || face.Textels.Count == 0)
                        continue;


                    var point0 = result.Item1;
                    var u0 = face.Textel(2).U;
                    var v0 = face.Textel(2).V;
                    var z0 = point0.Z;

                    var point1 = result.Item2;
                    var u1 = face.Textel(1).U;
                    var v1 = face.Textel(1).V;
                    var z1 = point1.Z;

                    var point2 = result.Item3;
                    var u2 = face.Textel(0).U;
                    var v2 = face.Textel(0).V;
                    var z2 = point2.Z;

                    var oldPoint0 = new PointF(point0.X, point0.Y);

                    // Переносим треугольник в начала координат;
                    point1 -= point0;
                    point2 -= point0;
                    point0 = new(0, 0, 0);

                    if (point2.Y == 0)
                    {
                        (point1, point2) = (point2, point1);
                        (z1, z2) = (z2, z1);
                        (u1, u2) = (u2, u1);
                        (v1, v2) = (v2, v1);
                    }

                    //Разницапо U и V координатам:
                    float deltaU0 = u1 - u0;
                    float deltaU1 = u2 - u0;

                    float deltaV0 = v1 - v0;
                    float deltaV1 = v2 - v0;


                    //Нужны будут для интерполирования по Z координате.
                    float deltaZ0 = z1 - z0;
                    float deltaZ1 = z2 - z0;

                    var values = new MyPoint[3] { point0, point1, point2 };

                    var minX = values.Min(value => value.X);
                    var maxX = values.Max(value => value.X);

                    var minY = values.Min(value => value.Y);
                    var maxY = values.Max(value => value.Y);


                    var width = _width / 2;
                    var height = _height / 2;

                    for (int y = (int)minY; y <= maxY; ++y)
                    {
                        for (int x = (int)minX; x <= maxX; ++x)
                        {
                            float w1 = ((y * point2.X - x * point2.Y) * 1.0f) / (point1.Y * point2.X - point1.X * point2.Y);
                            if (w1 >= 0 && w1 <= 1)
                            {
                                float w2 = ((y - w1 * point1.Y) * 1.0f) / point2.Y;

                                if (w2 >= 0 && (w1 + w2) <= 1)
                                {
                                    var texture = shape.Texture;
                                    float z = z0 + (deltaZ0 * w1) + (deltaZ1 * w2);
                                    var newX = (width + (x + (int)oldPoint0.X));
                                    var newY = (height - (y + (int)oldPoint0.Y));
                                    if (z > _buffer[newX, newY])
                                    {
                                        _buffer[newX, newY] = z;
                                        var textureX = (int)(texture.Width * (u0 + (deltaU0 * w1) + (deltaU1 * w2))) % texture.Width;

                                        var textureY = (int)(texture.Height * (v0 + (deltaV0 * w1) + (deltaV1 * w2))) % texture.Height;

                                        var color = shape.Texture.GetPixel(textureX, textureY);
                                        bitmap.SetPixel(newX, newY, color);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _canvas.Image = bitmap;
            _canvas.Update();
            _canvas.Invalidate();
        }

        private void Isometric(IList<Shape> shapes)
        {
            foreach (var shape in shapes)
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
                        _graphics.DrawLine(_pen, mat1[0, 0], mat1[0, 1], mat2[0, 0], mat2[0, 1]);
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
        }
        
        private MyPoint Transfer(MyMatrix transformation, MyPoint point)
        {
            var matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
            if (_projection == Projection.Isometric)
            {
                matrix *= transformation;
                return new(matrix[0, 0], matrix[0, 1], matrix[0, 2]);
            }
            else
            {
                matrix *= transformation * projectionMatrix;
                return new(matrix[0, 0] / matrix[0, 3], matrix[0, 1] / matrix[0, 3], matrix[0, 2] / matrix[0, 3]);
            }
        }



        private void RemoveEdges(IList<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                var matrix = shape.MatrixToWorld * ToCameraCoordinates;
                var pen = new Pen(Color.Red);
                foreach (var face in shape.Faces)
                {
                    var result = IsVisible(face, matrix, shape);
                    if (result.Item4)
                    {
                        var p1 = result.Item1;
                        var p2 = result.Item2;
                        var p3 = result.Item3;
                        _graphics.DrawLine(pen, new Point((int)p1.X, (int)p1.Y), new Point((int)p2.X, (int)p2.Y));
                        _graphics.DrawLine(pen, new Point((int)p2.X, (int)p2.Y), new Point((int)p3.X, (int)p3.Y));
                        _graphics.DrawLine(pen, new Point((int)p1.X, (int)p1.Y), new Point((int)p3.X, (int)p3.Y));
                    }
                }
            }
        }


        private void ZBuffer(IList<Shape> shapes)
        { 
            Bitmap bitmap = new Bitmap(_width, _height);
            foreach (var shape in shapes)
            {
                var matrix = shape.MatrixToWorld * ToCameraCoordinates;
                for (int j = 0; j < shape.Faces.Count; ++j) //В данном случае face - треугольник
                {
                    var face = shape.Faces[j];
                    var result = IsVisible(face, matrix, shape);
                    if (!result.Item4)
                        continue;
                    
                    var point0 = result.Item1;

                    var z0 = point0.Z;

                    var point1 = result.Item2;
                    var z1 = point1.Z;

                    var point2 = result.Item3;
                    var z2 = point2.Z;

                    var oldPoint0 = new PointF(point0.X, point0.Y);

                    // Переносим треугольник в начала координат;
                    point1 -= point0;
                    point2 -= point0;
                    point0 = new(0, 0, 0);

                    if (point2.Y == 0)
                    {
                        (point1, point2) = (point2, point1);
                        (z1, z2) = (z2, z1);
                    }

                    //Нужны будут для интерполирования по Z координате.
                    float deltaZ0 = z1 - z0;
                    float deltaZ1 = z2 - z0;

                    var values = new MyPoint[3] { point0, point1, point2 };

                    var minX = values.Min(value => value.X);
                    var maxX = values.Max(value => value.X);

                    var minY = values.Min(value => value.Y);
                    var maxY = values.Max(value => value.Y);


                    var width = _width / 2;
                    var height = _height / 2;

                    for (int y = (int)minY; y <= maxY; ++y)
                    {
                        for (int x = (int)minX; x <= maxX; ++x)
                        {
                            float w1 = ((y * point2.X - x * point2.Y) * 1.0f) / (point1.Y * point2.X - point1.X * point2.Y);
                            if (w1 >= 0 && w1 <= 1)
                            {
                                float w2 = ((y - w1 * point1.Y) * 1.0f) / point2.Y;

                                if (w2 >= 0 && (w1 + w2) <= 1)
                                {

                                    // (x, y, z)
                                    float z = z0 + (deltaZ0 * w1) + (deltaZ1 * w2);
                                    var newX = (width + (x + (int)oldPoint0.X));
                                    var newY = (height - (y + (int)oldPoint0.Y));

                                    if (z > _buffer[newX, newY])
                                    {
                                        _buffer[newX, newY] = z;
                                        bitmap.SetPixel(newX, newY, face.Color);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _canvas.Image = bitmap;
            _canvas.Update();
            _canvas.Invalidate();
        }










        private (MyPoint, MyPoint, MyPoint, bool) IsVisible(Face face, MyMatrix matrix, Shape shape)
        {
            //Debug.WriteLine("Old: " + shape.Points[face[0]].X + " " + shape.Points[face[0]].Y + " " + shape.Points[face[0]].Z);
            //Debug.WriteLine("New: " + pp1[0, 0] + " " + pp1[0, 1] + " " + pp1[0, 2]);

            var p1 = Transfer(matrix, shape.Points[face[2]]);
            var p2 = Transfer(matrix, shape.Points[face[1]]);
            var p3 = Transfer(matrix, shape.Points[face[0]]);

            //since encoding acts weirdly when I commit to github, I'll leave a comment in English
            //got it from some article on calculating a surface normal - it works given that face is a triangle
            float nx = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p2.Z - p1.Z) * (p3.Y - p1.Y);
            float ny = (p2.Z - p1.Z) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Z - p1.Z);
            float nz = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

            //var cam_pos = _viewer.Position;

            var center = shape.GetCenter();
            Vector3 vec = new Vector3(0 - center.X, 0 - center.Y, -400 - center.Z);
            Vector3 normal = new Vector3(nx, ny, nz);
            normal = Vector3.Normalize(normal);

            var cross = Vector3.Cross(vec, normal);
            var dot = Vector3.Dot(vec, normal);

            var angle = Math.PI - Math.Atan2(cross.Length(), dot);
            angle = angle * 360 / (2 * Math.PI);
            //Debug.WriteLine((float)angle);

            return (p1, p2, p3, angle < 90);
        }

        private void Perspective(IList<Shape> shapes)
        {
            foreach (var shape in shapes)
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
