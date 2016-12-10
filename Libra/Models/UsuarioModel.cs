using Libra.Communs;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    [Table("Usuario")]
    public class UsuarioModel : BaseModels
    {
        public UsuarioModel()
        {
            UsuarioID = Guid.NewGuid().ToString();
        }

        [Key]
        public string UsuarioID { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public Int16 Sexo { get; set; }

        public DateTime DataNascimento { get; set; }

        public Int16 Situacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }

    }
}