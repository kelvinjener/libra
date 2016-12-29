using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Controllers.Core
{
    public class CRTController : CRTBll
    {
        public List<CRT> RetornaTodos()
        {
            return base.RetornaTodos();
        }

        public CRT RetornaPorId(int id)
        {
            return base.RetornarPorId(id);
        }
    }
}