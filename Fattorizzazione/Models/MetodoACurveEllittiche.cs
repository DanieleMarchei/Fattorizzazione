using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    class MetodoACurveEllittiche : IAlgoritmo
    {
        private string nomeAlgoritmo = "Metodo a Curve Ellittiche";

        public string NomeAlgoritmo { get => nomeAlgoritmo; set => nomeAlgoritmo = value; }

        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();

            long power = Tools.GetPower(n);
            if (power > 1)
            {
                long factor = (long)Math.Round(Math.Pow(n, 1.0 / power));
                for (long i = 0; i < power; i++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            long bound = 1000000;
            double logBound = Math.Log(bound) / Math.Log(2);
            long k = 1;
            List<long> primi = Tools.ListaNumeriPrimi(bound);
            double logP = 0, exponent = 0;
            foreach (long primo in primi)
            {
                logP = Math.Log(primo) / Math.Log(2);
                exponent = (long)Math.Floor(logBound / logP);
                k *= (long)Math.Pow(primo, exponent);
            }

            long A = 0, x = 0, y = 0, B = 0;
            long singulatity;

            long gcd = n;
            while(gcd == n)
            {
                A = Tools.Randomlong(0, n);
                x = Tools.Randomlong(0, n);
                y = Tools.Randomlong(0, n);

                B = y * y - x * x * x - A * x;

                singulatity = 4 * A * A * A + 27 * B * B;

                gcd = (long)Tools.GCD(singulatity, n);
            }

            CurvaEllittica curva = new CurvaEllittica(A, B, n);
            Punto startP = new Punto(x, y);
            Punto p = new Punto(x, y);

            for(long i = 0; i < k; i++)
            {
                try
                {
                    p = curva.SommaPuntiModN(p, startP);
                }
                catch (Exception)
                {
                    long denom = 0;
                    if (p == startP)
                    {
                        denom = (2 * p.Y) % n;
                    }
                    else
                    {
                        denom = (p.X - startP.X) % n;
                    }
                    long fattore1 = (long)Tools.GCD(denom, n);
                    long fattore2 = n / fattore1;
                    if(fattore1 == 1 || fattore2 == 1)
                        fattori.Add(n);
                    else
                    {
                        fattori.AddRange(Fattorizza(fattore1));
                        fattori.AddRange(Fattorizza(fattore2));
                    }
                    return fattori;

                }
                
            }

            fattori.Add(n);
            return fattori;
        }
    }
}
