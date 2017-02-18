using Libra.Communs;
using Libra.Communs.Enumerators;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class UsuarioBll : LibraBusinessLogic<USUARIO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public USUARIO GetUsuarioById(int idUsuario)
        {
            return dc.USUARIOs.Where(u => u.USUARIOID == idUsuario).FirstOrDefault();
        }

        public USUARIO GetUsuarioByAspNetUsersId(string AspNetUserId)
        {
            return dc.USUARIOs.Where(u => u.ASPNETUSERID == AspNetUserId).FirstOrDefault();
        }

        public USUARIO GetUsuarioByCPF(string CPF)
        {
            return dc.USUARIOs.Where(u => u.CPF == CPF).FirstOrDefault();
        }

        public USUARIO GetUsuarioByLogin(string Login)
        {
            var result = (from usuario in dc.USUARIOs
                          join ANUser in dc.AspNetUsers on usuario.ASPNETUSERID equals ANUser.Id
                          where ANUser.UserName == Login
                          select usuario).FirstOrDefault();

            return result;
        }

        public void Salvar(USUARIO usuario)
        {
            if (usuario.USUARIOID == 0)
                this.Inserir(usuario);
            else
                this.Atualizar(usuario);
        }

        public List<USUARIO> GetAllUsuarios()
        {
            return dc.USUARIOs.ToList();
        }

        public IEnumerable<UsuarioModelGrid> GetAllUsuariosGridFiltro(List<bool> listAtivo, string nomeUsuario, string email, string CPF, List<int> listIdsUnidades, List<int> listIdsPerfis)
        {
            var result = (from usuario in dc.USUARIOs
                          join AspNetUser in dc.AspNetUsers on usuario.ASPNETUSERID equals AspNetUser.Id
                          join uu in dc.USUARIOUNIDADEs on usuario.USUARIOID equals uu.USUARIOID into uu_join
                          from uu in uu_join.DefaultIfEmpty()
                          join up in dc.USUARIOPERFIs on usuario.USUARIOID equals up.USUARIOID into up_join
                          from up in up_join.DefaultIfEmpty()
                          orderby usuario.NOME
                          select new
                          {
                              usuario,
                              AspNetUser,
                              uu,
                              up
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.usuario.ATIVO));
            }

            if (listIdsPerfis.Count > 0)
            {
                result = result.Where(a => listIdsPerfis.Contains(Convert.ToInt32(a.up.PERFILID)));
            }

            if (listIdsUnidades.Count > 0)
            {
                result = result.Where(a => listIdsUnidades.Contains(Convert.ToInt32(a.uu.UNIDADEID)));
            }

            if (!nomeUsuario.Equals(string.Empty))
                result = result.Where(a => a.usuario.NOME.ToUpper().Contains(nomeUsuario.ToUpper()));

            if (!email.Equals(string.Empty))
                result = result.Where(a => a.AspNetUser.Email.ToUpper().Contains(email.ToUpper()));

            if (!CPF.Equals(string.Empty))
            {
                string cpf = CPF.Replace(".", "").Replace("-", "");
                result = result.Where(a => a.usuario.CPF.Equals(cpf));
            }

            var x = from p in result
                    select new UsuarioModelGrid
                    {
                        UsuarioId = p.usuario.USUARIOID,
                        Nome = p.usuario.NOME,
                        CPF = p.usuario.CPF,
                        Telefone = p.usuario.TELEFONE,
                        Email = p.AspNetUser.Email,
                        Ativo = p.usuario.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public UsuarioInfo GetUsuarioInfo(string aspNetUserId, int UnidadeLogada)
        {
            try
            {
                UsuarioInfo usuarioInfo = null;
                LibraDataContext context = new LibraDataContext();
                List<int> UnidadesId = new List<int>();
                List<int> PerfisId = new List<int>();


                USUARIO usuario = (from user in context.USUARIOs
                                   where user.ASPNETUSERID == aspNetUserId
                                   select user).SingleOrDefault();

                UNIDADE unidade = (from u in context.UNIDADEs
                                   where u.UNIDADEID == UnidadeLogada
                                   select u).SingleOrDefault();


                if (usuario != null)
                {
                    usuarioInfo = SetUsuarioInfo(usuario.USUARIOID, usuario.AspNetUser.UserName, usuario.NOME, usuario.CPF, usuario.TELEFONE,
                        usuario.SEXO, Convert.ToDateTime(usuario.DATANASCIMENTO), usuario.AspNetUser.Email, usuario.ATIVO, UnidadeLogada, unidade.APELIDO, UnidadesId, PerfisId);
                }

                return usuarioInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UsuarioInfo SetUsuarioInfo(int IdUsuario,
                                           string Login,
                                           string Nome,
                                           string CPF,
                                           string Telefone,
                                           Int16? Sexo,
                                           DateTime DataNascimento,
                                           string Email,
                                           bool Ativo,
                                           int UnidadeLogada,
                                           string UnidadeLogadaDescricao,
                                           List<int> Unidades,
                                           List<int> Perfis)
        {
            UsuarioInfo usuarioInfo = new UsuarioInfo();

            usuarioInfo.IdUsuario = IdUsuario;
            usuarioInfo.Login = Login;
            usuarioInfo.Nome = Nome;
            usuarioInfo.CPF = CPF;
            usuarioInfo.Telefone = Telefone;
            usuarioInfo.Sexo = EnumUtils.stringValueOf((SexoEnum)Enum.Parse(typeof(SexoEnum), Sexo.ToString()));
            usuarioInfo.DataNascimento = DataNascimento;
            usuarioInfo.Email = Email;
            usuarioInfo.Ativo = Ativo;
            usuarioInfo.UnidadeLogada = UnidadeLogada;
            usuarioInfo.UnidadeLogadaDescricao = UnidadeLogadaDescricao;
            usuarioInfo.Unidades = Unidades;
            usuarioInfo.Perfis = Perfis;

            return usuarioInfo;
        }

    }

    public class UsuarioModelGrid
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }

    }
}
