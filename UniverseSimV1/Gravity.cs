using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    /// <summary>
    /// unable to fix(I think it is 2d gravity that is the problem)
    /// </summary>
    static class Gravity
    {
        /// <summary>
        /// Gravitational constant;(place holder value)
        /// </summary>
        private const double G = 0.0001;
        public static void UpdateGravity(Map map)
        {
            for (int i = 0;i < map.Height;i++)
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
                    if (map.map[i, j].mass != 0 && (coords[0] != i || coords[1] != j))
                    {
                        UpdateGravity(coords, new int[2] { i, j }, map);
                    }
                }
            }
        }
        private static void UpdateGravity(int[] coords1, int[] coords2, Map map)
        {
            int[] displacement = Helper.GetDisplacement(coords1, coords2);
            double gravity = GetGravity(Helper.GetDistance(displacement), map.tile(coords2).mass);
            Fraction[] vectorComplexe = Helper.GetVectorComplexe(displacement);
            vectorComplexe[0].Value *= gravity;
            vectorComplexe[1].Value *= gravity;
            map.tile(coords1).velocity[0] += vectorComplexe[0].Value;
            map.tile(coords1).velocity[1] += vectorComplexe[1].Value;
        }
        private static double GetGravity(int distance, int mass2)
        {
            if (distance > 0)
            {
                return G * (mass2 / Convert.ToDouble(distance));
            }
            return 0;
        }
    }
}
