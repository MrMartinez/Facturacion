using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("Detalles")]
    public class Detalle
    {
        [Key]
        public int Id { get; set; }
        public int FacturaId { get; set; }
        //public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        //public string Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }

        //Navegaciones
        public virtual Factura Factura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
