using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum TipoMovimentacaoEnum
    {
        [Description("Sangria")]
        [IntValue(0)]
        Sangria = 0,

        [Description("Suprimento")]
        [IntValue(1)]
        Suprimento = 1

    }
}
