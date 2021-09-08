using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final.Models;


namespace Proyecto_Final.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int name_usuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(name_usuario).nombre;
            }
        }

        public static string NombreCliente(int name_cliente)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(name_cliente).nombre;
            }
        }

        public ActionResult ListarUsuario()
        {
            using (var db = new inventario2021Entities())
                return PartialView(db.usuario.ToList());
        }

        public ActionResult ListarCliente()
        {
            using (var db = new inventario2021Entities())
                return PartialView(db.cliente.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)

                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.compra.Add(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventario2021Entities())
            {
                compra compra = db.compra.Where(kev => kev.id == id).FirstOrDefault();
                return View(compra);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Edit(compra compra)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var Product_Img = db.compra.Find(compra.id);
                    compra.fecha = compra.fecha;
                    compra.total = compra.total;
                    compra.id_usuario = compra.id_usuario;
                    compra.id_cliente = compra.id_cliente;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception kev)
            {
                ModelState.AddModelError("", "error" + kev);
                return View();
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    compra compra = db.compra.Find(id);
                    db.compra.Remove(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }


        public ActionResult Reporte()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from table_cliente in db.cliente
                            join table_compra in db.compra on table_cliente.id equals table_compra.id_cliente
                            select new Reporte
                            {
                                nombre_cliente = table_cliente.nombre,
                                fecha_compra = table_compra.fecha,
                                total_compra = table_compra.total,
                                correo_cliente = table_cliente.email
                            };
                return View(query);
            }
            catch (Exception kev)
            {
                ModelState.AddModelError("", "error " + kev);
                return View();
                throw;
            }
        }

























    }
}