using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libra.Communs;
using Libra.Communs.Enumerators;

namespace Libra.Control
{
    public class LogBll : LibraBusinessLogic<LOG>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaUsuarioAssociado(int usuarioId)
        {
            bool associado = false;

            List<LOG> usuarioLog = (from log in dc.LOGs
                                    where log.IDUSUARIO == usuarioId
                                    && ((TipoLogEnum)log.TIPO == TipoLogEnum.Delete
                                    || (TipoLogEnum)log.TIPO == TipoLogEnum.Insert
                                    || (TipoLogEnum)log.TIPO == TipoLogEnum.Update)
                                    select log).ToList();

            if (usuarioLog.Count > 0)
                associado = true;

            return associado;
        }
    }
}
