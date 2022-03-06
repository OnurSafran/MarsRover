using System;
using System.Collections.Generic;
using System.Linq;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using MarsRover.Domain.Enums;

namespace MarsRover.Domain.Entities
{
    public class Rover
    {
        public RoverSquad RoverSquad { get; }
        public Plateau Plateau { get; }
        public Coordinate Coordinate { get; protected set; }
        public Direction Direction { get; protected set; }
        public List<Instruction> Instructions { get; }
        private PassiveStateMachine<States, Instruction> StateMachine { get; }
        private Instruction? CurrentInstruction { get; set; }
        
        public static bool Debug = false;

        public Rover(RoverSquad roverSquad, Plateau plateau, Coordinate coordinate, Direction direction, List<Instruction> instructions)
        {
            RoverSquad = roverSquad;
            Plateau = plateau;
            Coordinate = coordinate;
            Direction = direction;
            Instructions = instructions;

            var builder = new StateMachineDefinitionBuilder<States, Instruction>();

            builder.In(States.Online)
                .On(Instruction.TurnLeft)
                .Execute(TurnLeft)
                .Execute(PrintCoordinate)
                .On(Instruction.TurnRight)
                .Execute(TurnRight)
                .Execute(PrintCoordinate)
                .On(Instruction.MoveForward)
                .Execute(MoveForward)
                .Execute(PrintCoordinate)
                .On(Instruction.GoOffline)
                .Goto(States.Offline)
                .Execute(GoOffline)
                .Execute(PrintCoordinate);

            builder.In(States.Offline)
                .On(Instruction.TurnLeft)
                .Execute(PrintNotResponding)
                .On(Instruction.TurnRight)
                .Execute(PrintNotResponding)
                .On(Instruction.MoveForward)
                .Execute(PrintNotResponding)
                .On(Instruction.GoOffline)
                .Execute(PrintNotResponding);

            builder.WithInitialState(States.Online);

            StateMachine = builder
                .Build()
                .CreatePassiveStateMachine();

            StateMachine.Start();
        }

        /// <summary>
        /// Execute given Instructions till nothing left
        /// </summary>
        public void ExecuteInstructions()
        {
            CurrentInstruction = GetNextInstruction();

            while (CurrentInstruction.HasValue)
            {
                PrintDebug("Executing " + CurrentInstruction.Value + " at " + Coordinate);
            
                StateMachine.Fire(CurrentInstruction.Value);
                
                CurrentInstruction = GetNextInstruction();
            }
            
            PrintDebug($"There is no Instruction Left. Rover Currently at {Coordinate}");
            Print($"{Coordinate} {Direction}");
        }

        /// <summary>
        /// Get Next Instruction and Remove From List
        /// </summary>
        protected Instruction? GetNextInstruction()
        {
            if (!Instructions.Any())
                return null;

            var instruction = Instructions[0];
            Instructions.RemoveAt(0);

            return instruction;
        }

        /// <summary>
        /// Turn Left
        /// </summary>
        protected void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.E:
                    Direction = Direction.N;
                    break;
                case Direction.N:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.E;
                    break;
            }
            
            PrintDebug("Rover turned left");
        }

        /// <summary>
        /// Turn Right
        /// </summary>
        protected void TurnRight()
        {
            switch (Direction)
            {
                case Direction.E:
                    Direction = Direction.S;
                    break;
                case Direction.N:
                    Direction = Direction.E;
                    break;
                case Direction.W:
                    Direction = Direction.N;
                    break;
                case Direction.S:
                    Direction = Direction.W;
                    break;
            }
            
            PrintDebug("Rover turned right");
        }
        
        /// <summary>
        /// Move Forward
        /// </summary>
        protected void MoveForward()
        {
            switch (Direction)
            {
                case Direction.E:
                    Coordinate += new Coordinate(1, 0);
                    break;
                case Direction.N:
                    Coordinate += new Coordinate(0, 1);
                    break;
                case Direction.W:
                    Coordinate += new Coordinate(-1, 0);
                    break;
                case Direction.S:
                    Coordinate += new Coordinate(0, -1);
                    break;
            }
            
            PrintDebug("Rover went forward");
        }

        /// <summary>
        /// Go Offline
        /// </summary>
        protected void GoOffline()
        {
            PrintDebug("Rover went offline");
        }

        /// <summary>
        /// Check if the coordinates is valid
        /// </summary>
        protected bool IsValidCoordinate()
        {
            return !Plateau.OutOfPlateau(Coordinate);
        }

        /// <summary>
        /// Print Coordinate
        /// </summary>
        protected void PrintCoordinate()
        {
            PrintDebug($"Rover at {Coordinate}");
        }

        /// <summary>
        /// Print Not Responding
        /// </summary>
        protected void PrintNotResponding()
        {
            PrintDebug("Rover is not responding");
        }
        
        public static void PrintDebug(string str)
        {
            if (!Debug)
                return;

            Console.WriteLine(str);
        }

        public static void Print(string str)
        {
            Console.WriteLine(str);
        }
    }
}