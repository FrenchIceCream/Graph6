using System.Windows.Forms;

namespace Graph6
{
    public partial class Form1 : Form
    {

        private IList<Face> _faces = new List<Face>();
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

            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);


            _viewer = new Viewer(_graphics, Projection.Isometric);
            _shape = Shapes.Empty();
        }

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
            if (_shape.points.Count == 0)
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

            MyMatrix MirrorMat = new MyMatrix(4, 4, new float[]
            {kx, 0, 0, 0,
            0, ky, 0, 0,
            0, 0, kz, 0,
            0, 0, 0, 1});
            AffineTransform(MirrorMat);
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            if (_shape.points.Count == 0)
                return;

            float k = float.Parse(ScaleValue.Text);

            MyMatrix ScaleMat = new MyMatrix(4, 4, new float[]
            {k, 0, 0, 0,
            0, k, 0, 0,
            0, 0, k, 0,
            0, 0, 0, 1});
            AffineTransform(ScaleMat);
            _viewer.View(_shape);
        }

        private void Button_Rotate_Click(object sender, EventArgs e)
        {
            if (_shape.points.Count == 0)
                return;

            float deg = float.Parse(Angle.Text) * (float)(Math.PI / 180);

            MyMatrix RotationMat = new MyMatrix(4, 4, new float[]
            {1, 0, 0, 0,
            0, (float)Math.Cos(deg), (float)Math.Sin(deg), 0,
            0, -(float)Math.Sin(deg), (float)Math.Cos(deg), 0,
            0, 0, 0, 1});

            switch (AxesList.Text)
            {
                case "X":
                    break;
                case "Y":
                    RotationMat = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(deg), 0, -(float)Math.Sin(deg), 0,
                        0, 1, 0, 0,
                        (float)Math.Sin(deg), 0, (float)Math.Cos(deg), 0,
                        0, 0, 0, 1});
                    break;
                case "Z":
                    RotationMat = new MyMatrix(4, 4, new float[]
                        {(float)Math.Cos(deg), (float)Math.Sin(deg), 0, 0,
                        -(float)Math.Sin(deg), (float)Math.Cos(deg), 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1});
                    break;
            }
            AffineTransform(RotationMat);
            _viewer.View(_shape);
        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {
            if (_shape.points.Count == 0)
                return;

            float deg = float.Parse(Angle.Text) * (float)(Math.PI / 180);

            MyPoint A = new MyPoint(float.Parse(X1.Text), float.Parse(Y1.Text), float.Parse(Z1.Text));
            MyPoint B = new MyPoint(float.Parse(X2.Text), float.Parse(Y2.Text), float.Parse(Z2.Text));

            MyPoint vec = B - A;
            float length = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
            float l = vec.X / length;
            float m = vec.Y / length;
            float n = vec.Z / length;

            MyMatrix ScaleMat = new MyMatrix(4, 4, new float[]
            {l*l + (float)Math.Cos(deg)*(1 - l*l), l*(1 - (float)Math.Cos(deg))*m + n*(float)Math.Sin(deg), l * (1 - (float)Math.Cos(deg)) * n - m * (float)Math.Sin(deg), 0,
            l*(1 - (float)Math.Cos(deg))*m - n*(float)Math.Sin(deg), m*m + (float)Math.Cos(deg) * (1 - m*m), m*(1 - (float)Math.Cos(deg)) * n + l* (float)Math.Sin(deg), 0,
            l * (1 - (float)Math.Cos(deg))*n + m * (float)Math.Sin(deg), m * (1 - (float)Math.Cos(deg)) * n - l*(float)Math.Sin(deg), n * n + (float)Math.Cos(deg) * (1 - n * n), 0,
            0, 0, 0, 1});
            AffineTransform(ScaleMat);
            _viewer.View(_shape);
        }

        void AffineTransform(MyMatrix mat)
        {
            List<MyPoint> new_points = new List<MyPoint>();

            var center = _shape.GetCenter();

            MyMatrix ToCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                 0, 1, 0, 0,
                                                                 0, 0, 1, 0,
                                                      -center.X, -center.Y, -center.Z, 1});

            MyMatrix FromCenter = new MyMatrix(4, 4, new float[] { 1, 0, 0, 0,
                                                                 0, 1, 0, 0,
                                                                 0, 0, 1, 0,
                                                      center.X, center.Y, center.Z, 1});

            MyMatrix TransformMatrix = ToCenter * mat * FromCenter;

            foreach (var point in _shape.points)
            {
                MyMatrix point_matrix = new MyMatrix(1, 4, new float[] { point.X, point.Y, point.Z, 1 });
                var res = point_matrix * TransformMatrix;
                new_points.Add(new MyPoint(res.matrix[0, 0], res.matrix[0, 1], res.matrix[0, 2]));
            }
            _shape.points = new_points;
            _viewer.View(_shape);
        }

        private void ParallelButton_Click(object sender, EventArgs e)
        {
            _viewer.SetProjection(Projection.Isometric);
            _viewer.View(_shape);
        }

        private void PerspectiveButton_Click(object sender, EventArgs e)
        {
            _viewer.SetProjection(Projection.Perspective);
            _viewer.View(_shape);
        }

        private void CubeButton_Click(object sender, EventArgs e)
        {
            _shape = Shapes.Cube();
            _viewer.View(_shape);
        }
    }
}
