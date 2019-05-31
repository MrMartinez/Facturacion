using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("Facturas")]
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime? Fecha { get; set; }

        //Navegacion a tabla Clientes
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
