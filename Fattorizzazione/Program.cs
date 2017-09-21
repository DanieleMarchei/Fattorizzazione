using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fattorizzazione.Models;
using Fattorizzazione.Utilities;

namespace Fattorizzazione
{
    class Program
    {
        static void Main(string[] args)
        {
            IAlgoritmo algo = new MetodoACurveEllittiche();
            List<ulong> fattori = algo.Fattorizza(6512468135138454);
            Utility.StampaASchermo(fattori);
            Console.Read();
        }
    }
}
