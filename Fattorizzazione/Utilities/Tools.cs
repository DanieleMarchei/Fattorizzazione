using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Models;

namespace Fattorizzazione.Utilities
{
    public class Tools
    {
        public static long DiscreteLog(long g, long h, long n)
        {
            BigInteger big_n = new BigInteger(n);
            BigInteger big_g = new BigInteger(g);
            BigInteger big_h = new BigInteger(h);
            BigInteger m = big_n.sqrt() + 1;

            Dictionary<long, long> table = new Dictionary<long, long>();

            long temp = 0;

            for (long i = 0; i <= m; i++)
            {
                temp = (long)new BigInteger(g).modPow(new BigInteger(i), new BigInteger(n)).LongValue();
                if (!table.ContainsKey(temp))
                    table.Add(temp, i);
            }
            BigInteger inverse = big_g.modInverse(n).LongValue();
            BigInteger inverse2 = inverse.modPow(m, n);

            long hg = 0;
            for (long i = 0; i <= m; i++)
            {
                hg = ((big_h * inverse2.modPow(new BigInteger(i), n)) % n).LongValue();
                try
                {
                    long any = table[hg];
                    if ((i * m + any) != 0)
                        return i * m.LongValue() + any;
                }
                catch (KeyNotFoundException e)
                {

                }
            }

            return 0;
        }

        public static bool IsPerfectSquare(long n)
        {
            long sqrt = (long)Math.Ceiling(Math.Sqrt(n));
            return sqrt * sqrt == n;
        }

        public static long GetPower(long n)
        {
            double result = 3;
            long power = 1;
            while (result >= 2)
            {
                power++;
                result = Math.Pow(n, 1.0 / power);
                if (Math.Abs(Math.Round(result) - result) <= 0.0000001d)
                    return power;
            }
            return 1;
        }

        public static long Fattoriale(long n)
        {
            if (n == 1 || n == 0) return 1;
            else return n * Fattoriale(n - 1);
        }

        public static long GCD(long a, long b)
        {
            return new BigInteger(a).gcd(new BigInteger(b)).LongValue();
        }

        public static List<long> ListaNumeriPrimi(long B)
        {
            List<long> risultato = new List<long>();
            risultato.Add(2);

            for (long i = 3; i <= B; i += 2)
            {
                risultato.Add(i);
            }

            long sqrtB = (long)Math.Sqrt(B);

            for (long i = 2; i <= sqrtB; i++)
            {
                risultato.RemoveAll(u => u % i == 0 && u != i);
            }

            return risultato;
        }

        public static long Randomlong(long min, long max)
        {
            Random rand = new Random();
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return longRand % (max - min) + min;
        }

        public static long ModInverse(long a, long n)
        {
            return new BigInteger(a).modInverse(new BigInteger(n)).LongValue();
        }

        public static int Legendre(long a, long p)
        {

            if (p < 2)  // prime test is expensive.
                throw new ArgumentOutOfRangeException("p", "p must not be < 2");

            if (a == 0)
            {
                return 0;
            }
            if (a == 1)
            {
                return 1;
            }
            int result;
            if (a % 2 == 0)
            {
                result = Legendre(a / 2, p);
                if (((p * p - 1) & 8) != 0) // instead of dividing by 8, shift the mask bit
                {
                    result = -result;
                }
            }
            else
            {
                result = Legendre(p % a, a);
                if (((a - 1) * (p - 1) & 4) != 0) // instead of dividing by 4, shift the mask bit
                {
                    result = -result;
                }
            }
            return result;
        }

        public static bool IsBSmooth(long n, long b)
        {
            IAlgoritmo algo = new CrivelloDiEratostene();
            List<long> primi = algo.Fattorizza(n);
            long maxP = primi.Max();
            return maxP <= b;
        }

        public static bool IsBSmooth(long n, long b, out List<long> fattori)
        {
            IAlgoritmo algo = new CrivelloDiEratostene();
            fattori = algo.Fattorizza(n);
            long maxP = fattori.Max();
            return maxP <= b;
        }

        public static List<int> FiltraLista(List<int> a, List<int> z)
        {
            List<int> result = a.Zip(z, (i, j) => i * j).ToList();
            result.RemoveAll(i => i == 0);
            return result;
        }

        public static List<long> ResuduiQuadratici(long n, long p)
        {
            List<long> residui = new List<long>();

            long n_modp = n % p;
            long x_sqr = 0;
            for (long x = 1; x < p; x++)
            {
                x_sqr = (x * x) % p;
                if (x_sqr == n_modp)
                {
                    residui.Add(x);
                }
            }

            return residui;
        }

        public static List<long> VettoreEsponentiModN(List<long> fattori, long n)
        {
            List<long> risultato = new List<long>();
            List<long> distinct = fattori.Distinct().ToList();

            foreach (long fatt in distinct)
            {
                if (fattori.Where(k => k == fatt).Count() % n != 0)
                    risultato.Add(fatt);
            }

            return risultato;
        }
    }
}
