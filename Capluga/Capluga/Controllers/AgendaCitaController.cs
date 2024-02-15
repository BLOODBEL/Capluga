using Capluga.Entities;
using Capluga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class AgendaCitaController : Controller
    {

        AgendaCitaModel modelAgenda = new AgendaCitaModel();
        CitaHorarioModel claseHorario = new CitaHorarioModel();

        [HttpGet]
        public ActionResult RegistrarCita()
        {
            ViewBag.horarios = claseHorario.verHorarios();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCita(AgendaEnt entidad)
        {
            string respuesta = modelAgenda.RegistrarCita(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su información";
                ViewBag.horarios = claseHorario.verHorarios();
                return View();
            }
        }

        [HttpGet]
        public ActionResult ConsultaCitas()
        {
            var datos = modelAgenda.ConsultaCitas();
            return View(datos);
        }


        [HttpGet]
        public ActionResult Actualizarcita(long q)
        {
            var datos = modelAgenda.ConsultaCita(q);
            ViewBag.horarios = claseHorario.verHorarios();
            return View(datos);
        }



        [HttpPost]
        public ActionResult Actualizarcita(AgendaEnt entidad)
        {
            string respuesta = modelAgenda.Actualizarcita(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("AgendaCita", "ConsultaCitas");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido Actualizar la cita";
                ViewBag.horarios = claseHorario.verHorarios();
                return View();
            }
        }

    }
}
