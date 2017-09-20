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
            Shor algo = new Shor();
            List<ulong> fattori = algo.Fattorizza(7 * 7 * 7 * 7 * 7);
            Utility.StampaASchermo(fattori, "*");

            Console.Read();
        }
    }
}
