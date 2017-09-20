using System;
using System.Collections.Generic;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class Shor : IAlgoritmo
    {
        public List<ulong> Fattorizza(ulong n)
        {
            List<ulong> fattori = new List<ulong>();

            ulong power = Tools.GetPower(n);
            if (power > 1)
            {
                ulong factor = (ulong)Math.Round(Math.Pow(n, 1.0/power));
                for(ulong i = 0; i < power; i++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            Random rand = new Random();

            ulong a, r;
            ulong pow = 0;
            bool exit = false;

            do
            {
                a = (ulong)(rand.NextDouble() * (n-2))+2;
                if (new BigInteger(a).gcd(n) != 1) continue;

                r = Tools.DiscreteLog(a, 1, n);

                exit = r % 2 == 0;
                if(exit)
                {
                    pow = (ulong)new BigInteger(a).modPow(r / 2, n).LongValue();
                    exit &= (pow + 1) % n != 0;
                }
                
            } while (!exit);

            BigInteger big_n = new BigInteger(n);
            ulong p = (ulong)new BigInteger(pow - 1).gcd(big_n).LongValue();
            ulong q = n / p;

            if (p <= 1 || q <= 1)
                fattori.Add(n);
            else
            {
                if (p > 1)
                    fattori.AddRange(Fattorizza(p));

                if (q > 1)
                    fattori.AddRange(Fattorizza(q));
            }
            

            return fattori;
        }

        
    }
}
