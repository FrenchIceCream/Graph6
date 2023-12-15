using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.Pkcs;

namespace Graph6
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private Viewer _viewer;
        private Shape _currentShape;
        private IList<Shape> _shapes = new List<Shape>();
        private List<PointF> _solidOfRevolution;
        private DrawingState _drawingState;
        private Pen _pen;
        public Form1()
        {
            InitializeComponent();
            InitListsAndBoxes();

            _drawingState = DrawingState.NODRAWING;
            _pen = new Pen(Color.Blue, 2);
            _solidOfRevolution = new List<PointF> { };

            //Указание начала координат в центре окна
            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);
            _viewer = new Viewer(_graphics, Projection.Isometric);
            _currentShape = Shapes.Empty();
        }

        private void InitListsAndBoxes()
        {
            AxesList.Items.Add("X");
            AxesList.Items.Add("Y");
            AxesList.Items.Add("Z");
            AxesList.SelectedIndex = 0;

            AxesList_Rt.Items.Add("X");
            AxesList_Rt.Items.Add("Y");
            AxesList_Rt.Items.Add("Z");
            AxesList_Rt.SelectedIndex = 0;

            AxesList_SolidOfRev.Items.Add("X");
            AxesList_SolidOfRev.Items.Add("Y");
            AxesList_SolidOfRev.Items.Add("Z");
            AxesList_SolidOfRev.SelectedIndex = 0;

            ScaleValue.Text = "2";
            Angle.Text = "90";
            X1.Text = "10";
            Y1.Text = "10";
            Z1.Text = "10";
            X2.Text = "10";
            Y2.Text = "20";
            Z2.Text = "10";

            NumOfSections.Text = "6";
        }

        enum DrawingState { NODRAWING, FREE_DRAWING }

        private void ViewShapes()
        {
            _viewer.View(_shapes);
        }

        private void SelectShape(Shape shape)
        {
            ShapesBox.Items.Add(shape);
            _shapes.Add(shape);
            ShapesBox.SelectedIndex = ShapesBox.Items.Count - 1;
            SelectItem();
        }

        private void ShapesBox_SelectedIndexChanged(object sender, EventArgs e) => SelectItem();

        private void SelectItem()
        {
            if (_shapes.Count > 0)
            {
                _currentShape = _shapes[ShapesBox.SelectedIndex];
            }
        }

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;

            int kx = -1;
            int ky = -1;
            int kz = -1;

            switch (AxesList.Text)
            {
                case "X":
                    kx = 1;
                    break;
                case "Y":
                    ky = 1;
                    break;
                case "Z":
                    kz = 1;
                    break;
            }

            MyMatrix mirrorMatrix = new MyMatrix(4, 4, new float[] {kx, 0,  0,  0,
                                                                    0,  ky, 0,  0,
                                                                    0,  0,  kz, 0,
                                                                    0,  0,  0,  1});
            AffineTransform(mirrorMatrix, _currentShape);
            ViewShapes();
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;

            ScaleValue.Text = ScaleValue.Text.Replace('.', ',');
            float k = float.Parse(ScaleValue.Text);

            MyMatrix ScaleMat = new MyMatrix(4, 4, new float[] {k, 0, 0, 0,
                                                                0, k, 0, 0,
                                                                0, 0, k, 0,
                                                                0, 0, 0, 1});
            AffineTransform(ScaleMat, _currentShape);
            ViewShapes();
        }

        private void Button_Rotate_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;

            float degree = float.Parse(Angle.Text) * (float)(Math.PI / 180);

            MyMatrix rotationMatrix = new MyMatrix(4, 4, new float[]   {1, 0, 0, 0,
                                                                     0, (float)Math.Cos(degree), (float)Math.Sin(degree), 0,
                                                                     0, -(float)Math.Sin(degree), (float)Math.Cos(degree), 0,
                                                                     0, 0, 0, 1});

            switch (AxesList_Rt.Text)
            {
                case "X":
                    break;
                case "Y":
                    rotationMatrix = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(degree), 0, -(float)Math.Sin(degree), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(degree), 0, (float)Math.Cos(degree), 0,
                        0, 0, 0, 1});
                    break;
                case "Z":
                    rotationMatrix = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(degree), (float)Math.Sin(degree), 0, 0,
                        -(float)Math.Sin(degree), (float)Math.Cos(degree), 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1});
                    break;
            }
            AffineTransform(rotationMatrix, _currentShape);
            ViewShapes();
        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;

            float degree = float.Parse(Angle.Text) * (float)(Math.PI / 180);

            MyPoint A = new MyPoint(float.Parse(X1.Text), float.Parse(Y1.Text), float.Parse(Z1.Text));
            MyPoint B = new MyPoint(float.Parse(X2.Text), float.Parse(Y2.Text), float.Parse(Z2.Text));

            MyPoint vector = B - A;
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            float l = vector.X / length;
            float m = vector.Y / length;
            float n = vector.Z / length;

            //Ужас, но работает
            MyMatrix mat = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(degree)*(1 - l*l), l*(1 - (float)Math.Cos(degree))*m + n*(float)Math.Sin(degree), l * (1 - (float)Math.Cos(degree)) * n - m * (float)Math.Sin(degree), 0,
            l*(1 - (float)Math.Cos(degree))*m - n*(float)Math.Sin(degree), m*m + (float)Math.Cos(degree) * (1 - m*m), m*(1 - (float)Math.Cos(degree)) * n + l* (float)Math.Sin(degree), 0,
            l * (1 - (float)Math.Cos(degree))*n + m * (float)Math.Sin(degree), m * (1 - (float)Math.Cos(degree)) * n - l*(float)Math.Sin(degree), n * n + (float)Math.Cos(degree) * (1 - n * n), 0,
            0, 0, 0, 1});

            _currentShape.MatrixToWorld = _currentShape.MatrixToWorld * mat;
            //TurnShape(mat, ref _currentShape);

            ViewShapes();
        }

        private void Button_Translation_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;

            if (!float.TryParse(Translation_X.Text, out var dx))
                return;
            if (!float.TryParse(Translation_Y.Text, out var dy))
                return;
            if (!float.TryParse(Translation_Z.Text, out var dz))
                return;

            MyMatrix translationMatrix = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                          0, 1, 0, 0,
                                                                          0, 0, 1, 0,
                                                                          -dx, -dy, -dz, 1});

            AffineTransform(translationMatrix, _currentShape);
            ViewShapes();
        }

        public void TurnShape(MyMatrix mat, ref Shape shape)
        {
            //_currentShape.MatrixToWorld = _currentShape.MatrixToWorld * mat;
            for (int i = 0; i < _currentShape.Points.Count; i++)
            {
                MyPoint point = _currentShape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * mat;
                _currentShape.Points[i] = new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]);
            }
        }

        private void AffineTransform(MyMatrix mat, Shape shape)
        {
            var center = shape.GetCenter();

            MyMatrix toCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                 0, 1, 0, 0,
                                                                 0, 0, 1, 0,
                                                                 -center.X, -center.Y, -center.Z, 1});

            MyMatrix fromCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                   0, 1, 0, 0,
                                                                   0, 0, 1, 0,
                                                                   center.X, center.Y, center.Z, 1});

            MyMatrix transformMatrix = toCenter * mat * fromCenter;

            shape.MatrixToWorld = shape.MatrixToWorld * transformMatrix;

            /*
            for (int i = 0; i < shape.Points.Count; i++)
            {
                MyPoint point = shape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * transformMatrix;
                shape.Points[i] = new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]);
            }*/
        }

        private void ParallelButton_Click(object sender, EventArgs e)
        {
            _viewer.SetProjection(Projection.Isometric);
            ViewShapes();
        }

        private void PerspectiveButton_Click(object sender, EventArgs e)
        {
            _viewer.SetProjection(Projection.Perspective);
            ViewShapes();
        }

        private void CubeButton_Click(object sender, EventArgs e)
        {
            SelectShape(Shapes.Cube());
            ViewShapes();
        }

        private void OctahedronButton_Click(object sender, EventArgs e)
        {
            SelectShape(Shapes.Octahedron());
            ViewShapes();
        }

        private void TetrahedronButton_Click(object sender, EventArgs e)
        {
            SelectShape(Shapes.Tetrahedron());
            ViewShapes();
        }

        //TODO
        private void IcosahedronButton_Click(object sender, EventArgs e)
        {
            //_shape = Shapes.Icosahedron();
            ViewShapes();
        }

        //TODO
        private void DodecahedronButton_Click(object sender, EventArgs e)
        {
            //_shape = Shapes.Dodecahedron();
            ViewShapes();
        }


        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog diaglog = new()
            {
                Filter = "(*.pgj)|*.pgj",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (diaglog.ShowDialog() == DialogResult.OK)
            {
                if (diaglog.CheckFileExists)
                {
                    string file = File.ReadAllText(diaglog.FileName);
                    _currentShape = JsonConvert.DeserializeObject<Shape>(file, new Converter());
                    ViewShapes();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_currentShape.IsEmpty)
                return;
            string file = JsonConvert.SerializeObject(_currentShape, Formatting.Indented, new Converter());
            SaveFileDialog diaglog = new()
            {
                Filter = "(*.pgj)|*.pgj",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (diaglog.ShowDialog() == DialogResult.OK)
            {
                if (diaglog.CheckFileExists)
                {
                    File.Delete(diaglog.FileName);
                }
                File.WriteAllText(diaglog.FileName, file);
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            CalculateShapeByFunction();
        }

        private void CalculateShapeByFunction()
        {
            double X0;
            double X1;
            double Y0;
            double Y1;
            double Xdelta;
            double Ydelta;
            if (!Double.TryParse(X0TextBox.Text, out X0))
            {
                return;
            }
            if (!Double.TryParse(X1TextBox.Text, out X1))
            {
                return;
            }
            if (!Double.TryParse(Y0TextBox.Text, out Y0))
            {
                return;
            }
            if (!Double.TryParse(Y1TextBox.Text, out Y1))
            {
                return;
            }
            if (!Double.TryParse(XDeltaTextBox.Text, out Xdelta))
            {
                return;
            }
            if (!Double.TryParse(YDeltaTextBox.Text, out Ydelta))
            {
                return;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string formul = formulTextBox.Text;

            Function f = new Function($"f(x,y)={formul}");

            List<MyPoint> Points = new();

            List<Face> Faces = new();

            double XT, YT;
            int i, j;
            int itemsInRow = (int)((X1 - X0) / Xdelta);
            for (i = 0, XT = X0; XT < X1; i++, XT += Xdelta)
            {
                for (j = 0, YT = Y0; YT < Y1; j++, YT += Xdelta)
                {

                    Expression expr = new Expression($"f({XT},{YT})", f);
                    Points.Add(new MyPoint((float)XT, (float)YT, (float)expr.calculate() + 15));
                    if (j != 0)
                    {
                        Faces.Add(new(new List<int> { Points.Count - 1, Points.Count - 2 }));
                    }
                    if (i != 0)
                    {
                        Faces.Add(new(new List<int> { Points.Count - 1, Points.Count - 2 - itemsInRow }));
                    }
                }

            }

            var function = new FunctionShape(Points, Faces);
            SelectShape(function);

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            ViewShapes();
        }

        private void Button_SolidOfRevolution_Click(object sender, EventArgs e)
        {
            if (_drawingState == DrawingState.NODRAWING)
            {
                _graphics.ScaleTransform(1, -1);
                _graphics.TranslateTransform(-Canvas.Width / 2, -Canvas.Height / 2);
            }
            _drawingState = DrawingState.FREE_DRAWING;

            _graphics.Clear(Color.White);
            _solidOfRevolution.Clear();
            _graphics.DrawLine(new Pen(Color.Black, 1), new Point(Canvas.Width / 2, Canvas.Height - 20), new Point(Canvas.Width / 2, 20));
            _graphics.DrawLine(new Pen(Color.Black, 1), new Point(20, Canvas.Height / 2), new Point(Canvas.Width - 20, Canvas.Height / 2));
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            if (_drawingState == DrawingState.FREE_DRAWING)
            {
                MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
                Point p = new Point(mouseEventArgs.X, mouseEventArgs.Y);
                FreeDraw(p);
            }
        }

        private void FreeDraw(PointF p)
        {
            if (_solidOfRevolution.Count() == 0)
            {
                _graphics.DrawRectangle(_pen, p.X, p.Y, 1, 1);
            }
            else
            {
                PointF prev = _solidOfRevolution.Last();
                _graphics.DrawLine(_pen, prev, p);
            }
            _solidOfRevolution.Add(p);
        }

        private void Button_SolidOfRev_Show_Click(object sender, EventArgs e)
        {
            CalculateSolidOfRevolution();
            ViewShapes();
        }

        private void CalculateSolidOfRevolution()
        {
            _graphics.Clear(Color.White);
            _drawingState = DrawingState.NODRAWING;
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);

            if (_solidOfRevolution.Count == 0)
                return;

            Shape solid_shape = Shapes.Empty();

            solid_shape.Points.Add(new MyPoint(_solidOfRevolution[0].X - Canvas.Width / 2, (_solidOfRevolution[0].Y - Canvas.Height / 2) * (-1), 0));
            for (int i = 1; i < _solidOfRevolution.Count; i++)
                solid_shape.Points.Add(new MyPoint(_solidOfRevolution[i].X - Canvas.Width / 2, (_solidOfRevolution[i].Y - Canvas.Height / 2) * (-1), 0));

            _currentShape = new Shape(solid_shape);
            SelectShape(_currentShape);
            //угол поворота
            int sections = int.Parse(NumOfSections.Text);
            float deg = (360 / sections) * (float)(Math.PI / 180);

            MyPoint A = new MyPoint(0, 0, 0);  //вокруг X
            MyPoint B = new MyPoint(10, 0, 0);

            switch (AxesList_SolidOfRev.Text)
            {
                case "X":
                    break;
                case "Y":
                    A = new MyPoint(0, 0, 0);
                    B = new MyPoint(0, 10, 0);
                    break;
                case "Z":
                    A = new MyPoint(0, 0, 0);
                    B = new MyPoint(0, 0, 10);
                    break;
            }

            MyPoint vector = B - A;
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            float l = vector.X / length;
            float m = vector.Y / length;
            float n = vector.Z / length;

            MyMatrix mat = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(deg)*(1 - l*l), l*(1 - (float)Math.Cos(deg))*m + n*(float)Math.Sin(deg), l * (1 - (float)Math.Cos(deg)) * n - m * (float)Math.Sin(deg), 0,
            l*(1 - (float)Math.Cos(deg))*m - n*(float)Math.Sin(deg), m*m + (float)Math.Cos(deg) * (1 - m*m), m*(1 - (float)Math.Cos(deg)) * n + l* (float)Math.Sin(deg), 0,
            l * (1 - (float)Math.Cos(deg))*n + m * (float)Math.Sin(deg), m * (1 - (float)Math.Cos(deg)) * n - l*(float)Math.Sin(deg), n * n + (float)Math.Cos(deg) * (1 - n * n), 0,
            0, 0, 0, 1});

            int c = solid_shape.Points.Count();

            for (int i = 1; i < sections; i++)
            {
                TurnShape(mat, ref solid_shape);
                _currentShape.Points.Add(solid_shape.Points[0]);
                for (int j = 1; j < c; j++)
                {
                    _currentShape.Points.Add(solid_shape.Points[j]);
                    var l1 = new Face(new List<int> { i * c + j - 1, i * c + j, (i - 1) * c + j });
                    var l2 = new Face(new List<int> { (i - 1) * c + j - 1, i * c + j - 1, (i - 1) * c + j });

                    _currentShape.Faces.Add(l1);  //точка перед текущей  + текующая точка + точка на предыдущей итерации
                    if (!l1.SequenceEqual(l2))
                        _currentShape.Faces.Add(l2); //точка перед ней + точка перед текущей + точка на предыдущей итерации 
                }
            }

            for (int j = 1; j < c; j++)
            {
                var l1 = new List<int> { (sections - 1) * c + j, (sections - 1) * c + j - 1, j };
                var l2 = new List<int> { (sections - 1) * c + j, j, j + 1 };
                _currentShape.Faces.Add(new Face(l1));
                if (!l1.SequenceEqual(l2))
                    _currentShape.Faces.Add(new Face(l2));
            }

            //Pen p = new Pen(Color.Green, 4);
            //for (int j = 1; j < c; j++)
            //{
            //    _graphics.DrawLine(p, _currentShape.Points[j].X, _currentShape.Points[j].Y, _currentShape.Points[(sections - 1) * c + j - 1].X, _currentShape.Points[(sections - 1) * c + j - 1].Y);
            //    _graphics.DrawLine(p,_currentShape.Points[(sections - 1) * c + j - 1].X, _currentShape.Points[(sections - 1) * c + j - 1].Y, _currentShape.Points[(sections - 1) * c + j].X, _currentShape.Points[(sections - 1) * c + j].Y);
            //    _currentShape.Faces.Add(new List<int> { j, (sections - 1) * c + j - 1, (sections - 1) * c + j });
            //}

            //Debug.WriteLine(_currentShape.GetCenter());
        }

        private void RemoveEdgesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Shape shape = new Shape(_currentShape);
            _graphics.Clear(Color.White);

            var matrix = _currentShape.MatrixToWorld * _viewer.ToCameraCoordinates;
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


        private (MyPoint, MyPoint, MyPoint, bool) IsVisible(Face face, MyMatrix matrix, Shape shape)
        {
            var pp1 = new MyMatrix(1, 4, new float[] { shape.Points[face[0]].X, shape.Points[face[0]].Y, shape.Points[face[0]].Z, 1 }) * matrix;
            var pp2 = new MyMatrix(1, 4, new float[] { shape.Points[face[1]].X, shape.Points[face[1]].Y, shape.Points[face[1]].Z, 1 }) * matrix;
            var pp3 = new MyMatrix(1, 4, new float[] { shape.Points[face[2]].X, shape.Points[face[2]].Y, shape.Points[face[2]].Z, 1 }) * matrix;

            //Debug.WriteLine("Old: " + shape.Points[face[0]].X + " " + shape.Points[face[0]].Y + " " + shape.Points[face[0]].Z);
            //Debug.WriteLine("New: " + pp1[0, 0] + " " + pp1[0, 1] + " " + pp1[0, 2]);

            var p1 = new MyPoint(pp3[0, 0], pp3[0, 1], pp3[0, 2]);
            var p2 = new MyPoint(pp2[0, 0], pp2[0, 1], pp2[0, 2]);
            var p3 = new MyPoint(pp1[0, 0], pp1[0, 1], pp1[0, 2]);

            //since encoding acts weirdly when I commit to github, I'll leave a comment in English
            //got it from some article on calculating a surface normal - it works given that polygon is a triangle
            float nx = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p2.Z - p1.Z) * (p3.Y - p1.Y);
            float ny = (p2.Z - p1.Z) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Z - p1.Z);
            float nz = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

            var cam_pos = _viewer.Position;

            var center = shape.GetCenter();
            Vector3 vec = new Vector3(cam_pos.X - center.X, cam_pos.Y - center.Y, cam_pos.Z - center.Z);
            Vector3 normal = new Vector3(nx, ny, nz);
            normal = Vector3.Normalize(normal);

            var cross = Vector3.Cross(vec, normal);
            var dot = Vector3.Dot(vec, normal);

            var angle = Math.PI - Math.Atan2(cross.Length(), dot);
            angle = angle * 360 / (2 * Math.PI);
            //Debug.WriteLine((float)angle);

            return (p1, p2, p3, angle < 90);
        }





        private int _index = 1;

        private void HideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var brush = new SolidBrush(Color.Black);
            var zBuffer = new double[Canvas.Width, Canvas.Height];
            for (int x = 0; x < Canvas.Width; x++)
            {
                for (int y = 0; y < Canvas.Height; y++)
                {
                    zBuffer[x, y] = double.MaxValue;
                }
            }
            Bitmap bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            var rectangles = new List<(Rectangle, Color)>();
            var k = 0;
            foreach (var shape in _shapes)
            {
                var matrix = shape.MatrixToWorld * _viewer.ToCameraCoordinates;
                for (int j = 0; j < shape.Faces.Count; ++j) //В данном случае face - треугольник
                {
                    var face = shape.Faces[j];
                    var result = IsVisible(face, matrix, shape);
                    if (!result.Item4)
                        continue;

                    //k++;
                    //if (k + 1 != _index)
                    //    continue;

                    var points = new List<(PointF, float)>();

                    for (int i = 0; i < face.Count; ++i)
                    {
                        var point = shape.Points[face[i]];
                        points.Add((_viewer.ToScreen(shape, point), point.Z));
                    }
                    var p0 = points[0];
                    var point0 = p0.Item1;
                    var z0 = p0.Item2;

                    var p1 = points[1];
                    var point1 = p1.Item1;
                    var z1 = p1.Item2;

                    var p2 = points[2];
                    var point2 = p2.Item1;
                    var z2 = p2.Item2;


                    var oldPoint0 = new PointF(point0.X, point0.Y);

                    // Переносим треугольник в начала координат;
                    point1 = point1.Difference(point0);
                    point2 = point2.Difference(point0);
                    point0 = new(0, 0);

                    if (point2.Y == 0)
                    {
                        (point1, point2) = (point2, point1);
                        (z1, z2) = (z2, z1);
                        (p1, p2) = (p2, p1);
                    }


                    //Нужны будут для интерполирования по Z координате.
                    float deltaZ0 = z1 - z0;
                    float deltaZ1 = z2 - z0;

                    var values = new PointF[3] { point0, point1, point2 };


                    var minX = values.Min(value => value.X);
                    var maxX = values.Max(value => value.X);

                    var minY = values.Min(value => value.Y);
                    var maxY = values.Max(value => value.Y);


                    var width = Canvas.Width / 2;
                    var height = Canvas.Height / 2;
                    //Debug.WriteLine("АКУАЦУКАУЦ");
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
                                    float z = z0 + (deltaZ0 * w1) + (deltaZ1 * w2);

                                    var newX = width + x + (int)oldPoint0.X;
                                    var newY = height - (y + (int)oldPoint0.Y);
                                    //Debug.WriteLine($"{z}");
                                    if (z < zBuffer[newX, newY])
                                    {
                                        zBuffer[newX, newY] = z;
                                        bitmap.SetPixel(newX, newY, face.Color);
                                    }
                                }
                            }

                        }
                    }

