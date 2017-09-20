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
            List<ulong> fattori = algo.Fattorizza(200016485312);
            Utility.StampaASchermo(fattori);

            Console.Read();
        }
    }
}
