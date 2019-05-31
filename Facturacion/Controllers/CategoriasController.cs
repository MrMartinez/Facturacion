using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Contexto;
using Data.Entidades;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Facturacion.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        Context db = new Context();
        Categoria categoria = new Categoria();
        // GET: Categorias
        public ActionResult Index()
        {
            var listado= db.Categorias.ToList();
            return View(listado);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Categoria c)
        {
            categoria.Descripcion = c.Descripcion;
            db.Categorias.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int? id)
        {
            var categoria = db.Categorias.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id != categoria.Id)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        public ActionResult Modificar(Categoria categoria)
        {
            db.Entry(categoria).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}