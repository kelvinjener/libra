using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public class IntValue : Attribute
    {
        public Int16 Value { get; set; }

        public IntValue(Int16 _value)
        {
            Value = _value;
        }
    }
}
