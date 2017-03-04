using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class VendaProdutoBll : LibraBusinessLogic<VENDAPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public VENDAPRODUTO GetVendaProdutoById(int idVendaProduto)
        {
            return dc.VENDAPRODUTOs.Where(u => u.VENDAPRODUTOID == idVendaProduto).FirstOrDefault();
        }

        public VENDAPRODUTO GetVendaProdutoByIdVendaAndItem(int idVenda, int item)
        {
            return dc.VENDAPRODUTOs.Where(u => u.VENDAID == idVenda && u.ITEM == item).FirstOrDefault();
        }

        public List<VENDAPRODUTO> GetAllVendaProdutosByIdVenda(int idVenda)
        {
            return dc.VENDAPRODUTOs.Where(u => u.VENDAID == idVenda).OrderBy(u => u.ITEM).ToList();
        }

        public List<VendaProdutoGrid> GetAllVendaProdutosByIdVendaForGrid(int idVenda)
        {
            var sql = from vp in dc.VENDAPRODUTOs
                      where vp.VENDAID == idVenda
                      select new VendaProdutoGrid
                      {
                          Item = vp.ITEM,
                          VendaId = vp.VENDAID,
                          VendaProdutosId = vp.VENDAPRODUTOID,
                          ClienteId = vp.VENDA.CLIENTEID,
                          VendedorId = vp.VENDA.VENDEDORID,
                          UnidadeId = vp.VENDA.UNIDADEID,
                          ProdutoId = vp.PRODUTOID,
                          Produto = vp.PRODUTO.DESCRICAO,
                          Quantidade = vp.QTD,
                          ValorUnitario = vp.VALORUNITARIO,
                          DescontoReal = vp.VALORDESCONTO,
                          DescontoPorcentagem = vp.PERCENTDESCONTO,
                          Acrescimo = vp.VALORACRESCIMO,
                          SubTotal = vp.SUBTOTAL
                      };

            return sql.OrderBy(u => u.Item).ToList();
        }

        public void Salvar(VENDAPRODUTO venda)
        {
            if (venda.VENDAPRODUTOID > 0)
                Atualizar(venda);
            else
                Inserir(venda);
        }

        public void DeletarProduto(int idVenda, int item)
        {
            VENDAPRODUTO vp = GetVendaProdutoByIdVendaAndItem(idVenda, item);
            if (vp != null)
                this.Deletar(vp);
        }
    }

    public class VendaProdutoGrid
    {
        public int Item { get; set; }
        public int VendaId { get; set; }
        public int VendaProdutosId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int UnidadeId { get; set; }
        public int? ProdutoId { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? DescontoReal { get; set; }
        public decimal? DescontoPorcentagem { get; set; }
        public decimal? Acrescimo { get; set; }
        public decimal? SubTotal { get; set; }
    }
}