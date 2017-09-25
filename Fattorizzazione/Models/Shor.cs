using System;
using System.Collections.Generic;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class Shor : IAlgoritmo
    {
        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();

            long power = Tools.GetPower(n);
            if (power > 1)
            {
                long factor = (long)Math.Round(Math.Pow(n, 1.0/power));
                for(long i = 0; i < power; i++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            Random rand = new Random();

            long a, r;
            long pow = 0;
            bool exit = false;

            do
            {
                a = (long)(rand.NextDouble() * (n-2))+2;
                if (new BigInteger(a).gcd(n) != 1) continue;

                r = Tools.DiscreteLog(a, 1, n);

                exit = r % 2 == 0;
                if(exit)
                {
                    pow = (long)new BigInteger(a).modPow(r / 2, n).LongValue();
                    exit &= (pow + 1) % n != 0;
                }
                
            } while (!exit);

            BigInteger big_n = new BigInteger(n);
            long p = (long)new BigInteger(pow - 1).gcd(big_n).LongValue();
            long q = n / p;

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

        public string NomeAlgoritmo()
        {
            return "Algoritmo di Shor";
        }
    }
}
