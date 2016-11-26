using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class FuncionalidadePerfilBll : LibraBusinessLogic<FUNCIONALIDADEPERFI>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaPerfilAssociadas(int perfilId)
        {
            bool associado = false;

            List<FUNCIONALIDADEPERFI> funcionalidadePerfis = (from FP in dc.FUNCIONALIDADEPERFIs
                                                              where FP.PERFILID == perfilId
                                                              select FP).ToList();

            if (funcionalidadePerfis.Count > 0)
                associado = true;

            return associado;
        }

        public List<FUNCIONALIDADEPERFI> GetListFuncionalidadePerfilByPerfilId(int PerfilId)
        {
            return dc.FUNCIONALIDADEPERFIs.Where(p => p.PERFILID == PerfilId).ToList();
        }

        public void DeletarTodosByPerfilId(int PerfilId)
        {
            foreach (FUNCIONALIDADEPERFI fp in GetListFuncionalidadePerfilByPerfilId(PerfilId))
            {
                this.Deletar(fp);
            }
        }

        public void Salvar(FUNCIONALIDADEPERFI fp)
        {
            this.Inserir(fp);
        }

    }
}
