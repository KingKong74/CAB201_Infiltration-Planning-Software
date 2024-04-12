using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Node : Grid
    {
        public int X { get; }
        public int Y { get; }
        public int Gcost { get; set; } // Cost to move from the starting point to this node
        public int Hcost { get; set; } // Heuristic cost to move from this node to the target
        public int Fcost { get { return Gcost + Hcost; } } // Total cost (Gcost + Hcost) to reach the target from the starting point
        public Node Parent { get; set; } 
        public NodeState State { get; set; }
        public Obstacle.ObstacleType ObstacleType { get; set; }

        // initialize the Node with X and Y coordinates
        public Node(int x, int y)
        {
            X = x;
            Y = y;
            State = NodeState.Open;
            ObstacleType = Obstacle.ObstacleType.None;
        }

    }
}
