namespace MarsRover.Domain.Entities
{
    public class Plateau
    {
        public Coordinate LowerLeftCoordinate { get; protected set; }
        public Coordinate UpperRightCoordinate { get; protected set; }

        /// <summary>
        /// End coordinates of Plateau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Plateau(int x, int y)
        {
            LowerLeftCoordinate = new Coordinate(0, 0);
            UpperRightCoordinate = new Coordinate(x, y);
        }

        /// <summary>
        /// Size of Plateau
        /// </summary>
        /// <param name="size"></param>
        public Plateau(int size)
        {
            LowerLeftCoordinate = new Coordinate(0, 0);
            UpperRightCoordinate = new Coordinate(size - 1, size - 1);
        }

        /// <summary>
        /// Is given Coordinate is out of Plateau ?
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool OutOfPlateau(Coordinate coordinate)
        {
            return coordinate.X < LowerLeftCoordinate.X || coordinate.Y < LowerLeftCoordinate.Y ||
                   coordinate.X > UpperRightCoordinate.X || coordinate.Y > UpperRightCoordinate.Y;
        }
    }
}