using System;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Graph6
{
    public class MyPoint
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;
        private readonly float _u;
        private readonly float _v;

        public Vector3 normal;
        public float intensity;

        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        public float U => _u;
        public float V => _v;


        public MyPoint()
        {

        }

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

        public static bool operator ==(MyPoint lhs, MyPoint rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        public static bool operator !=(MyPoint lhs, MyPoint rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

        public MyPoint Normalize()
        {
            float sum = _x + _y + _z;
            return new MyPoint(_x / sum, _y / sum, _z / sum);
        }

        public static MyPoint Zero()
        {
            return new MyPoint(0, 0, 0);
        }

        public override string ToString()
        {
            return $"<yPoint: {this.X}; {this.Y}; {this.Z}";
        }
    }


    public class Textel
    {
        private readonly float _u;
        private readonly float _v;

        public float U => _u;
        public float V => _v;

        public Textel(float u, float v)
        {
            _u = u;
            _v = v;
        }
    }



    public class Face
    {
        public List<int> _indexes = new();
        private readonly Color _color;
        private readonly List<Textel> _textels = new();
        public int this[int index] => _indexes[index];
        public Textel Textel(int index) => _textels[index];
        public Color Color => _color;
        public int Count => _indexes.Count;

        public List<Textel> Textels => _textels;

        public Face(List<int> indexes)
        {
            _indexes = indexes;
            _color = Utilities.RandomColor();
        }
        public Face(List<int> indexes, Color color) : this(indexes)
        {
            _color = color;
        }

        public Face(List<int> indexes, List<Textel> textels) : this(indexes)
        {
            _textels = textels;
        }

        public Face(List<int> indexes, Color color, List<Textel> textels) : this(indexes)
        {
            _textels = textels;
            _color = color;
        }

        public bool SequenceEqual(Face face) => _indexes.SequenceEqual(face._indexes);

        public int Last() => _indexes[^1];
        public int First() => _indexes[0];
    }



    public class Shape
    {
        public string Id { get; private set; }
        public Bitmap Texture { get; set; }
        public List<MyPoint> Points { get; private set; } = new();
        public List<Face> Faces { get; private set; } = new();
        public MyMatrix MatrixToWorld { get; set; } = new(4, 4, new float[] {
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1 });

        public bool IsEmpty => Points.Count == 0;

        public Shape(List<MyPoint> point, List<Face> faces)
        {
            Id = GenerateUniqueId();
            Points = point;
            Faces = faces;
        }


        public Shape(Shape anotherShape)
        {
            Id = anotherShape.Id;
            Points = new List<MyPoint>(anotherShape.Points);
            Faces = new List<Face>(anotherShape.Faces);
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
    public class Cube : Shape
    {
        public Cube(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {
        }

        public override string ToString()
        {
            return $"Cube: {Id}";
        }
    }
    public class Icosahedron : Shape
    {
        public Icosahedron(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {
        }

        public override string ToString()
        {
            return $"Icosahedron: {Id}";
        }
    }
    public class Dodecahedron : Shape
    {
        public Dodecahedron(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {
        }

        public override string ToString()
        {
            return $"Dodecahedron: {Id}";
        }
    }
    public class Tetrahedron : Shape
    {
        public Tetrahedron(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {
        }

        public override string ToString()
        {
            return $"Tetrahedron: {Id}";
        }
    }
    public class Octahedron : Shape
    {
        public Octahedron(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {
        }

        public override string ToString()
        {
            return $"Octahedron: {Id}";
        }
    }
    public class FunctionShape : Shape
    {
        public FunctionShape(List<MyPoint> point, List<Face> faces) : base(point, faces)
        {

        }
        public override string ToString()
        {
            return $"Function: {Id}";
        }
    }


    public class BoundingBox
    {
        public PointF TopLeft { get; private set; }
        public PointF TopRight { get; private set; }

        public PointF BottomLeft { get; private set; }
        public PointF BottomRight { get; private set; }

        public BoundingBox(params PointF[] values)
        {
            var minX = values.Min(value => value.X);
            var minY = values.Min(value => value.Y);
            var maxX = values.Max(value => value.X);
            var maxY = values.Max(value => value.Y);
            // Инициализация полей BoundingBox
            TopLeft = new PointF { X = minX, Y = minY };
            TopRight = new PointF { X = maxX, Y = minY };
            BottomLeft = new PointF { X = minX, Y = maxY };
            BottomRight = new PointF { X = maxX, Y = maxY };
        }
    }



    public static class Shapes
    {
        public static Cube Cube()
        {
            List<MyPoint> points = new List<MyPoint>
            {
                new MyPoint(-20, -20, 20), // 0
                new MyPoint(20, -20, 20), // 1
                new MyPoint(-20, 20, 20), // 2
                new MyPoint(-20, -20, 60), // 3
                new MyPoint(20, 20, 20), // 4
                new MyPoint(-20, 20, 60), // 5
                new MyPoint(20, -20, 60), // 6
                new MyPoint(20, 20, 60) // 7
            };

            var colors = Enumerable.Range(1, 6).Select(_ => Utilities.RandomColor()).ToArray();

            List<Face> faces = new List<Face>
            {
                 //Нижняя грань
                 new(new List<int>{ 0, 3, 1 }, colors[0],new(){  new(0f, 0f), new(1f, 0f), new(0f, 1f) }),
                 new(new List<int>{ 3, 6, 1 }, colors[0], new(){  new(1f, 0f), new(1f, 1f), new(0f, 1f) }),

                 //Задняя грань
                 new(new List<int>{ 1, 4, 0 }, colors[1],  new(){  new(0f, 0f), new(1f, 0f), new(0f, 1f) }),
                 new(new List<int>{ 4, 2, 0 }, colors[1], new(){  new(1f, 0f), new(1f, 1f), new(0f, 1f) }),

                 //Левая грань
                 new(new List<int>{ 2, 5, 0 }, colors[2], new(){  new(0f, 0f), new(1f, 0f), new(0f, 1f) }),
                 new(new List<int>{ 5, 3, 0 }, colors[2], new(){  new(1f, 0f), new(1f, 1f), new(0f, 1f) }),


                 //Правая грань
                 new(new List<int>{ 1, 6, 4 }, colors[3],  new(){  new(0f, 0f), new(1f, 0f), new(0f, 1f) } ),
                 new(new List<int>{ 6, 7, 4 }, colors[3],  new(){  new(1f, 0f), new(1f, 1f), new(0f, 1f) } ),


                 //Передняя грань
                 new(new List<int>{ 6, 3, 5 }, colors[4], new(){ new(1f, 0f), new(0f, 0f), new(0f, 1f) }),
                 new(new List<int>{ 7, 6, 5 }, colors[4], new(){ new(1f, 1f), new(1f, 0f), new(0f, 1f) }),

                 //Вверхняя грань
                 new(new List<int>{ 7, 5, 2 }, colors[5], new(){ new(1f, 0f), new(0f, 0f), new(0f, 1f) }),
                 new(new List<int>{ 4, 7, 2 }, colors[5], new(){ new(1f, 1f), new(1f, 0f), new(0f, 1f) }),
            };

            return new(points, faces);
        }
        /*
        new List<int>{ 0, 1, 6, 3},
            new List<int>{ 0, 2, 4, 1},
            new List<int>{ 0, 3, 5, 2},
            new List<int>{ 1, 6, 7, 4},
            new List<int>{ 6, 3, 5, 7},
            new List<int>{ 7, 5, 2, 4}
         */


        /*
        public static Shape Icosahedron()
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
        */
        /*
        public static Shape Dodecahedron()
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
        */

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
            var textels = new List<Textel>() { new(1f, 1f), new(0.5f, 0f), new(0f, 1f) };


            List<Face> faces = new List<Face>
            {
                //Задняя грань
                new(new List<int> { 1, 2, 0 }, textels),
                //Левая грань
                new(new List<int> { 0, 2, 3 }, textels),
                //Нижняя грань
                new(new List<int> { 3, 1, 0 }, textels),
                //Правая грань
                new(new List<int> { 3, 2, 1 }, textels)
            };
            return new(points, faces);
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

            var textels = new List<Textel>() { new(1f, 1f), new(0.5f, 0f), new(0f, 1f) };

            List<Face> faces = new List<Face>
            {
                new(new List<int>{ 1, 4, 0 }, textels),
                new(new List<int>{ 0, 4, 3 }, textels),
                new(new List<int>{ 0, 2, 1 }, textels),
                new(new List<int>{ 2, 0, 3 }, textels),
                new(new List<int>{ 3, 4, 5 }, textels),
                new(new List<int>{ 1, 5, 4 }, textels),
                new(new List<int>{ 2, 5, 1 }, textels),
                new(new List<int>{ 2, 3, 5 }, textels),
            };
            return new(points, faces);
        }

        public static Shape Empty()
        {
            return new Shape(new List<MyPoint>(), new List<Face>());
        }
    }
}
