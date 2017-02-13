using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class CaixaMovimentacaoBll : LibraBusinessLogic<CAIXAMOVIMENTACAO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public CAIXAMOVIMENTACAO GetCaixaMovimentacaoById(int idMovimentacao)
        {
            return dc.CAIXAMOVIMENTACAOs.Where(u => u.MOVIMENTACAOCAIXAID == idMovimentacao).FirstOrDefault();
        }

        public List<CAIXAMOVIMENTACAO> GetAllMovimentacaoesCaixas()
        {
            return dc.CAIXAMOVIMENTACAOs.OrderBy(u => u.MOVIMENTACAOCAIXAID).ToList();
        }

        public List<CAIXAMOVIMENTACAO> GetAllMovimentacaoesCaixasByDate(DateTime dataMovimentacao)
        {
            return dc.CAIXAMOVIMENTACAOs.Where(u => u.DATAHORAMOVIMENTACAO.Date == dataMovimentacao).OrderBy(u => u.MOVIMENTACAOCAIXAID).ToList();
        }

        public void Salvar(CAIXAMOVIMENTACAO cm)
        {
            if (cm.MOVIMENTACAOCAIXAID > 0)
                Atualizar(cm);
            else
                Inserir(cm);
        }
    }
}
