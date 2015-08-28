using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UniverseSimV1
{
    static class GameWindow
    {
        public static void Update(Grid grid, Map map)
        {
            grid.Children.Clear();
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Update(grid, map, new int[2] { i, j });
                }
            }
        }
        private static void Update(Grid grid,Map map,int[] coords)
        {
            Image image = new Image();
            image = TileTypes.GetTexture(map.tile(coords));
            Grid.SetRow(image, coords[0]);
            Grid.SetColumn(image, coords[1]);
            grid.Children.Add(image);
        }
        public static void Build(Window window, Grid grid, Map map)
        {
            window.WindowStyle = WindowStyle.None;
            window.Height = map.Height * TileTypes.textureSize;
            window.Width = map.Width * TileTypes.textureSize;
            grid.ShowGridLines = true;
            ColumnDefinition columnDefinition;
            for (int i = 0; i < map.Height; i++)
            {
                columnDefinition = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            RowDefinition rowDefinition;
            for (int j = 0; j < map.Width; j++)
            {
                rowDefinition = new RowDefinition();
                grid.RowDefinitions.Add(rowDefinition);
            }
            Update(grid, map);
            window.Content = grid;
        }
        private static void Build(Grid grid, Map map)
        {
            grid = new Grid();
            ColumnDefinition columnDefinition;
            for (int i = 0; i < map.Height; i++)
            {
                columnDefinition = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            RowDefinition rowDefinition;
            for (int j = 0; j < map.Width; j++)
            {
                rowDefinition = new RowDefinition();
                grid.RowDefinitions.Add(rowDefinition);
            }
            Update(grid, map);
        }
    }
}
