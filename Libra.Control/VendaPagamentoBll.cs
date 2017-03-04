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

        public List<VendaPagamentoGrid> GetAllVendaPagamentosByIdVendaPagamento(int idVenda)
        {
            var sql = from vp in dc.VENDAPAGAMENTOs
                      where vp.VENDAID == idVenda
                      select new VendaPagamentoGrid
                      {
                          VendaId = vp.VENDAID,
                          VendaPagamentoId = vp.VENDAPAGAMENTOID,
                          ClienteId = vp.VENDA.CLIENTEID,
                          VendedorId = vp.VENDA.VENDEDORID,
                          UnidadeId = vp.VENDA.UNIDADEID,
                          FormaPagamento = vp.VENDAFORMAPAGAMENTO.DESCRICAO,
                          Parcelas = vp.NUMEROPARCELA,
                          ValorParcela = vp.VALORPARCELA,
                          Total = vp.VALORTOTAL
                      };

            return sql.OrderBy(u => u.VendaPagamentoId).ToList();
        }

        public void Salvar(VENDAPAGAMENTO venda)
        {
            if (venda.VENDAPAGAMENTOID > 0)
                Atualizar(venda);
            else
                Inserir(venda);
        }

        public void DeletarPagamento(int idVendaPagamento)
        {
            VENDAPAGAMENTO vp = GetVendaPagamentoById(idVendaPagamento);
            if (vp != null)
                this.Deletar(vp);
        }
    }

    public class VendaPagamentoGrid
    {
        public int VendaId { get; set; }
        public int VendaPagamentoId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int UnidadeId { get; set; }
        public string FormaPagamento { get; set; }
        public int? Parcelas { get; set; }
        public decimal? ValorParcela { get; set; }
        public decimal? Total { get; set; }
    }
}