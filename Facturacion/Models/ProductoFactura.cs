using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facturacion.Models
{
    public class ProductoFactura:Producto
    {
        public int Cantidad { get; set; }
        public decimal? Valor { get { return Precio * Cantidad; }}
    }
}