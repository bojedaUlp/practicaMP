using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pruebaPracticas.Models;

namespace pruebaPracticas.vista
{
   // [Route("[controller]")]
    public class VistaController : Controller
    {
        private readonly RepositorioAuto repositorioAuto;
        private readonly IConfiguration configuration;
        public VistaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioAuto = new RepositorioAuto(configuration);
        }
        // GET: Vista
        public ActionResult Index()
        {
            var lista = repositorioAuto.ObtenerTodos();
            return View(lista);
        }

        // GET: Auto/Create
        public ActionResult Create(Auto a)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioAuto.Alta(a);
                    return RedirectToAction(nameof(Index));
                }
                else { return View(); }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        // GET: Auto/Edit/5
        public ActionResult Edit(int a)
        {
            try
            {
                Auto aux = repositorioAuto.ObtenerPorId(a);
                return View(aux);
            }
            catch (Exception ex)
            {
                ViewBag.Eror = ex.Message;
                return View();
            }
        }

     /*   public ActionResult Edit(int id, Auto a)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int res = repositorioAuto.Modificacion(a);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }




        // GET: Auto/Delete/5
        public ActionResult Delete(int id, Auto a)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioAuto.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
                else { return View(); }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        */
        public ActionResult Delete(int id)
        {
            try
            {

                Auto aux = repositorioAuto.ObtenerPorId(id);
                return View(aux);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}