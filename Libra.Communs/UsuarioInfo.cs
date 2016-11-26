using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs
{
    [Serializable]
    public class UsuarioInfo
    {
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public int UnidadeLogada { get; set; }
        public List<int> Unidades { get; set; }
        public List<int> Perfis { get; set; }
    }
}

