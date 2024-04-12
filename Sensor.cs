using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class Sensor : Obstacle
    {
        public float Range { get; }

        public Sensor(int x, int y, float range) : base(x, y, ObstacleType.Sensor)
        {
            Range = range;
        }

        /// <summary>
        /// Prompts user for input, creates Sensor Obstacle and adds to grid.
        /// </summary>
        /// <param name="grid"></param>
        public static void AddSensor(Grid grid)
        {
            float range;
            int x, y;

            string locationPrompt = "Enter the sensor's location (X,Y): ";
            string rangePrompt = "Enter the sensor's range (in klicks): ";


            GetLocation(locationPrompt, out x, out y);
            GetRange(rangePrompt, out range);


            List<(int, int)> affectedCoordinates = Coverage(x, y, range);

            foreach ((int newX, int newY) in affectedCoordinates)
            {
                Obstacle sensor = new Sensor(newX, newY, range);
                AddObstacle(grid, sensor, affectedCoordinates);
            }

        }

        /// <summary>
        /// Calculates the coordinates affected by a sensors range.
        /// </summary>
        /// <param name="sensorX"></param>
        /// <param name="sensorY"></param>
        /// <param name="range"></param>
        /// <returns> A list of coordinates that are affected by the sensor. </returns>
        private static List<(int, int)> Coverage(int sensorX, int sensorY, float range)
        {
            List<(int, int)> affectedCoordinates = new List<(int, int)>();

            for (int i = -1 * (int)range; i <= (int)range; i++)
            {
                for (int j = -1 * (int)range; j <= (int)range; j++)
                {
                    int newX = sensorX + i;
                    int newY = sensorY + j;

                    double distance = Math.Sqrt(i * i + j * j);

                    if (distance <= range)
                    {
                        affectedCoordinates.Add((newX, newY));
                    }
                }
            }
            return affectedCoordinates;
        }
    }
}
