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
        public List<ulong> Fattorizza(ulong n)
        {
            List<ulong> fattori = new List<ulong>();

            ulong power = Tools.GetPower(n);
            if (power > 1)
            {
                ulong factor = (ulong)Math.Round(Math.Pow(n, 1.0 / power));
                for (ulong i = 0; i < power; i++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            ulong bound = 1000000;
            double logBound = Math.Log(bound) / Math.Log(2);
            ulong k = 1;
            List<ulong> primi = Tools.ListaNumeriPrimi(bound);
            double logP = 0, exponent = 0;
            foreach (ulong primo in primi)
            {
                logP = Math.Log(primo) / Math.Log(2);
                exponent = (ulong)Math.Floor(logBound / logP);
                k *= (ulong)Math.Pow(primo, exponent);
            }

            ulong A = 0, x = 0, y = 0, B = 0;
            ulong singulatity;

            ulong gcd = 1;
            while(gcd == 1)
            {
                A = Tools.RandomULong(0, n);
                x = Tools.RandomULong(0, n);
                y = Tools.RandomULong(0, n);

                B = y * y - x * x * x - A * x;

                singulatity = 4 * A * A * A + 27 * B * B;

                gcd = Tools.GCD(singulatity, n);
            }

            CurvaEllittica curva = new CurvaEllittica(A, B, n);
            Punto startP = new Punto(x, y);
            Punto p = new Punto(x, y);

            for(ulong i = 0; i < k; i++)
            {
                try
                {
                    p = curva.SommaPuntiModN(p, startP);
                }
                catch (Exception)
                {
                    ulong denom = 0;
                    if (p == startP)
                    {
                        denom = (2 * p.Y) % n;
                    }
                    else
                    {
                        denom = (p.X - startP.X) % n;
                    }
                    ulong fattore1 = Tools.GCD(denom, n);
                    ulong fattore2 = n / fattore1;
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

        public string NomeAlgoritmo()
        {
            return "Metodo a curve ellittiche";
        }
    }
}
