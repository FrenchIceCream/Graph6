namespace Graph6
{
    public enum Perspective
    {
        Isometric,
        Axonometry,
    }

    public class Point
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;
        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        public Point(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }


    public class Edge
    {
        private readonly Point _start;
        private readonly Point _end;

        public Point Start => _start;
        public Point End => _end;

        public Edge(Point start, Point end)
        {
            _start = start;
            _end = end;
        }
    }


    public class Face
    {
        private readonly List<Edge> _edges = new();
        public IReadOnlyList<Edge> Edges => _edges;


        public Face(IEnumerable<Edge> edges)
        {
            _edges.AddRange(edges);
        }
    }

    public class Shape
    {
        private readonly List<Face> _faces = new();
        public IReadOnlyList<Face> Faces => _faces;

        public Shape(IEnumerable<Face> faces)
        {
            _faces.AddRange(faces);
        }
    }

}
