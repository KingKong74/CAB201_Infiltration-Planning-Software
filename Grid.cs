using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment_2_Final
{
    public class Grid
    {

        public enum NodeState
        {
            Open,
            Obstructed,
            Closed

        }

        /// <summary>
        /// A private dictionary that maps (X, Y) coordinates to nodes, representing a grid of nodes.
        /// </summary>
        private Dictionary<(int, int), Node> nodes = new Dictionary<(int, int), Node>();

        /// <summary>
        /// Retrieves a node from the grid based on its (X, Y) coordinates, creating it if it doesn't exist.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns> The node located at the specified (X, Y) coordinates. </returns>
        public Node GetNode(int x, int y)
        {
            var coordinates = (x, y);

            if (!nodes.ContainsKey(coordinates))
            {
                nodes[coordinates] = new Node(x, y);
            }

            return nodes[coordinates];
        }

/*        /// <summary>
        /// Retrieves a dictionary of grid nodes, with coordinates as keys and their associated node objects as values.
        /// </summary>
        /// <returns> A dictionary containing grid nodes </returns>
        public Dictionary<(int, int), Node> GetNodes()
        {
            return nodes;
        }*/


    }
}
