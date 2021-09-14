using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final.Models;
using System.IO;


namespace Proyecto_Final.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.cliente.Add(cliente);
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
                var findcliente = db.cliente.Find(id);
                return View(findcliente);
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findcliente = db.cliente.Find(id);
                    db.cliente.Remove(findcliente);
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
                    cliente findcliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findcliente);
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

        public ActionResult Edit(cliente editcliente)
        {
            try
            {

                using (var db = new inventario2021Entities())
                {
                    cliente cliente = db.cliente.Find(editcliente.id);

                    cliente.nombre = editcliente.nombre;
                    cliente.email = editcliente.email;
                    cliente.documento = editcliente.documento;

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

        //CARGA MASIVA

        public ActionResult Carga_CSV_Cliente()
        {
            return View();
        }

        [HttpPost]

        public ActionResult upload_csv_cliente(HttpPostedFileBase file_Form_Cliente)
        {
            try
            {
                //string para guardar la ruta
                string filePath = string.Empty;

                //condicion pra saber si el archivo llego
                if (file_Form_Cliente != null)
                {
                    //ruta de la carpeta que guarda el archivo
                    string path = Server.MapPath("~/Uploads_cliente/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //obtener el nombre del archivo
                    filePath = path + Path.GetFileName(file_Form_Cliente.FileName);

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(file_Form_Cliente.FileName);

                    //guardar el archivo
                    file_Form_Cliente.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newCliente = new cliente
                            {
                                nombre = row.Split(';')[0],
                                documento = row.Split(';')[1],
                                email = row.Split(';')[2]
                            };
                            
                            using (var kev_db = new inventario2021Entities())
                            {
                                kev_db.cliente.Add(newCliente);
                                kev_db.SaveChanges();
                            }
                        }
                    }
                }

                return RedirectToAction("Index");

            }
            catch (Exception kev)
            {
                ModelState.AddModelError("", "error " + kev);
                return View();
            }
        }











    }
}