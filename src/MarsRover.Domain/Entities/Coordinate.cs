namespace MarsRover.Domain.Entities
{
    public class Coordinate
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                   X.Equals(coordinate.X) &&
                   Y.Equals(coordinate.Y);
        }
    }
}