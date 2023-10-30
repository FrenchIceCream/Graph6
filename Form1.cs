using System.Windows.Forms;

namespace Graph6
{
    public partial class Form1 : Form
    {

        private IList<Face> _faces = new List<Face>();
        private Graphics _graphics;

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

            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);



            _graphics.Flush();

        }

        private void DrawCubePerspective()
        {
            List<MyPoint> points = new List<MyPoint>();

            points.Add(new MyPoint(-20, -20, 20));

            points.Add(new MyPoint(20, -20, 20));
            points.Add(new MyPoint(-20, 20, 20));
            points.Add(new MyPoint(-20, -20, 60));

            points.Add(new MyPoint(20, 20, 20));
            points.Add(new MyPoint(-20, 20, 60));
            points.Add(new MyPoint(20, -20, 60));

            points.Add(new MyPoint(20, 20, 60));
            List<(int, int)> edges = new();
            edges.Add((0,1));
            edges.Add((0,2));
            edges.Add((0,3));
            edges.Add((7,4));
            edges.Add((7,5));
            edges.Add((7,6));
            edges.Add((4,1));
            edges.Add((4,2));
            edges.Add((5,3));
            edges.Add((5,2));
            edges.Add((6,1));
            edges.Add((6,3));

            Shape s = new Shape(points, edges);


            Viewer v = new(_graphics, Projection.Perspective);

            v.View(s);
        }

        private void DrawCubeParallel()
        {
            List<MyPoint> points = new List<MyPoint>();

            points.Add(new MyPoint(-20, -20, 20));

            points.Add(new MyPoint(20, -20, 20));
            points.Add(new MyPoint(-20, 20, 20));
            points.Add(new MyPoint(-20, -20, 60));

            points.Add(new MyPoint(20, 20, 20));
            points.Add(new MyPoint(-20, 20, 60));
            points.Add(new MyPoint(20, -20, 60));

            points.Add(new MyPoint(20, 20, 60));
            List<(int, int)> edges = new();
            edges.Add((0, 1));
            edges.Add((0, 2));
            edges.Add((0, 3));
            edges.Add((7, 4));
            edges.Add((7, 5));
            edges.Add((7, 6));
            edges.Add((4, 1));
            edges.Add((4, 2));
            edges.Add((5, 3));
            edges.Add((5, 2));
            edges.Add((6, 1));
            edges.Add((6, 3));

            Shape s = new Shape(points, edges);


            Viewer v = new(_graphics, Projection.Isometric);

            v.View(s);
        }

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
            //DrawCubeParallel();
            DrawCubePerspective();
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            DrawCubeParallel();

        }

        private void Button_Rotate_Click(object sender, EventArgs e)
        {

        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {

        }
    }


}
