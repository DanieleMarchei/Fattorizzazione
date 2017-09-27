using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    class CrivelloQuadratico : IAlgoritmo
    {
        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();

            #region SETUP

            while (n % 2 == 0)
            {
                fattori.Add(2);
                n = n / 2;
            }

            long power = Tools.GetPower(n);
            if (power > 1)
            {
                long factor = (long)Math.Round(Math.Pow(n, 1.0 / power));
                for (long f = 0; f < power; f++)
                {
                    fattori.AddRange(Fattorizza(factor));
                }
                return fattori;
            }

            if (n == 1) return fattori;

            #endregion

            double Ln = Math.Pow(Math.E, Math.Sqrt(Math.Log(n) * Math.Log(Math.Log(n))));
            long B = (long)Math.Ceiling(3 * Math.Sqrt(Ln));
            long sqrtN = (long)Math.Ceiling(Math.Sqrt(n));

            List<long> primi = Tools.ListaNumeriPrimi(B);
            List<long> vectorBase = primi.Where(p => Tools.Legendre(n, p) == 1 && p != 2).ToList();

            vectorBase.Add(2);

            List<Triple> triple = new List<Triple>();

            long x = 0;
            long i = 0;
            long val = 0;
            List<long> temp;
            Triple t;
            while(triple.Count <= vectorBase.Count)
            {
                x = sqrtN + i;
                val = x * x - n;
                if (Tools.IsBSmooth(val, B, out temp))
                {
                    t = new Triple()
                    {
                        X = x,
                        XX_N = val,
                        Factors = temp
                    };
                    
                    triple.Add(t);
                }
                i++;
            }

            BMatrice matrice = new BMatrice(triple.Count, vectorBase.Count);

            vectorBase.Sort();
            int c = 0, r = 0;

            foreach (long p in vectorBase)
            {
                foreach (Triple tr in triple)
                {                    
                    tr.Factors = Tools.VettoreEsponentiModN(tr.Factors, 2);
                    if (tr.Factors.Contains(p))
                        matrice[c, r] = 1;
                    c++;
                }
                c = 0;
                r++;
            }

            matrice.EliminazioneGaussiana();

            long X = 1, Y = 1;
            while(X == Y)
            {
                int[] v = matrice.SoluzioneRandom();

                List<Triple> tripleScelte = triple.Zip(v, (tr, k) => k == 1 ? tr : null).ToList();
                tripleScelte.RemoveAll(tr => tr == null);

                foreach (Triple tr in tripleScelte)
                {
                    X *= tr.X;
                    Y *= tr.XX_N;
                }

                X = X % n;

                Y = (long)Math.Round(Math.Sqrt(Y)) % n;
            }
            

            long fatt1 = Tools.GCD(X + Y, n);
            long fatt2 = n / fatt1;

            if (fatt1 <= 1 || fatt2 <= 1)
                fattori.Add(n);
            else
            {
                fattori.AddRange(Fattorizza(fatt1));
                fattori.AddRange(Fattorizza(fatt2));
            }

            return fattori;
        }

        public string NomeAlgoritmo()
        {
            return "Crivello Quadratico";
        }

        private class Triple
        {
            public long X { get; set; }
            public long XX_N { get; set; }
            public List<long> Factors { get; set; }
        }
    }
}
