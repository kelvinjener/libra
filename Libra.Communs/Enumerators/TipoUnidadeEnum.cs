using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum TipoUnidadeEnum
    {
        [Description("Matriz")]
        [IntValue(0)]
        Matriz = 0,

        [Description("Depósito")]
        [IntValue(1)]
        Deposito = 1,

        [Description("Loja")]
        [IntValue(2)]
        Loja = 2
    }
}
