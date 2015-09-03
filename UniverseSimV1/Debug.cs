using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Timers;

namespace UniverseSimV1
{
    static class Debug
    {
        public static double[] ReturnValue(double[] value)
        {
            if (value[0] != 0 || value[1] != 0) { Write(value); }
            return value;
        }
        public static double[] ReturnValue(double[] value, string input0)
        {
            if (value[0] != 0 || value[1] != 0) { Write(input0, value); }
            return value;
        }

        public static void Write() => Write("Blank");
        public static void Write(Fraction[] input) => Write(new double[2] { input[0].Value, input[1].Value });
        public static void Write(string input0, double[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(string input0, int[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(string input0, short[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(double[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(int[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(short[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(string input0, double input) => Write(input0 + input);
        public static void Write(string input0, int input) => Write(input0 + input);
        public static void Write(double input) => Write(Convert.ToString(input));
        public static void Write(int input) => Write(Convert.ToString(input));
        public static void Write(string input)
        {
            System.Windows.MessageBox.Show(input);
        }
        public static void Tick() => Ticks++;
        public static void Frame() => Frames++;
        public static int Frames { get; private set; } = 0;
        private static int Ticks = 0;
        private static System.Threading.Thread CounterReset = new System.Threading.Thread(() =>
        {
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(1000);
                Update(new object(), null);
                Frames = 0;
                Ticks = 0;
            }
        });
        private static event EventHandler Update;
        private static Window framesAndTicksPerSecond;
        private static TextBlock framesPerSecond = new TextBlock();
        private static TextBlock ticksPerSecond = new TextBlock();
        public static void StartCounter()
        {
            if(framesAndTicksPerSecond == null)
            {
                framesAndTicksPerSecond = new Window();
                framesAndTicksPerSecond.Height = 64;
                framesAndTicksPerSecond.Width = 128;
                Grid map = new Grid();
                map.ColumnDefinitions.Add(new ColumnDefinition());
                map.RowDefinitions.Add(new RowDefinition());
                map.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(ticksPerSecond, 1);
                framesPerSecond.Text = "Frames:" + 37 + "/sec";
                ticksPerSecond.Text = "Ticks:" + 37 + "/sec";
                Update += (s, e) => 
                {
                    framesAndTicksPerSecond.Dispatcher.Invoke((Action)delegate () { UpdateWindow(); });
                };
                map.Children.Add(framesPerSecond);
                map.Children.Add(ticksPerSecond);
                framesAndTicksPerSecond.Content = map;
                framesAndTicksPerSecond.Show();
                CounterReset.Start();
            }
        }
        private static void UpdateWindow()
        {
            framesPerSecond.Text = "Frames:" + Frames + "/sec";
            ticksPerSecond.Text = "Ticks:" + Ticks + "/sec";
        }
    }
}
