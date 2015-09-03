using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverseSimV1
{
    class Fraction
    {
        private double Numerator { get; set; } = 0;
        private int denominator { get; set; } = 0;
        /// <summary>
        /// Numerator/Denominator
        /// </summary>
        public int Denominator
        {
            get
            {
                return denominator;
            }
            set
            {
                double num = Value;
                denominator = value;
                Value = num;
            }
        }
        public Fraction(double numerator,int denominator)
        {
            Numerator = numerator;
            this.denominator = denominator;
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
        public void Simplify()
        {
            if(denominator == 0)
            {
                Reset();
                return;
            }
            int j = -1;
            for(int i = Denominator;i > 0;i--)
            {
                if (Value % i == 0)
                {
                    j = i;
                }
            }
            if(j != -1)
            {
                double h = Value;
                Denominator = j;
                Value = h;
            }
        }
        public void Reset()
        {
            Numerator = 0;
            denominator = 0;
        }
        public void AddFraction(Fraction fraction)
        {
            double mainFractionValue = Value;
            denominator = fraction.denominator;
            Numerator = fraction.Numerator;
            Numerator += mainFractionValue * denominator;
        }
    }
}
