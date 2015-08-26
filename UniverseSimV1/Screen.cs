using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniverseSimV1
{
    class Screen
    {
        private bool started = false;
        private Window window;
        private Grid grid;
        private int[,] gridMap;
        private int gridHeight { get; }
        private int gridWidth { get; }
        private const bool showGridLines = true;
        public Screen(Map map)
        {
            window = new Window();
            gridHeight = map.height;
            gridWidth = map.width; 
            window.Height = gridHeight * TileTypes.textureSize;
            window.Width = gridWidth * TileTypes.textureSize;
            ConstructGrid();
            UpdateGridMap(map);
            UpdateGrid();
            grid.ShowGridLines = showGridLines;
            window.WindowStyle = WindowStyle.None;
            window.Content = grid;
            if(!started)
            {
                started = true;
                window.Show();
            }
        }
            
        public void Update(Map map)
        {
            UpdateGridMap(map);
            UpdateGrid();
            grid.ShowGridLines = showGridLines;
            window.WindowStyle = WindowStyle.None;
            window.Content = grid;
        }
        private void UpdateGridMap(Map map)
        {
            int Id;
            for(int i = 0;i < gridHeight;i++)
            {
                for(int j = 0;j < gridWidth;j++)
                {
                    Id = map.map[i, j].mass;
                    if (map.map[i, j].isPlayer)
                    {
                        gridMap[i, j] = (int)TileTypes.textures.player;
                    }
                    else if (Id >= (int)TileTypes.textures.player)
                    {
                        gridMap[i, j] = (int)TileTypes.textures.player - 1;
                    }
                    else
                    {
                        gridMap[i, j] = Id;
                    }
                }
            }
        }
        private void UpdateGrid()
        {
 
            Image image;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    image = new Image();
                    image = TileTypes.GetTexture(gridMap[i, j]);
                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);
                    grid.Children.Add(image);
                }
            }
        }
        public void ConstructGrid()
        {
            grid = new Grid();
            grid.ShowGridLines = showGridLines;
            gridMap = new int[gridHeight, gridWidth];
            RowDefinition height;
            ColumnDefinition width;
            
            for (int i = 0; i < gridWidth; i++)
            {
                width = new ColumnDefinition();
                width.MaxWidth = TileTypes.textureSize;
                width.MinWidth = TileTypes.textureSize;
                grid.ColumnDefinitions.Add(width);
            }
            for (int i = 0; i < gridHeight; i++)
            {
                height = new RowDefinition();
                height.MaxHeight = TileTypes.textureSize;
                height.MinHeight = TileTypes.textureSize;
                grid.RowDefinitions.Add(height);
            }
        }
    }
}
