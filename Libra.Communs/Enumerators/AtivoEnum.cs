using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum AtivoEnum
    {
        [Description("Inativo")]
        [IntValue(0)]
        Inativo = 0,

        [Description("Ativo")]
        [IntValue(1)]
        Ativo = 1
    }
}
