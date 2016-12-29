using Libra.Models.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libra.Models.Core
{
    [Table("CRT")]
    public class CRTModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }
    }
}