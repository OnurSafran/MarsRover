using System;
using MarsRover.Domain.Enums;

namespace MarsRover.Mappers
{
    public class InstructionMapper
    {
        public static Instruction Map(char input)
        {
            switch (input)
            {
                case 'R':
                    return Instruction.TurnRight;
                case 'L':
                    return Instruction.TurnLeft;
                case 'M':
                    return Instruction.MoveForward;
                default:
                    throw new Exception();
            }
        }
    }
}