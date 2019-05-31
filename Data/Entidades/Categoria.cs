using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entidades
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key,Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name ="Descripcion")]
        public string Descripcion { get; set; }

        //Propiedad de navegacion haciendo relacion a una lista de productos
        public virtual ICollection<Producto> Productos { get; set; }

        //Inicializando la coleccion en el constructor
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
    }
}