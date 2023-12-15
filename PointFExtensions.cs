namespace Graph6
{
    public static class PointFExtensions
    {
        public static PointF Difference(this PointF a, PointF b)
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }
    }
}
