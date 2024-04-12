using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Camera : Obstacle
    {
        public char Direction { get; }

        public Camera(int x, int y, char direction) : base(x, y, ObstacleType.Camera)
        {
            Direction = direction;
        }

        /// <summary>
        /// Prompts user for input, creates Camera Obstacle and adds to grid.
        /// </summary>
        /// <param name="grid">The grid to add the Camera obstacle to.</param>
        public static void AddCamera(Grid grid)
        {
            int x, y;
            char direction;

            string locationPrompt = "Enter the camera's location (X,Y): ";
            string directionPrompt = "Enter the direction the camera is facing (n, s, e or w): ";

            GetLocation(locationPrompt, out x, out y);
            GetDirection(directionPrompt, out direction);

            List<(int, int)> affectedCoordinates = Coverage(x, y, direction, 1001);

            foreach ((int newX, int newY) in affectedCoordinates)
            {
                Obstacle camera = new Camera(newX, newY, direction);
                AddObstacle(grid, camera, affectedCoordinates);
            }
        }
    }
}
