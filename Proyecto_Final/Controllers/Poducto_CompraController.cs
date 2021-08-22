using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final.Models;


namespace Proyecto_Final.Controllers
{
    public class Poducto_CompraController : Controller
    {
        // GET: Poducto_Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static string Nombre_Producto(int idproducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idproducto).nombre;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra productoComp)
        {
            if (!ModelState.IsValid)

                return View();
            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(productoComp);
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
                return View(db.producto_imagen.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventario2021Entities())
            {
                producto_imagen producto_Image_Edit = db.producto_imagen.Where(kev => kev.id == id).FirstOrDefault();
                return View(producto_Image_Edit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Edit(producto_imagen producto_Imagen_Edit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var Product_Img = db.producto_imagen.Find(producto_Imagen_Edit.id);
                    Product_Img.imagen = producto_Imagen_Edit.imagen;
                    Product_Img.id_producto = producto_Imagen_Edit.id_producto;
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
                    producto_imagen producto_Image = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(producto_Image);
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


















    }
}