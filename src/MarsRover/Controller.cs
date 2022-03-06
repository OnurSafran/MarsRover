using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Domain.Entities;
using MarsRover.Domain.Enums;
using MarsRover.Mappers;

namespace MarsRover
{
    public class Controller
    {
        public Plateau Plateau { get; }
        public RoverSquad RoverSquad { get; }

        public Controller(List<string> lines)
        {
            if (!lines.Any())
                throw new Exception("Plateau coordinates should be provided");

            // Init Plateau
            Plateau = GetPlateau(lines[0]);

            // Init RoverSquad
            RoverSquad = new RoverSquad(Plateau);

            for (int i = 1; i < lines.Count; i += 2)
            {
                // Deploy Rover
                RoverSquad.DeployRover(GetCoordinate(lines[i]), GetDirection(lines[i]), GetInstruction(lines[i + 1]));
            }
        }

        public void Execute()
        {
            RoverSquad.ExecuteInstructions();
        }

        public static Plateau GetPlateau(string input)
        {
            int[] points;

            try
            {
                points = Array.ConvertAll(input.Split(' '), Convert.ToInt32);
            }
            catch (Exception)
            {
                throw new Exception($"Land parameters should be integer values like '5 5' but sent this --> {input}");
            }

            if (points.Length != 2)
                throw new Exception($"Land parameters should look like this '5 5' but sent this --> {input}");

            return new Plateau(points[0], points[1]);
        }

        public static Coordinate GetCoordinate(string input)
        {
            string[] inputs = input.ToUpper().Split(' ');

            if (inputs.Length != 3)
                throw new Exception(
                    $"Rover position parameters should look like this '1 2 N' but sent this --> {input}");

            try
            {
                return CoordinateMapper.Map(inputs[0], inputs[1]);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Rover position parameters should be integer values like '5 5' but sent this --> {inputs[0]} {inputs[1]}");
            }
        }

        public static Direction GetDirection(string input)
        {
            string[] inputs = input.ToUpper().Split(' ');

            if (inputs.Length != 3)
                throw new Exception(
                    $"Rover position parameters should look like this '1 2 N' but sent this --> {input}");

            try
            {
                return DirectionMapper.Map(inputs[2]);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Rover direction should be one of these values 'N' 'S' 'W' 'E' but sent this --> {inputs[2]}");
            }
        }

        public static List<Instruction> GetInstruction(string input)
        {
            List<char> instructions = input.ToUpper().ToCharArray().ToList();

            try
            {
                return instructions.Select(InstructionMapper.Map).ToList();
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Rover instruction should be one of these values 'R' 'L' 'M' but sent this --> {input}");
            }
        }
    }
}