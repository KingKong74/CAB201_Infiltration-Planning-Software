using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public abstract class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ObstacleType Type { get; set; }

        protected Obstacle(int x, int y, ObstacleType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public enum ObstacleType
        {
            None,
            Guard,
            Fence,
            Sensor,
            Camera,
            Attackdog
        }

        /// <summary>
        /// Adds an obstacle to the grid. It updates the corresponding node in the grid with the obstacle's state and type.
        /// </summary>
        /// <param name="grid">The grid to add the obstacle to.</param>
        public static void AddObstacle(Grid grid, Obstacle obstacle, List<(int, int)> affectedCoordinates)
        {
            // This creates a new node with the given X and Y coords
            Node node = grid.GetNode(obstacle.X, obstacle.Y);
            node.State = Grid.NodeState.Obstructed;             // gives it a state
            node.ObstacleType = obstacle.Type;                           // gives it a type
        }

        /// <summary>
        /// Validates a string array as a location input and extracts the X and Y coordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True if the input is valid and the coordinates are extracted; otherwise, false.</returns>
        public static bool IsValidLocationInput(string[] input, out int x, out int y)
        {
            x = y = 0;
            if (input.Length != 2 || !int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the location of an obstacle by prompting the user for X and Y coordinates. (Fence obstacle). 
        /// </summary>
        /// <param name="prompt">The prompt message for user input.</param>
        /// <param name="input">An array containing the user's input as strings (X and Y).</param>
        /// <param name="x">The X coordinate of the location (output).</param>
        /// <param name="y">The Y coordinate of the location (output).</param>
        public static void GetLocation(string prompt, out string[] input, out int x, out int y)
        {
            x = y = 0;
            input = new string[2];

            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    input = Console.ReadLine().Split(',');

                    if (!IsValidLocationInput(input, out x, out y))
                    {
                        throw new InvalidOptionException();
                    }
                    break;
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Gets the location of an obstacle by prompting the user for X and Y coordinates.
        /// </summary>
        /// <param name="prompt">The prompt message for user input.</param>
        /// <param name="x">The X coordinate of the location (output).</param>
        /// <param name="y">The Y coordinate of the location (output).</param>
        public static void GetLocation(string prompt, out int x, out int y)
        {
            x = y = 0;
            string[] input;

            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    input = Console.ReadLine().Split(',');

                    if (!IsValidLocationInput(input, out x, out y))
                    {
                        throw new InvalidOptionException();
                    }
                    break;
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }


        /// <summary>
        /// Prompts the user for input and retrieves the direction for Obstacle.
        /// </summary>
        /// <param name="prompt">The retrieved direction.</param>
        /// <param name="directionInput">The retrieved direction.</param>
        protected static void GetDirection(string prompt, out char direction)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string inputStr = Console.ReadLine();
                    if (inputStr.Length > 0)
                    {
                        char input = char.ToLower(inputStr[0]);

                        if (input == 'n' || input == 's' || input == 'e' || input == 'w')
                        {
                            direction = input;
                            break;
                        }
                        else
                        {
                            throw new InvalidDirectionException("Invalid direction.");
                        }
                    }
                }
                catch (InvalidDirectionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }


        /// <summary>
        /// Prompts the user for input and retrieves the range for Obstacle. (sensor)
        /// </summary>
        /// <param name="obstacle"></param>
        /// <param name="prompt"></param>
        /// <param name="range"></param>
        protected static void GetRange(string prompt, out float range)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string rangeInput = Console.ReadLine();

                    if (!float.TryParse(rangeInput, out range) || range <= 0)
                    {
                        throw new InvalidOptionException();
                    }
                    break;
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }


        /// <summary>
        /// Prompts the user for input and retrieves the range for Obstacle. (attackdog)
        /// </summary>
        /// <param name="obstacle"></param>
        /// <param name="prompt"></param>
        /// <param name="range"></param>
        protected static void GetRange(string prompt, out int range)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string rangeInput = Console.ReadLine();

                    if (!int.TryParse(rangeInput, out range) || range <= 0)
                    {
                        throw new InvalidOptionException();
                    }
                    break;
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Calculates the coordinates affected by the obstacles direction. (camera)
        /// </summary>
        /// <param name="x">The X-coordinate of the Camera.</param>
        /// <param name="y">The X-coordinate of the Camera.</param>
        /// <param name="direction">The direction the Camera is facing.</param>
        /// <returns> A list of coordinates that are affected by the obstacle </returns>
        protected static List<(int, int)> Coverage(int x, int y, char direction, int limit)
        {
            int forwardLimit = limit;
            var affectedCoordinates = new List<(int, int)>();

            switch (direction)
            {
                case 'n':
                    for (int i = y; i >= y - forwardLimit; i--)
                    {
                        affectedCoordinates.Add((x, i));

                        // Obstruct diagonally to the northwest
                        for (int j = x - 1, k = i - 1; j >= x - forwardLimit && k >= y - forwardLimit; j--, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        // Obstruct diagonally to the southwest
                        for (int j = x + 1, k = i - 1; j <= x + forwardLimit && k >= y - forwardLimit; j++, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 's':
                    for (int i = y; i <= y + forwardLimit; i++)
                    {
                        affectedCoordinates.Add((x, i));

                        for (int j = x - 1, k = i + 1; j >= x - forwardLimit && k <= y + forwardLimit; j--, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        for (int j = x + 1, k = i + 1; j <= x + forwardLimit && k <= y + forwardLimit; j++, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 'e':
                    for (int i = x; i <= x + forwardLimit; i++)
                    {
                        affectedCoordinates.Add((i, y));

                        for (int j = i + 1, k = y - 1; j <= i + forwardLimit && k >= y - forwardLimit; j++, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }

                        for (int j = i + 1, k = y + 1; j <= i + forwardLimit && k <= y + forwardLimit; j++, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 'w':
                    for (int i = x; i >= x - forwardLimit; i--)
                    {
                        affectedCoordinates.Add((i, y));

                        // Obstruct diagonally to the northwest
                        for (int j = i - 1, k = y - 1; j >= i - forwardLimit && k >= y - forwardLimit; j--, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        // Obstruct diagonally to the southwest
                        for (int j = i - 1, k = y + 1; j >= i - forwardLimit && k <= y + forwardLimit; j--, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }

                    }
                    break;
                default:
                    break;
            }

            return affectedCoordinates;
        }

        /// <summary>
        /// Calculates the coordinates affected by the obstacles direction. (attackdog)
        /// </summary>
        /// <param name="x">The X-coordinate of the Obstacle.</param>
        /// <param name="y">The X-coordinate of the Obstacle.</param>
        /// <param name="direction">The direction the Obstacle is facing.</param>
        /// <returns> A list of coordinates that are affected by the obstacle </returns>
        protected static List<(int, int)> Coverage(int x, int y, char direction, int limit, char O_direction)
        {
            int forwardLimit = limit;
            int backwardLimit = limit / 2;
            var affectedCoordinates = new List<(int, int)>();

            switch (direction)
            {
                case 'n':
                    for (int i = y; i >= y - forwardLimit; i--)
                    {
                        affectedCoordinates.Add((x, i));

                        // Obstruct diagonally to the northwest
                        for (int j = x - 1, k = i - 1; j >= x - forwardLimit && k >= y - forwardLimit; j--, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        // Obstruct diagonally to the southwest
                        for (int j = x + 1, k = i - 1; j <= x + forwardLimit && k >= y - forwardLimit; j++, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 's':
                    for (int i = y; i <= y + forwardLimit; i++)
                    {
                        affectedCoordinates.Add((x, i));

                        for (int j = x - 1, k = i + 1; j >= x - forwardLimit && k <= y + forwardLimit; j--, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        for (int j = x + 1, k = i + 1; j <= x + forwardLimit && k <= y + forwardLimit; j++, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 'e':
                    for (int i = x; i <= x + forwardLimit; i++)
                    {
                        affectedCoordinates.Add((i, y));

                        for (int j = i + 1, k = y - 1; j <= i + forwardLimit && k >= y - forwardLimit; j++, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }

                        for (int j = i + 1, k = y + 1; j <= i + forwardLimit && k <= y + forwardLimit; j++, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                    }
                    break;
                case 'w':
                    for (int i = x; i >= x - forwardLimit; i--)
                    {
                        affectedCoordinates.Add((i, y));

                        // Obstruct diagonally to the northwest
                        for (int j = i - 1, k = y - 1; j >= i - forwardLimit && k >= y - forwardLimit; j--, k--)
                        {
                            affectedCoordinates.Add((j, k));
                        }
                        // Obstruct diagonally to the southwest
                        for (int j = i - 1, k = y + 1; j >= i - forwardLimit && k <= y + forwardLimit; j--, k++)
                        {
                            affectedCoordinates.Add((j, k));
                        }

                    }
                    break;
                default:
                    break;
            }

            // Calculate coverage in the backward direction;
            affectedCoordinates.AddRange(Coverage(x, y, O_direction, backwardLimit));


            return affectedCoordinates;
        }
    }
 }
