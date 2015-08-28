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
        public ClusterIdList ClusterIdList = new ClusterIdList();
        public Tile[,] map;
        public int Height { get; }
        public int Width { get; }
        public Map()
            :this(25,25)
        {
        }
        public Map(Map newMap)
        {
            Height = newMap.Height;
            Width = newMap.Width;
            SetMap(newMap);
        }
        public Map(int mapHeight,int mapWidth)
        {
            Height = mapHeight;
            Width = mapWidth;
            map = new Tile[Height, Width];
            CleanMap();
        }
        public void CleanMap()
        {
            for(int i = 0;i < Height;i++)
            {
                for(int j = 0;j < Width;j++)
                {
                    map[i, j] = new Tile();
                }
            }
        }
        public void HasMoved(bool newValue)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    map[i, j].HasMoved = newValue;
                }
            }
        }
        public void InternalTick()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
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
            tile.IsPlayer = true;
            PlaceOneTile(coords, tile);
        }
        /// <summary>
        /// cannot change Height/Width
        /// </summary>
        /// <param name="newMap"></param>
        private void SetMap(Map newMap)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    map[i, j].SetTile(newMap.map[i, j]);
                }
            }
            ClusterIdList.SetClusterIdList(newMap.ClusterIdList);
        }
    }
}
