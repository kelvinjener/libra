using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    public class StringValue : Attribute
    {
        public string Value { get; set; }

        public StringValue(string _value)
        {
            Value = _value;
        }
    }
}
