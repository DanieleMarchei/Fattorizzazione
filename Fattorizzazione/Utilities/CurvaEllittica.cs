using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    public class CurvaEllittica
    {
        public long A { get; private set; }
        public long B { get; private set; }
        public long N { get; private set; }

        public CurvaEllittica(long a, long b, long N)
        {
            A = a;
            B = b;
            this.N = N;
        }

        public bool ContienePunto(Punto p)
        {
            long sqrY = p.Y * p.Y;
            long cubX = p.X * p.X * p.X;
            long ax = A * p.X;
            long xPart = (cubX + ax + B) % N;
            return sqrY == xPart;
        }

        public Punto SommaPuntiModN(Punto p, Punto q)
        {
            long lambdaNum = 0, lambdaDenom = 0;
            long lambda = 0; ;
            if(p == q)
            {
                lambdaNum = 3 * p.X * p.X + A;
                lambdaDenom = 2 * p.Y;
            }
            else
            {
                lambdaNum = p.Y - q.Y;
                lambdaDenom = p.X - q.X;
            }

            long inverseDenom = Tools.ModInverse(lambdaDenom, N);
            lambda = (lambdaNum * inverseDenom) % N;
            long Xr = ((long)lambda * (long)lambda - (long)p.X - (long)q.X) % (long)N;
            if (Xr < 0) Xr = (long)N + Xr;

            long Yr = ((long)lambda * ((long)p.X - Xr) - (long)p.Y) % (long)N;
            if (Yr < 0) Yr = (long)N + Yr;

            return new Punto((long)Xr, (long)Yr);
        }


        public Punto MoltiplicaPunto(Punto p, long n)
        {
            Punto r = new Punto(p.X, p.Y);
            for(long i = 1; i < n; i++)
            {
                r = SommaPuntiModN(r, p);
            }

            return r;
        }
    }
}
