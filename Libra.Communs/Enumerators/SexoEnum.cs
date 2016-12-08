using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum SexoEnum
    {
        [Description("Feminino")]
        [IntValue(0)]
        Feminino = 0,

        [Description("Masculino")]
        [IntValue(1)]
        Masculino = 1
    }
}
