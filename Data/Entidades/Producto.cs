using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("Productos")]
    public class Producto
    {
        [Key, Display(Name ="Id")]        
        public int Id { get; set; }
        [Display(Name ="Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        [Display(Name = "Cantidad")]
        public int? Cantidad { get; set; }
        [Display(Name = "Precio")]
        public decimal? Precio { get; set; }
        [Display(Name = "% Ganancia")]
        public int PorcentajeGanancia { get; set; }
        [Display(Name = "Disponible")]
        public bool? Disponible { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Detalle> Detalles { get; set; }

    }
}
