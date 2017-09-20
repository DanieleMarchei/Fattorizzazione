using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Models
{
    public class CrivelloDiEratostene : IAlgoritmo
    {
        public List<ulong> Fattorizza(ulong n)
        {
            List<ulong> fattori = new List<ulong>();
            for(ulong i = 2; i <= n; i++)
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

        public string NomeAlgoritmo()
        {
            return "Crivello di Eratostene";
        }
    }
}
