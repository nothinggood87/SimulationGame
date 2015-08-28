using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    static class Gravity
    {
        /// <summary>
        /// Gravitational constant;(place holder value)
        /// </summary>
        private const double G = 0.001;
        public static void UpdateGravity(Map map)
        {
            for(int i = 0;i < map.Height;i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].mass != 0)
                    {
                        UpdateGravity(new int[2] { i, j }, map);
                    }
                }
            }
        }
        private static void UpdateGravity(int[] coords,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].mass != 0)
                        UpdateGravity(coords, new int[2] { i, j }, map);
                }
            }
        }
        private static void UpdateGravity(int[] coords1, int[] coords2, Map map)
        {
            double[] gravity = GetGravity(Helper.GetDisplacement(coords1, coords2), map.tile(coords1).mass, map.tile(coords2).mass);
            double[] addedVelocity = Helper.GetVelocity(gravity, map.tile(coords1).mass);
            map.tile(coords1).velocity[0] += addedVelocity[0] / 2;
            map.tile(coords1).velocity[1] += addedVelocity[0] / 2;
        }
        private static double[] GetGravity(int[] displacement, int mass1, int mass2)
        {
            
            double[] gravity = new double[2];
            if(displacement[0] != 0)
            {
                gravity[0] = G * (mass1 * mass2 / displacement[0]);
            }
            if (displacement[1] != 0)
            {
                gravity[1] = G * (mass1 * mass2 / displacement[1]);
            }
            return gravity;
        }
    }
}
