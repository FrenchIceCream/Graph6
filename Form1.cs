using Newtonsoft.Json;
using System.Windows.Forms;

namespace Graph6
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private Viewer _viewer;
        private Shape _shape;
        public Form1()
        {
            InitializeComponent();
            AxesList.Items.Add("X");
            AxesList.Items.Add("Y");
            AxesList.Items.Add("Z");
            AxesList.SelectedIndex = 0;

            AxesList_Rt.Items.Add("X");
            AxesList_Rt.Items.Add("Y");
            AxesList_Rt.Items.Add("Z");
            AxesList_Rt.SelectedIndex = 0;

            ScaleValue.Text = "2";
            Angle.Text = "90";
            X1.Text = "10";
            Y1.Text = "10";
            Z1.Text = "10";
            X2.Text = "10";
            Y2.Text = "20";
            Z2.Text = "10";

            //”казание начала координат в центре окна
            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);

            _viewer = new Viewer(_graphics, Projection.Isometric);
            _shape = Shapes.Empty();
        }

        private void ViewShape() =>
            _viewer.View(_shape);

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
            AffineTransform(mirrorMatrix);
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            if (_shape.Points.Count == 0)
                return;

            float k = float.Parse(ScaleValue.Text);

            MyMatrix ScaleMat = new MyMatrix(4, 4, new float[] {k, 0, 0, 0,
                                                                0, k, 0, 0,
                                                                0, 0, k, 0,
                                                                0, 0, 0, 1});
            AffineTransform(ScaleMat);
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
            AffineTransform(rotationMatrix);
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

            //”жас, но работает
            MyMatrix scaleMatrix = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(degree)*(1 - l*l), l*(1 - (float)Math.Cos(degree))*m + n*(float)Math.Sin(degree), l * (1 - (float)Math.Cos(degree)) * n - m * (float)Math.Sin(degree), 0,
            l*(1 - (float)Math.Cos(degree))*m - n*(float)Math.Sin(degree), m*m + (float)Math.Cos(degree) * (1 - m*m), m*(1 - (float)Math.Cos(degree)) * n + l* (float)Math.Sin(degree), 0,
            l * (1 - (float)Math.Cos(degree))*n + m * (float)Math.Sin(degree), m * (1 - (float)Math.Cos(degree)) * n - l*(float)Math.Sin(degree), n * n + (float)Math.Cos(degree) * (1 - n * n), 0,
            0, 0, 0, 1});
            AffineTransform(scaleMatrix);
            ViewShape();
        }

        private void AffineTransform(MyMatrix mat)
        {
            var center = _shape.GetCenter();

            MyMatrix toCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                 0, 1, 0, 0,
                                                                 0, 0, 1, 0,
                                                                 -center.X, -center.Y, -center.Z, 1});

            MyMatrix fromCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                   0, 1, 0, 0,
                                                                   0, 0, 1, 0,
                                                                   center.X, center.Y, center.Z, 1});

            MyMatrix transformMatrix = toCenter * mat * fromCenter;

            for (int i = 0; i < _shape.Points.Count; i++)
            {
                MyPoint point = _shape.Points[i];
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * transformMatrix;
                _shape.Points[i] = new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]);
            }
            ViewShape();
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
    }
}
