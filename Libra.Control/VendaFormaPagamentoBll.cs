using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class VendaFormaPagamentoBll : LibraBusinessLogic<VENDAFORMAPAGAMENTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public VENDAFORMAPAGAMENTO GetFormaPagamentoById(int idFormaPagamento)
        {
            return dc.VENDAFORMAPAGAMENTOs.Where(u => u.FORMAPAGAMENTOID == idFormaPagamento).FirstOrDefault();
        }

        public List<VENDAFORMAPAGAMENTO> GetAllFormasPagamento()
        {
            return dc.VENDAFORMAPAGAMENTOs.OrderBy(u => u.DESCRICAO).ToList();
        }

        public void Salvar(VENDAFORMAPAGAMENTO formaPagamento)
        {
            if (formaPagamento.FORMAPAGAMENTOID > 0)
                Atualizar(formaPagamento);
            else
                Inserir(formaPagamento);
        }
    }
}
