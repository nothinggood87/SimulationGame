using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace UniverseSimV1
{
    class GameWindowObj
    {
        private Image[,] ImageGrid;
        private Window GWindow;
        private Map GameMap = new Map();
        private Timer RefreshGameMap = new Timer(90);
        private bool notRunning = true;
        public GameWindowObj(Map map)
        {
            ImageGrid = new Image[map.Height,map.Width];
            GWindow = new Window();
            GWindow.Background = System.Windows.Media.Brushes.Black;
            GameMap = map;
            RefreshGameMap.Elapsed += tick;
            GameWindow.Build(GWindow, ImageGrid, TileTypes.GetProcessedIds(GameMap), GameMap.Height, GameMap.Width);
        }
        private void tick(object sender,ElapsedEventArgs e)
        {
            if (notRunning)
            {
                notRunning = false;
                ProcessedIdsMap = TileTypes.GetProcessedIds(GameMap);
                if (PrevProcessedIdsMap == null)
                {
                    PrevProcessedIdsMap = ProcessedIdsMap;
                }
                GameWindow.Update(ImageGrid, ProcessedIdsMap, PrevProcessedIdsMap, GameMap.Height, GameMap.Width, GWindow);
                PrevProcessedIdsMap = ProcessedIdsMap;
                UpdatingWindowInProgress = false;
                notRunning = true;
                Debug.Frame();
            }
        }
        public void Start()
        {
            notRunning = true;
            RefreshGameMap.Start();
        }
        public void Stop()
        {
            RefreshGameMap.Stop();
        }
        public bool UpdatingWindowInProgress = false;
        public void Show() => GWindow.Show();
        private int[,] ProcessedIdsMap;
        private int[,] PrevProcessedIdsMap;
    }
}
