using System.Collections.Generic;
using MarsRover.Domain.Enums;

namespace MarsRover.Domain.Entities
{
    public class RoverSquad
    {
        public List<Rover> RoverList { get; }
        public Plateau Plateau { get; }

        public RoverSquad(Plateau plateau)
        {
            Plateau = plateau;
            RoverList = new List<Rover>();
        }

        public void DeployRover(Coordinate coordinate, Direction direction, List<Instruction> instructions)
        {
            RoverList.Add(new Rover(this, Plateau, coordinate, direction, instructions));
        }

        public void ExecuteInstructions()
        {
            RoverList.ForEach(rover => rover.ExecuteInstructions());
        }
    }
}