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

        public List<VENDAPRODUTO> GetAllVendaProdutosByIdVenda(int idVenda)
        {
            return dc.VENDAPRODUTOs.Where(u => u.VENDAID == idVenda).OrderBy(u => u.ITEM).ToList();
        }

        public void Salvar(VENDAPRODUTO venda)
        {
            if (venda.VENDAPRODUTOID > 0)
                Atualizar(venda);
            else
                Inserir(venda);
        }
    }
}