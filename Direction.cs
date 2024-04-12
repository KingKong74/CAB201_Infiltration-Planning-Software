using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Direction : UI
    {
        /// <summary>
        /// Show safe directions for the agent
        /// </summary>
        /// <param name="grid"></param>
        public static void ShowSafeDirections(Grid grid)
        {
            int x, y;
            string prompt = "Enter your current location (X,Y): ";

            getLocation(prompt, out x, out y);

            Node currentNode = grid.GetNode(x, y);

            if (currentNode.State == Grid.NodeState.Obstructed)
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
                return;
            }

            bool canMoveNorth = CanMove(grid, x, y - 1);
            bool canMoveSouth = CanMove(grid, x, y + 1);
            bool canMoveEast = CanMove(grid, x + 1, y);
            bool canMoveWest = CanMove(grid, x - 1, y);

            if (!canMoveNorth && !canMoveSouth && !canMoveEast && !canMoveWest)
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
                return;
            }

            List<string> safeDirections = new List<string>();

            if (canMoveNorth)
            {
                safeDirections.Add("N");
            }
            if (canMoveSouth)
            {
                safeDirections.Add("S");
            }
            if (canMoveEast)
            {
                safeDirections.Add("E");
            }
            if (canMoveWest)
            {
                safeDirections.Add("W");
            }

            Console.WriteLine("You can safely take any of the following directions: " + string.Join("", safeDirections));

        }

        /// <summary>
        /// Check for nodes with state that aren't 'Obstructed' 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool CanMove(Grid grid, int x, int y)
        {
            Node node = grid.GetNode(x, y);
            return node.State != Grid.NodeState.Obstructed;
        }

    }
}
