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
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    if(map.map[i,j].clusterId == clusterId)
                    {
                        map.map[i, j].hasMoved = hasMoved;
                    }
                }
            }
        }
        public static int[] GetClusterVelocityMatrixRounded(int clusterId,Map map)
        {
            int[] velocity = new int[2];
            int size = 0;
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    if (map.map[i, j].clusterId == clusterId)
                    {
                        velocity[0] += map.map[i, j].velocityRoundedActual[0];
                        velocity[1] += map.map[i, j].velocityRoundedActual[1];
                        size++;
                    }
                }
            }
            if(size == 0)
            {
                System.Windows.MessageBox.Show("Cluster.GetClusterVelocityMatrixRounded.size = 0!");
                return new int[2];
            }
            velocity[0] /= size;
            velocity[1] /= size;
            return velocity;
        }
        public static void SetClusterId(Map map) => SetClusterId(map.clusterIdList, map);
        public static void SetClusterId(ClusterIdList clusterIdList,Map map)
        {
            clusterIdList.Reset();
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    map.map[i, j].clusterId = -1;                
                }
            }
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    if (map.map[i, j].clusterId == -1 && map.map[i, j].baseMass != 0)
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
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    if (boolMap[i,j])
                    {
                        map.map[i, j].clusterId = newClusterId;
                    }
                }
            }
        }
        public static void SetClusterId(int clusterId,int newClusterId,Map map)
        {
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    if (map.map[i, j].clusterId == clusterId)
                    {
                        map.map[i, j].clusterId = newClusterId;
                    }
                }
            }
        }
        private static bool[,] GetBoolMapOfCluster(int[] coords,Map map)
        {
            int[] velocity = map.tile(coords).velocityRoundedActual;
            bool stillFinding = true;
            bool[,] boolMap = new bool[map.height, map.height];
            boolMap[coords[0], coords[1]] = true;
            while(stillFinding)
            {
                stillFinding = false;
                for (int i = 0; i < map.height; i++)
                {
                    for (int j = 0; j < map.width; j++)
                    {
                        if (map.map[i, j].mass != 0)
                        {
                            if (!boolMap[i, j] && map.map[i, j].velocityRoundedActual[0] == velocity[0] && map.map[i, j].velocityRoundedActual[1] == velocity[1])
                            {
                                if (IsAdjacent(boolMap, new int[2] { i, j }))
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
        private static bool IsAdjacent(bool[,] boolMap, int[] checkingCoords)
        {
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
