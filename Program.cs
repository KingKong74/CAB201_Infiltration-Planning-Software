using System;

namespace Assessment_2_Final
{
    internal class Program
    {
        internal static Grid grid = new Grid();

        static void Main(string[] args)
        {
            char option; 

            do
            {
                UI.DisplayMenu();
                option = ReadOption();

                switch (option)
                {
                    case 'g':
                        Guard.AddGuard(grid);
                        break;
                    case 'f':
                        Fence.AddFence(grid);
                        break;
                    case 's':
                        Sensor.AddSensor(grid);
                        break;
                    case 'c':
                        Camera.AddCamera(grid);
                        break;
                    case 'a':
                        Attackdog.AddAttackdog(grid);
                        break;
                    case 'd':
                        Direction.ShowSafeDirections(grid);
                        break;
                    case 'm':
                        Map.DisplayObstacleMap(grid);
                        break;
                    case 'p':
                        Pathing.DisplayPath(grid);
                        break;
                    case 'x':
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        break;
                }
            } while (option != 'x');

        }

        /// <summary>
        /// Reads user input from terminal
        /// </summary>
        /// <returns> user input </returns>
        static char ReadOption()
        {
            while (true)
            {
                char input = (char)Console.Read();

                // Read the newline character to clear the input buffer
                Console.ReadLine();

                try
                {
                    Validate(input);
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                return input;
            }
        }

        /// <summary>
        /// Validates user input
        /// </summary>
        static void Validate (char input)
        {
            // Check if the input is not a character (e.g., a number or symbol)
            if (!char.IsLetter(input))
            {
                throw new InvalidOptionException();
            }

            // Check if the input is null or a blank space
            else if (char.IsWhiteSpace(input))
            {
                throw new InvalidOptionException();
            }
        }
    }

}