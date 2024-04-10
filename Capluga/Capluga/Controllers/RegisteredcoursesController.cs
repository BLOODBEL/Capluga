using Capluga.Entities;
using Capluga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class RegisteredcoursesController : Controller
    {

        RegisteredcoursesModel modelRegister = new RegisteredcoursesModel();


        [HttpGet]
        public ActionResult RegistrarInscripcion(int quantity, long medicalCourseID)
        {
            var entidad = new InscripCursEnt();
            entidad.UserID = long.Parse(Session["UserID"].ToString());
            entidad.MedicalCourseID = medicalCourseID;
            entidad.Quantity = quantity;
            entidad.Registrationdate = DateTime.Now;

            modelRegister.RegistrarInscripcion(entidad);

            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ConsultarInscripcion()
        {
            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["TotalPago"] = datos.AsEnumerable().Sum(x => x.Total);
            return View(datos);
        }

        //[HttpGet]
        //public ActionResult ConsultarInscripciones()
        //{
        //    var datos = modelRegister.ConsultarInscripciones(long.Parse(Session["UserID"].ToString()));
        //    return View(datos);
        //}

        [HttpGet]
        public ActionResult EliminarRegistroCurso(long q)
        {
            modelRegister.EliminarRegistroCurso(q);

            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            return RedirectToAction("ConsultarInscripcion", "Registeredcourses");
        }
    }
}