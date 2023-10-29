namespace Graph6
{
    public enum Projection
    {
        Perspective,
        Isometric,
        Axonometry,
    }

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


    //"Форма" для сохранения и загрузки в файд
    public class Shape
    {
        private readonly List<MyPoint> _points = new();

        private readonly Dictionary<int, List<int>> _faces = new Dictionary<int, List<int>>();

        public Shape(List<MyPoint> point, Dictionary<int, List<int>> faces)
        {
            _points = point;
            _faces = faces;
        }
    }
}
