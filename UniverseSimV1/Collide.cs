using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    static class Collide
    {
        /// <summary>
        /// return equels collide With
        /// </summary>
        /// <param name="clusterId"></param>
        /// <param name="flashVector"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static int CheckCollide(int clusterId,short[] flashVector,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId && Helper.SafeCoords(new int[2] {i + flashVector[0],j + flashVector[1] },map.Height,map.Width))
                    {
                        if (map.map[i + flashVector[0], j + flashVector[1]].BaseMass != 0 && map.map[i + flashVector[0], j + flashVector[1]].ClusterId != clusterId)
                        {
                            return map.map[i + flashVector[0], j + flashVector[1]].ClusterId;
                        }
                    }
                }
            }
            return -1;
        }
        private static int[] CheckCollideWith(int clusterId, short[] flashVector, Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId && Helper.SafeCoords(new int[2] { i + flashVector[0], j + flashVector[1] }, map.Height, map.Width))
                    {
                        for(int k = -1;k < 2;k += 2)
                        {
                            for (int h = -1; h < 2; h += 2)
                            {
                                if(map.map[i + flashVector[0], j + flashVector[1]].mass > 0 && map.map[i + flashVector[0], j + flashVector[1]].ClusterId != clusterId)
                                {
                                    return new int[2] { i, j };
                                }
                            }
                        }
                    }
                }
            }
            return CheckCollideWith(clusterId, map);
        }
        private static int[] CheckCollideWith(int clusterId, Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId)
                    {
                        for (int k = -1; k < 2; k += 2)
                        {
                            if (Helper.SafeCoords(new int[2] { i + k, j }, map.Height, map.Width))
                            {
                                if (map.map[i + k, j].mass > 0 && map.map[i + k, j].ClusterId != clusterId)
                                {
                                    return new int[2] { i + k, j };
                                }
                            }
                            if (Helper.SafeCoords(new int[2] { i, j + k }, map.Height, map.Width))
                            {
                                if (map.map[i, j + k].mass > 0 && map.map[i, j + k].ClusterId != clusterId)
                                {
                                    return new int[2] { i, j + k };
                                }
                            }
                        }
                    }
                }
            }
            throw null;
        }
        public static void CollisionFluidLike(int clusterId1,int[] cluster1VelocityMatrixRelativeLeft,short[] flashVector,int clusterId2,Map map)
        {
            short[] vector = flashVector;
            int[] velocity = cluster1VelocityMatrixRelativeLeft;
            velocity[0] /= 2;
            velocity[1] /= 2;
            CollisionFluidLike(clusterId1, vector, velocity,map);
            /*
            vector[0] *= -1;
            vector[1] *= -1;
            velocity[0] *= -1;
            velocity[1] *= -1;
            CollisionFluidLike(clusterId2, vector, velocity,map);
            */
        }
        private static void CollisionFluidLike(int clusterId, short[] vector, int[] velocityMatrixRounded, Map map)
        {
            bool updating = true;
            while (updating)
            {
                updating = false;
                for (int i = 0; i < map.Height; i++)
                {
                    for (int j = 0; j < map.Width; j++)
                    {
                        if (map.map[i, j].ClusterId == clusterId)
                        {
                            updating = true;
                            map.map[i, j].ClusterId = -2048;
                            MoveTile(new int[2] { i, j }, velocityMatrixRounded, map);
                        }
                    }
                }
            }
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == -2048)
                    {
                        map.map[i, j].ClusterId = clusterId;
                    }
                }
            }
        }
        private static void MoveTile(int[] coords,int[] velocityMatrixRoundedActual,Map map)
        {
            int[] velocity = velocityMatrixRoundedActual;
            int distance = Helper.GetDistance(velocity);
            int[] flashCoords = coords;
            short[] flashVector;
            for(int i = 0;i < distance;i++)
            {
                flashVector = Helper.GetFlashVector(velocity);
                velocity[0] -= flashVector[0];
                velocity[1] -= flashVector[1];
                if(Helper.SafeCoords(new int[2] { flashCoords[0] + flashVector[0], flashCoords[1] + flashVector[1] }, map.Height, map.Width))
                {
                    if (map.IsObject(flashCoords, flashVector))
                    {
                        CollideTile(flashCoords, new int[2] { flashCoords[0] + flashVector[0], flashCoords[1] + flashVector[1] }, map);
                    }
                    else
                    {
                        map.tile(flashCoords, flashVector).SetTile(map.tile(flashCoords));
                        map.tile(flashCoords).SetTile(new Tile());
                    }
                }
                else
                {
                    map.tile(flashCoords).SetTile(new Tile());
                    return;
                }
                flashCoords[0] -= flashVector[0];
                flashCoords[1] -= flashVector[1];
                if(!Helper.SafeCoords(new int[2] { flashCoords[0], flashCoords[1] }, map.Height, map.Width))
                {
                    return;
                }
            }
        }
        private static void CollideTile(int[] coords1,int[] coords2,Map map)
        {
            if(map.tile(coords1).BaseMass == map.tile(coords2).BaseMass)
            {
                CollideTilePressure(coords1, coords2,map);
                return;
            }
            CollideTileElastic(coords1, coords2,map);
        }
        /// <summary>
        /// needs work
        /// </summary>
        /// <param name="coords1"></param>
        /// <param name="coords2"></param>
        private static void CollideTilePressure(int[] coords1,int[] coords2,Map map)
        {
            double[] forceRelative = Helper.GetForce(map.tile(coords1), map.tile(coords2));
            int transferAmount = Convert.ToInt32(Math.Round(Helper.AddValues(forceRelative)));
            if(map.tile(coords1).mass <= transferAmount)
            {
                map.tile(coords2).pressure += map.tile(coords1).pressure;
                map.tile(coords2).velocity[0] += map.tile(coords1).velocity[0];
                map.tile(coords2).velocity[1] += map.tile(coords1).velocity[1];
                map.tile(coords1).SetTile(new Tile());
                return;
            }
            else
            {
                map.tile(coords2).pressure += transferAmount;
                map.tile(coords1).pressure -= transferAmount;
                CollideTileElastic(coords1, coords2,map);
                return;
            }
            
        }
        private static void CollideTileElastic(int[] coords1,int[] coords2,Map map)
        {
            //baseValues
            double[] velocity1 = map.tile(coords1).velocity;
            double[] force2 = Helper.GetForce(map.tile(coords2));
            double[] forceTotal = Helper.GetForce(map.tile(coords1));
            forceTotal[0] += force2[0];
            forceTotal[1] += force2[1];
            int mass2 = map.tile(coords2).mass;
            CollideTileElastic(coords1, coords2, map, velocity1, forceTotal, mass2);
        }
        private static void CollideTileElastic(int[] coords1, int[] coords2, Map map, double[] velocity1,double[] forceTotal, int mass2)
        {
            if (forceTotal[0] / mass2 >= velocity1[0] && forceTotal[1] / mass2 >= velocity1[0])//extra force or equel
            {
                map.tile(coords2).velocity = velocity1;
                double[] force1 = forceTotal;
                force1[0] -= velocity1[0] * mass2;
                force1[1] -= velocity1[1] * mass2;
                map.tile(coords1).velocity = Helper.GetVelocity(force1, map.tile(coords1).mass);
                return;
            }
            //less force
            map.tile(coords2).velocity = Helper.GetVelocity(forceTotal, map.tile(coords1).mass);
            map.tile(coords1).velocity = new double[2];
        }
    }
}
