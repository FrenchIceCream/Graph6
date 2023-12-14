namespace Graph6
{
    public static class Utilities
    {
        public static Color RandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
