using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace UniverseSimV1
{
    class Map
    {
        //public void start() => physics.tick.Start();
        public ClusterIdList clusterIdList = new ClusterIdList();
        public Tile[,] map;
        public int height { get; }
        public int width { get; }
        public Map()
            :this(25,25)
        {
        }
        public Map(int mapHeight,int mapWidth)
        {
            height = mapHeight;
            width = mapWidth;
            map = new Tile[height, width];
            CleanMap();
        }
        public void CleanMap()
        {
            for(int i = 0;i < height;i++)
            {
                for(int j = 0;j < width;j++)
                {
                    map[i, j] = new Tile();
                }
            }
        }
        public void HasMoved(bool newValue)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j].hasMoved = newValue;
                }
            }
        }
        public void InternalTick()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j].InternalTick();
                }
            }
        }
        public bool IsObject(int[] coords, short[] vector) => map[coords[0] + vector[0], coords[1] + vector[1]].mass != 0;
        public bool IsObject(int[] coords) => map[coords[0], coords[1]].mass != 0;
        public Tile tile(int[] coords, short[] vector) => tile(new int[2] { coords[0] + vector[0], coords[1] + vector[1] });
        public Tile tile(int[] coords) => map[coords[0], coords[1]];
        //place
        public void PlaceOneTile(int[] coords) => PlaceOneTile(coords, new Tile(1));
        public void PlaceOneTile(int[] coords, Tile tile) => map[coords[0], coords[1]].SetTile(tile);
        public void PlacePlayer(int[] coords) => PlacePlayer(coords, 1);
        public void PlacePlayer(int[] coords, int mass)
        {
            Tile tile = new Tile(mass);
            tile.isPlayer = true;
            PlaceOneTile(coords, tile);
        }
    }
}
