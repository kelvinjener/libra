using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum EstadoCivilEnum
    {
        [Description("Solteiro")]
        [IntValue(0)]
        Solteiro = 0,

        [Description("Casado")]
        [IntValue(1)]
        Casado = 1,

        [Description("Desquitado")]
        [IntValue(2)]
        Desquitado = 2,

        [Description("Divorciado")]
        [IntValue(3)]
        Divorciado = 3,

        [Description("Viúvo")]
        [IntValue(4)]
        Viuvo = 4
    }
}
