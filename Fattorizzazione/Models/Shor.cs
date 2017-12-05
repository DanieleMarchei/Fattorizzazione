using System;
using System.Collections.Generic;
using System.Numerics;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class Shor : IAlgoritmo
    {
        private string nomeAlgoritmo = "Algoritmo di Shor"; 

        public string NomeAlgoritmo { get => nomeAlgoritmo; set => nomeAlgoritmo = value; }

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
                if (Tools.GCD(a,n) != 1) continue;

                r = Tools.DiscreteLog(a, 1, n);

                exit = r % 2 == 0;
                if(exit)
                {
                    pow = (long)BigInteger.ModPow(a, r / 2, n);
                    exit &= (pow + 1) % n != 0;
                }
                
            } while (!exit);

            long p = (long)Tools.GCD(pow - 1, n);
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

    }
}
