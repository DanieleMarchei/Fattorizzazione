using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    public class CurvaEllittica
    {
        public uint A { get; private set; }
        public uint B { get; private set; }
        public ulong N { get; private set; }

        public CurvaEllittica(uint a, uint b, ulong N)
        {
            A = a;
            B = b;
            this.N = N;
        }

        public bool ContienePunto(Punto p)
        {
            ulong sqrY = p.Y * p.Y;
            ulong cubX = p.X * p.X * p.X;
            ulong ax = A * p.X;
            ulong xPart = (cubX + ax + B) % N;
            return sqrY == xPart;
        }

        public Punto SommaPuntiModN(Punto p, Punto q)
        {
            ulong lambdaNum = 0, lambdaDenom = 0;
            ulong lambda = 0; ;
            if(p == q)
            {
                lambdaNum = 3 * p.X * p.X + (ulong)A;
                lambdaDenom = 2 * p.Y;
            }
            else
            {
                lambdaNum = p.Y - q.Y;
                lambdaDenom = p.X - q.X;
            }

            BigInteger big_denom = new BigInteger(lambdaDenom);
            ulong inverseDenom = (ulong)big_denom.modInverse(N).LongValue();
            lambda = (lambdaNum * inverseDenom) % N;
            long Xr = ((long)lambda * (long)lambda - (long)p.X - (long)q.X) % (long)N;
            if (Xr < 0) Xr = (long)N + Xr;

            long Yr = ((long)lambda * ((long)p.X - Xr) - (long)p.Y) % (long)N;
            if (Yr < 0) Yr = (long)N + Yr;

            return new Punto((ulong)Xr, (ulong)Yr);
        }


        public Punto MoltiplicaPunto(Punto p, ulong n)
        {
            Punto r = new Punto(p.X, p.Y);
            for(ulong i = 1; i < n; i++)
            {
                r = SommaPuntiModN(r, p);
            }

            return r;
        }
    }
}
