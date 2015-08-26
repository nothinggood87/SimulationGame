using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    static class Debug
    {
        public static double[] ReturnValue(double[] value)
        {
            if (value[0] != 0 || value[1] != 0) { Write(value); }
            return value;
        }
        public static double[] ReturnValue(double[] value,string input0)
        {
            if(value[0] != 0 || value[1] != 0) { Write(input0, value); }
            return value;
        }

        public static void Write() => Write("Blank");
        public static void Write(string input0,double[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(string input0,int[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(string input0,short[] input) => Write(input0 + input[0] + " | " + input[1]);
        public static void Write(double[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(int[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(short[] input) => Write(input[0] + " | " + input[1]);
        public static void Write(string input0,double input) => Write(input0 + input);
        public static void Write(string input0,int input) => Write(input0+input);
        public static void Write(double input) => Write(Convert.ToString(input));
        public static void Write(int input) => Write(Convert.ToString(input));
        public static void Write(string input)
        {
            System.Windows.MessageBox.Show(input);
        }
    }
}
