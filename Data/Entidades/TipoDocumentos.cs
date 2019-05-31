using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("TipoDocumentos")]
    public class TipoDocumentos
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //Navegacion a Clientes
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
