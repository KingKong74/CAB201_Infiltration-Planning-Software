using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Assessment_2_Final.Obstacle;

namespace Assessment_2_Final
{
    public class Map : UI
    {
        /// <summary>
        /// Displays Obstacle map.
        /// </summary>
        /// <param name="grid"> The grid containing obstacle information. </param>
        public static void DisplayObstacleMap(Grid grid)
        {
           int topLeftX = 0, topLeftY = 0, bottomRightX = 0, bottomRightY = 0;

            string topLeftPrompt = "Enter the location of the top-left cell of the map (X,Y): ";
            string bottomRightPrompt = "Enter the location of the bottom-right cell of the map (X,Y): ";

            while (true)
            {
                try
                {
                    getLocation(topLeftPrompt, out topLeftX, out topLeftY);
                    getLocation(bottomRightPrompt, out bottomRightX, out bottomRightY);

                    if (!mapValidation(bottomRightX, topLeftX, bottomRightY, topLeftY))
                    {
                        throw new InvalidSpecificationException("Invalid map specifications.");
                    }
                    break;

                }
                catch (InvalidSpecificationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            }

            DrawObstacleMap(grid, topLeftX, topLeftY, bottomRightX, bottomRightY);
        }

        /// <summary>
        /// Draws the obstacle map within specified coordinates.
        /// </summary>
        /// <param name="grid">The grid containing obstacle information.</param>
        /// <param name="topLeftX">X-coordinate of the top-left cell.</param>
        /// <param name="topLeftY">Y-coordinate of the top-left cell.</param>
        /// <param name="bottomRightX">X-coordinate of the bottom-right cell.</param>
        /// <param name="bottomRightY">Y-coordinate of the bottom-right cell.</param>
        private static void DrawObstacleMap(Grid grid, int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
        {
            for (int y = topLeftY; y <= bottomRightY; y++)
            {
                for (int x = topLeftX; x <= bottomRightX; x++)
                {
                    Node node = grid.GetNode(x, y);

                    // Check the obstacle type and display the corresponding character
                    switch (node.ObstacleType)
                    {
                        case ObstacleType.Guard:
                            Console.Write('g');
                            break;
                        case ObstacleType.Fence:
                            Console.Write('f');
                            break;
                        case ObstacleType.Sensor:
                            Console.Write('s');
                            break;
                        case ObstacleType.Camera:
                            Console.Write('c');
                            break;
                        case ObstacleType.Attackdog:
                            Console.Write('a');
                            break;
                        default:
                            Console.Write('.');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Check for a valid map build.
        /// </summary>
        /// <param name="bottomRightX"></param>
        /// <param name="topLeftX"></param>
        /// <param name="bottomRightY"></param>
        /// <param name="topLeftY"></param>
        /// <returns> True if the map is valid, otherwise False. </returns>
        public static bool mapValidation(int bottomRightX, int topLeftX, int bottomRightY, int topLeftY)
        {
            return bottomRightX >= topLeftX && bottomRightY >= topLeftY;
        }


    }
}
