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
        public static ulong DiscreteLog(ulong g, ulong h, ulong n)
        {
            BigInteger big_n = new BigInteger(n);
            BigInteger big_g = new BigInteger(g);
            BigInteger big_h = new BigInteger(h);
            BigInteger m = big_n.sqrt() + 1;

            Dictionary<ulong, ulong> table = new Dictionary<ulong, ulong>();

            ulong temp = 0;

            for (ulong i = 0; i <= m; i++)
            {
                temp = (ulong)new BigInteger(g).modPow(new BigInteger(i), new BigInteger(n)).LongValue();
                if (!table.ContainsKey(temp))
                    table.Add(temp, i);
            }
            BigInteger inverse = big_g.modInverse(n).LongValue();
            BigInteger inverse2 = inverse.modPow(m, n);

            ulong hg = new ulong();
            for (ulong i = 0; i <= m; i++)
            {
                hg = (ulong)((big_h * inverse2.modPow(new BigInteger(i), n)) % n).LongValue();
                try
                {
                    ulong any = table[hg];
                    if ((i * m + any) != 0)
                        return i * (ulong)m.LongValue() + any;
                }
                catch (KeyNotFoundException e)
                {

                }
            }

            return 0;
        }

        public static bool IsPerfectSquare(ulong n)
        {
            ulong sqrt = (ulong)Math.Ceiling(Math.Sqrt(n));
            return sqrt * sqrt == n;
        }

        public static ulong GetPower(ulong n)
        {
            double result = 3;
            ulong power = 1;
            while (result >= 2)
            {
                power++;
                result = Math.Pow(n, 1.0 / power);
                if (Math.Abs(Math.Round(result) - result) <= 0.0000001d)
                    return power;
            }
            return 1;
        }

        public static ulong Fattoriale(ulong n)
        {
            if (n == 1 || n == 0) return 1;
            else return n * Fattoriale(n - 1);
        }

        public static ulong GCD(ulong a, ulong b)
        {
            return (ulong)new BigInteger(a).gcd(new BigInteger(b)).LongValue();
        }

        public static List<ulong> ListaNumeriPrimi(ulong B)
        {
            List<ulong> risultato = new List<ulong>();

            for (ulong i = 2; i <= B; i++)
            {
                risultato.Add(i);
            }

            ulong sqrtB = (ulong)Math.Sqrt(B);

            for (ulong i = 2; i <= sqrtB; i++)
            {
                risultato.RemoveAll(u => u % i == 0 && u != i);
            }

            return risultato;
        }

        public static ulong RandomULong(ulong min, ulong max)
        {
            Random rand = new Random();
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            ulong longRand = (ulong)BitConverter.ToInt64(buf, 0);

            return longRand % (max - min) + min;
        }

        public static ulong ModInverse(ulong a, ulong n)
        {
            return (ulong)(new BigInteger(a).modInverse(new BigInteger(n)).LongValue());
        }

        public static int Legendre(ulong a, ulong p)
        {
            if (p < 2)
                throw new ArgumentOutOfRangeException("p", "p deve essere >= 2");
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
                if (((p * p - 1) & 8) != 0)
                {
                    result = -result;
                }
            }
            else
            {
                result = Legendre(p % a, a);
                if (((a - 1) * (p - 1) & 4) != 0)
                {
                    result = -result;
                }
            }
            return result;
        }

        public static bool IsBSmooth(ulong n, ulong b)
        {
            IAlgoritmo algo = new CrivelloDiEratostene();
            List<ulong> primi = algo.Fattorizza(n);
            ulong maxP = primi.Max();
            return maxP <= b;
        }

        public static List<int> FiltraLista(List<int> a, List<int> z)
        {
            List<int> result = a.Zip(z, (i, j) => i * j).ToList();
            result.RemoveAll(i => i == 0);
            return result;
        }
    }
}
