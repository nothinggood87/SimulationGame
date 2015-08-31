using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    static class Helper
    {
        public static int[] GetDisplacement(int[] coords1, int[] coords2) => new int[2]
            {
                coords2[0] - coords1[0],
                coords2[1] - coords1[1]
            };
        public static double[] GetDisplacement(double[] coords1, double[] coords2) => new double[2]
            {
                coords2[0] - coords1[0],
                coords2[1] - coords1[1]
            };
        public static int GetDistance(int[] coords1, int[] coords2) => GetDistance(GetDisplacement(coords1, coords2));
        public static int GetDistance(int[] displacement) => GetPositive(displacement[0]) + GetPositive(displacement[1]);
        public static double GetDistance(double[] coords1, double[] coords2) => GetDistance(GetDisplacement(coords1, coords2));
        public static double GetDistance(double[] displacement) => GetPositive(displacement[0]) + GetPositive(displacement[1]);
        public static int GetPositive(int value)
        {
            if (value < 0) { return value * -1; }
            return value;
        }
        public static double GetPositive(double value)
        {
            if (value < 0) { return value * -1; }
            return value;
        }
        public static short[] GetFlashVector(int[] velocity)
        {
            short[] flashVector = new short[2];
            if (velocity[0] != 0) { flashVector[0] = Convert.ToInt16(velocity[0] / GetPositive(velocity[0])); }
            if (velocity[1] != 0) { flashVector[1] = Convert.ToInt16(velocity[1] / GetPositive(velocity[1])); }
            if(flashVector[0] != 0 && flashVector[1] != 0)
            {
                if(GetPositive(velocity[0]) >= GetPositive(velocity[1]))
                {
                    flashVector[1] = 0;
                }
                else// <
                {
                    flashVector[0] = 0;
                }
            }
            return flashVector;
        }
        public static bool SafeCoords(int[] coords, short[] vector, int mapHeight, int mapWidth) =>
            SafeCoords(new int[2] { coords[0] + vector[0], coords[1] + vector[1] }, new int[2] { mapHeight, mapWidth });
        public static bool SafeCoords(int[] coords, int mapHeight, int mapWidth) => SafeCoords(coords, new int[2] { mapHeight, mapWidth });
        public static bool SafeCoords(int[] coords, int[] mapSize)
        {
            for (int i = 0; i < 2; i++)
            {
                if (coords[i] >= mapSize[i]) { return false; }
                if (coords[i] < 0) { return false; }
            }
            return true;
        }
        public static int[] GetRoundedVelocity(Tile tile) => GetRoundedVelocity(tile.velocity);
        public static int[] GetRoundedVelocity(double[] velocity)
        {
            int[] roundedVelocity = new int[2];
            if (velocity[0] > 0)
            {
                roundedVelocity[0] = Convert.ToInt32(Math.Floor(velocity[0]));
            }
            else
            {
                roundedVelocity[0] = Convert.ToInt32(Math.Ceiling(velocity[0]));
            }
            if (velocity[1] > 0)
            {
                roundedVelocity[1] = Convert.ToInt32(Math.Floor(velocity[1]));
            }
            else
            {
                roundedVelocity[1] = Convert.ToInt32(Math.Ceiling(velocity[1]));
            }
            return roundedVelocity;
        }
        public static double[] GetVelocity(double[] force, int mass) => new double[2]
        {
            force[0] / mass,
            force[1] / mass
        };
        public static double[] GetForce(Tile tile) => GetForce(tile.velocity, tile.mass);
        public static double[] GetForce(double[] velocity, int mass) => new double[2]
        {
            velocity[0]*mass,
            velocity[1]*mass
        };
        /// <summary>
        /// gets tile1's relative Force to tile2.  
        /// </summary>
        /// <param name="tile1"></param>
        /// <param name="tile2"></param>
        /// <returns></returns>
        public static double[] GetForce(Tile tile1, Tile tile2)
        {
            double[] getForce = GetForce(tile1);
            double[] otherForce = GetForce(tile2);
            getForce[0] -= otherForce[0];
            getForce[1] -= otherForce[1];
            return getForce;
        }
        //addValues
        /// <summary>
        /// value1 = double[2];
        /// value2 = double[2];
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double[] AddValues(double[] value1, double[] value2) => new double[2]
        {
            value1[0] + value2[0],
            value1[1] + value2[1]
        };
        public static double AddValues(double[] value) => value[0] + value[1];
        /// <summary>
        /// value1 = int[2];
        /// value2 = int[2];
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static int[] AddValues(int[] value1, int[] value2) => new int[2]
        {
            value1[0] + value2[0],
            value1[1] + value2[1]
        };
        public static Fraction[] GetVectorComplexe(int[] displacement)
        {
            int divider = GetPositive(displacement[0]) + GetPositive(displacement[1]);
            return new Fraction[2] { new Fraction(displacement[0], divider), new Fraction(displacement[1], divider) };
        }
    }
}
