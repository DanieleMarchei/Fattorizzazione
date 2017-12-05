using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Estensioni;
using Fattorizzazione.Models;
using Fattorizzazione.Utilities;

namespace Fattorizzazione
{
    class Program
    {
        static void Main(string[] args)
        {
            IAlgoritmo algo = new CrivelloDiEratostene();
            List<long> fattori = algo.Fattorizza(100000111111111111);
            Utility.StampaASchermo(fattori);

            //Polinomio f = new Polinomio(1, 15, 29, 8);

            //Console.WriteLine(f);
            //Console.WriteLine();

            //List<Tuple<long, long>> A = new List<Tuple<long, long>>();
            ////(−2, 1), (1, 1), (13, 1), (15, 1), (23, 1), (3, 2), (33, 2), (5, 3),
            ////(19, 4), (14, 5), (15, 7), (119, 11), (175, 13), (−1, 19), (49, 26)
            //A.Add(new Tuple<long, long>(-2, 1));
            //A.Add(new Tuple<long, long>(1, 1));
            //A.Add(new Tuple<long, long>(13, 1));
            //A.Add(new Tuple<long, long>(15, 1));
            //A.Add(new Tuple<long, long>(23, 1));
            //A.Add(new Tuple<long, long>(3, 2));
            //A.Add(new Tuple<long, long>(33, 2));
            //A.Add(new Tuple<long, long>(5, 3));
            //A.Add(new Tuple<long, long>(19, 4));
            //A.Add(new Tuple<long, long>(14, 5));
            //A.Add(new Tuple<long, long>(15, 7));
            //A.Add(new Tuple<long, long>(119, 11));
            //A.Add(new Tuple<long, long>(175, 13));
            //A.Add(new Tuple<long, long>(-1, 19));
            //A.Add(new Tuple<long, long>(49, 26));

            //Polinomio g = new Polinomio(-1);
            //BigInteger S = 1;
            //BigInteger Sa = 1;

            //foreach (var a in A)
            //{
            //    if(g == new Polinomio(-1))
            //    {
            //        g = new Polinomio(a.Item2, a.Item1);
            //    }
            //    else
            //    {
            //        Polinomio k = new Polinomio(a.Item2, a.Item1);
            //        g *= k;
            //    }

            //    S *= a.Item1 + 31 * a.Item2;
            //    Sa *= (ulong)Math.Pow(a.Item2, f.Grado) * (ulong)f.Eval((double)(a.Item1)/ a.Item2);

            //}
            //Polinomio d = g % f;
            //Polinomio d_ = d.Derivata();

            //BigInteger r = 31;
            //BigInteger N = 45113;
            //BigInteger p = 183661;
            //Console.WriteLine(d);
            //Console.WriteLine(d.Eval(r) % p);
            //Console.WriteLine();
            //Console.WriteLine(d_);
            //Console.WriteLine();

            //Console.WriteLine();

            Console.Read();
        }

        //public static BigInteger newr(BigInteger r, BigInteger p, Polinomio f)
    }
}
