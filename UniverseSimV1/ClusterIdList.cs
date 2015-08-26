using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    class ClusterIdList
    {
        public void Reset()
        {
            clusterIdListSize = 0;
            clusterIdList = new bool[0];
        }
        private bool[] clusterIdList;
        public bool clusterId(int number) => clusterIdList[number];
        public int clusterIdListSize { get; private set; }
        public int GetNewClusterId()
        {
            for (int i = 0; i < clusterIdListSize; i++)
            {
                if (!clusterIdList[i])
                {
                    clusterIdList[i] = true;
                    return i;
                }
            }
            return CreateClusterId();
        }
        private int CreateClusterId()
        {
            bool[] copy = clusterIdList;
            clusterIdList = new bool[clusterIdListSize + 1];
            for (int i = 0; i < clusterIdListSize; i++)
            {
                clusterIdList[i] = copy[i];
            }
            clusterIdList[clusterIdListSize] = true;
            clusterIdListSize++;
            return clusterIdListSize - 1;
        }
        private void debug(string input) { /*System.Windows.MessageBox.Show(input);*/}
    }
}
