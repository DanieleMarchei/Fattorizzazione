using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Models
{
    interface IAlgoritmo
    {
        List<long> Fattorizza(long n);

        string NomeAlgoritmo { get; }
    }
}
