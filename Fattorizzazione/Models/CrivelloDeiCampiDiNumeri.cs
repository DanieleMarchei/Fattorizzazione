using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class CrivelloDeiCampiDiNumeri : IAlgoritmo
    {
        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();

            #region SETUP
            if (new BigInteger(n).isProbablePrime())
            {
                fattori.Add(n);
                return fattori;
            }

            

            while (n % 2 == 0)
            {
                fattori.Add(2);
                n = n / 2;
            }

            long power = Tools.GetPower(n);
            if (power > 1)
            {
                long factor = (long)Math.Round(Math.Pow(n, 1.0 / power));
                for (long _ = 0; _ < power; _++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            if (n == 1) return fattori;

            #endregion

            double lnN = Math.Log(n);
            double lnlnN = Math.Log(Math.Log(n));
            double lnN_1_3 = Math.Pow(lnN, 1.0 / 3.0);
            double lnlnN_2_3 = Math.Pow(lnlnN, 2.0 / 3.0);
            double div8_9 = Math.Pow(8.0 / 9.0, 1.0 / 3.0);

            long d = (long)Math.Floor(Math.Pow(3.0 * lnN / lnlnN, 1.0 / 3));
            long B = (long)Math.Floor(Math.Pow(Math.E, div8_9 * lnN_1_3 * lnlnN_2_3));
            long m = (long)Math.Floor(Math.Pow(n, 1.0 / d));
            long k = (long)Math.Floor(3 * Math.Log(n) / Math.Log(2));
            List<long> primi = Tools.ListaNumeriPrimi(B);

            List<long> coefficienti = new List<long>();
            
            long tempDiv = n;
            long tempMod = n;
            while(tempDiv >= 1)
            {
                coefficienti.Add(tempDiv % m);
                tempDiv = tempDiv / m;
            }

            coefficienti.Reverse();

            Polinomio f = new Polinomio(coefficienti.ToArray());
            Polinomio F = f;
            Polinomio G = new Polinomio(1, -m);

            Dictionary<long, List<long>> R = new Dictionary<long, List<long>>();
            foreach (long p in primi)
            {
                for (int r = 0; r < p; r++)
                {
                    if(f.Eval(r) % p == 0)
                    {
                        try
                        {
                            R[p].Add(r);
                        }
                        catch (Exception)
                        {
                            R[p] = new List<long>
                            {
                                r
                            };
                        }
                    }
                }
            }
            return fattori;
        }

        public string NomeAlgoritmo()
        {
            return "Crivello dei campi di numeri";
        }
    }
}
