using System;
using MarsRover.Domain.Entities;

namespace MarsRover.Mappers
{
    public class CoordinateMapper
    {
        public static Coordinate Map(string input1, string input2)
        {
            return new Coordinate(Convert.ToInt32(input1), Convert.ToInt32(input2));
        }
    }
}