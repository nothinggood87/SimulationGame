using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverseSimV1
{
    static class Move
    {
        public static void OneTick(Map map) => OneTick(map, new double[2]);
        public static void OneTick(Map map,double[] playerAcceleration)
        {
            Update(map);
            UpdatePlayer(playerAcceleration,map);
            Debug.Tick();
        }
        private static void Update(Map map)
        {
            Cluster.SetClusterId(map);
            map.HasMoved(false);
            map.InternalTick();
            for (int i = 0; i < map.ClusterIdList.clusterIdListSize; i++)
            {
                if(map.ClusterIdList.clusterId(i))
                {
                    Update(i, map);
                }
            }
        }
        private static void Update(int clusterId,Map map) => Update(clusterId,Cluster.GetClusterVelocityRounded(clusterId,map),map);
        private static void Update(int clusterId, int[] velocity,Map map)
        {
            int[] velocityOriginal = velocity;
            int[] velocityLeft = velocityOriginal;
            int distance = Helper.GetDistance(velocityLeft);
            if (distance > 0)
            {
                short[] flashVector;
                bool stillMoving = true;
                int clusterId2;
                for (int i = 0; i < distance; i++)
                {
                    flashVector = Helper.GetFlashVector(velocityLeft);
                    if (stillMoving && Helper.GetPositive(flashVector[0]) + Helper.GetPositive(flashVector[1]) != 0)
                    {
                        velocityLeft[0] -= flashVector[0];
                        velocityLeft[1] -= flashVector[1];
                        clusterId2 = Collide.CheckCollide(clusterId, flashVector,map);
                        stillMoving = clusterId2 == -1;
                        if (stillMoving)
                        {
                            MoveClusterOneTile(clusterId, flashVector,map);
                        }
                        else
                        {
                            Collide.CollisionFluidLike(clusterId, velocityLeft,flashVector,clusterId2 , map);
                            int[] newVelocity = Cluster.GetClusterVelocityRounded(clusterId,map);
                            for (int h = 0; h < 2; h++)
                            {
                                if (newVelocity[h] != 0)
                                {
                                    if (Helper.GetPositive(newVelocity[h]) + Helper.GetPositive(velocityLeft[h] - velocityOriginal[h]) > 0)
                                    {
                                        stillMoving = true;
                                        velocityLeft[h] += newVelocity[h] - velocityOriginal[h];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private static void MoveClusterOneTile(int clusterId, short[] flashVector, Map map)
        {
            // copy map
            Map copy = new Map(map.Height, map.Width);
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId && map.map[i, j].mass > 0)
                    {
                        copy.map[i, j].SetTile(map.map[i, j]);
                    }
                }
            }
            // delete
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId && map.map[i, j].mass != 0)
                    {
                        if (!(map.map[i, j].IsPlayer && !Helper.SafeCoords(new int[2] { i + flashVector[0], j + flashVector[1] }, map.Height, map.Width)))
                        {
                            map.map[i, j] = new Tile();
                        }
                    }
                }
            }
            // paste
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (copy.map[i, j].mass > 0 && copy.map[i, j].ClusterId == clusterId)
                    {
                        if (Helper.SafeCoords(new int[2] { i + flashVector[0], j + flashVector[1] }, map.Height, map.Width))
                        {
                            map.map[i + flashVector[0], j + flashVector[1]].SetTile(copy.map[i, j]);
                        }
                    }
                }
            }
        }
        private static void UpdatePlayer(double[] playerAcceleration,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].IsPlayer)
                    {
                        UpdatePlayer(new int[2] { i, j },playerAcceleration,map);
                    }
                }
            }
        }
        private static void UpdatePlayer(int[] coords,double[] playerAcceleration,Map map)
        {
            map.tile(coords).velocity[0] += playerAcceleration[0];
            map.tile(coords).velocity[1] += playerAcceleration[1];
            int[] velocity = Helper.GetRoundedVelocity(playerAcceleration);
            playerAcceleration = new double[2];
            int distance = Helper.GetDistance(velocity);
            short[] flashVector;
            int[] flashCoords = coords;
            for (int i = 0; i < distance; i++)
            {
                flashVector = Helper.GetFlashVector(velocity);
                UpdatePlayer(flashCoords, flashVector,map);
                velocity[0] -= flashVector[0];
                velocity[1] -= flashVector[1];
                flashCoords[0] += flashVector[0];
                flashCoords[1] += flashVector[1];
                playerAcceleration[0] = 0;
                playerAcceleration[1] = 0;
            }
        }
        private static void UpdatePlayer(int[] coords,short[] flashVector,Map map)
        {
            if (Helper.SafeCoords(coords, flashVector, map.Height, map.Width))
            {
                if(map.tile(coords, flashVector).mass == 0)
                {
                    map.tile(coords, flashVector).SetTile(map.tile(coords));
                    map.tile(coords).SetTile(new Tile());
                }
            }
        }
    }
}
