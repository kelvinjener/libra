using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum TipoLogEnum
    {
        [Description("Consulta")]
        [IntValue(0)]
        Select = 0,

        [Description("Inclusão")]
        [IntValue(1)]
        Insert = 1,

        [Description("Alteração")]
        [IntValue(2)]
        Update = 2,

        [Description("Exclusão")]
        [IntValue(3)]
        Delete = 3
    }

}
