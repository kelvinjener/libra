using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum SituacaoVendaEnum
    {
        [Description("Em Aberto")]
        [IntValue(0)]
        EmAberto = 0,

        [Description("Finalizada")]
        [IntValue(1)]
        Finalizada = 1
    }
}
