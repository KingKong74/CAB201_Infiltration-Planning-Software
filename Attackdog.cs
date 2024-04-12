using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Attackdog : Obstacle
    {
        public Attackdog(int x, int y, char direction, int range) : base(x, y, ObstacleType.Attackdog) { }

        /// <summary>
        /// Prompts the user for input, creates an Attack Dog Obstacle, and adds it to the grid.
        /// </summary>
        /// <param name="grid">The grid to add the Attack Dog obstacle to.</param>
        static public void AddAttackdog(Grid grid)
        {
            int x, y;
            char direction;
            int range;
            char O_direction;

            string locationPrompt = "Enter the Attack Dog's initial location (X,Y): ";
            string directionPrompt = "Enter the direction the Attack Dog is facing (n, s, e or w): ";
            string rangePrompt = "Enter the attackdogs's range: ";

            GetLocation(locationPrompt, out x, out y);
            GetDirection(directionPrompt, out direction);
            GetRange(rangePrompt, out range);

            O_direction = GetOppositeDirection(direction); 

            List<(int, int)> affectedCoordinates = Coverage(x, y, direction, range, O_direction);


            foreach ((int newX, int newY) in affectedCoordinates)
            {
                Obstacle attackdog = new Attackdog(newX, newY, direction, range);
                AddObstacle(grid, attackdog, affectedCoordinates);
            }
        }

        /// <summary>
        /// Gets the opposite direction of the specified direction.
        /// </summary>
        /// <param name="direction"> The input direction ('n', 's', 'e', or 'w'). </param>
        /// <returns> The opposite direction. </returns>
        private static char GetOppositeDirection(char direction)
        {
            switch (direction)
            {
                case 'n':
                    return 's';
                case 's':
                    return 'n';
                case 'e':
                    return 'w';
                case 'w':
                    return 'e';
                default:
                    return 'n'; // Default to 'n' if the direction is invalid
            }
        }
    }
}
