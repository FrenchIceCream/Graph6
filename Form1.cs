using Newtonsoft.Json;
using org.mariuszgromada.math.mxparser;
using static Graph6.FunctionShape;

namespace Graph6
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private Viewer _viewer;
        private Shape _shape;
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

            //Указание начала координат в центре окна
            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);

            _viewer = new Viewer(_graphics, Projection.Isometric);
            _shape = Shapes.Empty();
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

        private void ViewShape()
        {
            if (_shape is null)
                return;
            _viewer.View(_shape);
        }

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
            if (_shape.Points.Count == 0)
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
            AffineTransform(mirrorMatrix, _shape);
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            if (_shape.Points.Count == 0)
                return;

            ScaleValue.Text = ScaleValue.Text.Replace('.', ',');
            float k = float.Parse(ScaleValue.Text);

            MyMatrix ScaleMat = new MyMatrix(4, 4, new float[] {k, 0, 0, 0,
                                                                0, k, 0, 0,
                                                                0, 0, k, 0,
                                                                0, 0, 0, 1});
            AffineTransform(ScaleMat, _shape);
            ViewShape();
        }

        private void Button_Rotate_Click(object sender, EventArgs e)
        {
            if (_shape.Points.Count == 0)
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
            AffineTransform(rotationMatrix, _shape);
            ViewShape();
        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {
            if (_shape.Points.Count == 0)
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
            MyMatrix scaleMatrix = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(degree)*(1 - l*l), l*(1 - (float)Math.Cos(degree))*m + n*(float)Math.Sin(degree), l * (1 - (float)Math.Cos(degree)) * n - m * (float)Math.Sin(degree), 0,
            l*(1 - (float)Math.Cos(degree))*m - n*(float)Math.Sin(degree), m*m + (float)Math.Cos(degree) * (1 - m*m), m*(1 - (float)Math.Cos(degree)) * n + l* (float)Math.Sin(degree), 0,
            l * (1 - (float)Math.Cos(degree))*n + m * (float)Math.Sin(degree), m * (1 - (float)Math.Cos(degree)) * n - l*(float)Math.Sin(degree), n * n + (float)Math.Cos(degree) * (1 - n * n), 0,
            0, 0, 0, 1});

            TurnShape(scaleMatrix, ref _shape);

            ViewShape();
        }

        public void TurnShape(MyMatrix mat, ref Shape shape)
        {


            for (int i = 0; i < _shape.Points.Count; i++)
            {
                MyPoint point = _shape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * mat;
                _shape.Points[i] = new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]);
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
            /*for (int i = 0; i < shape.Points.Count; i++)
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
            ViewShape();
        }

        private void PerspectiveButton_Click(object sender, EventArgs e)
        {
            _viewer.SetProjection(Projection.Perspective);
            ViewShape();
        }

        private void CubeButton_Click(object sender, EventArgs e)
        {
            _shape = Shapes.Cube();
            ViewShape();
        }

        private void OctahedronButton_Click(object sender, EventArgs e)
        {
            _shape = Shapes.Octahedron();
            ViewShape();
        }

        private void TetrahedronButton_Click(object sender, EventArgs e)
        {
            _shape = Shapes.Tetrahedron();
            ViewShape();
        }

        //TODO
        private void IcosahedronButton_Click(object sender, EventArgs e)
        {
            //_shape = Shapes.Icosahedron();
            ViewShape();
        }

        //TODO
        private void DodecahedronButton_Click(object sender, EventArgs e)
        {
            //_shape = Shapes.Dodecahedron();
            ViewShape();
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
                    _shape = JsonConvert.DeserializeObject<Shape>(file, new Converter());
                    ViewShape();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_shape == null)
                return;
            string file = JsonConvert.SerializeObject(_shape, Formatting.Indented, new Converter());
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

            Shape s = new Shape(Points, Faces);

            _shape = s;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            ViewShape();
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
                _graphics.DrawRectangle(_pen, p.X, p.Y, 1, 1);
            }
            else
            {
                PointF prev = _solid_of_revolution.Last();
                _graphics.DrawLine(_pen, prev, p);
            }
            _solid_of_revolution.Add(p);
        }

        private void Button_SolidOfRev_Show_Click(object sender, EventArgs e)
        {
            CalculateSolidOfRevolution();
            ViewShape();
        }

        private void CalculateSolidOfRevolution()
        {
            _graphics.Clear(Color.White);
            _drawingState = DrawingState.NODRAWING;
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);

            if (_solid_of_revolution.Count == 0)
                return;

            Shape solid_shape = Shapes.Empty();

            solid_shape.Points.Add(new MyPoint(_solid_of_revolution[0].X - Canvas.Width / 2, (_solid_of_revolution[0].Y - Canvas.Height / 2) * (-1), 0));
            for (int i = 1; i < _solid_of_revolution.Count; i++)
            {
                solid_shape.Points.Add(new MyPoint(_solid_of_revolution[i].X - Canvas.Width / 2, (_solid_of_revolution[i].Y - Canvas.Height / 2) * (-1), 0));
            }
            _shape = new Shape(solid_shape);

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
                _shape.Points.Add(solid_shape.Points[0]);
                for (int j = 1; j < c; j++)
                {
                    _shape.Points.Add(solid_shape.Points[j]);
                    _shape.Faces.Add(new List<int> { (i - 1) * c + j, i * c + j, i * c + j - 1 });  //точка на предыдущей итерации + текующая точка + точка перед текущей
                    _shape.Faces.Add(new List<int> { (i - 1) * c + j, (i - 1) * c + j - 1, i * c + j - 1 }); //точка на предыдущей итерации + точка перед ней + точка перед текущей
                }
            }

            for (int j = 1; j < c; j++)
                _shape.Faces.Add(new List<int> { j, (sections - 1) * c + j - 1, (sections - 1) * c + j });
        }

        //TODO
        private void RemoveEdgesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Shape shape = new Shape(_shape);
            _graphics.Clear(Color.White);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _viewer.Graphics = _graphics;
            ViewShape();
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    _viewer.MoveLeft();
                    break;
            }
            ViewShape();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _viewer.MoveDown();
            ViewShape();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            _viewer.MoveLeft();
            ViewShape();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _viewer.MoveUp();
            ViewShape();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _viewer.MoveRight();
            ViewShape();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _viewer.MoveForward();
            ViewShape();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _viewer.MoveBackward();
            ViewShape();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _viewer.RotateUp();
            ViewShape();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            _viewer.RotateDown();
            ViewShape();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _viewer.RotateLeft();
            ViewShape();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //_viewer.RotateRight();
            _viewer.Rotate2(_shape);
            ViewShape();
        }

        private void Hide_CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
