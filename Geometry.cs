namespace Graph6
{
    

    public class MyPoint
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;
        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        public MyPoint(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }

    //public class Edge
    //{
    //    private readonly Point _start;
    //    private readonly Point _end;

    //    public Point Start => _start;
    //    public Point End => _end;

    //    public Edge(Point start, Point end)
    //    {
    //        _start = start;
    //        _end = end;
    //    }
    //}


    public class Face
    {
        private readonly List<MyPoint> _points = new();
        public IReadOnlyList<MyPoint> MyPoint => _points;


        public Face(IEnumerable<MyPoint> points)
        {
            _points.AddRange(points);
        }
    }

    public class Shape
    {
        public readonly List<MyPoint> points = new();

        public readonly List<(int,int)> edges = new();

        public Shape(List<MyPoint> point, List<(int, int)> edges)
        {
            this.points = point;
            this.edges = edges;
        }
    }

    //"Форма" для сохранения и загрузки в файд
    public class ShapeSaver
    {
        public readonly List<MyPoint> _points = new();

        public readonly Dictionary<int, List<int>> _faces = new Dictionary<int, List<int>>();

        public ShapeSaver(List<MyPoint> point, Dictionary<int, List<int>> faces)
        {
            _points = point;
            _faces = faces;
        }
    }
}
