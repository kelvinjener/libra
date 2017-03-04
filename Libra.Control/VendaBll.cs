using Libra.Communs.Enumerators;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class VendaBll : LibraBusinessLogic<VENDA>
    {
        private LibraDataContext dc = new LibraDataContext();

        public VENDA GetVendaById(int idVenda)
        {
            return dc.VENDAs.Where(u => u.VENDAID == idVenda).FirstOrDefault();
        }

        public List<VENDA> GetAllVendas()
        {
            return dc.VENDAs.OrderBy(u => u.CODIGOVENDA).ToList();
        }

        public List<VENDA> GetListVendaByPeriodoCaixa(DateTime dtInicio, DateTime? dtFim, int idUnidade)
        {
            var result = dc.VENDAs.Where(u => u.SITUACAO == EnumUtils.GetValueInt(SituacaoVendaEnum.Finalizada) && u.DATAALTERACAO >= dtInicio && u.UNIDADEID == idUnidade);

            if (dtFim != null)
                result = result.Where(u => u.DATAALTERACAO <= dtFim);
            else
                result = result.Where(u => u.DATAALTERACAO <= DateTime.Now);

            return result.ToList();
        }

        public void Salvar(VENDA venda)
        {
            if (venda.VENDAID > 0)
                Atualizar(venda);
            else
                Inserir(venda);
        }
    }
}

