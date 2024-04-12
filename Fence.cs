using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment_2_Final
{
    public class Fence : Obstacle
    {
        public int EndX { get; set; }
        public int EndY { get; set; }


        public Fence(int startX, int startY, int endX, int endY) : base(startX, startY, ObstacleType.Fence)
        {
            EndX = endX;
            EndY = endY;

        }

        /// <summary>
        /// Prompts user for input, creates Fence Obstacle and adds to grid
        /// </summary>
        /// <param name="grid"></param>
        /// <exception cref="InvalidOptionException"></exception>
        public static void AddFence(Grid grid)
        {
            //Obstacle fence = new Fence(0, 0, 0, 0);

            string[] startInput, endInput;
            int startX, startY, endX, endY;

            string startPrompt = "Enter the location where the fence starts (X,Y): ";
            string endPrompt = "Enter the location where the fence ends (X,Y): ";

            while (true)
            {
                try
                {
                    GetLocation(startPrompt, out startInput, out startX, out startY);
                    GetLocation(endPrompt, out endInput, out endX, out endY);

                    if (!IsValidFenceInput(startInput, endInput, out startX, out startY, out endX, out endY))
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

            List<(int, int)> affectedCoordinates = Coverage(startX, startY, endX, endY);

            foreach ((int x, int y) in affectedCoordinates)
            {
                Obstacle fence = new Fence(x, y, x, y);
                AddObstacle(grid, fence, affectedCoordinates);
            }

        }

        /// <summary>
        /// Validates the input for defining a fence, ensuring it's a valid location with distinct start and end points.
        /// </summary>
        /// <param name="startInput"></param>
        /// <param name="endInput"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns>True if the input is valid and distinct start and end points are provided; otherwise, false.</returns>
        private static bool IsValidFenceInput(string[] startInput, string[] endInput, out int startX, out int startY, out int endX, out int endY)
        {
            startX = startY = endX = endY = 0;
            if (!IsValidLocationInput(startInput, out startX, out startY) || !IsValidLocationInput(endInput, out endX, out endY))
            {
                return false;
            }
            if (startX != endX && startY != endY)
            {
                return false;
            }
            if (startX == endX && startY == endY)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Calculates the coordinates affected by a fence with specified start and end points.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns>A list of coordinates that are affected by the fence </returns>
        private static List<(int, int)> Coverage(int startX, int startY, int endX, int endY)
        {
            int minX = Math.Min(startX, endX);
            int maxX = Math.Max(startX, endX);
            int minY = Math.Min(startY, endY);
            int maxY = Math.Max(startY, endY);

            List<(int, int)> affectedCoordinates = new List<(int, int)>();

            if (startX == endX)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    affectedCoordinates.Add((startX, y));
                }
            }
            else
            {
                for (int x = minX; x <= maxX; x++)
                {
                    affectedCoordinates.Add((x, startY));
                }
            }

            return affectedCoordinates;
        }
    }
}
