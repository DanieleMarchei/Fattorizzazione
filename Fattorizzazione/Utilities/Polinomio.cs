using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    class Polinomio
    {
        public long[] Coefficienti { get; private set; }

        public Polinomio(params long[] coefficienti)
        {
            Coefficienti = coefficienti;
        }

        public long Eval(long x, long y = 1)
        {
            double risultato = 0;
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += Math.Pow(x, Coefficienti.Length - (i + 1)) * Coefficienti[i] * Math.Pow(y, i);
            }

            return (long)risultato;
        }

        public List<Polinomio> Fattorizza()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string risultato = "";
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += Coefficienti[i] + "(x^" + (Coefficienti.Length - (i + 1)) + ")(y^" + i+ ")";
                if (i < Coefficienti.Length - 1)
                    risultato += " + ";
            }

            return risultato;
        }
    }
}
