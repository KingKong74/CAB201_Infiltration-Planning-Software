using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Pathing : UI
    {
        /// <summary>
        /// Displays the path from the agent's current location to the mission objective.
        /// </summary>
        /// <param name="grid">The grid to navigate.</param>
        public static void DisplayPath(Grid grid)
        {
            int agentX, agentY, objectiveX, objectiveY;

            string currentLocationPrompt = "Enter your current location (X,Y): ";
            string missionLocationPrompt = "Enter the location of the mission objective (X,Y): ";

            getLocation(currentLocationPrompt, out agentX, out agentY);
            getLocation(missionLocationPrompt, out objectiveX, out objectiveY);

            Algorithm(grid, agentX, agentY, objectiveX, objectiveY);

            // Create nodes for the agent's current location and the objective location
            Node startNode = grid.GetNode(agentX, agentY);
            Node targetNode = grid.GetNode(objectiveX, objectiveY);
        }

        /// A* algorithm to find a path from the agent's current location to the mission objective.
        /// </summary>
        /// <param name="grid">The grid to navigate.</param>
        /// <param name="agentX">X-coordinate of the agent's current location.</param>
        /// <param name="agentY">Y-coordinate of the agent's current location.</param>
        /// <param name="objectiveX">X-coordinate of the mission objective.</param>
        /// <param name="objectiveY">Y-coordinate of the mission objective.</param>
        public static void Algorithm(Grid grid, int agentX, int agentY, int objectiveX, int objectiveY)
        {
            // Create nodes for the agent's current location and the objective location
            Node startNode = grid.GetNode(agentX, agentY);
            Node targetNode = grid.GetNode(objectiveX, objectiveY);

            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // Find the node with the lowest F cost in the open list
                Node currentNode = openList.OrderBy(node => node.Fcost).First();

                // Move the current node to the closed list
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                // Check if we've reached the target node
                if (currentNode == targetNode)
                {
                    // Path found, reconstruct and display it
                    List<Node> path = ReconstructPath(startNode, targetNode);
                    DisplayPath(path);
                    return;
                }

                // Generate neighbor nodes and process them
                List<Node> neighbors = GetNeighbors(currentNode, grid);

                foreach (Node neighbor in neighbors)
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue; // Skip neighbors in the closed list
                    }

                    int tentativeGCost = currentNode.Gcost + CalculateDistance(currentNode, neighbor);

                    if (!openList.Contains(neighbor) || tentativeGCost < neighbor.Gcost)
                    {
                        neighbor.Parent = currentNode;
                        neighbor.Gcost = tentativeGCost;
                        neighbor.Hcost = CalculateDistance(neighbor, targetNode);
                        neighbor.State = Node.NodeState.Open;

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            Console.WriteLine("There is no safe path to the objective.");
        }

        // Helper method to reconstruct the path from the target node to the start node
        private static List<Node> ReconstructPath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse(); // Reverse the path to start from the start node
            return path;
        }

        /// <summary>
        /// Calculates the Manhattan distance between two nodes.
        /// </summary>
        /// <param name="nodeA">First node.</param>
        /// <param name="nodeB">Second node.</param>
        /// <returns>The Manhattan distance between the two nodes.</returns>
        private static int CalculateDistance(Node nodeA, Node nodeB)
        {
            int distanceX = Math.Abs(nodeA.X - nodeB.X);
            int distanceY = Math.Abs(nodeA.Y - nodeB.Y);
            return distanceX + distanceY;
        }

        /// <summary>
        /// Gets valid neighbor nodes for a given node on the grid.
        /// </summary>
        /// <param name="node">The node for which to find neighbors.</param>
        /// <param name="grid">The grid to navigate.</param>
        /// <returns>A list of valid neighbor nodes.</returns>
        private static List<Node> GetNeighbors(Node node, Grid grid)
        {
            List<Node> neighbors = new List<Node>();

            // Check N, S, E, W neighbors
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int neighborX = node.X + dx[i];
                int neighborY = node.Y + dy[i];

                // Directly get the neighbor node without grid bounds check
                Node neighbor = grid.GetNode(neighborX, neighborY);

                if (neighbor.State != Node.NodeState.Obstructed)
                {
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Displays the path from the start to the target node.
        /// </summary>
        /// <param name="path">The path as a list of nodes.</param>
        private static void DisplayPath(List<Node> path)
        {
            Console.WriteLine("The following path will take you to the objective:");
            foreach (Node node in path)
            {
                if (node.X == node.Parent.X)
                {
                    if (node.Y < node.Parent.Y) Console.Write("N");
                    else Console.Write("S");
                }
                else
                {
                    if (node.X < node.Parent.X) Console.Write("W");
                    else Console.Write("E");
                }
            }
            Console.WriteLine();
        }

    }
    
}
