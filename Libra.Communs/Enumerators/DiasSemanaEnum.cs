using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public enum DiaSemanaEnum
    {
        [Description("Domingo")]
        [IntValue(0)]
        Domingo = 0,

        [Description("Segunda")]
        [IntValue(1)]
        Segunda = 1,

        [Description("Terça")]
        [IntValue(2)]
        Terça = 2,

        [Description("Quarta")]
        [IntValue(3)]
        Quarta = 3,

        [Description("Quinta")]
        [IntValue(4)]
        Quinta = 4,

        [Description("Sexta")]
        [IntValue(5)]
        Sexta = 5,

        [Description("Sábado")]
        [IntValue(6)]
        Sabado = 6
    }
}
