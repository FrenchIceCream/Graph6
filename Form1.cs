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

            _graphics = Canvas.CreateGraphics();
            _graphics.TranslateTransform(Canvas.Width / 2, Canvas.Height / 2);
            _graphics.ScaleTransform(1, -1);


            _viewer = new Viewer(_graphics, Projection.Isometric);
            _shape = Shapes.Empty();

        }

        private void Button_Mirror_Click(object sender, EventArgs e)
        {
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {

        }

        private void Button_Rotate_Click(object sender, EventArgs e)
        {

        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {

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
