using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class CaixaBll : LibraBusinessLogic<CAIXA>
    {
        private LibraDataContext dc = new LibraDataContext();

        public CAIXA GetCaixaById(int idCaixa)
        {
            return dc.CAIXAs.Where(u => u.CAIXAID == idCaixa).FirstOrDefault();
        }

        public List<CAIXA> GetAllCaixas()
        {
            return dc.CAIXAs.OrderBy(u => u.CAIXAID).ToList();
        }


        public void Salvar(CAIXA caixa)
        {
            if (caixa.CAIXAID > 0)
                Atualizar(caixa);
            else
                Inserir(caixa);
        }

    }
}
