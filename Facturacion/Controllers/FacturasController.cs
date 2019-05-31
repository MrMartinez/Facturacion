using Data.Contexto;
using Data.Entidades;
using Facturacion.Models;
using Facturacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Facturacion.Controllers
{
    [Authorize]
    public class FacturasController : Controller
    {
        Context db = new Context();
        // GET: Facturas
        public ActionResult NuevaFactura()
        {
            var facturaView = new FacturaView();
            facturaView.Cliente = new Cliente();
            facturaView.Productos = new List<ProductoFactura>();
            Session["facturaView"] = facturaView;
            //Crear una lista para ordernarla y armar el ViewBag.Cliente
            var lista = db.Clientes.ToList();
            lista.Add(new Cliente {Id = 0, Nombre="[Seleccione el Cliente]" });
            lista = lista.OrderBy(c => c.Nombre).ToList();
            ViewBag.Cliente = new SelectList(lista, "Id", "Nombre");
            return View(facturaView);
        }

        [HttpPost]
        public ActionResult NuevaFactura(FacturaView facturaView)
        {
            
            facturaView = (FacturaView)Session["facturaView"];
            var ClienteId = int.Parse(Request["Id"]);
            if (ClienteId==0)
            {
                ViewBag.Cliente = new SelectList(db.Clientes.ToList(), "Id", "Nombre");
                ViewBag.Error = "Debe seleccionar un Cliente";
            }
           

            if (facturaView.Productos.Count==0)
            {
                ViewBag.Error = "Factura en blanco";
            }
            var Cliente = db.Clientes.Find(ClienteId);
            if (Cliente==null)
            {
                ViewBag.Cliente = new SelectList(db.Clientes.ToList(), "Id", "Nombre");
                ViewBag.Error = "Este Cliente NO existe";
            }

            var facturaId = 0;
         
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var factura = new Factura
                    {
                        ClienteId = ClienteId,
                        Fecha = DateTime.Now,

                    };
                    db.Facturas.Add(factura);
                    db.SaveChanges();

                    facturaId = db.Facturas.ToList().Select(f => f.Id).Max();

                    var detalleId = db.Detalles.ToList().Select(d => d.Id).Max();
                    foreach (var item in facturaView.Productos)
                    {
                        var detalle = new Detalle
                        {
                            Id = detalleId,
                            ProductoId = item.Id,
                            //Descripcion = item.Descripcion,
                            Cantidad = item.Cantidad,
                            Precio = item.Precio,
                            FacturaId = facturaId

                        };
                        var producto = db.Productos.Where(x => x.Id == item.Id);
                        
                        db.Detalles.Add(detalle);
                        db.SaveChanges();                        
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ViewBag.Error = "No se pudo guardar la Factura";
                    var listaClientes = db.Clientes.ToList();
                    listaClientes.Add(new Cliente {Id = 0, Nombre="Seleccione un Cliente" });
                    listaClientes = listaClientes.OrderBy(x => x.Nombre).ToList();
                    ViewBag.Cliente = new SelectList(listaClientes, "Id", "Nombre");
                    return View(facturaView);
                }
            }
            ViewBag.Mensaje = string.Format("La orden ID: {0} se guardo con exito",facturaId);
            ViewBag.Cliente = new SelectList(db.Clientes.ToList(), "Id", "Nombre");

            //Armo la lista que le voy a pasar
            facturaView = new FacturaView();
            facturaView.Cliente = new Cliente();
            facturaView.Productos = new List<ProductoFactura>();
            Session["facturaView"] = facturaView;
            return View(facturaView);
        }

        public ActionResult AgregarProducto()
        {
            ViewBag.ProductoId = new SelectList(db.Productos.ToList(), "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(ProductoFactura productoFactura)
        {
            var productoId = int.Parse(Request["Id"]);
            if (productoId == 0)
            {
                ViewBag.ProductoId = new SelectList(db.Productos.ToList(), "Id", "Descripcion");
                return View(productoFactura);
            }

            //var cantidad = int.Parse(Request["Cantidad"]);
            var producto = db.Productos.Find(productoId);
            var facturaView = (FacturaView)Session["facturaView"];

            productoFactura = facturaView.Productos.Find(p => p.Id==productoId);
            if (productoFactura == null)
            {            
            productoFactura = new ProductoFactura
            {
                Id = producto.Id,
                Descripcion = producto.Descripcion,
                Precio = int.Parse(Request["Precio"]),
                Cantidad = int.Parse(Request["Cantidad"]),
                CategoriaId = producto.CategoriaId,
                Disponible = producto.Disponible
                          
            }; facturaView.Productos.Add(productoFactura);
            }
            else
            {
                productoFactura.Cantidad += int.Parse(Request["Cantidad"]);
            }
          
            ViewBag.Cliente = new SelectList(db.Clientes.ToList(), "Id", "Nombre");
            
            return View("NuevaFactura", facturaView);
        }
    }
}