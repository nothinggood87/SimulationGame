using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    class Fraction
    {
        private double Numerator { get; set; }
        public int Denominator { private get; set; }
        public Fraction(int numerator,int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public double Value
        {
            get
            {
                return Numerator / Denominator;
            }
            set
            {
                Numerator = value * Denominator;
            }
        }
    }
}
