using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class ClienteBll : LibraBusinessLogic<CLIENTE>
    {
        private LibraDataContext dc = new LibraDataContext();

        public CLIENTE GetClienteProdutoById(int idCliente)
        {
            return dc.CLIENTEs.Where(u => u.CLIENTEID == idCliente).FirstOrDefault();
        }

        public List<CLIENTE> GetAllClientes()
        {
            return dc.CLIENTEs.OrderBy(c => c.NOME).ToList();
        }

        public void Salvar(CLIENTE cliente)
        {
            if (cliente.CLIENTEID > 0)
                Atualizar(cliente);
            else
                Inserir(cliente);
        }
    }
}
