using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    class Complesso
    {
        public double Reale { get; set; }
        public double Immaginaria { get; set; }
        public static readonly Complesso I = new Complesso(0, 1);
        public static readonly Complesso UNO = new Complesso(1, 0);
        public static readonly Complesso ZERO = new Complesso(0, 0);

        public Complesso(double reale, double immaginaria)
        {
            Reale = reale;
            Immaginaria = immaginaria;
        }

        public static Complesso operator +(Complesso a, Complesso b)
        {
            return new Complesso(a.Reale + b.Reale, a.Immaginaria + b.Immaginaria);
        }

        public static Complesso operator -(Complesso a, Complesso b)
        {
            return new Complesso(a.Reale - b.Reale, a.Immaginaria - b.Immaginaria);
        }

        public static Complesso operator *(double a, Complesso b)
        {
            return new Complesso(a, 0) * b;
        }

        public static Complesso operator *(Complesso a, double b)
        {
            return a * new Complesso(b, 0);
        }

        public static Complesso operator *(Complesso a, Complesso b)
        {
            double rr = a.Reale * b.Reale;
            double ri = a.Reale * b.Immaginaria;
            double ir = a.Immaginaria * b.Reale;
            double ii = a.Immaginaria * b.Immaginaria;

            return new Complesso(rr - ii, ri + ir);
        }

        public static Complesso operator /(Complesso a, Complesso b)
        {
            double rr = a.Reale * b.Reale;
            double ri = a.Reale * b.Immaginaria;
            double ir = a.Immaginaria * b.Reale;
            double ii = a.Immaginaria * b.Immaginaria;
            double denom = b.Reale * b.Reale + b.Immaginaria * b.Immaginaria;

            return new Complesso((rr + ii) / denom, (ir + ri) / denom);
        }

        public Complesso Coniugato()
        {
            return new Complesso(Reale, -Immaginaria);
        }

        public Complesso Pow(int exp)
        {
            Complesso res = new Complesso(1, 0);
            for (int i = 0; i < exp; i++)
            {
                res *= this;
            }
            return res;
        }

        public bool IsReal()
        {
            return Math.Abs(Immaginaria) <= double.Epsilon;
        }


        public override string ToString()
        {
            return string.Format("({0}; {1})", Reale, Immaginaria);
        }

    }
}
