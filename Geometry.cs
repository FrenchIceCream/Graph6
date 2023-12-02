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
        public string Id { get; private set; }

        public List<MyPoint> Points { get; private set; } = new();

        public List<(int, int)> Edges { get; private set; } = new();

        public Shape(List<MyPoint> point, List<(int, int)> edges)
        {
            Id = GenerateUniqueId();
            Points = point;
            Edges = edges;
        }

        public Shape(Shape anotherShape)
        {
            Id = anotherShape.Id;
            Points = new List<MyPoint>(anotherShape.Points);
            Edges = new List<(int, int)>(anotherShape.Edges);
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

        private string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 4);
        }

        public override string ToString()
        {
            return $"Shape: {Id}";
        }
    }
    public class Cube: Shape
    {
        public Cube(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Cube: {Id}";
        }
    }
    public class Icosahedron : Shape
    {
        public Icosahedron(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Icosahedron: {Id}";
        }
    }
    public class Dodecahedron : Shape
    {
        public Dodecahedron(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Dodecahedron: {Id}";
        }
    }
    public class Tetrahedron : Shape
    {
        public Tetrahedron(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Tetrahedron: {Id}";
        }
    }
    public class Octahedron : Shape
    {
        public Octahedron(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Octahedron: {Id}";
        }
    }
    public class FunctionShape : Shape
    {
        public FunctionShape(List<MyPoint> point, List<(int, int)> edges) : base(point, edges)
        {
        }

        public override string ToString()
        {
            return $"Function: {Id}";
        }
    }

    public static class Shapes
    {
        public static Cube Cube()
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

        public static Icosahedron Icosahedron()
        {
            float phi = 1.6180f;
            float coef = (float)Math.Sqrt(1 + phi * phi);

            List<MyPoint> points = new()
            {
                new MyPoint(phi * 50 / coef, 1 * 50 / coef, 0),
                new MyPoint(phi * 50 / coef, -1 * 50 / coef, 0),
                new MyPoint(-phi * 50 / coef, -1 * 50 / coef, 0),
                new MyPoint(-phi * 50 / coef, 1 * 50 / coef, 0),
                new MyPoint(0, phi * 50 / coef, 1 * 50 / coef),
                new MyPoint(0, -phi * 50 / coef, 1 * 50 / coef),
                new MyPoint(0, -phi * 50 / coef, -1 * 50 / coef),
                new MyPoint(0, phi * 50 / coef, -1 * 50 / coef),
                new MyPoint(1 * 50 / coef, 0, phi * 50 / coef),
                new MyPoint(1 * 50 / coef, 0, -phi * 50 / coef),
                new MyPoint(-1 * 50 / coef, 0, -phi * 50 / coef),
                new MyPoint(-1 * 50 / coef, 0, phi * 50 / coef),
            };

            List<(int, int)> edges = new()
            {
            (7, 0),
            (7, 4),
            (7, 3),
            (7, 10),
            (7, 9),
            (5, 8),
            (5, 11),
            (5, 2),
            (5, 6),
            (5, 1),
            (0, 8),
            (0, 4),
            (8, 4),
            (8, 11),
            (4, 11),
            (4, 3),
            (11, 3),
            (11, 2),
            (3, 2),
            (3, 10),
            (2, 10),
            (2, 6),
            (10, 6),
            (10, 9),
            (6, 9),
            (6, 1),
            (9, 1),
            (9, 0),
            (1, 0),
            (1, 8),
            };
            return new(points, edges);
        }

        public static Dodecahedron Dodecahedron()
        {
            float phi = 1.6180f;
            List<MyPoint> points = new()
            {
                new MyPoint(35, 35, 35),
                new MyPoint(35, 35, -35),
                new MyPoint(35, -35, 35),
                new MyPoint(35, -35, -35),
                new MyPoint(-35, 35, 35),
                new MyPoint(-35, 35, -35),
                new MyPoint(-35, -35, 35),
                new MyPoint(-35, -35, -35),
                new MyPoint(0, 1 * 35 / phi, phi * 35),
                new MyPoint(0, 1 * 35 / phi, -phi * 35),
                new MyPoint(0, -1 * 35 / phi, phi * 35),
                new MyPoint(0, -1 * 35 / phi, -phi * 35),
                new MyPoint(1 * 35 / phi, phi * 35, 0),
                new MyPoint(1 * 35 / phi, -phi * 35, 0),
                new MyPoint(-1 * 35 / phi, phi * 35, 0),
                new MyPoint(-1 * 35 / phi, -phi * 35, 0),
                new MyPoint(phi * 35, 0, 1 * 35 / phi),
                new MyPoint(phi * 35, 0, -1 * 35 / phi),
                new MyPoint(-phi * 35, 0, 1 * 35 / phi),
                new MyPoint(-phi * 35, 0, -1 * 35 / phi),
            };

            List<(int, int)> edges = new()
            {
                (8,10),
                (8,0),
                (8,4),
                (10,2),
                (10,6),
                (9,11),
                (9,1),
                (9,5),
                (11,3),
                (11,7),
                (12,14),
                (13,15),
                (12,0),
                (12,1),
                (14,4),
                (14,5),
                (13,2),
                (13,3),
                (15,6),
                (15,7),
                (16,17),
                (18,19),
                (16,0),
                (16,2),
                (17,1),
                (17,3),
                (18,4),
                (18,6),
                (19,5),
                (19,7),
            };
            return new(points, edges);
        }

        public static Tetrahedron Tetrahedron()
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

        public static Octahedron Octahedron()
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
}
