using System;
using System.Collections.Generic;
using MarsRover.Domain.Entities;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            
            while (true)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;
                
                lines.Add(line);
            }

            Rover.Debug = true;
            Controller controller = new Controller(lines);
            controller.Execute();
            
            Console.ReadLine();
        }
    }
}