using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key,Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Tipo Documento")]
        public int TipoDocumento { get; set; }
        [Display(Name = "No. Documento")]
        public string Documento { get; set; }
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Telefono 1")]
        public string Telefono1 { get; set; }
        //[Display(Name = "Telefono 2")]
        //public string Telefono2 { get; set; }
        //[Display(Name = "Telefono 3")]
        //public string Telefono3 { get; set; }
        [Display(Name = "Fecha Creacion")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Disponible")]
        public bool Disponible { get; set; }

        //Navegacion a tipo de Documento
        [ForeignKey("TipoDocumento")]
        public virtual TipoDocumentos TipoDocumentos { get; set; }
       public virtual ICollection<Factura> Facturas { get; set; }
    }
}
