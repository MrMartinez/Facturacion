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
    public class ProductosController : Controller
    {
        Context db = new Context();
        Producto producto = new Producto();

        // GET: Productos
        public ActionResult Index()
        {
            var listado = db.Productos.ToList();
            return View(listado);
        }
        public ActionResult Crear()
        {
            ViewBag.Categoria = new SelectList(db.Categorias.ToList(), "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Producto p)
        {

            //producto.Nombre = p.Nombre;
            //producto.CategoriaId = p.CategoriaId;
            //producto.Cantidad = p.Cantidad;
            //producto.PorcentajeGanancia = p.PorcentajeGanancia;
            //producto.Disponible = p.Disponible;

            if (ModelState.IsValid)
            {
                db.Productos.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.Categorias.ToList(), "Id", "Descripcion");
            return View();
        }

        public ActionResult Modificar(int? id)
        {
            var producto = db.Productos.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id != producto.Id)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.Categorias.ToList(), "Id", "Descripcion");
            return View(producto);
        }
        [HttpPost]
        public ActionResult Modificar(Producto producto)
        {
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}