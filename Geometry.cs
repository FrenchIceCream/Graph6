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

        public static MyPoint operator +(MyPoint lhs, MyPoint rhs)
        {
            return new MyPoint(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static MyPoint operator -(MyPoint lhs, MyPoint rhs)
        {
            return new MyPoint(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
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
        public List<MyPoint> points = new();

        public List<(int, int)> edges = new();

        public Shape(List<MyPoint> point, List<(int, int)> edges)
        {
            this.points = point;
            this.edges = edges;
        }

       public MyPoint GetCenter()
        {
            float x = 0;
            float y = 0;
            float z = 0;
            foreach (var point in points)
            {
                x += point.X;
                y += point.Y;
                z += point.Z;
            }

            return new MyPoint(x / points.Count, y / points.Count, z / points.Count);
        }
    }

    public static class Shapes
    {
        public static Shape Cube()
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
            return s;
        }
        public static Shape Empty()
        {
            return new Shape(new List<MyPoint>(), new List<(int, int)>());
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
