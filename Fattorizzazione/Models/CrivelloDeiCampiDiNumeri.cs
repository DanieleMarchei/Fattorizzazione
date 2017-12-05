using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Fattorizzazione.Estensioni;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class CrivelloDeiCampiDiNumeri : IAlgoritmo
    {
        private string nomeAlgoritmo = "Crivello dei campi di numeri";

        public string NomeAlgoritmo { get => nomeAlgoritmo; set => nomeAlgoritmo = value; }

        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();

            #region SETUP
            
            //if (new BigInteger(n).isProbablePrime())
            //{
            //    fattori.Add(n);
            //    return fattori;
            //}



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

            #region NUMERI
            double lnN = Math.Log(n);
            double lnlnN = Math.Log(Math.Log(n));
            double lnN_1_3 = Math.Pow(lnN, 1.0 / 3.0);
            double lnlnN_2_3 = Math.Pow(lnlnN, 2.0 / 3.0);
            double div8_9 = Math.Pow(8.0 / 9.0, 1.0 / 3.0);

            long d = (long)Math.Floor(Math.Pow(3.0 * lnN / lnlnN, 1.0 / 3));
            long B = (long)Math.Floor(Math.Pow(Math.E, div8_9 * lnN_1_3 * lnlnN_2_3));
            long C = (long)Math.Floor(Math.Pow(Math.E, 1.2 * div8_9 * lnN_1_3 * lnlnN_2_3));
            long m = (long)Math.Floor(Math.Pow(n, 1.0 / d));

            //long k = (long)Math.Floor(3 * Math.Log(n) / Math.Log(2));
            long k = (long)Math.Floor(Math.Log(n) / Math.Log(2));

            #endregion

            #region Polinomio
            List<BigInteger> coefficienti = new List<BigInteger>();

            long tempDiv = n;
            long tempMod = n;
            while (tempDiv >= 1)
            {
                coefficienti.Add(tempDiv % m);
                tempDiv = tempDiv / m;
            }

            BigInteger[] coeff = coefficienti.ToArray();
            coeff.Reverse();

            Polinomio f = new Polinomio(coeff);
            #endregion

            #region RFB_AFB
            List<long> primi = Tools.ListaNumeriPrimi(B);
            List<Tuple<long, long>> RFB = new List<Tuple<long, long>>();
            List<Tuple<long, long>> AFB = new List<Tuple<long, long>>();

            foreach (long p in primi)
            {
                RFB.Add(new Tuple<long, long>(p, m % p));

                for (int r = 0; r < p; r++)
                {
                    if (f.Eval(r) % p == 0)
                        AFB.Add(new Tuple<long, long>(p, r));
                }
            }

            long nextP = primi.Max();

            while (AFB.Count < 3 * RFB.Count)
            {
                nextP = Tools.ProssimoPrimo(nextP);

                for (int r = 0; r < nextP; r++)
                {
                    if (f.Eval(r) % nextP == 0)
                        AFB.Add(new Tuple<long, long>(nextP, r));
                }
            }

            #endregion

            #region QCB
            List<Tuple<long, long>> QCB = new List<Tuple<long, long>>();

            while (QCB.Count < k)
            {
                nextP = Tools.ProssimoPrimo(nextP);

                for (int r = 0; r < nextP; r++)
                {
                    if (f.Eval(r) % nextP == 0)
                        QCB.Add(new Tuple<long, long>(nextP, r));
                }
            }

            #endregion

            #region SIEVE
            List<Tuple<long, long>> RELS = new List<Tuple<long, long>>();
            long V = RFB.Count() + AFB.Count + QCB.Count + 1;
            long b = 0;
            long[] a = new long[2 * C + 1];
            long[] e = new long[2 * C + 1];
            while (RELS.Count() < V)
            {
                b++;
                 
                for (long i = -C; i <= C; i++)
                {
                    a[i + C] = i + b * m;
                    e[i + C] = (long)(Math.Pow(b, f.Grado) * f.Eval((double)(i) / b));
                }

                foreach (var pr in RFB)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        while (a[i] % pr.Item1 == 0)
                            a[i] /= pr.Item1;
                    }
                }

                foreach (var pr in AFB)
                {
                    for (int i = 0; i < e.Length; i++)
                    {
                        while (e[i] % pr.Item1 == 0)
                            e[i] /= pr.Item1;
                    }
                }

                for (long i = -C; i <= C; i++)
                {
                    if(a[i + C] == 1 && e[i + C] == 1 && Tools.GCD(i,b) == 1)
                    {
                        RELS.Add(new Tuple<long, long>(i, b));
                    }
                }

                b++;
            }

            #endregion

            #region Algebra_lineare

            BMatrice M = new BMatrice(V, RELS.Count());

            long row = 0, col = 0;
            foreach (var ab in RELS)
            {
                long _a = ab.Item1;
                long _b = ab.Item2;

                if ((_a + _b * m) < 0) M[col, row] = 1;

                col++;

                foreach (var pr in RFB)
                {
                    long l = 0;
                    long temp = _a + _b * m;
                    while (temp % pr.Item1 == 0)
                    {
                        temp /= pr.Item1;
                        l++;
                    }

                    if (l % 2 != 0) M[col, row] = 1;
                    col++;
                }

                foreach (var pr in AFB)
                {
                    long l = 0;
                    long temp = (long)(Math.Pow(_b, f.Grado) * f.Eval((double)(_a) / _b)); ;
                    while (temp % pr.Item1 == 0)
                    {
                        temp /= pr.Item1;
                        l++;
                    }

                    if (l % 2 != 0) M[col, row] = 1;
                    col++;
                }

                foreach (var pr in QCB)
                {
                    if(Tools.Legendre(_a + _b * pr.Item1, pr.Item2) != 1)
                        M[col, row] = 1;
                    col++;
                }

                row++;
            }

            long[] v = M.SoluzioneRandom();
            List<Tuple<long, long>> DEPS = new List<Tuple<long, long>>();
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] == 1)
                    DEPS.Add(RELS[i]);
            }



            #endregion

            #region Rational_square_root

            BigInteger Sx = 0;
            Polinomio g = new Polinomio(1);
            foreach (var ab in DEPS)
            {
                Sx += ab.Item1 + ab.Item2 * m;
                g *= new Polinomio(ab.Item2, ab.Item1);
            }

            Sx *= f.Derivata().Eval(m) * f.Derivata().Eval(m);
            BigInteger Y = Sx.Sqrt();

            Polinomio fg = g % f;

            #endregion

            #region Da_Rivedere
            //Polinomio F = f;
            //Polinomio G = new Polinomio(1, -m);
            //List<Tuple<long, long>> R = new List<Tuple<long, long>>();
            //foreach (long p in primi)
            //{
            //    for (int r = 0; r < p; r++)
            //    {
            //        if (f.Eval(r) % p == 0)
            //        {
            //            R.Add(new Tuple<long, long>(r, p));
            //        }
            //    }
            //}

            //List<long> prossimiKPrimi = Tools.ProssimiKPrimi(B, k);
            //List<Tuple<long, long>> Q = new List<Tuple<long, long>>();
            //foreach (long q in prossimiKPrimi)
            //{
            //    for (int s = 0; s < q; s++)
            //    {
            //        if (f.Eval(s) % q == 0)
            //        {
            //            Q.Add(new Tuple<long, long>(s, q));
            //        }
            //    }
            //}

            //long V = 1 + primi.Count + R.Count + k;
            //long M = B;

            //List<Tuple<long, long>> S_1 = new List<Tuple<long, long>>();
            //long FG = 0;
            //int inc = 1;
            //List<long> temp;
            //for (long a = -M; a <= M && S_1.Count <= V; a++)
            //{
            //    if (a == 0)
            //        continue;
            //    Console.WriteLine(a);
            //    inc = Math.Abs(a) % 2 == 0 ? 2 : 1;
            //    for (long b = 1; b <= M && S_1.Count <= V; b += inc)
            //    {
            //        FG = F.Eval(a, b) * G.Eval(a, b);
            //        if (Tools.GCD(a, b) == 1)
            //            if (Tools.IsBSmooth(FG, B, out temp))
            //            {
            //                S_1.Add(new Tuple<long, long>(a, b));
            //            }
            //    }
            //}

            //BMatrice matrice = new BMatrice(V, S_1.Count);
            //long row = 0, col = 0;
            //long Gab = 0, absGab = 0;
            //List<long> factGab = new List<long>();
            //IAlgoritmo algo = new CrivelloDiEratostene();
            //foreach (var ab in S_1)
            //{
            //    col = 0;
            //    Gab = G.Eval(ab.Item1, ab.Item2);
            //    absGab = Math.Abs(Gab);
            //    matrice[col, row] = Gab < 0 ? 1 : 0;
            //    col++;
            //    factGab = algo.Fattorizza(absGab);
            //    factGab = Tools.VettoreEsponentiModN(factGab, 2);
            //    foreach (long p in primi)
            //    {
            //        matrice[col, row] = factGab.Contains(p) ? 1 : 0;
            //        col++;
            //    }

            //    foreach (var rp in R)
            //    {
            //        long br = -ab.Item2 * rp.Item1;
            //        long brModp = (br % rp.Item2 + rp.Item2) % rp.Item2;
            //        long aModp = (ab.Item1 % rp.Item2 + rp.Item2) % rp.Item2;
            //        matrice[col, row] = aModp == brModp ? 1 : 0;
            //        col++;
            //    }

            //    foreach (var sq in Q)
            //    {
            //        matrice[col, row] = Tools.Legendre(ab.Item1 - ab.Item2 * sq.Item1, sq.Item2) == -1 ? 1 : 0;
            //        col++;
            //    }

            //}

            //long[] risultato = matrice.SoluzioneRandom();
            //List<Tuple<long, long>> tupleScelte = new List<Tuple<long, long>>();
            //for (int i = 0; i < risultato.Length; i++)
            //{
            //    if (risultato[i] == 1)
            //        tupleScelte.Add(S_1[i]);
            //}

            //BigInteger abm = new BigInteger(1);
            //foreach (var tupla in tupleScelte)
            //{
            //    abm *= tupla.Item1 + tupla.Item2 * m;
            //}

            //abm = abm.sqrt();

            #endregion

            return fattori;
        }
    }
}
