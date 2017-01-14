using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class FornecedoresBll : LibraBusinessLogic<FORNECEDORE>
    {
        private LibraDataContext dc = new LibraDataContext();

        public List<FORNECEDORE> RetornaTodos()
        {
            return dc.FORNECEDOREs.OrderBy(c => c.NOMEFANTASIA).ToList();
        }

        public FORNECEDORE RetornarPorId(int id)
        {
            return dc.FORNECEDOREs.Where(c => c.FORNECEDORID == id).FirstOrDefault();
        }
    }
}
