using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.Pkcs;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
            _viewer = new Viewer(Canvas, Projection.Isometric);
            _graphics = _viewer.Graphics;
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

        private double F(double x, double y)
        {
            return 5 * (Math.Cos(x * x + y * y + 1) / (x * x + y * y + 1) + 0.1);
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

            Xdelta = Math.Abs(X0 - X1) / Canvas.Width;
            Ydelta = Math.Abs(Y0 - Y1) / 20;




            List<List<MyPoint>> allLine = new();
            List<MyPoint> line = new();

            double XT, YT;
            int i, j;
            for (i = 0, XT = X0; i < Canvas.Width; i++, XT += Xdelta)
            {
                line = new();
                for (j = 0, YT = Y0; j < 20; j++, YT += Ydelta)
                {

                    line.Add(new MyPoint((float)XT, (float)YT, (float)F(XT,YT)));
                }
                allLine .Add(line);

            }

            _shapes.Clear();
            _graphics.Clear(Color.White);
            _viewer.Wave(allLine, Canvas.Width,X0, Math.Abs(X0 - X1),Y0, Math.Abs(Y0 - Y1),Canvas.Height);
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

            var mat = GetSolidOfRevMatrix(deg);

            int c = solid_shape.Points.Count();

            for (int i = 0; i < sections; i++)
            {
                TurnShape(mat, ref solid_shape);

                for (int j = 0; j < c; j++)
                    _currentShape.Points.Add(solid_shape.Points[j]);
            }

            for (int i = 0; i < sections; i++)
                for (int j = 0; j < c - 1; j++)
                {
                    _currentShape.Faces.Add(new Face(new List<int> { i * c + j, (i + 1) % (sections) * c + j, (i + 1) % (sections) * c + j + 1 }));
                    _currentShape.Faces.Add(new Face(new List<int> { i * c + j, (i + 1) % (sections) * c + j + 1, i * c + j + 1 }));
                }
        }


        private MyMatrix GetSolidOfRevMatrix(float deg)
        {
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
            return mat;
        }


        private void RemoveEdgesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RemoveEdgesCheckBox.Checked && _viewer.RenderMode == RenderMode.Sceleton)
            {
                _viewer.RenderMode = RenderMode.Trimming;
            }
            else
            {
                Hide_CheckBox.Checked = false;
                TexturingCheckBox.Checked = false;
                _viewer.RenderMode = RenderMode.Sceleton;
            }
            ViewShapes();
        }

        private void HideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Hide_CheckBox.Checked && _viewer.RenderMode == RenderMode.Sceleton)
            {
                _viewer.RenderMode = RenderMode.Buffer;
            }
            else
            {
                RemoveEdgesCheckBox.Checked = false;
                TexturingCheckBox.Checked = false;
                _viewer.RenderMode = RenderMode.Sceleton;
            }
            ViewShapes();
        }


        private void TexturingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TexturingCheckBox.Checked && _viewer.RenderMode == RenderMode.Sceleton)
            {
                _viewer.RenderMode = RenderMode.Texturing;
            }
            else
            {
                RemoveEdgesCheckBox.Checked = false;
                Hide_CheckBox.Checked = false;
                _viewer.RenderMode = RenderMode.Sceleton;
            }
            ViewShapes();
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

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                try
                {
                    _currentShape.Texture = new Bitmap(imagePath);
                    ViewShapes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }
    }
}
