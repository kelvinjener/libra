using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum FormaPagamentoEnum
    {
        [Description("Á vista")]
        [IntValue(0)]
        AVista = 0,

        [Description("Débito")]
        [IntValue(1)]
        Debito = 1,

        [Description("Crédito")]
        [IntValue(2)]
        Credito = 2
    }
}
