using Newtonsoft.Json;
using org.mariuszgromada.math.mxparser;
using System;
using System.Diagnostics;
using System.Numerics;

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

            //shape.MatrixToWorld = shape.MatrixToWorld * transformMatrix;
            
            for (int i = 0; i < shape.Points.Count; i++)
            {
                MyPoint point = shape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * transformMatrix;
                shape.Points[i] = new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]);
            }
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

            List<List<int>> Faces = new();

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
                        Faces.Add(new List<int> { Points.Count - 1, Points.Count - 2 });
                    }
                    if (i != 0)
                    {
                        Faces.Add(new List<int> { Points.Count - 1, Points.Count - 2 - itemsInRow });
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
            {
                solid_shape.Points.Add(new MyPoint(_solidOfRevolution[i].X - Canvas.Width / 2, (_solidOfRevolution[i].Y - Canvas.Height / 2) * (-1), 0));
            }
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

            MyMatrix scaleMatrix = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(deg)*(1 - l*l), l*(1 - (float)Math.Cos(deg))*m + n*(float)Math.Sin(deg), l * (1 - (float)Math.Cos(deg)) * n - m * (float)Math.Sin(deg), 0,
            l*(1 - (float)Math.Cos(deg))*m - n*(float)Math.Sin(deg), m*m + (float)Math.Cos(deg) * (1 - m*m), m*(1 - (float)Math.Cos(deg)) * n + l* (float)Math.Sin(deg), 0,
            l * (1 - (float)Math.Cos(deg))*n + m * (float)Math.Sin(deg), m * (1 - (float)Math.Cos(deg)) * n - l*(float)Math.Sin(deg), n * n + (float)Math.Cos(deg) * (1 - n * n), 0,
            0, 0, 0, 1});

            int c = solid_shape.Points.Count();

            for (int i = 1; i < sections; i++)
            {
                TurnShape(scaleMatrix, ref solid_shape);
                _currentShape.Points.Add(solid_shape.Points[0]);
                for (int j = 1; j < c; j++)
                {
                    _currentShape.Points.Add(solid_shape.Points[j]);
                    _currentShape.Faces.Add(new List<int> { (i - 1) * c + j, i * c + j, i * c + j - 1 });  //точка на предыдущей итерации + текующая точка + точка перед текущей
                    _currentShape.Faces.Add(new List<int> { (i - 1) * c + j, i * c + j - 1, (i - 1) * c + j - 1 }); //точка на предыдущей итерации + точка перед текущей + точка перед ней 
                }
            }

            for (int j = 1; j < c; j++)
                _currentShape.Faces.Add(new List<int> { j, (sections - 1) * c + j - 1, (sections - 1) * c + j });
        }

        //TODO
        private void RemoveEdgesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Shape shape = new Shape(_currentShape);
            _graphics.Clear(Color.White);

            var mat = _currentShape.MatrixToWorld * _viewer.ToCameraCoordinates;

            foreach (var face in shape.Faces)
            {
                var pp1 = new MyMatrix(1, 4, new float[] { shape.Points[face[0]].X, shape.Points[face[0]].Y, shape.Points[face[0]].Z, 1 }) * mat;
                var pp2 = new MyMatrix(1, 4, new float[] { shape.Points[face[1]].X, shape.Points[face[1]].Y, shape.Points[face[1]].Z, 1 }) * mat;
                var pp3 = new MyMatrix(1, 4, new float[] { shape.Points[face[2]].X, shape.Points[face[2]].Y, shape.Points[face[2]].Z, 1 }) * mat;

                var p1 = new MyPoint(pp1[0,0], pp1[0, 1], pp1[0, 2]);
                var p2 = new MyPoint(pp2[0, 0], pp2[0, 1], pp2[0, 2]);
                var p3 = new MyPoint(pp3[0, 0], pp3[0, 1], pp3[0, 2]);

                //since encoding acts weirdly when I commit to github, I'll leave a comment in English
                //got it from some article on calculating a surface normal - it works given that polygon is a triangle
                float nx = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p2.Z - p1.Z) * (p3.Y - p1.Y);
                float ny = (p2.Z - p1.Z) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Z - p1.Z);
                float nz = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

                var center = shape.GetCenter();
                Vector3 vec = new Vector3(_viewer.Position.X - center.X, _viewer.Position.Y - center.Y, _viewer.Position.Z - center.Z);
                Vector3 normal = new Vector3(nx, ny, nz);
                Vector3 normal_check = new Vector3(center.X, center.Y, center.Z);


                //if ((normal - normal_check).Length() < (- normal + normal_check).Length())
                //{
                //    normal = -normal;
                //}

                var cross = Vector3.Cross(vec, normal);
                var dot = Vector3.Dot(vec, normal);

                var angle = Math.PI - Math.Atan2(cross.Length(), dot);
                angle = angle * 360 / (2 * Math.PI);
                //Debug.WriteLine((float)angle);

                var pen = new Pen(Color.Red);
                if (angle < 90)
                {
                    for (int i = 1; i < face.Count; ++i)
                        _graphics.DrawLine(pen, new Point((int)shape.Points[face[i - 1]].X, (int)shape.Points[face[i - 1]].Y), new Point((int)shape.Points[face[i]].X, (int)shape.Points[face[i]].Y));

                    _graphics.DrawLine(pen, new Point((int)shape.Points[face[face.Count - 1]].X, (int)shape.Points[face[face.Count - 1]].Y), new Point((int)shape.Points[face[0]].X, (int)shape.Points[face[0]].Y));
                }
            }
        }

        private void HideCheckBox_CheckedChanged(object sender, EventArgs e)
        {

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
            //_viewer.RotateRight();
            _viewer.Rotate2(_currentShape);
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
