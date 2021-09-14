﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final.Models;
using System.IO;


namespace Proyecto_Final.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
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


        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findproveedor = db.proveedor.Find(id);
                return View(findproveedor);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findproveedor = db.proveedor.Find(id);
                    db.proveedor.Remove(findproveedor);
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


        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor findproveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findproveedor);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(proveedor editproveedor)
        {
            try
            {

                using (var db = new inventario2021Entities())
                {
                    proveedor prove = db.proveedor.Find(editproveedor.id);

                    prove.nombre = editproveedor.nombre;
                    prove.direccion = editproveedor.direccion;
                    prove.telefono = editproveedor.telefono;
                    prove.nombre_contacto = editproveedor.nombre_contacto;
                    

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

        //metodos para cargar una caraga masiva en CSV
        public ActionResult Cargar_CSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadcsv(HttpPostedFileBase fileForm)
        {
            try
            {
                //string para guardar la ruta
                string filePath = string.Empty;

                //condicion pra saber si el archivo llego
                if (fileForm != null)
                {
                    //ruta de la carpeta que guarda el archivo
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(fileForm.FileName);

                    //guardar el archivo
                    fileForm.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newProveedor = new proveedor
                            {
                                nombre = row.Split(';')[0],
                                telefono = row.Split(';')[2],
                                direccion = row.Split(';')[1],
                                nombre_contacto = row.Split(';')[3]
                            };

                            using (var db = new inventario2021Entities())
                            {
                                db.proveedor.Add(newProveedor);
                                db.SaveChanges();
                            }
                        }
                    }
                }

                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }












    }
}