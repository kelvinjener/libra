using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum TipoPagamentoEnum
    {
        [Description("Á vista")]
        [IntValue(0)]
        AVista = 0,

        [Description("Crédito á Vista")]
        [IntValue(1)]
        CreditoAVista = 1,

        [Description("Crédito Parcelado")]
        [IntValue(2)]
        CreditoParcelado = 2
    }
}
