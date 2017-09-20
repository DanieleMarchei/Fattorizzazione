using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Utilities;

namespace Fattorizzazione.Models
{
    public class MetodoDiFermat : IAlgoritmo
    {
        public List<ulong> Fattorizza(ulong n)
        {
            
            List<ulong> fattori = new List<ulong>();
            while (n % 2 == 0)
            {
                n = n / 2;
                fattori.Add(2);
            }

            ulong a = (ulong)Math.Ceiling(Math.Sqrt(n));
            ulong b_sq = a * a - n;
            while (!Tools.IsPerfectSquare(b_sq))
            {
                a++;
                b_sq = a * a - n;
            }

            ulong b = (ulong)Math.Ceiling(Math.Sqrt(b_sq));
            if((a - b) != 1)
                fattori.AddRange(Fattorizza(a - b));

            if ((a + b) != n)
                fattori.AddRange(Fattorizza(a + b));
            else
                fattori.Add(n);

            return fattori;
        }

        public string NomeAlgoritmo()
        {
            return "Metodo di Fermat";
        }
    }
}
