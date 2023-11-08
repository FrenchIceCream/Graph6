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

    public class Shape
    {
        public List<MyPoint> Points { get; private set; } = new();

        public List<(int, int)> Edges { get; private set; } = new();

        public Shape(List<MyPoint> point, List<(int, int)> edges)
        {
            Points = point;
            Edges = edges;
        }

        public MyPoint GetCenter()
        {
            float x = 0;
            float y = 0;
            float z = 0;
            foreach (var point in Points)
            {
                x += point.X;
                y += point.Y;
                z += point.Z;
            }

            return new MyPoint(x / Points.Count, y / Points.Count, z / Points.Count);
        }
    }

    public static class Shapes
    {
        public static Shape Cube()
        {
            List<MyPoint> points = new List<MyPoint>
            {
                new MyPoint(-20, -20, 20),
                new MyPoint(20, -20, 20),
                new MyPoint(-20, 20, 20),
                new MyPoint(-20, -20, 60),
                new MyPoint(20, 20, 20),
                new MyPoint(-20, 20, 60),
                new MyPoint(20, -20, 60),
                new MyPoint(20, 20, 60)
            };
            List<(int, int)> edges = new()
            {
                (0, 1),
                (0, 2),
                (0, 3),
                (7, 4),
                (7, 5),
                (7, 6),
                (4, 1),
                (4, 2),
                (5, 3),
                (5, 2),
                (6, 1),
                (6, 3)
            };

            return new(points, edges);
        }
        public static Shape Tetrahedron()
        {
            float h = (float)Math.Sqrt(3) * 50;
            List<MyPoint> points = new()
            {
                new MyPoint(-50, -h / 3, 20),
                new MyPoint(50, -h / 3, 20),
                new MyPoint(0, 2 * h / 3, 20),
                new MyPoint(0, 0, 25 * (float)Math.Sqrt(13)),
            };
            List<(int, int)> edges = new()
            {
                (0, 1),  // Ребро AB
                (0, 2),  // Ребро AC
                (0, 3),  // Ребро AD
                (1, 2),  // Ребро BC
                (1, 3),  // Ребро BD
                (2, 3)   // Ребро CD
            };
            return new(points, edges);
        }


        public static Shape Octahedron()
        {
            List<MyPoint> points = new List<MyPoint>
            {
                new MyPoint(0, 0, 30),   
                new MyPoint(-30, 0, 0),
                new MyPoint(0, -30, 0),
                new MyPoint(30, 0, 0),
                new MyPoint(0, 30, 0),
                new MyPoint(0, 0, -30)
            };

            List<(int, int)> edges = new List<(int, int)>
            {
                (0, 1), 
                (0, 2), 
                (0, 3), 
                (0, 4),
                (1, 2), 
                (2, 3),
                (3, 4),
                (4, 1),
                (1, 5), 
                (2, 5),
                (3, 5),  
                (4, 5) 
            };
            return new(points, edges);
        }

        public static Shape Empty()
        {
            return new Shape(new List<MyPoint>(), new List<(int, int)>());
        }
    }

    //"Форма" для сохранения и загрузки в файл
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
