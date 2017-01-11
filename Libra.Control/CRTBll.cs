using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public abstract class CRTBll : LibraBusinessLogic<CRT>
    {
        private LibraDataContext dc = new LibraDataContext();

        public List<CRT> RetornaTodos()
        {
            return dc.CRTs.OrderBy(c => c.DESCRICAO).ToList();
        }

        public CRT RetornarPorId(int id)
        {
            return dc.CRTs.Where(c => c.ID == id).FirstOrDefault();
        }
    }
}
