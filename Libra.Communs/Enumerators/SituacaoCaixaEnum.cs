using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum SituacaoCaixaEnum
    {
        [Description("Fechado")]
        [IntValue(0)]
        Fechado = 0,

        [Description("Aberto")]
        [IntValue(1)]
        Aberto = 1
    }
}
