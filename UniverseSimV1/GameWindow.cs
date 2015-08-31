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
        public static void Update(Image[,] imageGrid, int[,] processedIdsMap,int[,] prevProcessedIdsMap,int height,int width,Window window)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (processedIdsMap[i,j] != prevProcessedIdsMap[i,j])
                    {
                        window.Dispatcher.Invoke((Action)delegate () { imageGrid[i, j].Source = TileTypes.GetTexture(processedIdsMap[i, j]).Source; });
                    }
                }
            }
        }
        public static void Build(Window window,Image[,] imageGrid, int[,] processedIdsMap, int height, int width)
        {
            Grid grid = new Grid();
            grid.ShowGridLines = false;
            window.WindowStyle = WindowStyle.None;
            window.Height = height * TileTypes.textureSize;
            window.Width = width * TileTypes.textureSize;
            ColumnDefinition columnDefinition;
            for (int i = 0; i < height; i++)
            {
                columnDefinition = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            RowDefinition rowDefinition;
            for (int j = 0; j < width; j++)
            {
                rowDefinition = new RowDefinition();
                grid.RowDefinitions.Add(rowDefinition);
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    imageGrid[i, j] = TileTypes.GetTexture(processedIdsMap[i, j]);
                    Grid.SetColumn(imageGrid[i, j], i);
                    Grid.SetRow(imageGrid[i, j], j);
                    grid.Children.Add(imageGrid[i, j]);
                }
            }
            window.Content = grid;
        }
    }
}