#if DEBUG
                    //Canvas.Image = bitmap;
                    //Canvas.Update();
                    //for (int q = 0; q < values.Count(); ++q)
                    //{
                    //    values[q] = values[q].Difference(new Point(-200, -200));
                    //}
                    //brush.Color = face.Color;
                    //_graphics.FillPolygon(brush, values);
                    //_graphics.DrawRectangle(_pen, new Rectangle((int)minX + 200, (int)minY + 200, Math.Abs((int)(minX - maxX)), Math.Abs((int)(minY - maxY))));
                    //Thread.Sleep(1000);
#endif
                }
#if DEBUG
                //Thread.Sleep(2000);
#endif
            }


            _index++;
            Canvas.Image = bitmap;
            Canvas.Update();


        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            /*
            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _viewer.Graphics = _graphics;
            ViewShapes();
            */
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    _viewer.MoveLeft();
                    break;
            }
            ViewShapes();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _viewer.MoveDown();
            ViewShapes();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            _viewer.MoveLeft();
            ViewShapes();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _viewer.MoveUp();
            ViewShapes();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _viewer.MoveRight();
            ViewShapes();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _viewer.MoveForward();
            ViewShapes();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _viewer.MoveBackward();
            ViewShapes();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _viewer.RotateUp();
            ViewShapes();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            _viewer.RotateDown();
            ViewShapes();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _viewer.RotateLeft();
            ViewShapes();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _viewer.RotateRight();
            ViewShapes();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _shapes.Clear();
            ShapesBox.SelectedItem = null;
            _currentShape = Shapes.Empty();
            ShapesBox.Items.Clear();
            ViewShapes();
        }


    }
}
