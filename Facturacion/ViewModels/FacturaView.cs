using Data.Entidades;
using Facturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facturacion.ViewModels
{
    public class FacturaView
    {
        public Cliente  Cliente { get; set; }
        public ProductoFactura ProductoFactura { get; set; }
        public List<ProductoFactura> Productos { get; set; }
    }
}