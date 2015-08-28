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
            InitializeComponent();
            world = new Map();
            world.PlaceOneTile(new int[2] { 2, 2 });
            //world.start();
            world.PlaceOneTile(new int[2] { 4, 4 });
            world.PlacePlayer(new int[2] { 20, 20 },5);
            world.PlaceOneTile(new int[2] { 4, 0 }, new Tile(2, new double[2] { 0, 20 }));
            world.PlaceOneTile(new int[2] { 13, 13 }, new Tile(1000));
            Input player = new Input(world);
            //tileTypes.GetImage needs work!
        }
    }
    // Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.  
// Replace the code in Program.cs with this code.  

        

// To avoid confusion with other Timer classes, this sample always uses the fully-qualified 
// name of System.Timers.Timer instead of a using statement for System.Timers. 

public class Example
    {
        private static System.Timers.Timer aTimer;

        public static void Mainer()
        {
            // Normally, the timer is declared at the class level, so that it stays in scope as long as it 
            // is needed. If the timer is declared in a long-running method, KeepAlive must be used to prevent 
            // the JIT compiler from allowing aggressive garbage collection to occur before the method ends. 
            // You can experiment with this by commenting out the class-level declaration and uncommenting  
            // the declaration below; then uncomment the GC.KeepAlive(aTimer) at the end of the method.         
            //System.Timers.Timer aTimer; 

            // Create a timer and set a two second interval.
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 2000;

            // Alternate method: create a Timer with an interval argument to the constructor. 
            //aTimer = new System.Timers.Timer(2000); 

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();

            // If the timer is declared in a long-running method, use KeepAlive to prevent garbage collection 
            // from occurring before the method ends.  
            //GC.KeepAlive(aTimer) 
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }

    // This example displays output like the following:  
    //       Press the Enter key to exit the program at any time...  
    //       The Elapsed event was raised at 5/20/2015 8:48:58 PM  
    //       The Elapsed event was raised at 5/20/2015 8:49:00 PM  
    //       The Elapsed event was raised at 5/20/2015 8:49:02 PM  
    //       The Elapsed event was raised at 5/20/2015 8:49:04 PM  
    //       The Elapsed event was raised at 5/20/2015 8:49:06 PM
}
