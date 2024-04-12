using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_2_Final
{
    public class UI
    {
        enum ObstacleOption
        {
            Guard,
            Fence,
            Sensor,
            Camera,
            AttackDog

        }

        enum MenuOption
        {
            Show_safe_directions,
            Display_obstacle_map,
            Find_safe_path,
            Exit
        }

        /// <summary>
        /// Displays menu options
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("Select one of the following options");

            // Display obstacle options
            foreach (ObstacleOption option in Enum.GetValues(typeof(ObstacleOption)))
            {
                char shortcut = char.ToLower(option.ToString()[0]);
                Console.WriteLine($"{shortcut}) Add '{option}' obstacle");
            }

            char[] charS = { 'd', 'm', 'p', 'x' };

            // Display other options using the char array
            for (int i = 0; i < charS.Length; i++)
            {
                char shortcut = charS[i];
                MenuOption option = (MenuOption)i;
                Console.WriteLine($"{shortcut}) {option.ToString().Replace("_", " ")}");
            }

            Console.WriteLine("Enter code: ");
        }

        /// <summary>
        /// Prompt user to get location coords.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void getLocation(string prompt, out int x, out int y)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string[] locationInput = Console.ReadLine().Split(',');

                    if (!Obstacle.IsValidLocationInput(locationInput, out x, out y))
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

        }
    }
}
