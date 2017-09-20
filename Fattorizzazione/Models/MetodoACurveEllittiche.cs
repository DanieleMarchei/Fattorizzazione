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

            while (n % 2 == 0)
            {
                fattori.Add(2);
                n = n / 2;
            }

            while (n % 3 == 0)
            {
                fattori.Add(3);
                n = n / 3;
            }

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

            Random rand = new Random();
            CurvaEllittica curva = new CurvaEllittica(1, 1, n);
            Punto p = new Punto(0, 1);
            ulong k = 2;
            Punto z = null;
            ulong fatt = 0;


            do
            {

                try
                {
                    fatt = Tools.Fattoriale(k);
                    z = curva.MoltiplicaPunto(p, fatt);
                    k++;
                }
                catch (Exception)
                {
                    ulong lambdaDenom = 0;
                    if (k % 2 == 0)
                    {
                        lambdaDenom = 2 * z.Y;
                    }
                    else
                    {
                        ulong limit = fatt - Tools.Fattoriale(k - 1);
                        Punto r = new Punto(p.X, p.Y);
                        for (ulong i = 1; i < limit; i++)
                        {
                            try
                            {
                                r = curva.SommaPuntiModN(r, p);
                            }
                            catch (Exception)
                            {
                                break;
                            }

                        }
                        lambdaDenom = z.X - r.X;
                    }
                    BigInteger big_denom = new BigInteger(lambdaDenom);
                    ulong fatt1 = (ulong)big_denom.gcd(new BigInteger(n)).LongValue();
                    ulong fatt2 = n / fatt1;
                    if (fatt1 == 1 || fatt2 == 1)
                    {
                        fattori.Add(n);
                        return fattori;
                    }
                    else
                    {
                        fattori.AddRange(Fattorizza(fatt1));
                        fattori.AddRange(Fattorizza(fatt2));
                        return fattori;
                    }
                }
            } while (k < 9); //21! è il limite massimo per un ulong

            //n è primo
            fattori.Add(n);
            return fattori;
        }

        public string NomeAlgoritmo()
        {
            return "Metodo a curve ellittiche";
        }
    }
}
