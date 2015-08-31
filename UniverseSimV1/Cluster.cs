using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    static class Cluster
    {
        public static void SetClusterHasMoved(int clusterId,bool hasMoved,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if(map.map[i,j].ClusterId == clusterId)
                    {
                        map.map[i, j].HasMoved = hasMoved;
                    }
                }
            }
        }
        public static int[] GetClusterVelocityRounded(int clusterId,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId)
                    {
                        return map.map[i, j].VelocityRoundedActual();
                    }
                }
            }
            System.Windows.MessageBox.Show("Cluster.GetClusterVelocityMatrixRounded.size = 0!");
            return new int[2];
        }
        public static void SetClusterId(Map map) => SetClusterId(map.ClusterIdList, map);
        public static void SetClusterId(ClusterIdList clusterIdList,Map map)
        {
            clusterIdList.Reset();
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    map.map[i, j].ClusterId = -1;                
                }
            }
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == -1 && map.map[i, j].BaseMass != 0)
                    {
                        SetClusterId(new int[2] { i, j }, clusterIdList.GetNewClusterId(),map);
                    }
                }
            }
        }
        public static void SetClusterId(int[] coords,ClusterIdList clusterIdList,Map map) => SetClusterId(coords,clusterIdList.GetNewClusterId(),map);
        public static void SetClusterId(int[] coords,int newClusterId,Map map)
        {
            bool[,] boolMap = GetBoolMapOfCluster(coords,map);
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (boolMap[i,j])
                    {
                        map.map[i, j].ClusterId = newClusterId;
                    }
                }
            }
        }
        public static void SetClusterId(int clusterId,int newClusterId,Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.map[i, j].ClusterId == clusterId)
                    {
                        map.map[i, j].ClusterId = newClusterId;
                    }
                }
            }
        }
        private static bool[,] GetBoolMapOfCluster(int[] coords,Map map)
        {
            int[] velocity = map.tile(coords).VelocityRoundedActual();
            int[] flashVelocity;
            bool stillFinding = true;
            bool[,] boolMap = new bool[map.Height, map.Height];
            boolMap[coords[0], coords[1]] = true;
            while(stillFinding)
            {
                stillFinding = false;
                for (int i = 0; i < map.Height; i++)
                {
                    for (int j = 0; j < map.Width; j++)
                    {
                        if (map.map[i, j].mass != 0)
                        {
                            flashVelocity = map.map[i, j].VelocityRoundedActual();
                            if (!boolMap[i, j] && flashVelocity[0] == velocity[0] && flashVelocity[1] == velocity[1])
                            {
                                if (IsAdjacent(boolMap, new int[2] { i, j },map.Height,map.Width))
                                {
                                    boolMap[i, j] = true;
                                    stillFinding = true;
                                }
                            }
                        }
                    }
                }
            }
            return boolMap;
        }
        private static bool IsAdjacent(bool[,] boolMap, int[] checkingCoords,int height,int width)
        {
            if(Helper.SafeCoords(checkingCoords,height,width))
            {
                return false;
            }
            if (boolMap[checkingCoords[0] + 1, checkingCoords[1]] || boolMap[checkingCoords[0], checkingCoords[1] + 1])
            {
                return true;
            }
            if (boolMap[checkingCoords[0] - 1, checkingCoords[1]] || boolMap[checkingCoords[0], checkingCoords[1] - 1])
            {
                return true;
            }
            return false;
        }
    }
}
