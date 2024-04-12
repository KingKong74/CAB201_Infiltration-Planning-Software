using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Assessment_2_Final.Obstacle;

namespace Assessment_2_Final
{
    public class Guard : Obstacle
    {
        public Guard(int x, int y) : base(x, y, ObstacleType.Guard) { }

        /// <summary>
        /// Prompts user for input, creates Guard Obstacle and adds to grid
        /// </summary>
        /// <param name="grid"></param>
        public static void AddGuard(Grid grid)
        {
            int x, y;
            string prompt = "Enter the guard's location (X,Y): ";



            GetLocation(prompt, out x, out y);


            List<(int, int)> affectedCoordinates = new List<(int, int)>();

            Obstacle guard = new Guard(x, y);
            affectedCoordinates.Add((x , y));

            AddObstacle(grid, guard, affectedCoordinates);
        }

    }
}
