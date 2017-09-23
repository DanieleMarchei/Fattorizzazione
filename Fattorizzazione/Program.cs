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
            //IAlgoritmo algo = new MetodoACurveEllittiche();
            //List<ulong> fattori = algo.Fattorizza(6512468135138454);
            //Utility.StampaASchermo(fattori);

            Random rand = new Random();
            BMatrice mat = new BMatrice(4, 5);
            mat[0, 0] = mat[1, 0] = mat[0, 1] = mat[1, 1] = mat[1, 2] = mat[2, 2] = mat[3, 2] = mat[3, 1] = mat[2, 3] = mat[3, 4] = 1;

            Console.WriteLine(mat);

            Console.WriteLine("---------------");
            Console.Write(mat.Riduci());
            Console.Read();
        }
    }
}
