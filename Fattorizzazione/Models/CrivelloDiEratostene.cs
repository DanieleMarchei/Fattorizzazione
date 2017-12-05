using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Models
{
    public class CrivelloDiEratostene : IAlgoritmo
    {
        private string nomeAlgoritmo = "Crivello di Eratostene";

        public string NomeAlgoritmo { get => nomeAlgoritmo; set => nomeAlgoritmo = value; }

        public List<long> Fattorizza(long n)
        {
            List<long> fattori = new List<long>();
            if(n < 0)
            {
                fattori.Add(-1);
                n = -n;
            }
            for(long i = 2; i <= n; i++)
            {
                if(n % i == 0)
                {
                    fattori.Add(i);
                    n = n / i;
                    i--;
                }
            }
            return fattori;
        }

    }
}
