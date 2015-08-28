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
using System.Timers;


namespace UniverseSimV1
{
    class Input
    {
        private GameWindowObj window;
        private Map map;
        public Input(Map worldMap)
        {
            map = worldMap;
            input = new Window();
            input.Height = height;
            input.Width = width;
            Construct();
            input.Show();
            window = new GameWindowObj(map);
            window.Show();
            Updating = new System.Threading.Thread(() =>
            {
                running = true;
                while(!pause)
                {
                    System.Threading.Thread.Sleep(100);
                    Tick();
                }
                running = false;
            });
        }
        public System.Threading.Thread Updating;
        public int[] playerAcceleration { get; private set; } = new int[2];
        public void playerAccelerationReset() { playerAcceleration = new int[2]; }
        private Window input;
        private Grid grid;
        private Button[,] buttons;
        private int height => 192;
        private int width => 192;
        private short gridHeight => 3;
        private short gridWidth => 3;
        private void Construct()
        {
            ConstructButtons();
            ConstructGrid();
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    grid.Children.Add(buttons[i, j]);
                }
            }
            input.Content = grid;
        }
        private void ConstructButtons()
        {
            buttons = new Button[3, 3];
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    buttons[i, j] = new Button();
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                }
            }
            buttons[0, 0].Content = "Start";
            buttons[0, 1].Content = "Up";
            buttons[0, 2].Content = "Stop";

            buttons[1, 0].Content = "Left";
            buttons[1, 1].Content = "Down";
            buttons[1, 2].Content = "Right";

            buttons[2, 0].Content = "Play";
            buttons[2, 1].Content = "Blank";
            buttons[2, 2].Content = "Blank";


            buttons[0, 0].Click += InputStart;
            buttons[0, 1].Click += InputUp;
            buttons[0, 2].Click += InputStop;

            buttons[1, 0].Click += InputLeft;
            buttons[1, 1].Click += InputDown;
            buttons[1, 2].Click += InputRight;

            buttons[2, 0].Click += InputTogglePause;
            buttons[2, 1].Click += InputBlank;
            buttons[2, 2].Click += InputBlank;
        }
        private void ConstructGrid()
        {
            grid = new Grid();
            RowDefinition height;
            ColumnDefinition width;

            for (int i = 0; i < gridWidth; i++)
            {
                width = new ColumnDefinition();
                grid.ColumnDefinitions.Add(width);
            }
            for (int i = 0; i < gridHeight; i++)
            {
                height = new RowDefinition();
                grid.RowDefinitions.Add(height);
            }
        }
        private void InputStart(object sender, RoutedEventArgs e)
        {
            window.Start();
        }
        private void InputUp(object sender, RoutedEventArgs e)
        {
            playerAcceleration[0]--;
        }
        private void InputStop(object sender, RoutedEventArgs e)
        {
            window.Stop();
        }

        private void InputLeft(object sender, RoutedEventArgs e)
        {
            playerAcceleration[1]--;
        }
        private void InputDown(object sender, RoutedEventArgs e)
        {
            playerAcceleration[0]++;
        }
        private void InputRight(object sender, RoutedEventArgs e)
        {
            playerAcceleration[1]++;
        }
        private bool pause = true;
        private bool running = false;
        private void InputTogglePause(object sender, RoutedEventArgs e)
        {
            pause = !pause;
            System.Threading.Thread.Sleep(100);
            if(!pause && !running)
            {
                Updating.Start();
                buttons[2, 0].Content = "Pause";
                return;
            }
            buttons[2, 0].Content = "Play";

        }
        private void InputBlank(object sender, RoutedEventArgs e)
        {
            window.UpdateWindow();
        }
        private void Tick()
        {
            Move.OneTick(map);
            Gravity.UpdateGravity(map);
        }
    }
}
