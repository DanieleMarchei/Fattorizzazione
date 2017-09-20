using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            while(result >= 2)
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

        public static ulong GCD(ulong a,ulong b)
        {
            return (ulong)new BigInteger(a).gcd(new BigInteger(b)).LongValue();
        }

        public static ulong LCM(ulong a, ulong b)
        {
            ulong ab = a * b;
            ulong gdc = GCD(a,b);
            return (a * b) / gdc;
        }

        public static ulong LCM(params ulong[] a)
        {
            ulong lcm = 0;
            int i = 0;
            while(i < a.Length)
            {
                if (lcm == 0)
                {
                    lcm = LCM(a[i], a[i+1]);
                    i++;
                }   
                else
                    lcm = LCM(lcm, a[i]);
                i++;
            }

            return lcm;
        }
    }
}
