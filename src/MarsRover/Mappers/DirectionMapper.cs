using System;
using MarsRover.Domain.Enums;

namespace MarsRover.Mappers
{
    public class DirectionMapper
    {
        public static Direction Map(string input)
        {
            switch (input)
            {
                case "N":
                    return Direction.N;
                case "E":
                    return Direction.E;
                case "W":
                    return Direction.W;
                case "S":
                    return Direction.S;
                default:
                    throw new Exception();
            }
        }
    }
}