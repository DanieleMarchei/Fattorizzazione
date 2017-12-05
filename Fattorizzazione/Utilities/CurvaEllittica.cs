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
            long Xr = (lambda * lambda - p.X - q.X) % N;
            if (Xr < 0) Xr = N + Xr;

            long Yr = (lambda * (p.X - Xr) - p.Y) % N;
            if (Yr < 0) Yr = N + Yr;

            return new Punto(Xr, Yr);
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
