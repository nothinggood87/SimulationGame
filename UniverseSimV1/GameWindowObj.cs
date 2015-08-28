using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Threading;

namespace UniverseSimV1
{
    class GameWindowObj
    {
        //private delegate void updtWindow(Grid grid,Map map);
        //private updtWindow Update = new updtWindow(GameWindow.Update);
        private Grid WindowGrid;
        private Window GWindow;
        private Map GameMap = new Map();
        private Thread RefreshGameMap;
        private bool RefreshingGameMap = false;
        private bool notRunning = true;
        public GameWindowObj(Map map)
        {
            GWindow = new Window();
            WindowGrid = new Grid();
            GameMap = map;
            RefreshGameMap = new Thread(() =>
            {
                RefreshingGameMap = true;
                notRunning = false;
                while (RefreshingGameMap)
                {
                    Thread.Sleep(50);
                    Update(this,new EventArgs());
                }
                notRunning = true;
            });
            GameWindow.Build(GWindow, WindowGrid, GameMap);
            WindowGrid.ShowGridLines = true;
            Update += (s, e) => {
                GWindow.Dispatcher.Invoke((Action)delegate () { UpdateWindow(); });
            };
        }
        public void Show() => GWindow.Show();
        public void Start()
        {
            if (!RefreshingGameMap && notRunning)
            {
                RefreshGameMap.Start();
            }
        }
        public void Stop() => RefreshingGameMap = false;
        public void UpdateWindow() => GameWindow.Update(WindowGrid, GameMap);
        private event EventHandler Update;
    }
}
