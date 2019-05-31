using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Contexto;
using Data.Entidades;

namespace Facturacion.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        Context db = new Context();
        Cliente cliente = new Cliente();
      
        // GET: Clientes
        public ActionResult Index()
        {
            var listado = db.Clientes.ToList();
            return View(listado);
        }
        //[Authorize]
        public ActionResult Crear()
        {
            var lista = db.tipoDocumentos.ToList();            
            lista.Add(new TipoDocumentos {Id=0, Descripcion="[..Seleccione un Tipo de Documento...]"});
            lista = lista.OrderBy(t => t.Descripcion).ToList();
            ViewBag.TipoDocumento = new SelectList(lista,"Id", "Descripcion");
            return View();
        } 

        [HttpPost]
        public ActionResult Crear(Cliente c)
        {
            /* Este bloque crear el cliente asignando las propiedades al objeto recibido
            cliente.TipoDocumento = c.TipoDocumento;
            cliente.Documento = c.Documento;
            cliente.Direccion = c.Direccion;
            cliente.Email = c.Email;
            cliente.Telefono1 = c.Telefono1;
            cliente.Telefono2 = c.Telefono2;
            cliente.Telefono3 = c.Telefono3;
            cliente.FechaCreacion = c.FechaCreacion;
            cliente.Disponible = c.Disponible;*/

            if (ModelState.IsValid)
            {
                db.Clientes.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Modificar(int? id)
        {
            var cliente = db.Clientes.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id != cliente.Id)
            {
                return HttpNotFound();
            }
            var lista = db.tipoDocumentos.ToList();
            lista.Add(new TipoDocumentos { Id = 0, Descripcion = "[Seleccione un Tipo de Documento]" });
            lista = lista.OrderBy(t => t.Descripcion).ToList();
            ViewBag.TipoDocumento = new SelectList(lista, "Id", "Descripcion");
            return View(cliente);
        }
        [HttpPost]
        public ActionResult Modificar(Cliente cliente)
        {
            var lista = db.tipoDocumentos.ToList();
            lista.Add(new TipoDocumentos { Id = 0, Descripcion = "[Seleccione un Tipo de Documento]" });
            lista = lista.OrderBy(t => t.Descripcion).ToList();
            ViewBag.TipoDocumento = new SelectList(lista, "Id", "Descripcion");
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


    }
}