using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class VendaPagamentoBll : LibraBusinessLogic<VENDAPAGAMENTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public VENDAPAGAMENTO GetVendaPagamentoById(int idVendaPagamento)
        {
            return dc.VENDAPAGAMENTOs.Where(u => u.VENDAPAGAMENTOID == idVendaPagamento).FirstOrDefault();
        }

        public List<VENDAPAGAMENTO> GetAllVendaPagamentosByIdVenda(int idVenda)
        {
            return dc.VENDAPAGAMENTOs.Where(u => u.VENDAID == idVenda).OrderByDescending(u => u.VALORTOTAL).ToList();
        }

        public void Salvar(VENDAPAGAMENTO venda)
        {
            if (venda.VENDAPAGAMENTOID > 0)
                Atualizar(venda);
            else
                Inserir(venda);
        }
    }
}