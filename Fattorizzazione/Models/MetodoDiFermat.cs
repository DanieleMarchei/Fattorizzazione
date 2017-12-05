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
        private string nomeAlgoritmo = "Metodo di Fermat";

        public string NomeAlgoritmo { get => nomeAlgoritmo; set => nomeAlgoritmo = value; }

        public List<long> Fattorizza(long n)
        {
            
            List<long> fattori = new List<long>();
            while (n % 2 == 0)
            {
                n = n / 2;
                fattori.Add(2);
            }

            long a = (long)Math.Ceiling(Math.Sqrt(n));
            long b_sq = a * a - n;
            while (!Tools.IsPerfectSquare(b_sq))
            {
                a++;
                b_sq = a * a - n;
            }

            long b = (long)Math.Ceiling(Math.Sqrt(b_sq));
            if((a - b) != 1)
                fattori.AddRange(Fattorizza(a - b));

            if ((a + b) != n)
                fattori.AddRange(Fattorizza(a + b));
            else
                fattori.Add(n);

            return fattori;
        }

    }
}
