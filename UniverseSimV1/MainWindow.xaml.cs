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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void GameWindowObj(Grid grid, Map map);
        private Map world;
        public MainWindow()
        {
            Debug.StartCounter();
            //InitializeComponent();
            world = new Map(100,100);
            //world.PlaceOneTile(new int[2] { 2, 2 });
            //world.start();
            //world.PlaceOneTile(new int[2] { 4, 4 });
            //world.PlacePlayer(new int[2] { 20, 20 },1);
            //world.PlaceOneTile(new int[2] { 2, 2 }, new Tile(2, new double[2] { 0, 0.2 }));
            //world.PlaceOneTile(new int[2] { 4, 2 }, new Tile(2, new double[2] { 0, 0.4 }));
            //world.PlaceOneTile(new int[2] { 6, 2 }, new Tile(2, new double[2] { 0, 0.8 }));
            //world.PlaceOneTile(new int[2] { 8, 2 }, new Tile(2, new double[2] { 0, 1.6 }));
            world.PlaceOneTile(new int[2] { 50, 50 }, new Tile(10000));
            world.PlaceOneTile(new int[2] { 50, 45 }, new Tile(1, new double[2] { 0.5,0 }));
            //world.PlaceOneTile(new int[2] { 15, 10 });
            //world.PlaceOneTile(new int[2] { 10, 15 });
            //world.PlaceOneTile(new int[2] { 20, 15 });
            //world.PlaceOneTile(new int[2] { 15, 17 }, new Tile(1, new double[2] { -0.01, 0 }));
            Input player = new Input(world);
            //tileTypes.GetImage needs work!
            //gravity needs work
        }
    }
}
