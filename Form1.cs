using Newtonsoft.Json;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;
using System.Drawing;
using System.Configuration;
using System.Diagnostics;

namespace Graph6
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private Viewer _viewer;
        private IList<Shape> _shapes = new List<Shape>();
        private Shape _currentShape;
        private List<PointF> _solid_of_revolution;
        private DrawingState _drawingState;
        private Pen _pen;
        public Form1()
        {
            InitializeComponent();
            InitListsAndBoxes();

            _drawingState = DrawingState.NODRAWING;
            _pen = new Pen(Color.Blue, 2);
            _solid_of_revolution = new List<PointF> { };

            //�������� ������ ��������� � ������ ����
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

        private void ViewShapes() => _viewer.View(_shapes);

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
            if (_currentShape.Points.Count == 0)
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
        }


        private void Button_Translation_Click(object sender, EventArgs e)
        {
            if (_currentShape.Points.Count == 0)
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

        private void Button_Scale_Click(object sender, EventArgs e)
        {


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
            if (_currentShape.Points.Count == 0)
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
            if (_currentShape.Points.Count == 0)
                return;

            float degree = float.Parse(Angle.Text) * (float)(Math.PI / 180);

            MyPoint A = new MyPoint(float.Parse(X1.Text), float.Parse(Y1.Text), float.Parse(Z1.Text));
            MyPoint B = new MyPoint(float.Parse(X2.Text), float.Parse(Y2.Text), float.Parse(Z2.Text));

            MyPoint vector = B - A;
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            float l = vector.X / length;
            float m = vector.Y / length;
            float n = vector.Z / length;

            //����, �� ��������
            MyMatrix scaleMatrix = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(degree)*(1 - l*l), l*(1 - (float)Math.Cos(degree))*m + n*(float)Math.Sin(degree), l * (1 - (float)Math.Cos(degree)) * n - m * (float)Math.Sin(degree), 0,
            l*(1 - (float)Math.Cos(degree))*m - n*(float)Math.Sin(degree), m*m + (float)Math.Cos(degree) * (1 - m*m), m*(1 - (float)Math.Cos(degree)) * n + l* (float)Math.Sin(degree), 0,
            l * (1 - (float)Math.Cos(degree))*n + m * (float)Math.Sin(degree), m * (1 - (float)Math.Cos(degree)) * n - l*(float)Math.Sin(degree), n * n + (float)Math.Cos(degree) * (1 - n * n), 0,
            0, 0, 0, 1});

            TurnShape(scaleMatrix);

            ViewShapes();
        }

        private void TurnShape(MyMatrix matrix)
        {
            for (int i = 0; i < _currentShape.Points.Count; i++)
            {
                MyPoint point = _currentShape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * matrix;
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


        private void SelectShape(Shape shape)
        {
            ShapesBox.Items.Add(shape);
            _shapes.Add(shape);
            ShapesBox.SelectedIndex = ShapesBox.Items.Count - 1;
            SelectItem();
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

        private void IcosahedronButton_Click(object sender, EventArgs e)
        {
            SelectShape(Shapes.Icosahedron());
            ViewShapes();
        }

        private void DodecahedronButton_Click(object sender, EventArgs e)
        {
            SelectShape(Shapes.Dodecahedron());
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
            if (_currentShape == null)
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

        private void CalculateButton_Click(object sender, EventArgs e) => CalculateShapeByFunction();

        private void CalculateShapeByFunction()
        {
            if (!double.TryParse(X0TextBox.Text, out var X0))
            {
                return;
            }
            if (!double.TryParse(X1TextBox.Text, out var X1))
            {
                return;
            }
            if (!double.TryParse(Y0TextBox.Text, out var Y0))
            {
                return;
            }
            if (!double.TryParse(Y1TextBox.Text, out var Y1))
            {
                return;
            }
            if (!double.TryParse(XDeltaTextBox.Text, out var Xdelta))
            {
                return;
            }
            if (!double.TryParse(YDeltaTextBox.Text, out var Ydelta))
            {
                return;
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string formul = formulTextBox.Text;

            Function f = new Function($"f(x,y)={formul}");

            List<MyPoint> Points = new();

            List<(int, int)> Edges = new();

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
                        Edges.Add((Points.Count - 1, Points.Count - 2));
                    }
                    if (i != 0)
                    {
                        Edges.Add((Points.Count - 1, Points.Count - 2 - itemsInRow));
                    }
                }

            }

            SelectShape(new FunctionShape(Points, Edges));
            _shapes.Add(_currentShape);
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
            _solid_of_revolution.Clear();
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
            if (_solid_of_revolution.Count() == 0)
            {
                //   p.X = Canvas.Width / 2;
                _graphics.DrawRectangle(_pen, p.X, p.Y, 1, 1);
            }
            else
            {
                //��������������� ����� - ��� ������������� �������������
                /*
                if (p.X < Canvas.Width / 2)
                    p.X = Canvas.Width / 2;
                */
                PointF prev = _solid_of_revolution.Last();
                _graphics.DrawLine(_pen, prev, p);
            }
            _solid_of_revolution.Add(p);
        }

        private void Button_SolidOfRev_Show_Click(object sender, EventArgs e)
        {
            ShowSolidOfRevolution();
        }
        private void ShowSolidOfRevolution()
        {
            _graphics.Clear(Color.White);
            _drawingState = DrawingState.NODRAWING;
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);

            if (_solid_of_revolution.Count == 0)
                return;

            Shape solid_shape = Shapes.Empty();

            /*
            if (_solid_of_revolution.Last().X != Canvas.Width / 2)
                _solid_of_revolution.Add(new PointF(Canvas.Width / 2, _solid_of_revolution.Last().Y));
            */
            solid_shape.Points.Add(new MyPoint(_solid_of_revolution[0].X - Canvas.Width / 2, (_solid_of_revolution[0].Y - Canvas.Height / 2) * (-1), 0));
            for (int i = 1; i < _solid_of_revolution.Count; i++)
            {
                solid_shape.Points.Add(new MyPoint(_solid_of_revolution[i].X - Canvas.Width / 2, (_solid_of_revolution[i].Y - Canvas.Height / 2) * (-1), 0));
                solid_shape.Edges.Add((i - 1, i));
            }
            _currentShape = new Shape(solid_shape);

            //���� ��������
            int sections = int.Parse(NumOfSections.Text);
            float deg = (360 / sections) * (float)(Math.PI / 180);

            MyPoint A = new MyPoint(0, 0, 0);  //������ X
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
                TurnShape(scaleMatrix);
                for (int j = 0; j < c; j++)
                {
                    _currentShape.Points.Add(solid_shape.Points[j]);
                    _currentShape.Edges.Add(((i - 1) * c + j, i * c + j));
                }

                for (int j = 1; j < c; j++)
                    _currentShape.Edges.Add((i * c + j - 1, i * c + j));
            }

            for (int j = 0; j < c; j++)
                _currentShape.Edges.Add((j, (sections - 1) * c + j));

            ViewShapes();
        }

        private void Clear_Button_Click(object sender, EventArgs e)
        {
            _shapes.Clear();
            ShapesBox.SelectedItem = null;
            _currentShape = Shapes.Empty();
            ShapesBox.Items.Clear();
            ViewShapes();
        }

        private void SelectItem()
        {
            if (_shapes.Count > 0)
            {
                _currentShape = _shapes[ShapesBox.SelectedIndex];
            }
        }

        private void ShapesBox_SelectedIndexChanged(object sender, EventArgs e) => SelectItem();

        private void HideEdges_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!Hide_CheckBox.Checked)
            {

            }
        }
    }
}
