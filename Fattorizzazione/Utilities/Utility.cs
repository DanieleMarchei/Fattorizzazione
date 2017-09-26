using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    public class Utility
    {
        public static void StampaASchermo<T>(List<T> lista, string separator = " ")
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (i < lista.Count - 1)
                    Console.Write(lista[i] + separator);
                else
                    Console.Write(lista[i]);
            }
        }

        public static void StampaASchermo<T>(T[] lista, string separator = " ")
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (i < lista.Length - 1)
                    Console.Write(lista[i] + separator);
                else
                    Console.Write(lista[i]);
            }
        }
    }
}
