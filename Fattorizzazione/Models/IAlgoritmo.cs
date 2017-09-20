using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Models
{
    interface IAlgoritmo
    {
        List<ulong> Fattorizza(ulong n);
    }
}
